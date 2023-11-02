using System;
using System.Linq;
using Qurre;
using Qurre.API;
using System.Collections.Generic;
using MEC;
using Respawning;
using Qurre.Events.Structs;
using Qurre.Events;
using Qurre.API.Attributes;

namespace RespawnTimer
{
    internal static class Handler
    {
        internal static List<Player> Spectators = new List<Player>();

        public static int TimeUntilRespawn => Convert.ToInt32(Round.NextRespawn);

        public static bool IsSpawning => RespawnManager.CurrentSequence() == RespawnManager.RespawnSequencePhase.PlayingEntryAnimations || RespawnManager.CurrentSequence() == RespawnManager.RespawnSequencePhase.SpawningSelectedTeam;


        static CoroutineHandle timerCoroutine = new CoroutineHandle();

        static string text { get; set; } = "";

        [EventMethod(RoundEvents.Start)]
        internal static  void OnRoundStart(RoundStartedEvent ev)
        {
            Config.Reload();
            if (!Config.IsEnabled) return;
            if (timerCoroutine.IsRunning)
            {
                Timing.KillCoroutines(timerCoroutine);
            }

            timerCoroutine = Timer().RunCoroutine("respawntimer");
        }

        private static IEnumerator<float> Timer()
        {
            for(; Round.Started ; )
            {
                yield return Timing.WaitForSeconds(Config.Interval);
                try
                {
                    if (!IsSpawning && Config.ShowTimerOnlyOnSpawn) continue;

                    text = string.Empty;

                    text += new string('\n', Config.TextLowering);

                    text += $"{Config.translations.YouWillRespawnIn}\n";

                    if (Config.ShowMinutes) text += Config.translations.Minutes;

                    if (Config.ShowSeconds) text += Config.translations.Seconds;

                    if (IsSpawning)
                    {
                        if (Config.ShowMinutes) text = text.Replace("{minutes}", (TimeUntilRespawn / 60).ToString()); ;

                        if (Config.ShowSeconds)
                        {
                            if (Config.ShowMinutes) text = text.Replace("{seconds}", (TimeUntilRespawn % 60).ToString());

                            else text = text.Replace("{seconds}", TimeUntilRespawn.ToString());
                        }
                    }
                    else
                    {
                        if (Config.ShowMinutes) text = text.Replace("{minutes}", $"{(TimeUntilRespawn + 15) / 60}");

                        if (Config.ShowSeconds)
                        {
                            if (Config.ShowMinutes) text = text.Replace("{seconds}", $"{(TimeUntilRespawn + 15) % 60}");

                            else text = text.Replace("{seconds}", $"{(TimeUntilRespawn + 15) % 60}");
                        }
                    }
                    
                    text += "\n";

                    if (RespawnManager.Singleton.NextKnownTeam != SpawnableTeamType.None)
                    {
                        text += Config.translations.YouWillSpawnAs;

                        if (RespawnManager.Singleton.NextKnownTeam == SpawnableTeamType.NineTailedFox)
                        {
                            text += Config.translations.Ntf;

                        }
                        else
                        {
                            text += Config.translations.Ci;
                        }
                    }

                    text += new string('\n', 14 - Config.TextLowering - Convert.ToInt32(Config.ShowNumberOfSpectators));


                    Spectators = Player.List.Where(p=> p.RoleInfomation.Team== PlayerRoles.Team.Dead).ToList();

                    if (Config.ShowNumberOfSpectators)
                    {
                        text += $"<align=right>{Config.translations.Spectators} {Config.translations.SpectatorsNum}\n</align>";
                        text = text.Replace("{spectators_num}", Spectators.Count().ToString());
                    }

                    if (Config.ShowTickets)
                    {
                        text += $"<align=right>{Config.translations.NtfTickets} {Config.translations.NtfTicketsNum}</align>" +
                                    $"\n<align=right>{Config.translations.CiTickets} {Config.translations.CiTicketsNum}</align>";


                        text = text.Replace("{ntf_tickets_num}", RespawnTokensManager.GetTeamDominance(SpawnableTeamType.NineTailedFox).ToString());
                        text = text.Replace("{ci_tickets_num}", RespawnTokensManager.GetTeamDominance(SpawnableTeamType.ChaosInsurgency).ToString());
                    }   
                    

                    foreach (Player ply in Spectators)
                    {
                        ply.Client.HintDisplay.Show(new Hints.TextHint(text, new Hints.HintParameter[] { new Hints.StringHintParameter("") }, null, Config.Interval + 0.3f));
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

