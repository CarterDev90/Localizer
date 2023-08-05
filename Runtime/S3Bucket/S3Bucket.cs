using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Net.Http;
using System;

public static class S3Bucket   
{
    private const string bucketName = "unitylocale"; // Replace with your S3 bucket URL
    private const string region = ".s3-us-west-1.";

    public static async Task UploadJSONAsync(string jsonContent)
    {
        var url = $"https://{bucketName}{region}amazonaws.com/localdata.json";
        var bytes = System.Text.Encoding.UTF8.GetBytes(jsonContent);
        using var httpClient = new HttpClient();
        var content = new ByteArrayContent(bytes);
        content.Headers.Add("Content-Type", "application/json");

        try
        {
            var response = await httpClient.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                Debug.Log("JSON file uploaded to S3 successfully!");
            }
            else
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Debug.LogError(responseData);
                Debug.LogError("Error uploading JSON file to S3: " + response.ReasonPhrase);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error uploading JSON file to S3: " + ex.ToString());
        }
    }

    public static async Task<string> DownloadJSONAsync()
    { 
        Debug.LogError("DOWNLOADING LOCALE CONFIG");
        var url = $"https://{bucketName}{region}amazonaws.com/localdata.json";
        using var httpClient = new HttpClient();
        try
        {
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Debug.LogError(responseData);
                Debug.LogError("Error downloading JSON file from S3: " + response.ReasonPhrase);
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error downloading JSON file from S3: " + ex.ToString());
            return string.Empty;
        }
    }
}


