using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace scpsl.AdminSystem
{
    public static class AdminPermissions
    {
        // Базовые разрешения
        public const string BASIC_ADMIN = "admin.basic";
        public const string MODERATOR = "admin.moderator";
        public const string SENIOR_ADMIN = "admin.senior";
        public const string OWNER = "admin.owner";

        // Разрешения для команд
        public const string HEAL = "admin.heal";
        public const string TELEPORT = "admin.teleport";
        public const string GIVE_ITEM = "admin.give";
        public const string FORCE_CLASS = "admin.forceclass";
        public const string KICK = "admin.kick";
        public const string BAN = "admin.ban";
        public const string MUTE = "admin.mute";
        public const string GOD_MODE = "admin.god";
        public const string NOCLIP = "admin.noclip";
        public const string CLEANUP = "admin.cleanup";
        public const string BROADCAST = "admin.broadcast";
        public const string PLAYER_INFO = "admin.playerinfo";
        public const string SERVER_INFO = "admin.serverinfo";
        public const string FORCE_ROUND_END = "admin.forceend";
        public const string FORCE_ROUND_RESTART = "admin.forcerestart";

        public static bool HasPermission(Player player, string permission)
        {
            if (player == null)
                return true; // Console всегда имеет доступ

            // Владелец сервера имеет все права
            if (player.CheckPermission(OWNER))
                return true;

            // Проверяем конкретное разрешение
            return player.CheckPermission(permission);
        }

        public static bool IsAdmin(Player player)
        {
            if (player == null)
                return true;

            return player.CheckPermission(BASIC_ADMIN) || 
                   player.CheckPermission(MODERATOR) || 
                   player.CheckPermission(SENIOR_ADMIN) || 
                   player.CheckPermission(OWNER);
        }

        public static string GetAdminLevel(Player player)
        {
            if (player == null)
                return "Console";

            if (player.CheckPermission(OWNER))
                return "Owner";
            if (player.CheckPermission(SENIOR_ADMIN))
                return "Senior Admin";
            if (player.CheckPermission(MODERATOR))
                return "Moderator";
            if (player.CheckPermission(BASIC_ADMIN))
                return "Admin";

            return "Player";
        }
    }
}
