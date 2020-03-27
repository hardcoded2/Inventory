using UnityEngine;

namespace Datastores
{
    public class StringDatastore
    {
        public enum Keys
        {
            ITEM_VERSION=0,
        }

        public string GetKey(Keys key)
        {
            return PlayerPrefs.GetString(key.ToString());
        }

        public void Set(Keys key, string value)
        {
            PlayerPrefs.SetString(key.ToString(),value);
        }
    }
}
