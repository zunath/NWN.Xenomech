using Anvil.Services;
using XM.Shared.Core.EventManagement;

namespace XM.Shared.Core.Activity
{
    [ServiceBinding(typeof(ActivityService))]
    public class ActivityService
    {
        private const string IsBusyVariable = "IS_BUSY";
        private const string BusyTypeVariable = "BUSY_TYPE";
        private readonly XMEventService _event;

        public ActivityService(XMEventService @event)
        {
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerEnter>(OnModuleEnter);
            _event.Subscribe<ModuleEvent.OnPlayerDeath>(OnPlayerDeath);
        }

        /// <summary>
        /// Marks a target as being busy with a particular type of action.
        /// </summary>
        /// <param name="target">The target to modify.</param>
        /// <param name="type">The type of activity to assign.</param>
        public void SetBusy(uint target, ActivityStatusType type)
        {
            SetLocalBool(target, IsBusyVariable, true);
            SetLocalInt(target, BusyTypeVariable, (int)type);
        }

        /// <summary>
        /// Determines if a target is busy with any type of action.
        /// </summary>
        /// <param name="target">The target to check.</param>
        /// <returns>true if busy, false otherwise</returns>
        public bool IsBusy(uint target)
        {
            return GetLocalBool(target, IsBusyVariable);
        }

        /// <summary>
        /// Retrieves the type of busy action a target is currently engaged with.
        /// If user isn't busy, ActivityStatusType.Invalid will be returned.
        /// </summary>
        /// <param name="target">The target to check.</param>
        /// <returns>The type of activity status.</returns>
        public ActivityStatusType GetBusyType(uint target)
        {
            if (!IsBusy(target))
                return ActivityStatusType.Invalid;

            return (ActivityStatusType)GetLocalInt(target, BusyTypeVariable);
        }

        /// <summary>
        /// Clears the busy status of a single target.
        /// </summary>
        /// <param name="target">The target whose status will be cleared.</param>
        public void ClearBusy(uint target)
        {
            DeleteLocalBool(target, IsBusyVariable);
            DeleteLocalInt(target, BusyTypeVariable);
        }

        /// <summary>
        /// When a player enters the module, wipe their temporary "busy" status.
        /// </summary>
        private void OnModuleEnter()
        {
            var player = GetEnteringObject();
            ClearBusy(player);
        }

        /// <summary>
        /// When a player dies, wipe their temporary "busy" status.
        /// </summary>
        private void OnPlayerDeath()
        {
            var player = GetLastPlayerDied();
            ClearBusy(player);
        }
    }
}
