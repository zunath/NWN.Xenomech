using System.Collections.Generic;
using Anvil.Services;
using NLog;
using XM.API;
using XM.API.Constants;

namespace XM.Dialog.Snippet.SnippetDefinition
{
    [ServiceBinding(typeof(ISnippetListDefinition))]
    public class MerchantSnippetDefinition: ISnippetListDefinition
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly SnippetBuilder _builder = new();

        public Dictionary<string, SnippetDetail> BuildSnippets()
        {
            // Conditions

            // Actions
            OpenStore();

            return _builder.Build();
        }

        private void OpenStore()
        {
            _builder.Create("action-open-store")
                .Description("Opens a store. If store tag isn't specified, the nearest store to the NPC will be opened.")
                .ActionsTakenAction((player, args) =>
                {

                    var npc = OBJECT_SELF;
                    var store = GetNearestObject(ObjectType.Store, npc);
                    if (args.Length > 0)
                    {
                        var storeTag = args[0];
                        store = GetNearestObjectByTag(storeTag, npc);
                    }

                    if (!GetIsObjectValid(store))
                    {
                        _logger.Error($"{GetName(npc)} could not locate a valid store. Check conversation for incorrect snippet parameters.");
                    }

                    NWScript.OpenStore(store, player);
                });
        }

    }
}
