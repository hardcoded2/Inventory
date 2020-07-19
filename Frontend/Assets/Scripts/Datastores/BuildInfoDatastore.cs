using System;
using System.IO;
using UnityEngine;

namespace Datastores
{
    public class BuildInfoDatastore
    {
        public enum Keys
        {
            ITEM_DATA_VERSION,
        }
        //FIXME: accessing streamingassetspath is not thread safe
        private string KeyFilePath(Keys key)
        {
            return Path.Combine(Application.streamingAssetsPath, key.ToString());
        }
        public string Get(Keys key)
        {
            try
            {
                return File.ReadAllText(KeyFilePath(key));
            }
            catch (Exception e)
            {
                Debug.LogWarning($"error reading buildinfo file: {key} {e.Message}");
                return string.Empty;
            }
        }

#if UNITY_EDITOR || !UNITY_5_3_OR_NEWER //not sure backend code will use this, but don't disallow
        public void Set(Keys key,string value)
        {
            var valueBytes = System.Text.Encoding.UTF8.GetBytes(value);
            File.WriteAllBytes(KeyFilePath(key),valueBytes);
        }
#else
#error Trying to use set build data from usermode scripts
#endif
    }
}
