namespace XM.Core.EventManagement.PlayerEvent
{
    public interface IPlayerOnEndCombatRoundEvent: IXMEvent
    {
        void PlayerOnEndCombatRound();
    }
}