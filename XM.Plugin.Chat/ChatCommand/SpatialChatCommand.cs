using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.ChatCommand;
using XM.Shared.Core.Localization;

namespace XM.Chat.ChatCommand
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    public class SpatialChatCommand : IChatCommandListDefinition
    {
        private readonly ChatCommandBuilder _builder = new();

        public Dictionary<LocaleString, ChatCommandDetail> BuildChatCommands()
        {
            Coordinates();
            Position();
            Time();

            return _builder.Build();
        }

        private void Coordinates()
        {
            _builder.Create(LocaleString.coord)
                .Description(LocaleString.DisplaysYourCurrentCoordinatesInTheArea)
                .Permissions(AuthorizationLevel.All)
                .Action((user, target, location, args) =>
                {
                    var position = GetPosition(user);
                    var cellX = (int)(position.X / 10);
                    var cellY = (int)(position.Y / 10);

                    var message = LocaleString.CurrentAreaCoordinatesXY.ToLocalizedString(cellX, cellY);
                    SendMessageToPC(user, message);
                });
        }

        private void Position()
        {
            _builder.Create(LocaleString.pos)
                .Description(LocaleString.DisplaysYourCurrentPositionInTheArea)
                .Permissions(AuthorizationLevel.All)
                .Action((user, target, location, args) =>
                {
                    var position = GetPosition(user);
                    SendMessageToPC(user, LocaleString.CurrentPositionXYZ.ToLocalizedString(position.X, position.Y, position.Z));
                });
        }

        private void Time()
        {
            _builder.Create(LocaleString.time)
                .Description(LocaleString.ReturnsTheCurrentUTCServerTimeAndTheInGameTime)
                .Permissions(AuthorizationLevel.All)
                .Action((user, target, location, args) =>
                {
                    var now = DateTime.UtcNow;
                    var nowText = now.ToString("yyyy-MM-dd HH:mm:ss");
                    var gameTime = GetTimeHour().ToString().PadLeft(2, '0') + ":" +
                                   GetTimeMinute().ToString().PadLeft(2, '0') + ":" +
                                   GetTimeSecond().ToString().PadLeft(2, '0');

                    SendMessageToPC(user, LocaleString.CurrentServerDateX.ToLocalizedString(nowText));
                    SendMessageToPC(user, LocaleString.CurrentWorldTimeX.ToLocalizedString(gameTime));
                });
        }
    }
}
