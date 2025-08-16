using Exiled.API.Features;
using System;
using System.IO;

namespace scpsl.AdminSystem
{
    public static class AdminLogger
    {
        private static readonly string LogPath = Path.Combine(Paths.Configs, "AdminLogs");
        private static readonly string LogFile = Path.Combine(LogPath, $"admin_actions_{DateTime.Now:yyyy-MM-dd}.log");

        static AdminLogger()
        {
            // Создаем папку для логов если не существует
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }
        }

        public static void LogAction(Player admin, string action, string target = "", string details = "")
        {
            try
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var adminInfo = $"{admin?.Nickname ?? "Console"} ({admin?.UserId ?? "SERVER"})";
                var logEntry = $"[{timestamp}] {adminInfo} | {action}";
                
                if (!string.IsNullOrEmpty(target))
                    logEntry += $" | Target: {target}";
                
                if (!string.IsNullOrEmpty(details))
                    logEntry += $" | Details: {details}";

                // Записываем в файл
                File.AppendAllText(LogFile, logEntry + Environment.NewLine);
                
                // Также выводим в консоль сервера
                Log.Info($"[ADMIN ACTION] {logEntry}");
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to log admin action: {ex.Message}");
            }
        }

        public static void LogCommand(Player admin, string command, string arguments = "")
        {
            var details = string.IsNullOrEmpty(arguments) ? command : $"{command} {arguments}";
            LogAction(admin, "COMMAND_EXECUTED", "", details);
        }

        public static void LogPlayerAction(Player admin, string action, Player target, string details = "")
        {
            var targetInfo = target != null ? $"{target.Nickname} ({target.UserId})" : "Unknown";
            LogAction(admin, action, targetInfo, details);
        }
    }
}
