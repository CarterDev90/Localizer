using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public static class LocalizedDataService
{
    public class LocalizedData
    {
        [System.Serializable]
        public class DataItem
        {
            public string key;
            public string value;
            public DataItem(string k, string v)
            {
                key = k;
                value = v;
            }
        }
        public List<DataItem> localizedItems = new List<DataItem>();

        public string GetLocalizedText(string key, string targetLang)
        {
            var item = localizedItems.Where(o => o.key == key + targetLang).FirstOrDefault();
            return item != null ? item.value : string.Empty;
        }

        public void Add(string key, string value)
        {
            localizedItems.Add(new DataItem(key, value));
            Save();
        }

        private async void Save()
        {
            await S3Bucket.UploadJSONAsync(JsonUtility.ToJson(this));
        }
    }

    public static LocalizedData _localizedData;

    private static bool _requestingLocalizedData;

    public static async Task<LocalizedData> LoadData()
    {
        while (_requestingLocalizedData)
            await Task.Delay(500);

        _requestingLocalizedData = true;

        if (_localizedData != null)
        {
            _requestingLocalizedData = false;
            return _localizedData;
        }

        string data = await S3Bucket.DownloadJSONAsync();
        _localizedData = JsonUtility.FromJson<LocalizedData>(data) ?? new LocalizedData();
        _requestingLocalizedData = false;

        return _localizedData;
    }
 }
