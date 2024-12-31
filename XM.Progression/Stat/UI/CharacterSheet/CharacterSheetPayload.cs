using XM.UI;

namespace XM.Progression.Stat.UI.CharacterSheet
{
    internal class CharacterSheetPayload : GuiPayloadBase
    {
        public uint Target { get; set; }
        public bool IsPlayerMode { get; set; }

        public CharacterSheetPayload(uint target, bool isPlayerMode)
        {
            Target = target;
            IsPlayerMode = isPlayerMode;
        }
    }
}
