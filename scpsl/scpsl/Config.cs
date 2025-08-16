using Exiled.API.Interfaces;
using System.ComponentModel;

namespace scpsl
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enabled or not.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether debug messages should be shown.")]
        public bool Debug { get; set; } = false;

        [Description("Welcome message shown to players when they join. Leave empty to disable.")]
        public string WelcomeMessage { get; set; } = "Успешно подключились к серверу";

        [Description("Show join/leave messages in console.")]
        public bool ShowJoinLeaveMessages { get; set; } = true;

        [Description("Enable admin action logging to files.")]
        public bool EnableAdminLogging { get; set; } = true;

        [Description("Show admin actions in server console.")]
        public bool ShowAdminActionsInConsole { get; set; } = true;

        [Description("Broadcast admin joins/leaves to players.")]
        public bool BroadcastAdminActivity { get; set; } = false;


    }
}
