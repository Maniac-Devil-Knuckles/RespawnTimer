using System;
using Qurre;
using Qurre.API;
using System.Reflection;

using ServerEvent = Qurre.Events.Round;
using System.Collections.Generic;

namespace RespawnTimer
{
    public class RespawnTimer : Plugin
    {
        public static RespawnTimer Singleton;

        public override string Developer => "Michal78900, exported to Qurre by Maniac Devil Knuckles";
        public override string Name => "RespawnTimer";
        public override Version Version => new Version(2, 2, 0);
        public override Version NeededQurreVersion => new Version(1, 5, 1);
        public override int Priority => int.MaxValue;

        public static Config CustomConfig { get; } = new Config();

        private Handler handler;

        internal static bool assemblySH { get; set; } = false;

        public override void Enable()
        {
            CustomConfigs.Add(CustomConfig);
            if (!CustomConfig.IsEnabled) return;
            Singleton = this;
            handler = new Handler(CustomConfig);
           
            ServerEvent.Start += handler.OnRoundStart;
        }

        public override void Disable()
        {
            ServerEvent.Start -= handler.OnRoundStart;
            CustomConfigs.Remove(CustomConfig);
            handler = null;
            Singleton = null;
        }
    }
}
