using Qurre.API.Addons;

namespace RespawnTimerQurre
{
    public static class Cfg
    {
        public static bool IsEnabled { get; private set; } = true;

        public static bool ShowDebugMessages { get; private set; } = false;

        public static float Interval { get; private set; } = 1.5f;

        public static byte TextLowering { get; private set; } = 8;

        public static bool ShowMinutes { get; private set; } = true;

        public static bool ShowSeconds { get; private set; } = true;

        public static bool ShowTimerOnlyOnSpawn { get; private set; } = false;

        public static bool ShowNumberOfSpectators { get; private set; } = false;

        public static bool ShowTickets { get; private set; } = false;

        public static void Reload()
        {
            Qurre.Plugin.Config.Reload();

            IsEnabled = Qurre.Plugin.Config.GetBool("respawn_timer_enable", IsEnabled, "is enabled?");

            ShowDebugMessages = Qurre.Plugin.Config.GetBool("respawn_timer_debug", ShowDebugMessages, "show debug messages");

            Interval = Qurre.Plugin.Config.GetFloat("respawn_timer_interval", Interval, "interval betweeen show");

            TextLowering = Qurre.Plugin.Config.GetByte("respawn_timer_textlowering", TextLowering, "But not upper");

            ShowMinutes = Qurre.Plugin.Config.GetBool("respawn_timer_showminutes", ShowMinutes, "show minutes");

            ShowSeconds = Qurre.Plugin.Config.GetBool("respawn_timer_showseconds", ShowSeconds, "show seconds");

            ShowTimerOnlyOnSpawn = Qurre.Plugin.Config.GetBool("respawn_timer_showtimeronlyonspawn", ShowTimerOnlyOnSpawn, "?");

            ShowNumberOfSpectators = Qurre.Plugin.Config.GetBool("respawn_timer_shownumberofspectators", ShowNumberOfSpectators, "Num");

            ShowTickets = Qurre.Plugin.Config.GetBool("respawn_timer_showtickets", ShowTickets, "show tickets?");
        }
    }
    public class Translation : IConfig
    {
        public string Name { get; set; } = "RespawnTimer";

        public string YouWillRespawnIn { get; private set; } = "<color=orange>You will respawn in: </color>";

        public string YouWillSpawnAs { get; private set; } = "You will spawn as: ";

        public string Ntf { get; private set; } = "<color=blue>Nine-Tailed Fox</color>";

        public string Ci { get; private set; } = "<color=green>Chaos Insurgency</color>";

        public string Spectators { get; private set; } = "<color=#B3B6B7>Spectators: </color>";

        public string NtfTickets { get; private set; } = "<color=blue>NTF Tickets: </color>";

        public string CiTickets { get; private set; } = "<color=green>CI Tickets: </color>";

        public string Seconds { get; private set; } = " <b>{seconds} s</b>";

        public string Minutes { get; private set; } = "<b>{minutes} min.</b>";

        public string SpectatorsNum { get; private set; } = "{spectators_num}";

        public string NtfTicketsNum { get; private set; } = "{ntf_tickets_num}";

        public string CiTicketsNum { get; private set; } = "{ci_tickets_num}";
    }
}
