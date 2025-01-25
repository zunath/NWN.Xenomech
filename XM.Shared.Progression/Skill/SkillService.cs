using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill.SkillDefinition;
using XM.Shared.Core.Data;

namespace XM.Progression.Skill
{
    [ServiceBinding(typeof(SkillService))]
    public class SkillService: IInitializable
    {
        private readonly DBService _db;

        private readonly IList<ISkillDefinition> _skillDefinitions;
        private readonly Dictionary<SkillType, ISkillDefinition> _skills = new();
        private readonly SkillGrades _skillGrades = new();

        public SkillService(
            DBService db,
            IList<ISkillDefinition> skillDefinitions)
        {
            _db = db;
            _skillDefinitions = skillDefinitions;
        }

        public void Init()
        {
            LoadSkillDefinitions();
        }

        private void LoadSkillDefinitions()
        {
            foreach (var definition in _skillDefinitions)
            {
                _skills[definition.Type] = definition;
            }
        }
    }
}
