# SCP:SL Admin Tools Plugin

A stable base plugin for SCP: Secret Laboratory using the EXILED framework with admin tools foundation.

## Features

- ✅ **Stable Plugin Structure** - Proper inheritance from `Plugin<Config>`
- ✅ **Safe Event Handling** - Uses `Player.Verified` and `Player.Destroying` events to prevent connection issues
- ✅ **Admin Command System** - Comprehensive admin tools with logging
- ✅ **Action Logging** - All admin actions logged to files and console
- ✅ **Permission System** - Flexible admin permission management
- ✅ **Configuration System** - Simple and effective config management
- ✅ **Welcome Messages** - Customizable player welcome system
- ✅ **Join/Leave Logging** - Optional player connection logging
- ✅ **EXILED 9.x Compatible** - Works with latest EXILED framework
- ✅ **No Player Kicks** - Tested and stable connection handling

## Setup

1. Make sure you have the EXILED framework installed on your SCP:SL server
2. Clone or download this repository
3. Modify the plugin details in `Plugin.cs`:
   - Change the `Name`, `Author`, and other properties
   - Update the namespace if desired
4. Build the project using `dotnet build` or Visual Studio
5. Copy the compiled DLL to your EXILED plugins folder

## Configuration

The plugin includes a configuration file (`Config.cs`) with the following options:

- `IsEnabled`: Whether the plugin is enabled
- `Debug`: Enable debug logging  
- `WelcomeMessage`: Message shown to joining players
- `ShowJoinLeaveMessages`: Whether to log player join/leave events
- `EnableAdminLogging`: Enable admin action logging to files
- `ShowAdminActionsInConsole`: Show admin actions in server console
- `BroadcastAdminActivity`: Broadcast admin joins/leaves to players

## Event Handlers

The plugin includes basic event handlers for:

- Server waiting for players
- Round start/end events
- Player verified/destroying events (safe alternatives to join/leave)

**Note**: We use `Player.Verified` instead of `Player.Joined` and `Player.Destroying` instead of `Player.Left` to avoid connection issues in EXILED 9.x.

You can extend the `EventHandlers.cs` file to add more functionality.

## Admin Commands

The plugin includes a comprehensive admin command system:

### Available Commands:
- `ahelp` - Show available admin commands
- `aonline` - Show online players with admin levels
- `abroadcast <message>` - Broadcast message to all players
- `alog <message>` - Log admin message to files
- `aserver` - Show detailed server information

### Admin Logging:
All admin actions are automatically logged to:
- **Files**: `%ExiledConfigs%/AdminLogs/admin_actions_YYYY-MM-DD.log`
- **Console**: Real-time admin action display
- **Tracking**: Admin joins, leaves, and all command usage

### Permission System:
The plugin includes a flexible permission system with levels:
- `admin.basic` - Basic admin permissions
- `admin.moderator` - Moderator level
- `admin.senior` - Senior admin level  
- `admin.owner` - Full owner permissions

## Building

```bash
dotnet restore
dotnet build
```

## Requirements

- .NET Framework 4.8
- EXILED 9.0.0 or higher
- SCP: Secret Laboratory server

## Version Compatibility

This plugin is built for EXILED 9.x. If you're using an older version of EXILED, you may need to adjust the API calls accordingly.

## What Makes This Plugin Special

- **🛡️ Connection Stability**: Uses safe `Player.Verified` and `Player.Destroying` events instead of problematic `Player.Joined`/`Player.Left` events
- **🔧 EXILED 9.x Ready**: Compatible with latest EXILED framework, handles API changes properly
- **⚡ Performance**: Lightweight and efficient, no unnecessary features
- **🔒 Error Handling**: Comprehensive exception handling prevents crashes
- **📝 Extensible**: Perfect foundation for building more complex plugins

## Extending the Plugin

This plugin serves as a solid foundation. You can easily add:

- **More Admin Commands**: Extend the `Commands` folder with additional commands
- **Advanced Player Management**: Add kick, ban, mute functionality
- **Custom Events**: Extend the `EventHandlers.cs` file
- **Database Integration**: Add database support for persistent data
- **API Integration**: Connect to external services
- **Game Modifications**: Add custom game mechanics

## File Structure:
```
scpsl/
├── AdminSystem/
│   ├── AdminLogger.cs      - Action logging system
│   └── AdminPermissions.cs - Permission management
├── Commands/
│   └── BasicAdminCommands.cs - Admin command implementations
├── Plugin.cs               - Main plugin class
├── Config.cs              - Configuration system
├── EventHandlers.cs       - Event handling
└── README.md             - Documentation
```

## License

This project is provided as-is for educational and development purposes.
