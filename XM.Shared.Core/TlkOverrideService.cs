using Anvil.Services;
using XM.Shared.Core.Localization;

namespace XM.Shared.Core
{
    [ServiceBinding(typeof(TlkOverrideService))]
    internal class TlkOverrideService: IInitializable
    {
        public void Init()
        {
            OverrideAttributeNames();
            OverrideMenuNames();
            OverrideAttackBonus();
        }

        private static void OverrideAttributeNames()
        {
            SetTlkOverride(131, LocaleString.Social.ToLocalizedString()); // Charisma
            SetTlkOverride(132, LocaleString.Vitality.ToLocalizedString()); // Constitution
            SetTlkOverride(133, LocaleString.Perception.ToLocalizedString()); // Dexterity
            SetTlkOverride(134, LocaleString.Agility.ToLocalizedString()); // Intelligence
            SetTlkOverride(135, LocaleString.Might.ToLocalizedString()); // Strength
            SetTlkOverride(136, LocaleString.Willpower.ToLocalizedString()); // Wisdom

            SetTlkOverride(328, LocaleString.IncreasedMightBy.ToLocalizedString()); // Strength
            SetTlkOverride(329, LocaleString.IncreasedPerceptionBy.ToLocalizedString()); // Dexterity
            SetTlkOverride(330, LocaleString.IncreasedAgilityBy.ToLocalizedString()); // Intelligence
            SetTlkOverride(331, LocaleString.IncreasedVitalityBy.ToLocalizedString()); // Constitution
            SetTlkOverride(332, LocaleString.IncreasedWillpowerBy.ToLocalizedString()); // Wisdom
            SetTlkOverride(333, LocaleString.IncreasedSocialBy.ToLocalizedString()); // Charisma

            SetTlkOverride(473, LocaleString.MightInformation.ToLocalizedString()); // Strength
            SetTlkOverride(474, LocaleString.PerceptionInformation.ToLocalizedString()); // Dexterity
            SetTlkOverride(475, LocaleString.VitalityInformation.ToLocalizedString()); // Constitution
            SetTlkOverride(476, LocaleString.WillpowerInformation.ToLocalizedString()); // Wisdom
            SetTlkOverride(477, LocaleString.AgilityInformation.ToLocalizedString()); // Intelligence
            SetTlkOverride(479, LocaleString.SocialInformation.ToLocalizedString()); // Charisma

            SetTlkOverride(457, LocaleString.RecommendedButtonText.ToLocalizedString());

            SetTlkOverride(459, LocaleString.MightDetails.ToLocalizedString());
            SetTlkOverride(460, LocaleString.PerceptionDetails.ToLocalizedString());
            SetTlkOverride(461, LocaleString.VitalityDetails.ToLocalizedString());
            SetTlkOverride(462, LocaleString.WillpowerDetails.ToLocalizedString());
            SetTlkOverride(463, LocaleString.AgilityDetails.ToLocalizedString());
            SetTlkOverride(464, LocaleString.SocialDetails.ToLocalizedString());

            SetTlkOverride(535, LocaleString.Credit.ToLocalizedString()); // Gold Piece

            SetTlkOverride(1027, LocaleString.Poison.ToLocalizedString()); // Acid

            SetTlkOverride(3593, LocaleString.GiveCredits.ToLocalizedString()); // GP
            SetTlkOverride(5025, LocaleString.CreditDescription.ToLocalizedString()); // GP desc
            SetTlkOverride(6407, LocaleString.Credits.ToLocalizedString()); // GP
            SetTlkOverride(7059, LocaleString.DropOrGiveCreditsEtc.ToLocalizedString());

            SetTlkOverride(7099, LocaleString.Evasion.ToLocalizedString());

            SetTlkOverride(8035, LocaleString.Resting.ToLocalizedString());
            SetTlkOverride(8049, LocaleString.Horrified.ToLocalizedString());
            SetTlkOverride(8056, LocaleString.AccuracyIncreased.ToLocalizedString());
            SetTlkOverride(8057, LocaleString.AccuracyDecreased.ToLocalizedString());
            SetTlkOverride(8060, LocaleString.DefenseIncreased.ToLocalizedString());
            SetTlkOverride(8061, LocaleString.DefenseDecreased.ToLocalizedString());
            SetTlkOverride(8062, LocaleString.EvasionIncreased.ToLocalizedString());
            SetTlkOverride(8063, LocaleString.EvasionDecreased.ToLocalizedString());
            SetTlkOverride(8077, LocaleString.EtherDrained.ToLocalizedString());

            SetTlkOverride(58369, LocaleString.MightIncreased.ToLocalizedString());
            SetTlkOverride(58370, LocaleString.MightDecreased.ToLocalizedString());
            SetTlkOverride(58371, LocaleString.PerceptionIncreased.ToLocalizedString());
            SetTlkOverride(58372, LocaleString.PerceptionDecreased.ToLocalizedString());
            SetTlkOverride(58373, LocaleString.VitalityIncreased.ToLocalizedString());
            SetTlkOverride(58374, LocaleString.VitalityDecreased.ToLocalizedString());
            SetTlkOverride(58375, LocaleString.AgilityIncreased.ToLocalizedString());
            SetTlkOverride(58376, LocaleString.AgilityDecreased.ToLocalizedString());
            SetTlkOverride(58377, LocaleString.WillpowerIncreased.ToLocalizedString());
            SetTlkOverride(58378, LocaleString.WillpowerDecreased.ToLocalizedString());
            SetTlkOverride(58379, LocaleString.SocialIncreased.ToLocalizedString());
            SetTlkOverride(58380, LocaleString.SocialDecreased.ToLocalizedString());

            SetTlkOverride(61619, LocaleString.SellItemForCredits.ToLocalizedString());
            SetTlkOverride(61620, LocaleString.BuyItemForCredits.ToLocalizedString());
            SetTlkOverride(62489, LocaleString.AcquiredXCredits.ToLocalizedString());
            SetTlkOverride(62490, LocaleString.LostXCredits.ToLocalizedString());

            SetTlkOverride(66129, LocaleString.Premonition.ToLocalizedString());

            SetTlkOverride(66751, LocaleString.Global.ToLocalizedString());
            SetTlkOverride(66755, LocaleString.Comms.ToLocalizedString());

            SetTlkOverride(83393, LocaleString.Poison.ToLocalizedString()); // Acid
        }


        private static void OverrideMenuNames()
        {
            // Journal - List as Quests
            SetTlkOverride(7037, LocaleString.Quests.ToLocalizedString());

            // Spell Book - List as Player Guide
            SetTlkOverride(7038, LocaleString.Codex.ToLocalizedString());
        }

        private static void OverrideAttackBonus()
        {
            SetTlkOverride(660, LocaleString.AccuracyAndDamagePenalty.ToLocalizedString());
            SetTlkOverride(734, LocaleString.AccuracyVsAlignmentGroup.ToLocalizedString());
            SetTlkOverride(735, LocaleString.Accuracy.ToLocalizedString());
            SetTlkOverride(736, LocaleString.DecreasedAccuracy.ToLocalizedString());
            SetTlkOverride(737, LocaleString.AccuracyVsRacialGroup.ToLocalizedString());
            SetTlkOverride(738, LocaleString.AccuracyVsSpecificAlignment.ToLocalizedString());
            SetTlkOverride(757, LocaleString.PreferredTargetRace.ToLocalizedString());
            SetTlkOverride(1085, LocaleString.AccuracyDescription.ToLocalizedString());
            SetTlkOverride(1086, LocaleString.AccuracyVsMonsterTypeDescription.ToLocalizedString());
            SetTlkOverride(1087, LocaleString.AccuracyVsSpecificAlignmentDescription.ToLocalizedString());
            SetTlkOverride(1088, LocaleString.AccuracyVsAlignmentGroupDescription.ToLocalizedString());
            SetTlkOverride(1458, LocaleString.AccuracyPenaltyDescription1.ToLocalizedString());
            SetTlkOverride(1460, LocaleString.AccuracyPenaltyDescription2.ToLocalizedString());
            SetTlkOverride(2227, LocaleString.AccuracyBonus.ToLocalizedString());
            SetTlkOverride(5519, LocaleString.AccuracyBonusColon.ToLocalizedString());
            SetTlkOverride(5520, LocaleString.AccuracyBonusVs.ToLocalizedString());
            SetTlkOverride(5521, LocaleString.AccuracyPenalty.ToLocalizedString());
            SetTlkOverride(8038, LocaleString.EnemyAccuracyBonus.ToLocalizedString());
            SetTlkOverride(40557, LocaleString.TotalBaseAccuracyBonusIs.ToLocalizedString());
            SetTlkOverride(58292, LocaleString.AccuracybonusColon.ToLocalizedString());
        }

    }
}
