using System;
using Qurre;
using Qurre.API;
using ServerEvent = Qurre.Events.RoundEvents;
using Qurre.API.Attributes;

namespace RespawnTimer
{
    [PluginInit("RespawnTimer","Maniac Devil Knuckles","1.0.1")]
    public class RespawnTimer 
    {
        private static bool firstenabled = true;
        [PluginEnable]
        public static void Enable()
        {
            Config.Reload();
            if (firstenabled) Log.Info(nameof(RespawnTimer) + "is " + (Config.IsEnabled ? "enabled": "disabled"));
            firstenabled = false;
        }
        [PluginDisable]
        public static void Disable()
        {
            Log.Info(nameof(RespawnTimer) + " is disabled");
            Config.IsEnabled = false;
        }
    }
}
