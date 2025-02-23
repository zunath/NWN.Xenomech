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
        public const string PlayerHPAdjustedScript = "pc_hp_adjusted";
        public const string PlayerEPAdjustedScript = "pc_ep_adjusted";
        public const string PlayerTPAdjustedScript = "pc_tp_adjusted";

        // Telegraphs
        public const string TelegraphEffectScript = "telegraph_effect";
        public const string TelegraphApplied = "telegraph_apply";
        public const string TelegraphTicked = "telegraph_ticked";
        public const string TelegraphRemoved = "telegraph_remove";
    }
}
