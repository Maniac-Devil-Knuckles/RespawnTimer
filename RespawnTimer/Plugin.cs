using System;
using Qurre;
using Qurre.API;
using System.Reflection;

using ServerEvent = Qurre.Events.Round;

namespace RespawnTimer
{
    public class RespawnTimer : Plugin
    {
        public static RespawnTimer Singleton;

        public override string Developer => "Michal78900, exported to Qurre by Maniac Devil Knuckles";
        public override string Name => "RespawnTimer";
        public override Version Version => new Version(2, 2, 0);
        public override Version NeededQurreVersion => new Version(1, 2, 4);

        private Handler handler;

        internal Config cfg = new Config();

        internal static bool assemblySH { get; set; } = false;

        public override void Enable()
        {
            Singleton = this;

            handler = new Handler(this);

            ServerEvent.Start += handler.OnRoundStart;


            foreach (var plugin in PluginManager.plugins)
            {
                if (plugin.Name == "SerpentsHand"&&plugin.Developer== "Cyanox,Exported to Qurre by Maniac Devil Knuckles")
                {
                    assemblySH = true;
                 if(cfg.ShowDebugMessages)   Log.Debug("SerpentsHand plugin detected!");
                }
            }

        }

        public override void Disable()
        {
            ServerEvent.Start -= handler.OnRoundStart;

            handler = null;
            Singleton = null;
        }
    }
}
