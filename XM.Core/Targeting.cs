using System;
using System.Collections.Generic;
using Anvil.API;
using Anvil.API.Events;
using Anvil.Services;
using XM.API;
using XM.API.Constants;

namespace XM.Core
{
    [ServiceBinding(typeof(Targeting))]
    public class Targeting
    {
        private static readonly Dictionary<uint, Action<uint>> _playerTargetingActions = new();

        public Targeting()
        {
            HookEvents();
        }

        private void HookEvents()
        {
            NwModule.Instance.OnPlayerTarget += RunTargetedItemAction;
        }

        /// <summary>
        /// When a player targets an object, execute the assigned action.
        /// </summary>
        private void RunTargetedItemAction(ModuleEvents.OnPlayerTarget obj)
        {
            var player = GetLastPlayerToSelectTarget();
            if (!_playerTargetingActions.ContainsKey(player))
                return;
            var targetedObject = GetTargetingModeSelectedObject();

            if (GetIsObjectValid(targetedObject))
            {
                _playerTargetingActions[player](targetedObject);
                _playerTargetingActions[player] = null;
            }
        }

        /// <summary>
        /// Forces player to enter targeting mode.
        /// When the player targets an object, the selectionAction specified will run.
        /// </summary>
        /// <param name="player">The player entering targeting mode.</param>
        /// <param name="objectType">The types of objects allowed to be targeted.</param>
        /// <param name="selectionAction">The action to run when an object is targeted.</param>
        /// <param name="message">The message to send to the player when entering targeting mode.</param>
        public static void EnterTargetingMode(
            uint player,
            ObjectType objectType,
            string message,
            Action<uint> selectionAction)
        {
            NWScript.EnterTargetingMode(player, objectType);
            _playerTargetingActions[player] = selectionAction;

            if (!string.IsNullOrWhiteSpace(message))
            {
                SendMessageToPC(player, message);
            }
        }
    }
}
