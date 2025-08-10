using System;
using Anvil.Services;
using XM.Progression.Skill;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Plugin.Craft.UI
{
    [ServiceBinding(typeof(IViewModel))]
    internal class SelectCraftViewModel: ViewModel<SelectCraftViewModel>
    {
        private SkillType _skill;

        public string Message
        {
            get => Get<string>();
            set => Set(value);
        }

        [Inject]
        public DBService DB { get; set; }

        [Inject]
        public SkillService Skill { get; set; }

        public override void OnOpen()
        {
            var payload = GetInitialData<SelectCraftPayload>();
            _skill = payload.Skill;

            var playerId = PlayerId.Get(Player);
            var dbPlayerCraft = DB.Get<PlayerCraft>(playerId);
            var skillDefinition = Skill.GetCraftSkillDefinition(_skill);

            if (dbPlayerCraft.PrimaryCraftSkillCode == 0)
            {
                Message = LocaleString.YouCanOnlyLearnTwoCraftingSkillsWouldYouLikeXToBeYourFirstOne.ToLocalizedString(skillDefinition.Name.ToLocalizedString());
            }
            else if (dbPlayerCraft.SecondaryCraftSkillCode == 0)
            {
                Message = LocaleString.YouCanOnlyLearnTwoCraftingSkillsWouldYouLikeXToBeYourSecondOne.ToLocalizedString(skillDefinition.Name.ToLocalizedString());
            }
        }

        public override void OnClose()
        {

        }

        public Action OnClickConfirm() => () =>
        {
            var skillDefinition = Skill.GetCraftSkillDefinition(_skill);
            ShowModal(LocaleString.AreYouSureYouWantToLearnTheXCraftSkill.ToLocalizedString(skillDefinition.Name.ToLocalizedString()), () =>
            {
                var playerId = PlayerId.Get(Player);
                var dbPlayerCraft = DB.Get<PlayerCraft>(playerId);

                if (dbPlayerCraft.PrimaryCraftSkillCode == 0)
                {
                    dbPlayerCraft.PrimaryCraftSkillCode = (int)_skill;
                }
                else if (dbPlayerCraft.SecondaryCraftSkillCode == 0)
                {
                    dbPlayerCraft.SecondaryCraftSkillCode = (int)_skill;
                }

                DB.Set(dbPlayerCraft);

                var message = LocaleString.YouLearnTheXSkill.ToLocalizedString(skillDefinition.Name.ToLocalizedString());
                SendMessageToPC(Player, message);

                CloseWindow();
            });
        };

        public Action OnClickCancel() => CloseWindow;


    }
}
