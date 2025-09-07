using System.Collections.Generic;

namespace XM.Plugin.Mech
{
    public class MechFrameBuilder
    {
        private readonly Dictionary<string, MechFrame> _mechFrames = new();
        private MechFrame _activeFrame;

        /// <summary>
        /// Creates a new mech frame definition.
        /// </summary>
        /// <param name="resref">The item resref of the mech frame.</param>
        /// <returns>A mech frame builder with the configured options</returns>
        public MechFrameBuilder Create(string resref)
        {
            _activeFrame = new MechFrame();
            _mechFrames.Add(resref, _activeFrame);

            return this;
        }

        /// <summary>
        /// Sets the level requirement for this mech frame.
        /// </summary>
        /// <param name="level">The level requirement.</param>
        /// <returns>A mech frame builder with the configured options</returns>
        public MechFrameBuilder LevelRequirement(int level)
        {
            _activeFrame.LevelRequirement = level;
            return this;
        }

        /// <summary>
        /// Sets the base HP for this mech frame.
        /// </summary>
        /// <param name="hp">The base HP value.</param>
        /// <returns>A mech frame builder with the configured options</returns>
        public MechFrameBuilder BaseHP(int hp)
        {
            _activeFrame.BaseHP = hp;
            return this;
        }

        /// <summary>
        /// Sets the base Fuel for this mech frame.
        /// </summary>
        /// <param name="fuel">The base Fuel value.</param>
        /// <returns>A mech frame builder with the configured options</returns>
        public MechFrameBuilder BaseFuel(int fuel)
        {
            _activeFrame.BaseFuel = fuel;
            return this;
        }

        /// <summary>
        /// Sets the base Attack for this mech frame.
        /// </summary>
        /// <param name="attack">The base Attack value.</param>
        /// <returns>A mech frame builder with the configured options</returns>
        public MechFrameBuilder BaseAttack(int attack)
        {
            _activeFrame.BaseAttack = attack;
            return this;
        }

        /// <summary>
        /// Sets the base Ether Attack for this mech frame.
        /// </summary>
        /// <param name="etherAttack">The base Ether Attack value.</param>
        /// <returns>A mech frame builder with the configured options</returns>
        public MechFrameBuilder BaseEtherAttack(int etherAttack)
        {
            _activeFrame.BaseEtherAttack = etherAttack;
            return this;
        }

        /// <summary>
        /// Sets the base Defense for this mech frame.
        /// </summary>
        /// <param name="defense">The base Defense value.</param>
        /// <returns>A mech frame builder with the configured options</returns>
        public MechFrameBuilder BaseDefense(int defense)
        {
            _activeFrame.BaseDefense = defense;
            return this;
        }

        /// <summary>
        /// Sets the base Ether Defense for this mech frame.
        /// </summary>
        /// <param name="etherDefense">The base Ether Defense value.</param>
        /// <returns>A mech frame builder with the configured options</returns>
        public MechFrameBuilder BaseEtherDefense(int etherDefense)
        {
            _activeFrame.BaseEtherDefense = etherDefense;
            return this;
        }

        /// <summary>
        /// Sets the base Accuracy for this mech frame.
        /// </summary>
        /// <param name="accuracy">The base Accuracy value.</param>
        /// <returns>A mech frame builder with the configured options</returns>
        public MechFrameBuilder BaseAccuracy(int accuracy)
        {
            _activeFrame.BaseAccuracy = accuracy;
            return this;
        }

        /// <summary>
        /// Sets the base Evasion for this mech frame.
        /// </summary>
        /// <param name="evasion">The base Evasion value.</param>
        /// <returns>A mech frame builder with the configured options</returns>
        public MechFrameBuilder BaseEvasion(int evasion)
        {
            _activeFrame.BaseEvasion = evasion;
            return this;
        }

        /// <summary>
        /// Sets the base Fuel Consumption for this mech frame.
        /// </summary>
        /// <param name="fuelConsumption">The base Fuel Consumption value.</param>
        /// <returns>A mech frame builder with the configured options</returns>
        public MechFrameBuilder BaseFuelConsumption(int fuelConsumption)
        {
            _activeFrame.BaseFuelConsumption = fuelConsumption;
            return this;
        }

        /// <summary>
        /// Returns a built dictionary of mech frame stats.
        /// </summary>
        /// <returns>A dictionary of mech frame stats keyed by resref.</returns>
        public Dictionary<string, MechFrame> Build()
        {
            return _mechFrames;
        }
    }
}