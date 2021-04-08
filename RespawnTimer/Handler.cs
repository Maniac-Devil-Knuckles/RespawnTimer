using System;
using System.Text;
using System.Linq;
using Qurre;
using Qurre.API;
using System.Collections.Generic;
using MEC;
using Respawning;
using UnityEngine.Playables;
using UnityEngine;

namespace RespawnTimer
{
    class Handler
    {
        public static int TimeUntilRespawn => Mathf.RoundToInt(RespawnManager.Singleton._timeForNextSequence - (float)RespawnManager.Singleton._stopwatch.Elapsed.TotalSeconds);
        public static bool IsSpawning => RespawnManager.Singleton._curSequence == RespawnManager.RespawnSequencePhase.PlayingEntryAnimations || RespawnManager.Singleton._curSequence == RespawnManager.RespawnSequencePhase.SpawningSelectedTeam;
        private readonly RespawnTimer plugin;
        public Handler(RespawnTimer plugin) => this.plugin = plugin;

        CoroutineHandle timerCoroutine = new CoroutineHandle();

        string text;

        List<Player> Spectators = new List<Player>();


        public void OnRoundStart()
        {
            if(timerCoroutine.IsRunning)
            {
                Timing.KillCoroutines(timerCoroutine);
            }

            timerCoroutine = Timing.RunCoroutine(Timer());

          if(plugin.cfg.ShowDebugMessages)  Log.Debug($"RespawnTimer coroutine started successfully! The timer will be refreshed every {plugin.cfg.Interval} second/s!");
        }

        private IEnumerator<float> Timer()
        {
            for(; ; )
            {
                
                yield return Timing.WaitForSeconds(plugin.cfg.Interval);
                if (!Round.IsStarted) continue;
                try
                {
                    if (!IsSpawning && plugin.cfg.ShowTimerOnlyOnSpawn) continue;

                    text = string.Empty;

                    text += new string('\n', plugin.cfg.TextLowering);

                    text += $"{plugin.cfg.translations.YouWillRespawnIn}\n";


                    if (plugin.cfg.ShowMinutes) text += plugin.cfg.translations.Minutes;
                    if (plugin.cfg.ShowSeconds) text += plugin.cfg.translations.Seconds;

                    if (IsSpawning)
                    {
                        if (plugin.cfg.ShowMinutes) text = text.Replace("{minutes}", (TimeUntilRespawn / 60).ToString()); ;

                        if (plugin.cfg.ShowSeconds)
                        {
                            if (plugin.cfg.ShowMinutes) text = text.Replace("{seconds}", (TimeUntilRespawn % 60).ToString());

                            else text = text.Replace("{seconds}", TimeUntilRespawn.ToString());
                        }
                    }
                    else
                    {
                        if (plugin.cfg.ShowMinutes) text = text.Replace("{minutes}", $"{(TimeUntilRespawn + 15) / 60}");

                        if (plugin.cfg.ShowSeconds)
                        {
                            if (plugin.cfg.ShowMinutes) text = text.Replace("{seconds}", $"{(TimeUntilRespawn + 15) % 60}");

                            else text = text.Replace("{seconds}", $"{(TimeUntilRespawn + 15) % 60}");
                        }
                    }
                    
                    text += "\n";

                    if (RespawnManager.Singleton.NextKnownTeam != SpawnableTeamType.None)
                    {
                        text += plugin.cfg.translations.YouWillSpawnAs;

                        if (RespawnManager.Singleton.NextKnownTeam == SpawnableTeamType.NineTailedFox)
                        {
                            text += plugin.cfg.translations.Ntf;

                        }
                        else
                        {
                            text += plugin.cfg.translations.Ci;
                        }

                        
                           if(RespawnTimer.assemblySH) SerpentsHandTeam();
                    }

                    text += new string('\n', 14 - plugin.cfg.TextLowering - Convert.ToInt32(plugin.cfg.ShowNumberOfSpectators));


                    Spectators = Player.Get(Team.RIP).ToList();

                    //if (RespawnTimer.assemblyGS)
                      //  GhostSpectatorPlayers();

                    if (plugin.cfg.ShowNumberOfSpectators)
                    {
                        text += $"<align=right>{plugin.cfg.translations.Spectators} {plugin.cfg.translations.SpectatorsNum}\n</align>";
                        text = text.Replace("{spectators_num}", Spectators.Count().ToString());
                    }

                    if (plugin.cfg.ShowTickets)
                    {
                        text += $"<align=right>{plugin.cfg.translations.NtfTickets} {plugin.cfg.translations.NtfTicketsNum}</align>" +
                                    $"\n<align=right>{plugin.cfg.translations.CiTickets} {plugin.cfg.translations.CiTicketsNum}</align>";


                        text = text.Replace("{ntf_tickets_num}", $"{RespawnTickets.Singleton.GetAvailableTickets(SpawnableTeamType.NineTailedFox)}");
                        text = text.Replace("{ci_tickets_num}", $"{RespawnTickets.Singleton.GetAvailableTickets(SpawnableTeamType.ChaosInsurgency)}");
                    }   
                    

                    foreach (Player ply in Spectators)
                    {
                        ply.ShowHint(text,  plugin.cfg.Interval+0.3f);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }



        public void SerpentsHandTeam()
        {
            try
            {
                if (SerpentsHand.EventHandlers.isSpawnable)
                {
                    if (RespawnManager.Singleton.NextKnownTeam == SpawnableTeamType.ChaosInsurgency) text = text.Replace(plugin.cfg.translations.Ci, plugin.cfg.translations.Sh);
                    else text = text.Replace(plugin.cfg.translations.Ntf, plugin.cfg.translations.Sh);
                }
            }
            catch (Exception) { }
        }
    }
}

