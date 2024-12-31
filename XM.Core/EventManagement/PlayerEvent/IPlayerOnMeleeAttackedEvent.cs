namespace XM.Core.EventManagement.PlayerEvent
{
    public interface IPlayerOnMeleeAttackedEvent: IXMEvent
    {
        void PlayerOnMeleeAttacked();
    }
}