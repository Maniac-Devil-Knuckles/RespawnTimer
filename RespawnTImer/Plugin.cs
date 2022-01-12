using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Qurre;
using Qurre.API;
using Qurre.API.Events;
using Respawning;

namespace RespawnTimerQurre
{
    public class Plugin : Qurre.Plugin
    {
        public override string Developer => "Manniac Devil Knuckles";

        public override string Name => "RespawnTimer";

        public override Version NeededQurreVersion => new Version(1,11,4);

        public override Version Version => new Version(1,1,4);

        public override int Priority => 10;

        public Config Cfg { get; } = new Config();

        private CoroutineHandle Timer = new CoroutineHandle();

        private string text = string.Empty;

        private static int TimeUntilRespawn => Convert.ToInt32(Round.NextRespawn);
        private static bool IsSpawning => RespawnManager.CurrentSequence() == RespawnManager.RespawnSequencePhase.PlayingEntryAnimations || RespawnManager.CurrentSequence() == RespawnManager.RespawnSequencePhase.SpawningSelectedTeam;

        public override void Enable()
        {
              Log.Info("Enabling RespawnTimer...");

            CustomConfigs.Add(Cfg);

            if (!Cfg.IsEnabled)
            {
                Log.Warn("Plugin Is Disabled By Cfg");
                return;
            }
            Qurre.Events.Round.Start += Round_Start;
            Qurre.Events.Round.End += Round_End;
        }

        public override void Disable()
        {
            Qurre.Events.Round.Start -= Round_Start;
            Qurre.Events.Round.End -= Round_End;
            CustomConfigs.Remove(Cfg);
        }
        internal void Round_Start()
        {
            Timer = RunTimer().RunCoroutine("timerrespawn");
        }
        internal void Round_End(RoundEndEvent _)
        {
            if(Timer.IsRunning) Timing.KillCoroutines(Timer);
        }

        internal IEnumerator<float> RunTimer()
        {
            yield return 1f;
            for (; ; )
            {
                yield return Cfg.Interval;
                try
                {
                    if (!IsSpawning && Cfg.ShowTimerOnlyOnSpawn) continue;

                    text = string.Empty;

                    text += new string('\n', Cfg.TextLowering);

                    text += $"{Cfg.YouWillRespawnIn}\n";

                    if (Cfg.ShowMinutes) text += Cfg.Minutes;

                    if (Cfg.ShowSeconds) text += Cfg.Seconds;

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
                        text += Cfg.YouWillSpawnAs;

                        if (RespawnManager.Singleton.NextKnownTeam == SpawnableTeamType.NineTailedFox)
                        {
                            text += Cfg.Ntf;
                        }
                        else
                        {
                            text += Cfg.Ci;
                        }
                    }
                    if (Cfg.ShowTickets)
                    {
                        text += $"<align=right>{Cfg.NtfTickets} {Cfg.NtfTicketsNum}</align>" +
                                    $"\n<align=right>{Cfg.CiTickets} {Cfg.CiTicketsNum}</align>";

                        text = text.Replace("{ntf_tickets_num}", $"{RespawnTickets.Singleton.GetAvailableTickets(SpawnableTeamType.NineTailedFox)}");

                        text = text.Replace("{ci_tickets_num}", $"{RespawnTickets.Singleton.GetAvailableTickets(SpawnableTeamType.ChaosInsurgency)}");
                    }

                    foreach (Player player in Player.Get(RoleType.Spectator))
                    {
                        player.ShowHint(text, Cfg.Interval + 0.5f);
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