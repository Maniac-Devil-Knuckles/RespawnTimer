using Qurre.API.Addons;

namespace RespawnTimer
{
    internal static class Config
    {
        internal static bool IsEnabled { get; set; } = true;

        internal static bool ShowDebugMessages { get; set; } = false;

        internal static float Interval { get; set; } = 1.5f;

        internal static byte TextLowering { get; set; } = 8;

        internal static bool ShowMinutes { get; set; } = true;

        internal static bool ShowSeconds { get; set; } = true;

        internal static bool ShowTimerOnlyOnSpawn { get; set; } = false;

        internal static bool ShowNumberOfSpectators { get; set; } = false;

        internal static bool ShowTickets { get; set; } = false;

        internal static Translations translations { get; set; } = new Translations();

        private static JsonConfig jsonConfig = new JsonConfig("RespawnTimer");

        internal static void Reload() 
        {
            IsEnabled = jsonConfig.SafeGetValue("IsEnabled", IsEnabled);
            ShowDebugMessages = jsonConfig.SafeGetValue("ShowDebugMessages", ShowDebugMessages);
            Interval = jsonConfig.SafeGetValue("Interval", Interval);
            TextLowering = jsonConfig.SafeGetValue("TextLowering", TextLowering);
            ShowMinutes = jsonConfig.SafeGetValue("ShowMinutes", ShowMinutes);
            ShowSeconds = jsonConfig.SafeGetValue("ShowSeconds", ShowSeconds);
            ShowTimerOnlyOnSpawn = jsonConfig.SafeGetValue("ShowTimerOnlyOnSpawn", ShowTimerOnlyOnSpawn);
            ShowNumberOfSpectators = jsonConfig.SafeGetValue("ShowNumberOfSpectators", ShowNumberOfSpectators);
            ShowTickets = jsonConfig.SafeGetValue("ShowTickets", ShowTickets);
            translations = jsonConfig.SafeGetValue("translations", new Translations());
        }

    }
}