using CommandSystem;
using Exiled.API.Features;
using scpsl.AdminSystem;
using System;
using System.Linq;
using System.Text;

namespace scpsl.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AdminHelpCommand : ICommand
    {
        public string Command => "ahelp";
        public string[] Aliases => new[] { "adminhelp", "ah" };
        public string Description => "Show available admin commands";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (Plugin.Instance?.Config.EnableAdminLogging == true)
            {
                AdminLogger.LogAction(null, "HELP_COMMAND", "", "Admin help requested");
            }

            var sb = new StringBuilder();
            sb.AppendLine("=== ADMIN COMMANDS ===");
            sb.AppendLine("• ahelp - Show this help");
            sb.AppendLine("• aonline - Show online players");
            sb.AppendLine("• abroadcast <message> - Broadcast to all players");
            sb.AppendLine("• alog <message> - Log admin message");
            sb.AppendLine("• aserver - Show server info");
            sb.AppendLine("======================");

            response = sb.ToString();
            return true;
        }
    }

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AdminOnlineCommand : ICommand
    {
        public string Command => "aonline";
        public string[] Aliases => new[] { "alist", "players" };
        public string Description => "Show online players list";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (Plugin.Instance?.Config.EnableAdminLogging == true)
            {
                AdminLogger.LogAction(null, "ONLINE_LIST", "", "Player list requested");
            }

            var players = Player.List.ToList();
            var sb = new StringBuilder();
            
            sb.AppendLine($"=== ONLINE PLAYERS ({players.Count}/{Server.MaxPlayerCount}) ===");
            
            foreach (var player in players.OrderBy(p => p.Nickname))
            {
                var adminLevel = AdminPermissions.GetAdminLevel(player);
                var status = player.IsAlive ? $"Alive ({player.Role.Type})" : "Dead";
                var adminTag = adminLevel != "Player" ? $" [{adminLevel}]" : "";
                
                sb.AppendLine($"• {player.Nickname}{adminTag} | {status}");
            }

            response = sb.ToString();
            return true;
        }
    }

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AdminBroadcastCommand : ICommand
    {
        public string Command => "abroadcast";
        public string[] Aliases => new[] { "abc", "adminbc" };
        public string Description => "Broadcast a message to all players";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 1)
            {
                response = "Usage: abroadcast <message>";
                return false;
            }

            var message = string.Join(" ", arguments);
            
            Map.Broadcast(10, $"<color=red>[ADMIN]:</color>\n<color=white>{message}</color>");
            
            if (Plugin.Instance?.Config.EnableAdminLogging == true)
            {
                AdminLogger.LogAction(null, "BROADCAST", "", message);
            }
            
            response = $"Broadcasted message: {message}";
            return true;
        }
    }

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AdminLogCommand : ICommand
    {
        public string Command => "alog";
        public string[] Aliases => new[] { "adminlog" };
        public string Description => "Log an admin message";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 1)
            {
                response = "Usage: alog <message>";
                return false;
            }

            var message = string.Join(" ", arguments);
            
            if (Plugin.Instance?.Config.EnableAdminLogging == true)
            {
                AdminLogger.LogAction(null, "ADMIN_NOTE", "", message);
            }
            
            Log.Info($"[ADMIN LOG] {message}");
            response = $"Logged message: {message}";
            return true;
        }
    }

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AdminServerCommand : ICommand
    {
        public string Command => "aserver";
        public string[] Aliases => new[] { "serverinfo", "sinfo" };
        public string Description => "Show server information";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (Plugin.Instance?.Config.EnableAdminLogging == true)
            {
                AdminLogger.LogAction(null, "SERVER_INFO", "", "Server info requested");
            }

            var sb = new StringBuilder();
            
            sb.AppendLine("=== SERVER INFORMATION ===");
            sb.AppendLine($"• Server Name: {Server.Name}");
            sb.AppendLine($"• Players: {Player.List.Count()}/{Server.MaxPlayerCount}");
            sb.AppendLine($"• Port: {Server.Port}");
            sb.AppendLine($"• TPS: {Server.Tps:F1}");
            sb.AppendLine($"• Round Status: {(Round.IsStarted ? "In Progress" : "Waiting")}");
            sb.AppendLine($"• Plugin Version: {Plugin.Instance.Version}");
            sb.AppendLine($"• Admin Logging: {Plugin.Instance.Config.EnableAdminLogging}");

            response = sb.ToString();
            return true;
        }
    }
}
