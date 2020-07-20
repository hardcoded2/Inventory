using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;

namespace DevTests
{
    public class DevTestPlatformTools
    {
        [Pure]
        public bool IsWindows()
        {
            return RunningOnPlatform(OSPlatform.Windows);
        }
        [Pure]
        public bool IsMac()
        {
            return RunningOnPlatform(OSPlatform.OSX);
        }
        [Pure]
        public bool RunningOnPlatform(OSPlatform platform)
        {
            return RuntimeInformation.IsOSPlatform(platform);
        }
        
        [Pure]
        public OSPlatform FromOsPlatformEnum(OSPlatformEnum platformEnum)
        {
            return new Dictionary<OSPlatformEnum, OSPlatform>()
            {
                {OSPlatformEnum.MAC,OSPlatform.OSX},
                {OSPlatformEnum.LINUX,OSPlatform.Linux},
                {OSPlatformEnum.WINDOWS,OSPlatform.Windows},
                {OSPlatformEnum.FREEBSD,OSPlatform.FreeBSD}
            }[platformEnum];
        }
        [Pure]
        public OSPlatformEnum GetPlatform()
        {
            foreach (var platformEnum in System.Enum.GetValues(typeof(OSPlatformEnum)).Cast<OSPlatformEnum>())
            {
                var platform = FromOsPlatformEnum(platformEnum);
                if (RuntimeInformation.IsOSPlatform(platform)) return platformEnum;
            }
            throw new Exception("Unhandled platform!");
        }

        public enum OSPlatformEnum
        {
            FREEBSD,
            MAC,
            LINUX,
            WINDOWS,
        }
    }
}
