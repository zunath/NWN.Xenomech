namespace XM.Plugin.Combat.Event
{
    internal class CombatEventScript
    {
        // Status Effects
        public const string OnApplyStatusEffectScript = "status_apply";
        public const string OnRemoveStatusEffectScript = "status_remove";
        public const string OnStatusEffectIntervalScript = "status_interval";

        // Telegraphs
        public const string TelegraphEffectScript = "telegraph_effect";
        public const string TelegraphApplied = "telegraph_apply";
        public const string TelegraphTicked = "telegraph_ticked";
        public const string TelegraphRemoved = "telegraph_remove";
    }
}
