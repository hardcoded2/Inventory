using System;
using System.IO;
using UnityEngine;

namespace Datastores
{
    public class BinaryDatastore
    {
        public enum Keys
        {
            PlayerData,
        }

        private string KeyFilePath(Keys key)
        {
            //FIXME: persistentdatapath not unique enough for CI/multiple instances and off-thread
            return Path.Combine(Application.persistentDataPath, key.ToString());
        }
        public byte[] GetData(Keys key)
        {
            try
            {
                return File.ReadAllBytes(KeyFilePath(key));
            }
            catch
            {
                return new byte[0];
            }
        }
    }
}
