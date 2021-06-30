
using System.Collections.Generic;
using System.ComponentModel;

namespace RespawnTimer
{
    public class Config 
    {
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

        public void Load()
        {
            IsEnabled = Qurre.Plugin.Config.GetBool("respawn_timer_enable", IsEnabled, "is enabled?");
            ShowDebugMessages = Qurre.Plugin.Config.GetBool("respawn_timer_debug", ShowDebugMessages, "show debug messages");  
            Interval = Qurre.Plugin.Config.GetFloat("respawn_timer_interval", Interval, "interval betweeen show");
            TextLowering = Qurre.Plugin.Config.GetByte("respawn_timer_textlowering", TextLowering, "But not upper");
            ShowMinutes = Qurre.Plugin.Config.GetBool("respawn_timer_showminutes", ShowMinutes, "show minutes");
            ShowSeconds = Qurre.Plugin.Config.GetBool("respawn_timer_showseconds", ShowSeconds, "show seconds");
            ShowTimerOnlyOnSpawn = Qurre.Plugin.Config.GetBool("respawn_timer_showtimeronlyonspawn", ShowTimerOnlyOnSpawn, "?");
            ShowNumberOfSpectators = Qurre.Plugin.Config.GetBool("respawn_timer_shownumberofspectators", ShowNumberOfSpectators, "Num");
            ShowTickets = Qurre.Plugin.Config.GetBool("respawn_timer_showtickets", ShowTickets, "show tickets?");
            translations.Ci= Qurre.Plugin.Config.GetString("respawn_timer_translation_ci", translations.Ci, "ci");
            translations.CiTickets = Qurre.Plugin.Config.GetString("respawn_timer_translation_citickets", translations.CiTickets, "citickets");
            translations.CiTicketsNum = Qurre.Plugin.Config.GetString("respawn_timer_translation_citicketsnum", translations.CiTicketsNum, "num");
            translations.Minutes = Qurre.Plugin.Config.GetString("respawn_timer_translation_minutes", translations.Minutes);
            translations.Seconds = Qurre.Plugin.Config.GetString("respawn_timer_translation_seconds", translations.Seconds);
            translations.Sh = Qurre.Plugin.Config.GetString("respawn_timer_translation_sh", translations.Sh);
            translations.Spectators = Qurre.Plugin.Config.GetString("respawn_timer_translation_spectators", translations.Spectators);
            translations.SpectatorsNum = Qurre.Plugin.Config.GetString("respawn_timer_translation_spectatorsnum", translations.SpectatorsNum);
            translations.YouWillRespawnIn = Qurre.Plugin.Config.GetString("respawn_timer_translation_youwillrespawnin", translations.YouWillRespawnIn);
            translations.YouWillSpawnAs = Qurre.Plugin.Config.GetString("respawn_timer_translation_youwillrespawnas", translations.YouWillSpawnAs);
            translations.Ntf = Qurre.Plugin.Config.GetString("respawn_timer_translation_ntf", translations.Ntf);
            translations.NtfTickets = Qurre.Plugin.Config.GetString("respawn_timer_translation_ntftickets", translations.NtfTickets);
            translations.NtfTicketsNum = Qurre.Plugin.Config.GetString("respawn_timer_translation_ntfticketsnum", translations.NtfTicketsNum);
        }
    }
}