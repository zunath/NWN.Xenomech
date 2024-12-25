using NWN.Xenomech.Core.Interop;
using NWN.Xenomech.Core.NWScript.Enum;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {/// <summary>
     /// Parse the given string as a valid json value, and returns the corresponding type.
     /// Returns a JSON_TYPE_NULL on error.
     /// Check JsonGetError() to see the parse error, if any.
     /// NB: The parsed string needs to be in game-local encoding, but the generated json structure
     ///     will contain UTF-8 data.
     /// </summary>
        public static Json JsonParse(string jValue, int nIndent = -1)
        {
            NWNXPInvoke.StackPushInteger(nIndent);
            NWNXPInvoke.StackPushString(jValue);
            NWNXPInvoke.CallBuiltIn(968);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Dump the given json value into a string that can be read back in via JsonParse.
        /// nIndent describes the indentation level for pretty-printing; a value of -1 means no indentation and no linebreaks.
        /// Returns a string describing JSON_TYPE_NULL on error.
        /// NB: The dumped string is in game-local encoding, with all non-ascii characters escaped.
        /// </summary>
        public static string JsonDump(Json jValue, int nIndent = -1)
        {
            NWNXPInvoke.StackPushInteger(nIndent);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.CallBuiltIn(969);

            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Describes the type of the given json value.
        /// Returns JSON_TYPE_NULL if the value is empty.
        /// </summary>
        public static JsonType JsonGetType(Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.CallBuiltIn(970);

            return (JsonType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the length of the given json type.
        /// For objects, returns the number of top-level keys present.
        /// For arrays, returns the number of elements.
        /// Null types are of size 0.
        /// All other types return 1.
        /// </summary>
        public static int JsonGetLength(Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.CallBuiltIn(971);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the error message if the value has errored out.
        /// Currently only describes parse errors.
        /// </summary>
        public static string JsonGetError(Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.CallBuiltIn(972);

            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Create a NULL json value, seeded with a optional error message for JsonGetError().
        /// </summary>
        public static Json JsonNull(string sError = "")
        {
            NWNXPInvoke.StackPushString(sError);
            NWNXPInvoke.CallBuiltIn(973);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Create a empty json object.
        /// </summary>
        public static Json JsonObject()
        {
            NWNXPInvoke.CallBuiltIn(974);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Create a empty json array.
        /// </summary>
        public static Json JsonArray()
        {
            NWNXPInvoke.CallBuiltIn(975);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Create a json string value.
        /// NB: Strings are encoded to UTF-8 from the game-local charset.
        /// </summary>
        public static Json JsonString(string sValue)
        {
            NWNXPInvoke.StackPushString(sValue);
            NWNXPInvoke.CallBuiltIn(976);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Create a json integer value.
        /// </summary>
        public static Json JsonInt(int nValue)
        {
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.CallBuiltIn(977);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Create a json floating point value.
        /// </summary>
        public static Json JsonFloat(float fValue)
        {
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.CallBuiltIn(978);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Create a json bool value.
        /// </summary>
        public static Json JsonBool(bool bValue)
        {
            NWNXPInvoke.StackPushInteger(bValue ? 1 : 0);
            NWNXPInvoke.CallBuiltIn(979);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns a string representation of the json value.
        /// Returns "" if the value cannot be represented as a string, or is empty.
        /// NB: Strings are decoded from UTF-8 to the game-local charset.
        /// </summary>
        public static string JsonGetString(Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.CallBuiltIn(980);

            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Returns a int representation of the json value, casting where possible.
        /// Returns 0 if the value cannot be represented as a float.
        /// Use this to parse json bool types.
        /// NB: This will narrow down to signed 32 bit, as that is what NWScript int is.
        ///     If you are trying to read a 64 bit or unsigned integer, you will lose data.
        ///     You will not lose data if you keep the value as a json element (via Object/ArrayGet).
        /// </summary>
        public static int JsonGetInt(Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.CallBuiltIn(981);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns a float representation of the json value, casting where possible.
        /// Returns 0.0 if the value cannot be represented as a float.
        /// NB: This will narrow doubles down to float.
        ///     If you are trying to read a double, you will lose data.
        ///     You will not lose data if you keep the value as a json element (via Object/ArrayGet).
        /// </summary>
        public static float JsonGetFloat(Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.CallBuiltIn(982);

            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Returns a json array containing all keys of jObject.
        /// Returns a empty array if the object is empty or not a json object, with GetJsonError() filled in.
        /// </summary>
        public static Json JsonObjectKeys(Json jObject)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jObject);
            NWNXPInvoke.CallBuiltIn(983);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }
        /// <summary>
        /// Returns the key value of sKey on the object jObject.
        /// Returns a null json value if jObject is not an object or sKey does not exist on the object, with GetJsonError() filled in.
        /// </summary>
        public static Json JsonObjectGet(Json jObject, string sKey)
        {
            NWNXPInvoke.StackPushString(sKey);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jObject);
            NWNXPInvoke.CallBuiltIn(984);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns a modified copy of jObject with the key at sKey set to jValue.
        /// Returns a json null value if jObject is not an object, with GetJsonError() filled in.
        /// </summary>
        public static Json JsonObjectSet(Json jObject, string sKey, Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.StackPushString(sKey);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jObject);
            NWNXPInvoke.CallBuiltIn(985);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns a modified copy of jObject with the key at sKey deleted.
        /// Returns a json null value if jObject is not an object, with GetJsonError() filled in.
        /// </summary>
        public static Json JsonObjectDel(Json jObject, string sKey)
        {
            NWNXPInvoke.StackPushString(sKey);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jObject);
            NWNXPInvoke.CallBuiltIn(986);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Gets the json object at jArray index position nIndex.
        /// Returns a json null value if the index is out of bounds, with GetJsonError() filled in.
        /// </summary>
        public static Json JsonArrayGet(Json jArray, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jArray);
            NWNXPInvoke.CallBuiltIn(987);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns a modified copy of jArray with position nIndex set to jValue.
        /// Returns a json null value if jArray is not actually an array, with GetJsonError() filled in.
        /// Returns a json null value if nIndex is out of bounds, with GetJsonError() filled in.
        /// </summary>
        public static Json JsonArraySet(Json jArray, int nIndex, Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jArray);
            NWNXPInvoke.CallBuiltIn(988);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns a modified copy of jArray with jValue inserted at position nIndex.
        /// All succeeding objects in the array will move by one.
        /// By default (-1), inserts objects at the end of the array ("push").
        /// nIndex = 0 inserts at the beginning of the array.
        /// Returns a json null value if jArray is not actually an array, with GetJsonError() filled in.
        /// Returns a json null value if nIndex is not 0 or -1 and out of bounds, with GetJsonError() filled in.
        /// </summary>
        public static Json JsonArrayInsert(Json jArray, Json jValue, int nIndex = -1)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jArray);
            NWNXPInvoke.CallBuiltIn(989);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns a modified copy of jArray with the element at position nIndex removed,
        /// and the array resized by one.
        /// Returns a json null value if jArray is not actually an array, with GetJsonError() filled in.
        /// Returns a json null value if nIndex is out of bounds, with GetJsonError() filled in.
        /// </summary>
        public static Json JsonArrayDel(Json jArray, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jArray);
            NWNXPInvoke.CallBuiltIn(990);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Transforms the given object into a json structure.
        /// The json format is compatible with what https://github.com/niv/neverwinter.nim@1.4.3+ emits.
        /// Returns the null json type on errors, or if oObject is not serializable, with GetJsonError() filled in.
        /// Supported object types: creature, item, trigger, placeable, door, waypoint, encounter, store, area (combined format)
        /// If bSaveObjectState is TRUE, local vars, effects, action queue, and transition info (triggers, doors) are saved out
        /// (except for Combined Area Format, which always has object state saved out).
        /// </summary>
        public static Json ObjectToJson(uint oObject, bool bSaveObjectState = false)
        {
            NWNXPInvoke.StackPushInteger(bSaveObjectState ? 1 : 0);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(991);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Deserializes the game object described in jObject.
        /// Returns OBJECT_INVALID on errors.
        /// Supported object types: creature, item, trigger, placeable, door, waypoint, encounter, store, area (combined format)
        /// For areas, locLocation is ignored.
        /// If bLoadObjectState is TRUE, local vars, effects, action queue, and transition info (triggers, doors) are read in.
        /// </summary>
        public static uint JsonToObject(Json jObject, Location locLocation, uint oOwner = OBJECT_INVALID, bool bLoadObjectState = false)
        {
            NWNXPInvoke.StackPushInteger(bLoadObjectState ? 1 : 0);
            NWNXPInvoke.StackPushObject(oOwner);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, locLocation);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jObject);
            NWNXPInvoke.CallBuiltIn(992);

            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Returns the element at the given JSON pointer value.
        /// See https://datatracker.ietf.org/doc/html/rfc6901 for details.
        /// Returns a json null value on error, with GetJsonError() filled in.
        /// </summary>
        public static Json JsonPointer(Json jData, string sPointer)
        {
            NWNXPInvoke.StackPushString(sPointer);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jData);
            NWNXPInvoke.CallBuiltIn(993);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Return a modified copy of jData with jValue inserted at the path described by sPointer.
        /// See https://datatracker.ietf.org/doc/html/rfc6901 for details.
        /// Returns a json null value on error, with GetJsonError() filled in.
        /// jPatch is an array of patch elements, each containing a op, a path, and a value field. Example:
        /// [
        ///   { "op": "replace", "path": "/baz", "value": "boo" },
        ///   { "op": "add", "path": "/hello", "value": ["world"] },
        ///   { "op": "remove", "path": "/foo"}
        /// ]
        /// Valid operations are: add, remove, replace, move, copy, test
        /// </summary>
        public static Json JsonPatch(Json jData, Json jPatch)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jPatch);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jData);
            NWNXPInvoke.CallBuiltIn(994);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns the diff (described as a json structure you can pass into JsonPatch) between the two objects.
        /// Returns a json null value on error, with GetJsonError() filled in.
        /// </summary>
        public static Json JsonDiff(Json jLHS, Json jRHS)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jRHS);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jLHS);
            NWNXPInvoke.CallBuiltIn(995);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns a modified copy of jData with jMerge merged into it. This is an alternative to
        /// JsonPatch/JsonDiff, with a syntax more closely resembling the final object.
        /// See https://datatracker.ietf.org/doc/html/rfc7386 for details.
        /// Returns a json null value on error, with GetJsonError() filled in.
        /// </summary>
        public static Json JsonMerge(Json jData, Json jMerge)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jMerge);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jData);
            NWNXPInvoke.CallBuiltIn(996);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Get oObject's local json variable sVarName
        /// * Return value on error: json null type
        /// </summary>
        public static Json GetLocalJson(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(997);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Set oObject's local json variable sVarName to jValue
        /// </summary>
        public static void SetLocalJson(uint oObject, string sVarName, Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(998);
        }

        /// <summary>
        /// Delete oObject's local json variable sVarName
        /// </summary>
        public static void DeleteLocalJson(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(999);
        }

        /// <summary>
        /// Deserializes the given resref/template into a JSON structure.
        /// Supported GFF resource types:
        /// * RESTYPE_CAF (and RESTYPE_ARE, RESTYPE_GIT, RESTYPE_GIC)
        /// * RESTYPE_UTC
        /// * RESTYPE_UTI
        /// * RESTYPE_UTT
        /// * RESTYPE_UTP
        /// * RESTYPE_UTD
        /// * RESTYPE_UTW
        /// * RESTYPE_UTE
        /// * RESTYPE_UTM
        /// * RESTYPE_DLG
        /// * RESTYPE_UTS
        /// * RESTYPE_IFO
        /// * RESTYPE_FAC
        /// * RESTYPE_ITP
        /// * RESTYPE_GUI
        /// * RESTYPE_GFF
        /// Returns a valid gff-type json structure, or a null value with GetJsonError() set.
        /// </summary>
        public static Json TemplateToJson(string sResRef, ResType nResType)
        {
            NWNXPInvoke.StackPushInteger((int)nResType);
            NWNXPInvoke.StackPushString(sResRef);
            NWNXPInvoke.CallBuiltIn(1007);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns a modified copy of jArray with the value order changed according to nTransform:
        /// JSON_ARRAY_SORT_ASCENDING, JSON_ARRAY_SORT_DESCENDING
        /// Sorting is dependent on the type and follows JSON standards (.e.g., 99 < "100").
        /// </summary>
        public static Json JsonArrayTransform(Json jArray, JsonArraySort nTransform)
        {
            NWNXPInvoke.StackPushInteger((int)nTransform);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jArray);
            NWNXPInvoke.CallBuiltIn(1030);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns the nth-matching index or key of jNeedle in jHaystack.
        /// Supported haystacks: object, array.
        /// Returns null when not found or on any error.
        /// </summary>
        public static Json JsonFind(Json jHaystack, Json jNeedle, int nNth = 0, JsonFind nConditional = Enum.JsonFind.Equal)
        {
            NWNXPInvoke.StackPushInteger((int)nConditional);
            NWNXPInvoke.StackPushInteger(nNth);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jNeedle);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jHaystack);
            NWNXPInvoke.CallBuiltIn(1031);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns a copy of the range (nBeginIndex, nEndIndex) inclusive of jArray.
        /// Returns a null type on error, including type mismatches.
        /// </summary>
        public static Json JsonArrayGetRange(Json jArray, int nBeginIndex, int nEndIndex)
        {
            NWNXPInvoke.StackPushInteger(nEndIndex);
            NWNXPInvoke.StackPushInteger(nBeginIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jArray);
            NWNXPInvoke.CallBuiltIn(1032);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Returns the result of a set operation on two arrays.
        /// </summary>
        public static Json JsonSetOp(Json jValue, JsonSet nOp, Json jOther)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jOther);
            NWNXPInvoke.StackPushInteger((int)nOp);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.CallBuiltIn(1033);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Applies sRegExp on sValue, returning an array containing all matching groups.
        /// </summary>
        public static Json RegExpMatch(string sRegExp, string sValue, RegularExpressionType nSyntaxFlags = RegularExpressionType.Ecmascript, RegularExpressionFormatType nMatchFlags = RegularExpressionFormatType.Default)
        {
            NWNXPInvoke.StackPushInteger((int)nMatchFlags);
            NWNXPInvoke.StackPushInteger((int)nSyntaxFlags);
            NWNXPInvoke.StackPushString(sValue);
            NWNXPInvoke.StackPushString(sRegExp);
            NWNXPInvoke.CallBuiltIn(1068);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Iterates sValue with sRegExp, returning an array of arrays where each sub-array contains first the full match and then all matched groups.
        /// </summary>
        public static Json RegExpIterate(string sRegExp, string sValue, RegularExpressionType nSyntaxFlags = RegularExpressionType.Ecmascript, RegularExpressionFormatType nMatchFlags = RegularExpressionFormatType.Default)
        {
            NWNXPInvoke.StackPushInteger((int)nMatchFlags);
            NWNXPInvoke.StackPushInteger((int)nSyntaxFlags);
            NWNXPInvoke.StackPushString(sValue);
            NWNXPInvoke.StackPushString(sRegExp);
            NWNXPInvoke.CallBuiltIn(1069);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Serializes the given JSON structure (which must be a valid template spec) into a template.
        /// Supported GFF resource types are the same as TemplateToJson().
        /// Returns TRUE if the serialization was successful.
        /// </summary>
        public static int JsonToTemplate(Json jTemplateSpec, string sResRef, ResType nResType)
        {
            NWNXPInvoke.StackPushInteger((int)nResType);
            NWNXPInvoke.StackPushString(sResRef);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jTemplateSpec);
            NWNXPInvoke.CallBuiltIn(1133);

            return NWNXPInvoke.StackPopInteger();
        }
        // Modifies jObject in-place (with no memory copies of the full object).
        // jObject will have the key at sKey set to jValue.
        public static void JsonObjectSetInplace(Json jObject, string sKey, Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.StackPushString(sKey);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jObject);
            NWNXPInvoke.CallBuiltIn(1134);
        }

        // Modifies jObject in-place (with no memory copies needed).
        // jObject will have the element at the key sKey removed.
        // Will do nothing if jObject is not an object, or sKey does not exist on the object.
        public static void JsonObjectDelInplace(Json jObject, string sKey)
        {
            NWNXPInvoke.StackPushString(sKey);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jObject);
            NWNXPInvoke.CallBuiltIn(1135);
        }

        // Modifies jArray in-place (with no memory copies needed).
        // jArray will have jValue inserted at position nIndex.
        // All succeeding elements in the array will move by one.
        // By default (-1), inserts elements at the end of the array ("push").
        // nIndex = 0 inserts at the beginning of the array.
        public static void JsonArrayInsertInplace(Json jArray, Json jValue, int nIndex = -1)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jArray);
            NWNXPInvoke.CallBuiltIn(1136);
        }

        // Modifies jArray in-place (with no memory copies needed).
        // jArray will have jValue set at position nIndex.
        // Will do nothing if jArray is not an array or nIndex is out of range.
        public static void JsonArraySetInplace(Json jArray, int nIndex, Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jArray);
            NWNXPInvoke.CallBuiltIn(1137);
        }

        // Modifies jArray in-place (with no memory copies needed).
        // jArray will have the element at nIndex removed, and the array will be resized accordingly.
        // Will do nothing if jArray is not an array or nIndex is out of range.
        public static void JsonArrayDelInplace(Json jArray, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jArray);
            NWNXPInvoke.CallBuiltIn(1138);
        }

    }
}
