using System.IO.Compression;

namespace Protobufs
{
    public class ColorMigration
    {
        public ColorProtobufv2 UpgradeColor(ColorProtobufv1 colorIn)
        {
            //could use something like reflection/automapper or some domain specirfic rules for defaults or migrations
            //could have pure C# dll perform this in dev and a service/dll do this in prod
            //default alpha will be 1 by a domain specific rule
            var color = new ColorProtobufv2()
            {
                R = colorIn.R,
                G = colorIn.G,
                B = colorIn.B,
                A = 1f,
            };
            return color;
        }

    }
}
