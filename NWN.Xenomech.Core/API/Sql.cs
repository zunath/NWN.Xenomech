using System.Numerics;
using NWN.Xenomech.Core.API.Enum;
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.API
{
    public partial class NWScript
    {/// <summary>
     /// Destroys the given sqlite database, clearing out all data and schema.
     /// This operation is _immediate_ and _irreversible_, even when
     /// inside a transaction or running query.
     /// Existing active/prepared sqlqueries will remain functional, but any references
     /// to stored data or schema members will be invalidated.
     /// oObject: Same as SqlPrepareQueryObject().
     ///          To reset a campaign database, please use DestroyCampaignDatabase().
     /// </summary>
        public static void SqlDestroyDatabase(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(921);
        }

        /// <summary>
        /// Returns "" if the last Sql command succeeded; or a human-readable error otherwise.
        /// Additionally, all SQL errors are logged to the server log.
        /// </summary>
        public static string SqlGetError(IntPtr sqlQuery)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(922);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Sets up a query.
        /// This will NOT run the query; only make it available for parameter binding.
        /// To run the query, you need to call SqlStep(); even if you do not
        /// expect result data.
        /// sDatabase: The name of a campaign database.
        ///            Note that when accessing campaign databases, you do not write access
        ///            to the builtin tables needed for CampaignDB functionality.
        /// N.B.: You can pass sqlqueries into DelayCommand; HOWEVER
        ///       *** they will NOT survive a game save/load ***
        ///       Any commands on a restored sqlquery will fail.
        /// </summary>
        public static IntPtr SqlPrepareQueryCampaign(string sDatabase, string sQuery)
        {
            NWNXPInvoke.StackPushString(sQuery);
            NWNXPInvoke.StackPushString(sDatabase);
            NWNXPInvoke.CallBuiltIn(923);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.SQLQuery);
        }

        /// <summary>
        /// Sets up a query.
        /// This will NOT run the query; only make it available for parameter binding.
        /// To run the query, you need to call SqlStep(); even if you do not
        /// expect result data.
        /// oObject: Can be either the module (GetModule()), or a player character.
        ///          The database is persisted to savegames in case of the module,
        ///          and to character files in case of a player characters.
        ///          Other objects cannot carry databases, and this function call
        ///          will error for them.
        /// N.B: Databases on objects (especially player characters!) should be kept
        ///      to a reasonable size. Delete old data you no longer need.
        ///      If you attempt to store more than a few megabytes of data on a
        ///      player creature, you may have a bad time.
        /// N.B.: You can pass sqlqueries into DelayCommand; HOWEVER
        ///       *** they will NOT survive a game save/load ***
        ///       Any commands on a restored sqlquery will fail.
        /// </summary>
        public static IntPtr SqlPrepareQueryObject(uint oObject, string sQuery)
        {
            NWNXPInvoke.StackPushString(sQuery);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(924);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.SQLQuery);
        }

        /// <summary>
        /// Bind an integer to a named parameter of the given prepared query.
        /// Example:
        ///   sqlquery v = SqlPrepareQueryObject(GetModule(), "insert into test (col) values (@myint);");
        ///   SqlBindInt(v, "@v", 5);
        ///   SqlStep(v);
        /// </summary>
        public static void SqlBindInt(IntPtr sqlQuery, string sParam, int nValue)
        {
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.StackPushString(sParam);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(925);
        }

        /// <summary>
        /// Bind a float to a named parameter of the given prepared query.
        /// </summary>
        public static void SqlBindFloat(IntPtr sqlQuery, string sParam, float fFloat)
        {
            NWNXPInvoke.StackPushFloat(fFloat);
            NWNXPInvoke.StackPushString(sParam);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(926);
        }

        /// <summary>
        /// Bind a string to a named parameter of the given prepared query.
        /// </summary>
        public static void SqlBindString(IntPtr sqlQuery, string sParam, string sString)
        {
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.StackPushString(sParam);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(927);
        }

        /// <summary>
        /// Bind a vector to a named parameter of the given prepared query.
        /// </summary>
        public static void SqlBindVector(IntPtr sqlQuery, string sParam, Vector3 vVector)
        {
            NWNXPInvoke.StackPushVector(vVector);
            NWNXPInvoke.StackPushString(sParam);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(928);
        }

        /// <summary>
        /// Bind a object to a named parameter of the given prepared query.
        /// Objects are serialized, NOT stored as a reference!
        /// Currently supported object types: Creatures, Items, Placeables, Waypoints, Stores, Doors, Triggers, Encounters, Areas (CAF format)
        /// If bSaveObjectState is TRUE, local vars, effects, action queue, and transition info (triggers, doors) are saved out
        /// (except for Combined Area Format, which always has object state saved out).
        /// </summary>
        public static void SqlBindObject(IntPtr sqlQuery, string sParam, uint oObject, bool bSaveObjectState = false)
        {
            NWNXPInvoke.StackPushInteger(bSaveObjectState ? 1 : 0);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.StackPushString(sParam);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(929);
        }

        /// <summary>
        /// Executes the given query and fetches a row; returning true if row data was
        /// made available; false otherwise. Note that this will return false even if
        /// the query ran successfully but did not return data.
        /// You need to call SqlPrepareQuery() and potentially SqlBind* before calling this.
        /// Example:
        ///   sqlquery n = SqlPrepareQueryObject(GetFirstPC(), "select widget from widgets;");
        ///   while (SqlStep(n))
        ///     SendMessageToPC(GetFirstPC(), "Found widget: " + SqlGetString(n, 0));
        /// </summary>
        public static int SqlStep(IntPtr sqlQuery)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(930);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Retrieve a column cast as an integer of the currently stepped row.
        /// You can call this after SqlStep() returned TRUE.
        /// In case of error, 0 will be returned.
        /// In traditional fashion, nIndex starts at 0.
        /// </summary>
        public static int SqlGetInt(IntPtr sqlQuery, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(931);
            return NWNXPInvoke.StackPopInteger();
        }
        /// <summary>
        /// Retrieve a column cast as a float of the currently stepped row.
        /// You can call this after SqlStep() returned TRUE.
        /// In case of error, 0.0f will be returned.
        /// In traditional fashion, nIndex starts at 0.
        /// </summary>
        public static float SqlGetFloat(SQLQuery sqlQuery, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(932);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Retrieve a column cast as a string of the currently stepped row.
        /// You can call this after SqlStep() returned TRUE.
        /// In case of error, a empty string will be returned.
        /// In traditional fashion, nIndex starts at 0.
        /// </summary>
        public static string SqlGetString(SQLQuery sqlQuery, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(933);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Retrieve a vector of the currently stepped query.
        /// You can call this after SqlStep() returned TRUE.
        /// In case of error, a zero vector will be returned.
        /// In traditional fashion, nIndex starts at 0.
        /// </summary>
        public static Vector3 SqlGetVector(SQLQuery sqlQuery, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(934);
            return NWNXPInvoke.StackPopVector();
        }

        /// <summary>
        /// Retrieve a object of the currently stepped query.
        /// You can call this after SqlStep() returned TRUE.
        /// The object will be spawned into a inventory if it is a item and the receiver
        /// has the capability to receive it, otherwise at lSpawnAt.
        /// Objects are serialized, NOT stored as a reference!
        /// In case of error, INVALID_OBJECT will be returned.
        /// In traditional fashion, nIndex starts at 0.
        /// If bLoadObjectState is TRUE, local vars, effects, action queue, and transition info (triggers, doors) are read in.
        /// </summary>
        public static uint SqlGetObject(SQLQuery sqlQuery, int nIndex, Location lSpawnAt, uint oInventory = OBJECT_INVALID, bool bLoadObjectState = false)
        {
            NWNXPInvoke.StackPushInteger(bLoadObjectState ? 1 : 0);
            NWNXPInvoke.StackPushObject(oInventory);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lSpawnAt);
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(935);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Bind an json to a named parameter of the given prepared query.
        /// Json values are serialised into a string.
        /// Example:
        ///   sqlquery v = SqlPrepareQueryObject(GetModule(), "insert into test (col) values (@myjson);");
        ///   SqlBindJson(v, "@myjson", myJsonObject);
        ///   SqlStep(v);
        /// </summary>
        public static void SqlBindJson(SQLQuery sqlQuery, string sParam, Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.StackPushString(sParam);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(1000);
        }

        /// <summary>
        /// Retrieve a column cast as a json value of the currently stepped row.
        /// You can call this after SqlStep() returned TRUE.
        /// In case of error, a json null value will be returned.
        /// In traditional fashion, nIndex starts at 0.
        /// </summary>
        public static Json SqlGetJson(SQLQuery sqlQuery, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(1001);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Reset the given sqlquery, readying it for re-execution after it has been stepped.
        /// All existing binds are kept untouched, unless bClearBinds is TRUE.
        /// This command only works on successfully-prepared queries that have not errored out.
        /// </summary>
        public static void SqlResetQuery(SQLQuery sqlQuery, bool bClearBinds = false)
        {
            NWNXPInvoke.StackPushInteger(bClearBinds ? 1 : 0);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(1111);
        }

        /// <summary>
        /// Retrieve the column count of a prepared query.  
        /// * sqlQuery must be prepared before this function is called, but can be called before or after parameters are bound.
        /// * If the prepared query contains no columns (such as with an UPDATE or INSERT query), 0 is returned.
        /// * If a non-SELECT query contains a RETURNING clause, the number of columns in the RETURNING clause will be returned.
        /// * A returned value greater than 0 does not guarantee the query will return rows.
        /// </summary>
        public static int SqlGetColumnCount(SQLQuery sqlQuery)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(1126);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Retrieve the column name of the Nth column of a prepared query.
        /// * sqlQuery must be prepared before this function is called, but can be called before or after parameters are bound.
        /// * If the prepared query contains no columns (such as with an UPDATE or INSERT query), an empty string is returned.
        /// * If a non-SELECT query contains a RETURNING clause, the name of the nNth column in the RETURNING clause is returned.
        /// * If nNth is out of range, an sqlite error is broadcast and an empty string is returned.
        /// * The value of the AS clause will be returned, if the clause exists for the nNth column.
        /// * A returned non-empty string does not guarantee the query will return rows.
        /// </summary>
        public static string SqlGetColumnName(SQLQuery sqlQuery, int nNth)
        {
            NWNXPInvoke.StackPushInteger(nNth);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.SQLQuery, sqlQuery);
            NWNXPInvoke.CallBuiltIn(1127);
            return NWNXPInvoke.StackPopString();
        }

    }
}
