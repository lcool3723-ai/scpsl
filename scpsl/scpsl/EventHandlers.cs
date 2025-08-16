using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using scpsl.AdminSystem;
using System;

namespace scpsl
{
    public class EventHandlers
    {
        private readonly Plugin plugin;

        public EventHandlers(Plugin plugin)
        {
            this.plugin = plugin;
        }

        public void OnWaitingForPlayers()
        {
            if (plugin.Config.Debug)
                Log.Info("[DEBUG] Server is waiting for players.");
        }

        public void OnRoundStarted()
        {
            if (plugin.Config.Debug)
                Log.Info("[DEBUG] Round has started.");
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            if (plugin.Config.Debug)
                Log.Info($"[DEBUG] Round ended. Leading team: {ev.LeadingTeam}");
        }

        public void OnPlayerVerified(VerifiedEventArgs ev)
        {
            try
            {
                // Проверяем, что игрок не null
                if (ev?.Player == null)
                {
                    if (plugin.Config.Debug)
                        Log.Info("[DEBUG] OnPlayerVerified: Player is null");
                    return;
                }

                // В событии Verified игрок уже полностью инициализирован
                string nickname = ev.Player.Nickname ?? "Unknown";
                string userId = ev.Player.UserId ?? "Unknown";
                string ipAddress = ev.Player.IPAddress ?? "Unknown";

                // Сохраняем время подключения для статистики
                ev.Player.SessionVariables["JoinTime"] = DateTime.Now;

                if (plugin.Config.ShowJoinLeaveMessages)
                {
                    Log.Info($"{nickname} ({userId}) joined the server.");
                }

                // Проверяем, является ли игрок админом
                var adminLevel = AdminPermissions.GetAdminLevel(ev.Player);
                if (adminLevel != "Player")
                {
                    if (plugin.Config.EnableAdminLogging)
                    {
                        AdminLogger.LogAction(ev.Player, "ADMIN_JOINED", "", $"Level: {adminLevel}");
                    }
                    
                    if (plugin.Config.BroadcastAdminActivity)
                    {
                        Map.Broadcast(3, $"<color=yellow>{adminLevel} {nickname} joined the server</color>");
                    }
                }

                // Показываем приветственное сообщение
                if (!string.IsNullOrEmpty(plugin.Config.WelcomeMessage))
                {
                    try
                    {
                        if (ev.Player.IsConnected)
                        {
                            ev.Player.ShowHint(plugin.Config.WelcomeMessage, 5);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Log.Error($"Error showing welcome message: {ex.Message}");
                    }
                }

                if (plugin.Config.Debug)
                    Log.Info($"[DEBUG] Player {nickname} verified with IP: {ipAddress}");
            }
            catch (System.Exception ex)
            {
                Log.Error($"Error in OnPlayerVerified: {ex.Message}");
                if (plugin.Config.Debug)
                    Log.Error($"[DEBUG] OnPlayerVerified exception: {ex}");
            }
        }

        public void OnPlayerDestroying(DestroyingEventArgs ev)
        {
            try
            {
                // Проверяем, что игрок не null
                if (ev?.Player == null)
                {
                    if (plugin.Config.Debug)
                        Log.Info("[DEBUG] OnPlayerDestroying: Player is null");
                    return;
                }

                // Безопасно получаем информацию об игроке
                string nickname = ev.Player.Nickname ?? "Unknown";
                string userId = ev.Player.UserId ?? "Unknown";

                // Проверяем, был ли это админ
                var adminLevel = AdminPermissions.GetAdminLevel(ev.Player);
                if (adminLevel != "Player" && plugin.Config.EnableAdminLogging)
                {
                    AdminLogger.LogAction(ev.Player, "ADMIN_LEFT", "", $"Level: {adminLevel}");
                }

                if (plugin.Config.ShowJoinLeaveMessages)
                {
                    Log.Info($"{nickname} ({userId}) left the server.");
                }

                if (plugin.Config.Debug)
                    Log.Info($"[DEBUG] Player {nickname} is leaving the server.");
            }
            catch (System.Exception ex)
            {
                Log.Error($"Error in OnPlayerDestroying: {ex.Message}");
                if (plugin.Config.Debug)
                    Log.Error($"[DEBUG] OnPlayerDestroying exception: {ex}");
            }
        }
    }
}
