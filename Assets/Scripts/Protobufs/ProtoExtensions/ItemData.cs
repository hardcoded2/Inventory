using System.IO;
using Datastores;
using ExampleData;
using UnityEngine;
#if UNITY_EDITOR
using System;
using System.Security.Cryptography;
using UnityEditor;
#endif

namespace Protobufs
{
    [InitializeOnLoad]
    public partial class ItemData
    {

        private static string GetItemDataHash()
        {
#if UNITY_EDITOR
            //FIXME: file path should be better controlled through a post processor import script, guid, or something else less brittle
            //referential integrity is important -- breaking in sane ways, etc
            string itemDataGuid = AssetDatabase.FindAssets("item_data")[0]; //"Assets/Scripts/Protobufs/item_data.proto";
            string itemDataPath = AssetDatabase.GUIDToAssetPath(itemDataGuid);
            var protoItemBytes = File.ReadAllBytes(itemDataPath);
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(protoItemBytes);
                return BitConverter.ToString(hash);
            }
#else
            return new BuildInfoDatastore().Get(BuildInfoDatastore.Keys.ITEM_DATA_VERSION);
#endif

        }

//without main game loop, these will make sure they run in editor and in build.
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#endif
        [RuntimeInitializeOnLoadMethod]
        private static void TestForMigration()
        {
            //TODO: tie this into game intialization loop instead of disparate like it is here
            //TODO: use datastores as part of game init rather than parallel route
            var buildInfo = new BuildInfoDatastore();
            var lastKnownItemHash = buildInfo.Get(BuildInfoDatastore.Keys.ITEM_DATA_VERSION);
            var currentHash = GetItemDataHash();


            bool needsMigration = lastKnownItemHash != currentHash;
            Debug.Log($"TestForMigration last hash {lastKnownItemHash} current hash {currentHash} need migration {needsMigration}");
            //TODO: editor pref for migration re-running
            if (needsMigration) 
            {
                Debug.LogWarning("Data needed Migration");
                var playerDataBytes = new BinaryDatastore().GetData(BinaryDatastore.Keys.PlayerData);
                if (playerDataBytes == null) return;
                try
                {
                    var playerData = new ProtoParser().Parse<PlayerData>(playerDataBytes);
                    //current migration deals with color data
                    var colorMigration = new ColorMigration(); 
                    foreach (var item in playerData.Items)
                    {
                        foreach (var artAsset in item.ArtAssets)
                        {
                            if (artAsset.Color != null)
                            {
                                artAsset.ColorNew = colorMigration.UpgradeColor(artAsset.Color);
                            }
                        }
                    }
                    //now set item version string to indicate that we've upgraded
#if UNITY_EDITOR
                    buildInfo.Set(BuildInfoDatastore.Keys.ITEM_DATA_VERSION,GetItemDataHash());
                    Debug.Log($"Migration successful to new item data version {GetItemDataHash()}");
#endif
                }
                catch (Exception e)
                {
                 Debug.LogError($"Migraiton failed {e.Message} stack{e.StackTrace}");   
                }
            }
        }

        

    }

    
}
