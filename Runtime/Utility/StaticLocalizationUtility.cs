using System;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class StaticLocalizationUtility
{
    public const string LocalizationAPIKey = "AIzaSyAXERpiFldx5rnj8D2PwWnB9pjIzJxGx4Q";
    public const string BaseLocalizationSource = "en";

    #region Saving/Loading localization cache

    public static void SaveLocalizationData(object obj)
    {
        try
        {
            string path = Application.persistentDataPath + "/LocalizedDataCache.json";
            string jsonDataString = JsonUtility.ToJson(obj, true);
            File.WriteAllText(path, jsonDataString);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save localization data cache. Error: {ex}");
        }
    }

    public static object LoadLocalizationData(Type typeToLoad, object defaultObj = null)
    {
        try
        {
            string path = $"{Application.persistentDataPath}{"/LocalizedDataCache.json"}";
            return File.Exists(path) ? JsonUtility.FromJson(File.ReadAllText(path), typeToLoad) : defaultObj;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load localization data. Error: {ex}");
            return defaultObj;
        }

    }

    #endregion

    #region Extensions
    /// <summary>
    /// Localizae tmpro text
    /// </summary>
    /// <param name="textMeshPro"></param>
    /// <param name="locale"></param>
    public static async void Localize(this TMP_Text textMeshPro, string locale = BaseLocalizationSource)
    {
        try
        {
            if (textMeshPro == null)
            {
                return;
            }
            textMeshPro.text = await new Localizer().TranslateText(textMeshPro.text, locale);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to translate text mesh pro text. Error: {ex}");
        }
    }

    /// <summary>
    /// Localize unity ui text
    /// </summary>
    /// <param name="text"></param>
    /// <param name="locale"></param>
    public static async void Localize(this Text text, string locale = BaseLocalizationSource)
    {
        try
        {
            if (text == null)
            {
                return;
            }
            text.text = await new Localizer().TranslateText(text.text, locale);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to translate text mesh pro text. Error: {ex}");
        }
    }

    /// <summary>
    /// Localize a string with a custom locale
    /// </summary>
    /// <param name="text"></param>
    /// <param name="locale"></param>
    public static void Localize(this string text, string locale, Action<string> onLocalized = null)
    {
        try
        {
            if (text == null)
            {
                return;
            }
            LocalizeString(text, locale, onLocalized);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to translate text mesh pro text. Error: {ex}");
        }
    }

    /// <summary>
    /// Localize a string with current device locale
    /// </summary>
    /// <param name="text"></param>
    /// <param name="onLocalized"></param>
    public static void Localize(this string text, Action<string> onLocalized = null)
    {
        try
        {
            if (text == null)
            {
                return;
            }
            LocalizeString(text, BaseLocalizationSource, onLocalized);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to translate text mesh pro text. Error: {ex}");
        }
    }

    private static async void LocalizeString(this string text, string locale = BaseLocalizationSource, Action<string> onLocalized = null)
    {
        try
        {

            var translation = await new Localizer().TranslateText(text, locale);
            onLocalized?.Invoke(translation);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to translate text mesh pro text. Error: {ex}");

        }
    }
    #endregion

}

