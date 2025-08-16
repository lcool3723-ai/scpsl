using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using Exiled.API.Structs;
using AdminToys;
using System;
using UnityEngine;

namespace scpsl.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class PlayerParticleTextCommand : ICommand
    {
        public string Command => "pwrite";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Создаёт жёлтую частицу там, куда смотрит игрок";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is not Player player)
            {
                response = "Команду можно использовать только в игре.";
                return false;
            }

            var position = player.CameraTransform.position + player.CameraTransform.forward * 2f;

            var settings = new PrimitiveSettings
            {
                PrimitiveType = PrimitiveType.Sphere,
                Flags = PrimitiveFlags.None,
                Color = Color.yellow,
                Position = position,
                Rotation = Quaternion.identity,
                Scale = Vector3.one * 0.2f,
                IsStatic = true,
                ShouldSpawn = true,
            };

            Primitive.Create(settings);

            response = "Частица создана.";
            return true;
        }
    }
}
