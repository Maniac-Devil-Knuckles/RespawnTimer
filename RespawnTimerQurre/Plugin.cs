using MEC;
using Qurre;
using Qurre.API;
using Qurre.API.Events;
using Respawning;

namespace RespawnTimerQurre
{
    public class Plugin : Qurre.Plugin
    {
        public Translation Translation { get; } = new ();

        private CoroutineHandle Timer = new();

        private string text = string.Empty;
        
        private static List<Player> Spectators => Player.Get(Team.RIP).ToList();

        private static int TimeUntilRespawn => Convert.ToInt32(Round.NextRespawn);
        private static bool IsSpawning => RespawnManager.CurrentSequence() == RespawnManager.RespawnSequencePhase.PlayingEntryAnimations || RespawnManager.CurrentSequence() == RespawnManager.RespawnSequencePhase.SpawningSelectedTeam;

        public override void Enable()
        {
            Log.Info("Enabling RespawnTimer...");

            Cfg.Reload();

            CustomConfigs.Add(Translation);

            if (!Cfg.IsEnabled)
            {
                Log.Warn("Plugin Is Disabled By Config");
                return;
            }

            Qurre.Events.Round.Start += Round_Start;
            Qurre.Events.Round.End += Round_End;
        }

        public override void Disable()
        {

        }
        private void Round_Start()
        {
            Timer = RunTimer().RunCoroutine("respawntimer2");
        }
        private void Round_End(RoundEndEvent _)
        {
            Timing.KillCoroutines(Timer);
        }

        private IEnumerator<float> RunTimer()
        {
            yield return 1f;
            for (; Round.Started && !Round.Ended;)
            {
                yield return Cfg.Interval;
                try
                {
                    if (!IsSpawning && Cfg.ShowTimerOnlyOnSpawn) continue;

                    text = string.Empty;

                    text += new string('\n', Cfg.TextLowering);

                    text += $"{Translation.YouWillRespawnIn}\n";

                    if (Cfg.ShowMinutes) text += Translation.Minutes;

                    if (Cfg.ShowSeconds) text += Translation.Seconds;

                    if (IsSpawning)
                    {
                        if (Cfg.ShowMinutes) text = text.Replace("{minutes}", (TimeUntilRespawn / 60).ToString());

                        if (Cfg.ShowSeconds && Cfg.ShowMinutes) text = text.Replace("{seconds}", (TimeUntilRespawn % 60).ToString());

                        else if (Cfg.ShowSeconds) text = text.Replace("{seconds}", TimeUntilRespawn.ToString());
                    }
                    else
                    {
                        if (Cfg.ShowMinutes) text = text.Replace("{minutes}", $"{(TimeUntilRespawn + 15) / 60}");

                        if (Cfg.ShowSeconds && Cfg.ShowMinutes) text = text.Replace("{seconds}", $"{(TimeUntilRespawn + 15) % 60}");

                        else if (Cfg.ShowSeconds) text = text.Replace("{seconds}", $"{(TimeUntilRespawn + 15) % 60}");
                    }

                    text += "\n";

                    if (RespawnManager.Singleton.NextKnownTeam != SpawnableTeamType.None)
                    {
                        text += Translation.YouWillSpawnAs;

                        if (RespawnManager.Singleton.NextKnownTeam == SpawnableTeamType.NineTailedFox)
                        {
                            text += Translation.Ntf;

                        }
                        else
                        {
                            text += Translation.Ci;
                        }
                    }

                    if (Cfg.ShowTickets)
                    {
                        text += $"<align=right>{Translation.NtfTickets} {Translation.NtfTicketsNum}</align>" +
                                    $"\n<align=right>{Translation.CiTickets} {Translation.CiTicketsNum}</align>";

                        text = text.Replace("{ntf_tickets_num}", $"{RespawnTickets.Singleton.GetAvailableTickets(SpawnableTeamType.NineTailedFox)}");

                        text = text.Replace("{ci_tickets_num}", $"{RespawnTickets.Singleton.GetAvailableTickets(SpawnableTeamType.ChaosInsurgency)}");
                    }

                    foreach(Player player in Spectators)
                    {
                        player.ShowHint(text, Cfg.Interval + 0.3f);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    continue;
                }
            }
        }
    }
}