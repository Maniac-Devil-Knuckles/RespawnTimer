
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

        public bool ShowNumberOfSpectators { get; set; } = true;

        public bool ShowTickets { get; set; } = true;

        public Translations translations { get; set; } = new Translations();

        public void Load()
        {
            IsEnabled = RespawnTimer.Config.GetBool("respawn_timer_enable", IsEnabled);
            ShowDebugMessages = RespawnTimer.Config.GetBool("respawn_timer_debug", ShowDebugMessages);
            Interval = RespawnTimer.Config.GetFloat("respawn_timer_interval", Interval);
            TextLowering = RespawnTimer.Config.GetByte("respawn_timer_textlowering", TextLowering);
            ShowMinutes = RespawnTimer.Config.GetBool("respawn_timer_showminutes", ShowMinutes);
            ShowSeconds = RespawnTimer.Config.GetBool("respawn_timer_showseconds", ShowSeconds);
            ShowTimerOnlyOnSpawn = RespawnTimer.Config.GetBool("respawn_timer_showtimeronlyonspawn", ShowTimerOnlyOnSpawn);
            ShowNumberOfSpectators = RespawnTimer.Config.GetBool("respawn_timer_shownumberofspectators", ShowNumberOfSpectators);
            ShowTickets = RespawnTimer.Config.GetBool("respawn_timer_showtickets", ShowTickets);
            translations.Ci= RespawnTimer.Config.GetString("respawn_timer_translation_ci", translations.Ci);
            translations.CiTickets = RespawnTimer.Config.GetString("respawn_timer_translation_citickets", translations.CiTickets);
            translations.CiTicketsNum = RespawnTimer.Config.GetString("respawn_timer_translation_citicketsnum", translations.CiTicketsNum);
            translations.Minutes = RespawnTimer.Config.GetString("respawn_timer_translation_minutes", translations.Minutes);
            translations.Seconds = RespawnTimer.Config.GetString("respawn_timer_translation_seconds", translations.Seconds);
            translations.Sh = RespawnTimer.Config.GetString("respawn_timer_translation_sh", translations.Sh);
            translations.Spectators = RespawnTimer.Config.GetString("respawn_timer_translation_spectators", translations.Spectators);
            translations.SpectatorsNum = RespawnTimer.Config.GetString("respawn_timer_translation_spectatorsnum", translations.SpectatorsNum);
            translations.YouWillRespawnIn = RespawnTimer.Config.GetString("respawn_timer_translation_youwillrespawnin", translations.YouWillRespawnIn);
            translations.YouWillSpawnAs = RespawnTimer.Config.GetString("respawn_timer_translation_youwillrespawnas", translations.YouWillSpawnAs);
            translations.Ntf = RespawnTimer.Config.GetString("respawn_timer_translation_ntf", translations.Ntf);
            translations.NtfTickets = RespawnTimer.Config.GetString("respawn_timer_translation_ntftickets", translations.NtfTickets);
            translations.NtfTicketsNum = RespawnTimer.Config.GetString("respawn_timer_translation_ntfticketsnum", translations.NtfTicketsNum);
        }
    }
}