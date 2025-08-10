using System.Collections.Generic;
using Anvil.Services;
using XM.Codex.Codex.Model;
using XM.Shared.Core.Localization;

namespace XM.Codex.Codex
{
    [ServiceBinding(typeof(CodexContentService))]
    internal sealed class CodexContentService
    {
        public IReadOnlyList<CodexEntryRecord> GetAllEntries()
        {
            var entries = new List<CodexEntryRecord>();

            // History (Lore)
            entries.Add(new CodexEntryRecord(
                title: LocaleString.History,
                category: CodexCategory.Lore,
                content: LocaleString.WorldguideHistory));

            // Factions
            entries.Add(new CodexEntryRecord(
                title: LocaleString.HouseVerain,
                category: CodexCategory.Factions,
                content: LocaleString.HouseVerainDescription));
            entries.Add(new CodexEntryRecord(
                title: LocaleString.AurionUniversity,
                category: CodexCategory.Factions,
                content: LocaleString.AurionUniversityDescription));
            entries.Add(new CodexEntryRecord(
                title: LocaleString.TwilightConsortium,
                category: CodexCategory.Factions,
                content: LocaleString.TwilightConsortiumDescription));
            entries.Add(new CodexEntryRecord(
                title: LocaleString.VanguardWardens,
                category: CodexCategory.Factions,
                content: LocaleString.VanguardWardensDescription));

            // Regions
            entries.Add(new CodexEntryRecord(
                title: LocaleString.Zenithia,
                category: CodexCategory.Regions,
                content: LocaleString.ZenithiaDescription));
            entries.Add(new CodexEntryRecord(
                title: LocaleString.OvergrownExpanse,
                category: CodexCategory.Regions,
                content: LocaleString.OvergrownExpanseDescription));
            entries.Add(new CodexEntryRecord(
                title: LocaleString.OutlyingWastes,
                category: CodexCategory.Regions,
                content: LocaleString.OutlyingWastesDescription));
            entries.Add(new CodexEntryRecord(
                title: LocaleString.CrystalRavines,
                category: CodexCategory.Regions,
                content: LocaleString.CrystalRavinesDescription));

            // Culture (Lore)
            entries.Add(new CodexEntryRecord(
                title: LocaleString.CultureAndDailyLife,
                category: CodexCategory.Lore,
                content: LocaleString.CultureAndDailyLifeDescription));

            // Religions (Lore)
            entries.Add(new CodexEntryRecord(
                title: LocaleString.ReligionsAndBeliefs,
                category: CodexCategory.Lore,
                content: LocaleString.ReligionsAndBeliefsDescription));

            // Myths (Lore)
            entries.Add(new CodexEntryRecord(
                title: LocaleString.MythsAndLegends,
                category: CodexCategory.Lore,
                content: LocaleString.MythsAndLegendsDescription));

            // Threats - Summary and individual pages
            entries.Add(new CodexEntryRecord(
                title: LocaleString.BioMonsters,
                category: CodexCategory.Threats,
                content: LocaleString.BioMonstersDescription));
            entries.Add(new CodexEntryRecord(
                title: LocaleString.EpsilonFrames,
                category: CodexCategory.Threats,
                content: LocaleString.EpsilonFramesThreatDescription));
            entries.Add(new CodexEntryRecord(
                title: LocaleString.SerrakisCollective,
                category: CodexCategory.Threats,
                content: LocaleString.SerrakisCollectiveDescription));

            // Mechs
            entries.Add(new CodexEntryRecord(
                title: LocaleString.AurionFrames,
                category: CodexCategory.Technology,
                content: LocaleString.AurionFramesDescription));
            entries.Add(new CodexEntryRecord(
                title: LocaleString.EpsilonFrames,
                category: CodexCategory.Technology,
                content: LocaleString.EpsilonFramesDescription));
            entries.Add(new CodexEntryRecord(
                title: LocaleString.DivineFrames,
                category: CodexCategory.Technology,
                content: LocaleString.DivineFramesDescription));

            // Systems
            entries.Add(new CodexEntryRecord(
                title: LocaleString.ResonanceNodes,
                category: CodexCategory.Systems,
                content: LocaleString.ResonanceNodesDescription));
            entries.Add(new CodexEntryRecord(
                title: LocaleString.Teleportation,
                category: CodexCategory.Systems,
                content: LocaleString.TeleportationDescription));

            // Classes (summarized)
            entries.Add(new CodexEntryRecord(LocaleString.Keeper, CodexCategory.Classes, LocaleString.KeeperDescription));
            entries.Add(new CodexEntryRecord(LocaleString.Mender, CodexCategory.Classes, LocaleString.MenderDescription));
            entries.Add(new CodexEntryRecord(LocaleString.Beastmaster, CodexCategory.Classes, LocaleString.BeastmasterDescription));
            entries.Add(new CodexEntryRecord(LocaleString.Brawler, CodexCategory.Classes, LocaleString.BrawlerDescription));
            entries.Add(new CodexEntryRecord(LocaleString.Nightstalker, CodexCategory.Classes, LocaleString.NightstalkerDescription));
            entries.Add(new CodexEntryRecord(LocaleString.Hunter, CodexCategory.Classes, LocaleString.HunterDescription));
            entries.Add(new CodexEntryRecord(LocaleString.Elementalist, CodexCategory.Classes, LocaleString.ElementalistDescription));
            entries.Add(new CodexEntryRecord(LocaleString.Techweaver, CodexCategory.Classes, LocaleString.TechweaverDescription));

            return entries;
        }
    }
}


