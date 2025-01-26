using XM.Shared.Core.Localization;

namespace XM.Progression.Stat.ResistDefinition
{
    public interface IResistDefinition
    {
        ResistType Type { get; }
        LocaleString Name { get; }
        string IconResref { get; }
    }
}
