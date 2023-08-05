using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Localizer
{
    private readonly HttpClient _httpClient;
  
    public Localizer()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> TranslateText(string inputText, string locale = StaticLocalizationUtility.BaseLocalizationSource)
    {
        try
        {
            var _localizedData = await LocalizedDataService.LoadData();         
            return (locale == StaticLocalizationUtility.BaseLocalizationSource)
            ? inputText : (string.IsNullOrEmpty(_localizedData?.GetLocalizedText(inputText, locale))
            ? await TranslateTextFromAPI(inputText, locale) : _localizedData.GetLocalizedText(inputText, locale));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to localize. Error: {ex}");
            return inputText;
        }

    }

    private async Task<string> TranslateTextFromAPI(string inputText, string locale)
    {
        try
        {
            var apiUrl = $"https://translation.googleapis.com/language/translate/v2?key={StaticLocalizationUtility.LocalizationAPIKey}";
            var requestJson = $@"
            {{
                ""q"": ""{inputText}"",
                ""source"": ""{StaticLocalizationUtility.BaseLocalizationSource}"",
                ""target"": ""{locale}"",
                ""format"": ""text""
            }}";

            var response = await _httpClient.PostAsync(apiUrl, new StringContent(requestJson, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseBody);
            var translatedText = jsonResponse["data"]["translations"][0]["translatedText"].ToString();
            var _localizedData = await LocalizedDataService.LoadData();
            _localizedData?.Add(inputText + locale, translatedText);
            return translatedText;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to localize through API call. Error: {ex}");
            return inputText;
        }
    }

}

