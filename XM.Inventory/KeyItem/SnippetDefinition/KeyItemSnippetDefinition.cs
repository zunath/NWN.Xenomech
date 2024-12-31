using System.Collections.Generic;
using Anvil.Services;
using NLog;
using XM.Dialog.Snippet;

namespace XM.Inventory.KeyItem.SnippetDefinition
{
    [ServiceBinding(typeof(ISnippetListDefinition))]
    public class KeyItemSnippetDefinition : ISnippetListDefinition
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly SnippetBuilder _builder = new();
        private readonly KeyItemService _keyItem;

        public KeyItemSnippetDefinition(KeyItemService keyItem)
        {
            _keyItem = keyItem;
        }

        public Dictionary<string, SnippetDetail> BuildSnippets()
        {
            // Conditions
            ConditionAllKeyItems();

            // Actions
            ActionGiveKeyItem();

            return _builder.Build();
        }

        private void ConditionAllKeyItems()
        {
            _builder.Create("condition-all-key-items")
                .Description("Checks whether a player has all of the specified key items.")
                .AppearsWhenAction((player, args) =>
                {
                    if (args.Length <= 0)
                    {
                        const string Error = "'condition-all-key-items' requires a keyItemId argument.";
                        SendMessageToPC(player, Error);
                        _logger.Error(Error);
                        return false;
                    }

                    foreach (var arg in args)
                    {
                        KeyItemType type;

                        // Try searching by Id first.
                        if (int.TryParse(arg, out var argId))
                        {
                            type = _keyItem.GetKeyItemTypeById(argId);
                        }
                        // Couldn't parse an integer. Look by name.
                        else
                        {
                            type = _keyItem.GetKeyItemTypeByName(arg);
                        }

                        // Type is invalid, log an error and end.
                        if (type == KeyItemType.Invalid)
                        {
                            _logger.Error($"{arg} is not a valid KeyItemType");
                            return false;
                        }

                        // Player doesn't have the specified key item.
                        if (!_keyItem.HasKeyItem(player, type))
                        {
                            return false;
                        }
                    }

                    return true;
                });

        }

        private void ActionGiveKeyItem()
        {
            _builder.Create("action-give-key-items")
                .Description("Gives a one or more key items to the player.")
                .ActionsTakenAction((player, args) =>
                {
                    if (args.Length <= 0)
                    {
                        const string Error = "'action-give-key-items' requires a keyItemId argument.";
                        SendMessageToPC(player, Error);
                        _logger.Error(Error);
                        return;
                    }

                    foreach (var arg in args)
                    {
                        KeyItemType type;

                        // Try searching by Id first.
                        if (int.TryParse(arg, out var argId))
                        {
                            type = _keyItem.GetKeyItemTypeById(argId);
                        }
                        // Couldn't parse an integer. Look by name.
                        else
                        {
                            type = _keyItem.GetKeyItemTypeByName(arg);
                        }

                        // Type is invalid, log an error and end.
                        if (type == KeyItemType.Invalid)
                        {
                            _logger.Error($"{arg} is not a valid KeyItemType");
                            return;
                        }

                        _keyItem.GiveKeyItem(player, type);
                    }
                });
        }
    }
}
