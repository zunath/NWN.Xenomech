using System.Collections.Generic;

namespace XM.Plugin.Mech
{
    public class MechPartBuilder
    {
        private readonly Dictionary<string, MechPartStats> _mechParts = new();
        private MechPartStats _activePart;

        /// <summary>
        /// Creates a new mech part definition.
        /// </summary>
        /// <param name="resref">The item resref of the mech part.</param>
        /// <param name="partType">The type of mech part.</param>
        /// <returns>A mech part builder with the configured options</returns>
        public MechPartBuilder Create(string resref, MechPartType partType)
        {
            _activePart = new MechPartStats
            {
                PartType = partType
            };
            _mechParts.Add(resref, _activePart);

            return this;
        }

        /// <summary>
        /// Sets the HP percentage bonus/penalty for this mech part.
        /// </summary>
        /// <param name="percent">The HP percentage modifier.</param>
        /// <returns>A mech part builder with the configured options</returns>
        public MechPartBuilder HPPercent(int percent)
        {
            _activePart.HpPercent = percent;
            return this;
        }

        /// <summary>
        /// Sets the Fuel percentage bonus/penalty for this mech part.
        /// </summary>
        /// <param name="percent">The Fuel percentage modifier.</param>
        /// <returns>A mech part builder with the configured options</returns>
        public MechPartBuilder FuelPercent(int percent)
        {
            _activePart.FuelPercent = percent;
            return this;
        }

        /// <summary>
        /// Sets the Attack percentage bonus/penalty for this mech part.
        /// </summary>
        /// <param name="percent">The Attack percentage modifier.</param>
        /// <returns>A mech part builder with the configured options</returns>
        public MechPartBuilder AttackPercent(int percent)
        {
            _activePart.AttackPercent = percent;
            return this;
        }

        /// <summary>
        /// Sets the Ether Attack percentage bonus/penalty for this mech part.
        /// </summary>
        /// <param name="percent">The Ether Attack percentage modifier.</param>
        /// <returns>A mech part builder with the configured options</returns>
        public MechPartBuilder EtherAttackPercent(int percent)
        {
            _activePart.EtherAttackPercent = percent;
            return this;
        }

        /// <summary>
        /// Sets the Defense percentage bonus/penalty for this mech part.
        /// </summary>
        /// <param name="percent">The Defense percentage modifier.</param>
        /// <returns>A mech part builder with the configured options</returns>
        public MechPartBuilder DefensePercent(int percent)
        {
            _activePart.DefensePercent = percent;
            return this;
        }

        /// <summary>
        /// Sets the Ether Defense percentage bonus/penalty for this mech part.
        /// </summary>
        /// <param name="percent">The Ether Defense percentage modifier.</param>
        /// <returns>A mech part builder with the configured options</returns>
        public MechPartBuilder EtherDefensePercent(int percent)
        {
            _activePart.EtherDefensePercent = percent;
            return this;
        }

        /// <summary>
        /// Sets the Accuracy percentage bonus/penalty for this mech part.
        /// </summary>
        /// <param name="percent">The Accuracy percentage modifier.</param>
        /// <returns>A mech part builder with the configured options</returns>
        public MechPartBuilder AccuracyPercent(int percent)
        {
            _activePart.AccuracyPercent = percent;
            return this;
        }

        /// <summary>
        /// Sets the Evasion percentage bonus/penalty for this mech part.
        /// </summary>
        /// <param name="percent">The Evasion percentage modifier.</param>
        /// <returns>A mech part builder with the configured options</returns>
        public MechPartBuilder EvasionPercent(int percent)
        {
            _activePart.EvasionPercent = percent;
            return this;
        }

        /// <summary>
        /// Sets the Fuel Consumption percentage bonus/penalty for this mech part.
        /// </summary>
        /// <param name="percent">The Fuel Consumption percentage modifier.</param>
        /// <returns>A mech part builder with the configured options</returns>
        public MechPartBuilder FuelConsumptionPercent(int percent)
        {
            _activePart.FuelConsumptionPercent = percent;
            return this;
        }

        /// <summary>
        /// Returns a built dictionary of mech part stats.
        /// </summary>
        /// <returns>A dictionary of mech part stats keyed by resref.</returns>
        public Dictionary<string, MechPartStats> Build()
        {
            return _mechParts;
        }
    }
}