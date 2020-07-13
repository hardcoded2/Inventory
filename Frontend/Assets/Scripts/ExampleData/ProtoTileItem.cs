using UnityEngine;

namespace ExampleData
{
    public class ProtoTileItem //synchronous version
    {
        public TileItem TileItemFromTileData(TileData tileData)
        {
            var tileItem = new TileItem()
            {
                Tex = new Texture2D(tileData.TileXSize,tileData.TileYSize)
            };
            var bytes = tileData.TileImageData.ToByteArray(); //FIXME: ToByteArray creates an extra copy of tiledata
            tileItem.Tex.LoadRawTextureData(bytes);
            return tileItem;
        }
        //conflates parsing with the domain specific stuff like tranlating to usable texture
        public TileItem TileItemFromTileData(byte[] tileBytes)
        {
            var tileData = TileData.Parser.ParseFrom(tileBytes);
            return TileItemFromTileData(tileData);
        }
    }
}
