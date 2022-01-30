using System;
using System.Linq;
using Qurre;
using Qurre.API;
using System.Collections.Generic;
using MEC;
using Respawning;

namespace RespawnTimer
{
    class Handler
    {
        List<Player> Spectators = new List<Player>();

        public static int TimeUntilRespawn => Convert.ToInt32(Round.NextRespawn);

        public static bool IsSpawning => RespawnManager.CurrentSequence() == RespawnManager.RespawnSequencePhase.PlayingEntryAnimations || RespawnManager.CurrentSequence() == RespawnManager.RespawnSequencePhase.SpawningSelectedTeam;

        private readonly Config CustomConfig;

        public Handler(Config CustomConfig) => this.CustomConfig = CustomConfig;

        CoroutineHandle timerCoroutine = new CoroutineHandle();

        string text { get; set; } = "";

        public void OnRoundStart()
        {
            if (timerCoroutine.IsRunning)
            {
                Timing.KillCoroutines(timerCoroutine);
            }

            timerCoroutine = Timer().RunCoroutine("respawntimer");
        }

        private IEnumerator<float> Timer()
        {
            for(; Round.Started ; )
            {
                yield return Timing.WaitForSeconds(CustomConfig.Interval);
                try
                {
                    if (!IsSpawning && CustomConfig.ShowTimerOnlyOnSpawn) continue;

                    text = string.Empty;

                    text += new string('\n', CustomConfig.TextLowering);

                    text += $"{CustomConfig.translations.YouWillRespawnIn}\n";

                    if (CustomConfig.ShowMinutes) text += CustomConfig.translations.Minutes;

                    if (CustomConfig.ShowSeconds) text += CustomConfig.translations.Seconds;

                    if (IsSpawning)
                    {
                        if (CustomConfig.ShowMinutes) text = text.Replace("{minutes}", (TimeUntilRespawn / 60).ToString()); ;

                        if (CustomConfig.ShowSeconds)
                        {
                            if (CustomConfig.ShowMinutes) text = text.Replace("{seconds}", (TimeUntilRespawn % 60).ToString());

                            else text = text.Replace("{seconds}", TimeUntilRespawn.ToString());
                        }
                    }
                    else
                    {
                        if (CustomConfig.ShowMinutes) text = text.Replace("{minutes}", $"{(TimeUntilRespawn + 15) / 60}");

                        if (CustomConfig.ShowSeconds)
                        {
                            if (CustomConfig.ShowMinutes) text = text.Replace("{seconds}", $"{(TimeUntilRespawn + 15) % 60}");

                            else text = text.Replace("{seconds}", $"{(TimeUntilRespawn + 15) % 60}");
                        }
                    }
                    
                    text += "\n";

                    if (RespawnManager.Singleton.NextKnownTeam != SpawnableTeamType.None)
                    {
                        text += CustomConfig.translations.YouWillSpawnAs;

                        if (RespawnManager.Singleton.NextKnownTeam == SpawnableTeamType.NineTailedFox)
                        {
                            text += CustomConfig.translations.Ntf;

                        }
                        else
                        {
                            text += CustomConfig.translations.Ci;
                        }
                    }

                    text += new string('\n', 14 - CustomConfig.TextLowering - Convert.ToInt32(CustomConfig.ShowNumberOfSpectators));


                    Spectators = Player.Get(Team.RIP).ToList();

                    if (CustomConfig.ShowNumberOfSpectators)
                    {
                        text += $"<align=right>{CustomConfig.translations.Spectators} {CustomConfig.translations.SpectatorsNum}\n</align>";
                        text = text.Replace("{spectators_num}", Spectators.Count().ToString());
                    }

                    if (CustomConfig.ShowTickets)
                    {
                        text += $"<align=right>{CustomConfig.translations.NtfTickets} {CustomConfig.translations.NtfTicketsNum}</align>" +
                                    $"\n<align=right>{CustomConfig.translations.CiTickets} {CustomConfig.translations.CiTicketsNum}</align>";


                        text = text.Replace("{ntf_tickets_num}", RespawnTickets.Singleton.GetAvailableTickets(SpawnableTeamType.NineTailedFox).ToString());
                        text = text.Replace("{ci_tickets_num}", RespawnTickets.Singleton.GetAvailableTickets(SpawnableTeamType.ChaosInsurgency).ToString());
                    }   
                    

                    foreach (Player ply in Spectators)
                    {
                        ply.ShowHint(text,  CustomConfig.Interval + 0.3f);
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

