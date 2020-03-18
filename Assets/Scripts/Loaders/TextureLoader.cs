using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Loaders
{
    public class TextureLoader
    {
        //non-assetbundle / addressable
        public async Task<Texture2D> GetTextureFromURL(string url)
        {
            using (var www = new UnityWebRequest {downloadHandler = new DownloadHandlerTexture(false)})
            {
                //set texture to non-readable
                await www.SendWebRequest();
                if (!www.isDone)
                {
                    Debug.LogError($"Item returned without finishing, this is real bad"); //happens often enough to handle one way or another
                    //sometimes spinning on isdone hope that it works is the workaround, but until this is seen, do not  do that
                    return null;
                }
                if (www.isNetworkError || www.isHttpError || !www.isDone)
                {
                    Debug.LogError($"Error with www {www.error}");
                    return null;
                }
                return DownloadHandlerTexture.GetContent(www);
            }
        }
    }
}
