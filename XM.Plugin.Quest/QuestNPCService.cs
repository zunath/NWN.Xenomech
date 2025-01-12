using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using NLog;
using XM.Shared.Core.Extension;

namespace XM.Quest
{
    [ServiceBinding(typeof(QuestNPCService))]
    [ServiceBinding(typeof(IInitializable))]
    internal class QuestNPCService: IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<QuestNPCGroupType, QuestNPCGroupAttribute> _npcGroups = new();

        public void Init()
        {
            LoadQuestNPCGroups();
        }

        private void LoadQuestNPCGroups()
        {
            var npcGroups = Enum.GetValues(typeof(QuestNPCGroupType)).Cast<QuestNPCGroupType>();
            foreach (var npcGroupType in npcGroups)
            {
                var npcGroupDetail = npcGroupType.GetAttribute<QuestNPCGroupType, QuestNPCGroupAttribute>();
                _npcGroups[npcGroupType] = npcGroupDetail;
            }

            _logger.Info($"Loaded {_npcGroups.Count} NPC groups.");
        }

        /// <summary>
        /// Retrieves an NPC group detail by the type.
        /// </summary>
        /// <param name="type">The type of NPC group to retrieve.</param>
        /// <returns>An NPC group detail</returns>
        public QuestNPCGroupAttribute GetQuestNPCGroup(QuestNPCGroupType type)
        {
            return _npcGroups[type];
        }

    }
}
