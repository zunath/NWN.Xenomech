using NWN.Xenomech.Core.Interop;
using System.Numerics;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {
        /// <summary>
        /// Math operation: absolute value of fValue
        /// </summary>
        public static float fabs(float fValue)
        {
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.CallBuiltIn(67);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Math operation: cosine of fValue
        /// </summary>
        public static float cos(float fValue)
        {
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.CallBuiltIn(68);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Math operation: sine of fValue
        /// </summary>
        public static float sin(float fValue)
        {
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.CallBuiltIn(69);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Math operation: tan of fValue
        /// </summary>
        public static float tan(float fValue)
        {
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.CallBuiltIn(70);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Math operation: arccosine of fValue
        /// * Returns zero if fValue > 1 or fValue < -1
        /// </summary>
        public static float acos(float fValue)
        {
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.CallBuiltIn(71);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Math operation: arcsine of fValue
        /// * Returns zero if fValue > 1 or fValue < -1
        /// </summary>
        public static float asin(float fValue)
        {
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.CallBuiltIn(72);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Math operation: arctan of fValue
        /// </summary>
        public static float atan(float fValue)
        {
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.CallBuiltIn(73);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Math operation: log of fValue
        /// * Returns zero if fValue <= zero
        /// </summary>
        public static float log(float fValue)
        {
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.CallBuiltIn(74);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Math operation: fValue is raised to the power of fExponent
        /// * Returns zero if fValue == 0 and fExponent < 0
        /// </summary>
        public static float pow(float fValue, float fExponent)
        {
            NWNXPInvoke.StackPushFloat(fExponent);
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.CallBuiltIn(75);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Math operation: square root of fValue
        /// * Returns zero if fValue < 0
        /// </summary>
        public static float sqrt(float fValue)
        {
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.CallBuiltIn(76);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Math operation: integer absolute value of nValue
        /// * Return value on error: 0
        /// </summary>
        public static int abs(int nValue)
        {
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.CallBuiltIn(77);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Normalize vVector
        /// </summary>
        public static Vector3 VectorNormalize(Vector3 vVector)
        {
            NWNXPInvoke.StackPushVector(vVector);
            NWNXPInvoke.CallBuiltIn(137);
            return NWNXPInvoke.StackPopVector();
        }

        /// <summary>
        /// Get the magnitude of vVector; this can be used to determine the
        /// distance between two points.
        /// * Return value on error: 0.0f
        /// </summary>
        public static float VectorMagnitude(Vector3 vVector)
        {
            NWNXPInvoke.StackPushVector(vVector);
            NWNXPInvoke.CallBuiltIn(104);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Convert fFeet into a number of meters.
        /// </summary>
        public static float FeetToMeters(float fFeet)
        {
            NWNXPInvoke.StackPushFloat(fFeet);
            NWNXPInvoke.CallBuiltIn(218);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Convert fYards into a number of meters.
        /// </summary>
        public static float YardsToMeters(float fYards)
        {
            NWNXPInvoke.StackPushFloat(fYards);
            NWNXPInvoke.CallBuiltIn(219);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Get the distance from the caller to oObject in metres.
        /// * Return value on error: -1.0f
        /// </summary>
        public static float GetDistanceToObject(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(41);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Returns whether or not there is a direct line of sight
        /// between the two objects. (Not blocked by any geometry).
        /// PLEASE NOTE: This is an expensive function and may
        /// degrade performance if used frequently.
        /// </summary>
        public static bool LineOfSightObject(uint oSource, uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushObject(oSource);
            NWNXPInvoke.CallBuiltIn(752);
            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        /// Returns whether or not there is a direct line of sight
        /// between the two vectors. (Not blocked by any geometry).
        /// This function must be run on a valid object in the area
        /// it will not work on the module or area.
        /// PLEASE NOTE: This is an expensive function and may
        /// degrade performance if used frequently.
        /// </summary>
        public static bool LineOfSightVector(Vector3 vSource, Vector3 vTarget)
        {
            NWNXPInvoke.StackPushVector(vTarget);
            NWNXPInvoke.StackPushVector(vSource);
            NWNXPInvoke.CallBuiltIn(753);
            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        /// Convert fAngle to a vector
        /// </summary>
        public static Vector3 AngleToVector(float fAngle)
        {
            NWNXPInvoke.StackPushFloat(fAngle);
            NWNXPInvoke.CallBuiltIn(144);
            return NWNXPInvoke.StackPopVector();
        }

        /// <summary>
        /// Convert vVector to an angle
        /// </summary>
        public static float VectorToAngle(Vector3 vVector)
        {
            NWNXPInvoke.StackPushVector(vVector);
            NWNXPInvoke.CallBuiltIn(145);
            return NWNXPInvoke.StackPopFloat();
        }
    }
}
