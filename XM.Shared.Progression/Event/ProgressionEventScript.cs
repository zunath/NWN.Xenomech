namespace XM.Progression.Event
{
    internal class ProgressionEventScript
    {
        // Status Effects
        public const string OnApplyStatusEffectScript = "status_apply";
        public const string OnRemoveStatusEffectScript = "status_remove";
        public const string OnStatusEffectIntervalScript = "status_interval";

        // Jobs
        public const string PlayerChangedJobScript = "pc_change_job";
        public const string PlayerLeveledUpScript = "pc_job_level_up";
        public const string JobFeatRemovedScript = "job_feat_remove";
        public const string JobFeatAddScript = "job_feat_add";

        // Stats
        public const string OnPlayerHPAdjustedScript = "pc_hp_adjusted";
        public const string OnPlayerEPAdjustedScript = "pc_ep_adjusted";
        public const string OnPlayerTPAdjustedScript = "pc_tp_adjusted";
    }
}
