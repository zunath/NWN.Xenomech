namespace XM.Core.EventManagement.PlayerEvent
{
    public interface IPlayerOnSpellCastAtEvent: IXMEvent
    {
        void PlayerOnSpellCastAt();
    }
}