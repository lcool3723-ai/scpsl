using Exiled.API.Features;
using System;

namespace scpsl
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "SCP:SL Admin Tools";
        public override string Author { get; } = "amongas";
        public override Version Version { get; } = new Version(1, 1, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public static Plugin Instance { get; private set; } = null!;
        
        private EventHandlers? eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            
            // Сохраняем время запуска сервера
            Server.Host.SessionVariables["ServerStartTime"] = System.DateTime.Now;
            
            eventHandlers = new EventHandlers(this);
            RegisterEvents();
            
            Log.Info($"{Name} v{Version} by {Author} has been enabled!");
            Log.Info($"Admin logging: {Config.EnableAdminLogging}");
            Log.Info($"Admin activity broadcast: {Config.BroadcastAdminActivity}");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
            eventHandlers = null;
            Instance = null!;
            
            Log.Info($"{Name} has been disabled!");
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            if (eventHandlers != null)
            {
                // Серверные события
                Exiled.Events.Handlers.Server.WaitingForPlayers += eventHandlers.OnWaitingForPlayers;
                Exiled.Events.Handlers.Server.RoundStarted += eventHandlers.OnRoundStarted;
                Exiled.Events.Handlers.Server.RoundEnded += eventHandlers.OnRoundEnded;
                
                // Безопасные события игроков (срабатывают после полной инициализации)
                Exiled.Events.Handlers.Player.Verified += eventHandlers.OnPlayerVerified;
                Exiled.Events.Handlers.Player.Destroying += eventHandlers.OnPlayerDestroying;
                
                Log.Info("Events registered (using safe player events)");
            }
        }

        private void UnregisterEvents()
        {
            if (eventHandlers != null)
            {
                // Серверные события
                Exiled.Events.Handlers.Server.WaitingForPlayers -= eventHandlers.OnWaitingForPlayers;
                Exiled.Events.Handlers.Server.RoundStarted -= eventHandlers.OnRoundStarted;
                Exiled.Events.Handlers.Server.RoundEnded -= eventHandlers.OnRoundEnded;
                
                // Безопасные события игроков
                Exiled.Events.Handlers.Player.Verified -= eventHandlers.OnPlayerVerified;
                Exiled.Events.Handlers.Player.Destroying -= eventHandlers.OnPlayerDestroying;
                
                Log.Info("Events unregistered");
            }
        }


    }
}
