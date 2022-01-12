using Qurre.API.Addons;
using System.ComponentModel;

namespace RespawnTimerQurre
{
    public class Config : IConfig
    {
        public string Name { get; set; } = "RespawnTimer";

        [Description("is enabled?")]
        public bool IsEnabled { get; private set; } = true;

        public string YouWillRespawnIn { get; private set; } = "<color=orange>You will respawn in: </color>";

        public string YouWillSpawnAs { get; private set; } = "You will spawn as: ";

        public string Ntf { get; private set; } = "<color=blue>Nine-Tailed Fox</color>";

        public string Ci { get; private set; } = "<color=green>Chaos Insurgency</color>";

        public string Spectators { get; private set; } = "<color=#B3B6B7>Spectators: </color>";

        public string NtfTickets { get; private set; } = "<color=blue>NTF Tickets: </color>";

        public string CiTickets { get; private set; } = "<color=green>CI Tickets: </color>";

        public string Seconds { get; private set; } = " <b>{seconds} s</b>";

        public string Minutes { get; private set; } = "<b>{minutes} min.</b>";

        [Description("interval betweeen show timer")]
        public float Interval { get; private set; } = 1f;

        public byte TextLowering { get; private set; } = 8;

        public bool ShowMinutes { get; private set; } = true;

        public bool ShowSeconds { get; private set; } = true;

        public bool ShowTimerOnlyOnSpawn { get; private set; } = false;

        public bool ShowNumberOfSpectators { get; private set; } = false;

        public bool ShowTickets { get; private set; } = false;

        public string NtfTicketsNum { get; private set; } = "{ntf_tickets_num}";

        public string CiTicketsNum { get; private set; } = "{ci_tickets_num}";
    }
}
