using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public interface IResistDefinition
    {
        LocaleString Name { get; }
        string IconResref { get; }
    }
}
