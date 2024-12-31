namespace XM.UI.WindowDefinition.Payload
{
    public class CustomizeCharacterPayload: GuiPayloadBase
    {
        public uint Target { get; set; }

        public CustomizeCharacterPayload(uint target)
        {
            Target = target;
        }
    }
}
