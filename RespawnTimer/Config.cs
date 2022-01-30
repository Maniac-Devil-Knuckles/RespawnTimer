using Qurre.API.Addons;

namespace RespawnTimer
{
    public class Config : IConfig
    {
        public string Name { get; set; } = "RespawnTimer";

        public bool IsEnabled { get; set; } = true;

        public bool ShowDebugMessages { get; set; } = false;

        public float Interval { get; set; } = 1.5f;

        public byte TextLowering { get; set; } = 8;

        public bool ShowMinutes { get; set; } = true;

        public bool ShowSeconds { get; set; } = true;

        public bool ShowTimerOnlyOnSpawn { get; set; } = false;

        public bool ShowNumberOfSpectators { get; set; } = false;

        public bool ShowTickets { get; set; } = false;

        public Translations translations { get; set; } = new Translations();
    }
}