namespace XM.UI.UI.Payload
{
    public class AppearanceEditorPayload: GuiPayloadBase
    {
        public uint Target { get; set; }

        public AppearanceEditorPayload()
        {
            Target = OBJECT_INVALID;
        }

        public AppearanceEditorPayload(uint target)
        {
            Target = target;
        }
    }
}
