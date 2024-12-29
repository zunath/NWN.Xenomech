namespace NWN.Xenomech.API.NWNX.ProfilerPlugin
{
    public static class ProfilerPlugin
    {
        /// <summary>
        /// Pushes a timing metric scope.
        /// </summary>
        /// <param name="name">The name to use for the metric.</param>
        /// <param name="tag0Tag">An optional tag to filter metrics.</param>
        /// <param name="tag0Value">The value of the tag for filtering.</param>
        public static void PushPerfScope(string name, string tag0Tag = "", string tag0Value = "")
        {
            if (!string.IsNullOrEmpty(tag0Tag) && !string.IsNullOrEmpty(tag0Value))
            {
                NWN.Core.NWNX.ProfilerPlugin.PushPerfScope(name, tag0Tag, tag0Value);
            }
            else
            {
                NWN.Core.NWNX.ProfilerPlugin.PushPerfScope(name);
            }
        }

        /// <summary>
        /// Pops a timing metric.
        /// </summary>
        /// <remarks>A metric must already be pushed before calling this method.</remarks>
        public static void PopPerfScope()
        {
            NWN.Core.NWNX.ProfilerPlugin.PopPerfScope();
        }

    }
}
