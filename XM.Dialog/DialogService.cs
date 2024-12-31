using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Anvil.API;
using Anvil.API.Events;
using Anvil.Services;
using NLog;
using XM.API.Constants;
using XM.Core.EventManagement.XMEvent;
using XM.Dialog.Event;
using EventScriptType = XM.API.Constants.EventScriptType;

namespace XM.Dialog
{
    [ServiceBinding(typeof(DialogService))]
    [ServiceBinding(typeof(IXMOnCacheDataBefore))]
    public class DialogService: IXMOnCacheDataBefore
    {

        [ScriptHandler("bread_test")]
        public void BreadTest()
        {
            var player = GetLastUsedBy();
            StartConversation<TestDialog>(player, OBJECT_SELF);
        }

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private const int NumberOfDialogs = 255;
        private const int NumberOfResponsesPerPage = 12;
        private Dictionary<string, PlayerDialog> PlayerDialogs { get; } = new();
        private Dictionary<int, bool> DialogFilesInUse { get; } = new();

        private readonly Dictionary<string, IConversation> _conversations = new();

        public IList<IConversation> Definitions { get; set; }

        private readonly IServiceManager _serviceManager;

        public DialogService(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;

            NwModule.Instance.OnModuleLoad += OnModuleLoad;
        }

        /// <summary>
        /// When the module is loaded, the assembly will be searched for conversations.
        /// These will be added to the cache for use at a later time.
        /// </summary>
        public void OnCacheDataBefore()
        {
            // Use reflection to get all of the conversation implementations.
            var classes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(p => typeof(IConversation).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                .ToArray();

            foreach (var type in classes)
            {
                var instance = _serviceManager.AnvilServiceContainer.GetInstance(type) as IConversation;
                if (instance == null)
                {
                    throw new NullReferenceException("Unable to activate instance of type: " + type);
                }
                _conversations.Add(type.Name, instance);
            }

            Console.WriteLine($"Loaded {_conversations.Count} conversations.");
        }

        private void OnModuleLoad(ModuleEvents.OnModuleLoad obj)
        {
            InitializeDialogs();
        }

        private void InitializeDialogs()
        {
            for (var x = 1; x <= NumberOfDialogs; x++)
            {
                DialogFilesInUse.Add(x, false);
            }
        }

        /// <summary>
        /// Retrieves a conversation from the cache.
        /// </summary>
        /// <param name="key">The name of the conversation.</param>
        /// <returns>A conversation instance</returns>
        public IConversation GetConversation(string key)
        {
            if (!_conversations.ContainsKey(key))
            {
                throw new KeyNotFoundException("Conversation '" + key + "' is not registered. Did you create a class for it?");
            }

            return _conversations[key];
        }

        /// <summary>
        /// Handles when a dialog is started.
        /// </summary>
        [ScriptHandler(DialogEventScript.DialogStartScript)]
        public void Start()
        {
            var eventScript = GetCurrentlyRunningEvent();
            var player = OBJECT_INVALID;

            switch (eventScript)
            {
                case EventScriptType.PlaceableOnUsed:
                    player = GetLastUsedBy();
                    break;
                case EventScriptType.CreatureOnDialogue:
                    player = GetPCSpeaker();
                    break;
                case EventScriptType.DoorOnFailToOpen:
                    player = GetClickingObject();
                    break;
            }
            
            var conversation = GetLocalString(OBJECT_SELF, "CONVERSATION");

            if (!string.IsNullOrWhiteSpace(conversation))
            {
                var objectType = GetObjectType(OBJECT_SELF);
                if (objectType == ObjectType.Placeable)
                {
                    var talkTo = OBJECT_SELF;
                    StartConversation(player, talkTo, conversation);
                }
                else
                {
                    var talkTo = (OBJECT_SELF);
                    StartConversation(player, talkTo, conversation);
                }
            }
            else
            {
                ActionStartConversation(player, "", true, false);
            }
        }

        [ScriptHandler(DialogEventScript.DialogAction0Script)]
        public void NodeAction0()
        {
            ActionsTaken(0);
        }

        [ScriptHandler(DialogEventScript.DialogAction1Script)]
        public void NodeAction1()
        {
            ActionsTaken(1);
        }

        [ScriptHandler(DialogEventScript.DialogAction2Script)]
        public void NodeAction2()
        {
            ActionsTaken(2);
        }

        [ScriptHandler(DialogEventScript.DialogAction3Script)]
        public void NodeAction3()
        {
            ActionsTaken(3);
        }

        [ScriptHandler(DialogEventScript.DialogAction4Script)]
        public void NodeAction4()
        {
            ActionsTaken(4);
        }

        [ScriptHandler(DialogEventScript.DialogAction5Script)]
        public void NodeAction5()
        {
            ActionsTaken(5);
        }

        [ScriptHandler(DialogEventScript.DialogAction6Script)]
        public void NodeAction6()
        {
            ActionsTaken(6);
        }

        [ScriptHandler(DialogEventScript.DialogAction7Script)]
        public void NodeAction7()
        {
            ActionsTaken(7);
        }

        [ScriptHandler(DialogEventScript.DialogAction8Script)]
        public void NodeAction8()
        {
            ActionsTaken(8);
        }

        [ScriptHandler(DialogEventScript.DialogAction9Script)]
        public void NodeAction9()
        {
            ActionsTaken(9);
        }

        [ScriptHandler(DialogEventScript.DialogAction10Script)]
        public void NodeAction10()
        {
            ActionsTaken(10);
        }

        [ScriptHandler(DialogEventScript.DialogAction11Script)]
        public void NodeAction11()
        {
            ActionsTaken(11);
        }

        [ScriptHandler(DialogEventScript.DialogAppears0Script)]
        public bool NodeAppears0()
        {
            return AppearsWhen(2, 0);
        }
        [ScriptHandler(DialogEventScript.DialogAppears1Script)]
        public bool NodeAppears1()
        {
            return AppearsWhen(2, 1);
        }

        [ScriptHandler(DialogEventScript.DialogAppears2Script)]
        public bool NodeAppears2()
        {
            return AppearsWhen(2, 2);
        }

        [ScriptHandler(DialogEventScript.DialogAppears3Script)]
        public bool NodeAppears3()
        {
            return AppearsWhen(2, 3);
        }

        [ScriptHandler(DialogEventScript.DialogAppears4Script)]
        public bool NodeAppears4()
        {
            return AppearsWhen(2, 4);
        }

        [ScriptHandler(DialogEventScript.DialogAppears5Script)]
        public bool NodeAppears5()
        {
            return AppearsWhen(2, 5);
        }

        [ScriptHandler(DialogEventScript.DialogAppears6Script)]
        public bool NodeAppears6()
        {
            return AppearsWhen(2, 6);
        }

        [ScriptHandler(DialogEventScript.DialogAppears7Script)]
        public bool NodeAppears7()
        {
            return AppearsWhen(2, 7);
        }

        [ScriptHandler(DialogEventScript.DialogAppears8Script)]
        public bool NodeAppears8()
        {
            return AppearsWhen(2, 8);
        }

        [ScriptHandler(DialogEventScript.DialogAppears9Script)]
        public bool NodeAppears9()
        {
            return AppearsWhen(2, 9);
        }

        [ScriptHandler(DialogEventScript.DialogAppears10Script)]
        public bool NodeAppears10()
        {
            return AppearsWhen(2, 10);
        }

        [ScriptHandler(DialogEventScript.DialogAppears11Script)]
        public bool NodeAppears11()
        {
            return AppearsWhen(2, 11);
        }

        [ScriptHandler(DialogEventScript.DialogAppearsHeaderScript)]
        public bool HeaderAppearsWhen()
        {
            return AppearsWhen(1, 0);
        }

        [ScriptHandler(DialogEventScript.DialogAppearsNextScript)]
        public bool NextAppearsWhen()
        {
            return AppearsWhen(3, 12);
        }

        [ScriptHandler(DialogEventScript.DialogActionNextScript)]
        public void NextAction()
        {
            ActionsTaken(12);
        }

        [ScriptHandler(DialogEventScript.DialogAppearsPreviousScript)]
        public bool PreviousAppearsWhen()
        {
            return AppearsWhen(4, 13);
        }

        [ScriptHandler(DialogEventScript.DialogActionPreviousScript)]
        public void PreviousAction()
        {
            ActionsTaken(13);
        }

        [ScriptHandler(DialogEventScript.DialogAppearsBackScript)]
        public bool BackAppearsWhen()
        {
            return AppearsWhen(5, 14);
        }

        [ScriptHandler(DialogEventScript.DialogActionBackScript)]
        public void BackAction()
        {
            ActionsTaken(14);
        }

        /// <summary>
        /// Fires when the "End Dialog" node is clicked.
        /// </summary>
        [ScriptHandler(DialogEventScript.DialogEndScript)]
        public void End()
        {
            var player = GetPCSpeaker();
            var playerId = GetObjectUUID(player);
            if (!HasPlayerDialog(playerId)) return;

            var dialog = LoadPlayerDialog(playerId);

            foreach (var endAction in dialog.EndActions)
            {
                endAction();
            }

            RemovePlayerDialog(playerId);
            DeleteLocalInt(player, "DIALOG_SYSTEM_INITIALIZE_RAN");
        }

        /// <summary>
        /// This fires any time the "Appears When" event fires on a node.
        /// This handles showing/hiding nodes depending on the rules set up by the conversation.
        /// It will also handle pagination with the Next/Previous buttons.
        /// </summary>
        /// <param name="nodeType">The type of node we're working with.</param>
        /// <param name="nodeId">The Id number of the node.</param>
        /// <returns>true if the node is visible, false otherwise</returns>
        private bool AppearsWhen(int nodeType, int nodeId)
        {
            var player = GetPCSpeaker();
            var playerId = GetObjectUUID(player);
            var hasDialog = HasPlayerDialog(playerId);
            if (!hasDialog) return false;
            var dialog = LoadPlayerDialog(playerId);

            var page = dialog.CurrentPage;

            // Initialization should run one time per conversation.
            if (GetLocalInt(player, "DIALOG_SYSTEM_INITIALIZE_RAN") != 1)
            {
                foreach (var initializationAction in dialog.InitializationActions)
                {
                    initializationAction();
                }

                SetLocalInt(player, "DIALOG_SYSTEM_INITIALIZE_RAN", 1);
            }

            // The AppearsWhen call happens for every node.
            // We only want to load the header and responses one time so ensure it only happens for the first node.
            if (!dialog.IsEnding && nodeType == 1 && nodeId == 0)
            {
                page.Header = string.Empty;
                page.Responses.Clear();
                page.PageInit?.Invoke(page);
            }

            var currentSelectionNumber = nodeId + 1;
            var displayNode = false;
            var newNodeText = string.Empty;
            var dialogOffset = (NumberOfResponsesPerPage + 1) * (dialog.DialogNumber - 1);

            if (currentSelectionNumber == NumberOfResponsesPerPage + 1) // Next page
            {
                var displayCount = page.NumberOfResponses - (NumberOfResponsesPerPage * dialog.PageOffset);

                if (displayCount > NumberOfResponsesPerPage)
                {
                    displayNode = true;
                }
            }
            else if (currentSelectionNumber == NumberOfResponsesPerPage + 2) // Previous Page
            {
                if (dialog.PageOffset > 0)
                {
                    displayNode = true;
                }
            }
            else if (currentSelectionNumber == NumberOfResponsesPerPage + 3) // Back
            {
                if (dialog.NavigationStack.Count > 0 && dialog.EnableBackButton)
                {
                    displayNode = true;
                }
            }
            else if (nodeType == 2)
            {
                var responseID = (dialog.PageOffset * NumberOfResponsesPerPage) + nodeId;
                if (responseID + 1 <= page.NumberOfResponses)
                {
                    var response = page.Responses[responseID];

                    if (response != null)
                    {
                        newNodeText = response.Text;
                        displayNode = response.IsActive;
                    }
                }
            }
            else if (nodeType == 1)
            {
                if (dialog.IsEnding)
                {
                    foreach (var endAction in dialog.EndActions)
                    {
                        endAction();
                    }
                    RemovePlayerDialog(playerId);
                    DeleteLocalInt(player, "DIALOG_SYSTEM_INITIALIZE_RAN");
                    return false;
                }

                page = dialog.CurrentPage;
                newNodeText = page.Header;

                SetCustomToken(90000 + dialogOffset, newNodeText);
                return true;
            }

            SetCustomToken(90001 + nodeId + dialogOffset, newNodeText);
            return displayNode;
        }

        /// <summary>
        /// This fires any time an "Action Taken" node is clicked on by the player.
        /// This executes the specific node actions such as next page, previous page, back, etc.
        /// </summary>
        /// <param name="nodeId">The Id of the node selected.</param>
        private void ActionsTaken(int nodeId)
        {
            var player = GetPCSpeaker();
            var playerId = GetObjectUUID(player);
            var dialog = LoadPlayerDialog(playerId);

            var selectionNumber = nodeId + 1;
            var responseID = nodeId + (NumberOfResponsesPerPage * dialog.PageOffset);

            if (selectionNumber == NumberOfResponsesPerPage + 1) // Next page
            {
                dialog.PageOffset = dialog.PageOffset + 1;
            }
            else if (selectionNumber == NumberOfResponsesPerPage + 2) // Previous page
            {
                dialog.PageOffset = dialog.PageOffset - 1;
            }
            else if (selectionNumber == NumberOfResponsesPerPage + 3) // Back
            {
                var currentPageName = dialog.CurrentPageName;
                var previous = dialog.NavigationStack.Pop();

                // This might be a little confusing but we're passing the active page as the "old page" to the Back() method.
                // This is because we need to run any dialog-specific clean up prior to moving the conversation backwards.
                foreach (var action in dialog.BackActions)
                {
                    action(currentPageName, previous.PageName);
                }


                // Previous page was in a different conversation. Switch to it.
                if (previous.DialogName != dialog.ActiveDialogName)
                {
                    LoadConversation(player, dialog.DialogTarget, previous.DialogName, dialog.DialogNumber);
                    dialog = LoadPlayerDialog(playerId);
                    dialog.ResetPage();

                    dialog.CurrentPageName = previous.PageName;
                    dialog.PageOffset = 0;
                    
                    foreach (var initializationAction in dialog.InitializationActions)
                    {
                        initializationAction();
                    }

                    SetLocalInt(player, "DIALOG_SYSTEM_INITIALIZE_RAN", 1);
                }
                // Otherwise it's in the same conversation. Switch to that.
                else
                {
                    dialog.CurrentPageName = previous.PageName;
                    dialog.PageOffset = 0;
                }
            }
            else if (selectionNumber != NumberOfResponsesPerPage + 4) // End
            {
                dialog.Pages[dialog.CurrentPageName].Responses[responseID].Action.Invoke();
            }
        }

        /// <summary>
        /// When an object executes this script, the custom dialog specified on their local variables
        /// will be started.
        /// </summary>
        [ScriptHandler(DialogEventScript.StartConversationScript)]
        public void StartConversationEvent()
        {
            var self = OBJECT_SELF;
            var conversation = GetLocalString(self, "CONVERSATION");

            if (string.IsNullOrWhiteSpace(conversation)) return;

            var talker = GetLastUsedBy();
            StartConversation(talker, self, conversation);
        }

        /// <summary>
        /// Begins a new conversation for a player.
        /// </summary>
        /// <param name="player">The player to start the conversation for.</param>
        /// <param name="talkTo">The creature to speak to.</param>
        /// <param name="class">The name of the conversation class.</param>
        private void StartConversation(uint player, uint talkTo, string @class)
        {
            if (!GetIsPC(player) || !GetIsObjectValid(player))
            {
                _logger.Error($"Conversation '{@class}' could not be started because player '{GetName(player)}' is not a valid target.");
                return;
            }

            var playerId = GetObjectUUID(player);
            LoadConversation(player, talkTo, @class, -1);
            var dialog = PlayerDialogs[playerId];

            // NPC conversations

            if (GetObjectType(talkTo) == ObjectType.Creature &&
                !GetIsPC(talkTo) &&
                !GetIsDM(talkTo))
            {
                BeginConversation("dialog" + dialog.DialogNumber);
            }
            // Everything else
            else
            {
                AssignCommand(player, () => ActionStartConversation(talkTo, "dialog" + dialog.DialogNumber, true, false));
            }
        }

        /// <summary>
        /// Begins a new conversation for a player.
        /// </summary>
        /// <param name="player">The player to start the conversation for.</param>
        /// <param name="talkTo">The creature to speak to.</param>
        public void StartConversation<T>(uint player, uint talkTo)
            where T: IConversation
        {
            StartConversation(player, talkTo, typeof(T).Name);
        }

        /// <summary>
        /// Checks whether a player has an active dialog in cache.
        /// </summary>
        /// <param name="playerId">The player's unique Id.</param>
        /// <returns>true if the player has the dialog, false otherwise</returns>
        public bool HasPlayerDialog(string playerId)
        {
            return PlayerDialogs.ContainsKey(playerId);
        }

        /// <summary>
        /// Loads a specific player's cached dialog information.
        /// </summary>
        /// <param name="playerId">The player's unique Id</param>
        /// <returns>The cached player dialog object.</returns>
        public PlayerDialog LoadPlayerDialog(string playerId)
        {
            if (!PlayerDialogs.ContainsKey(playerId)) throw new Exception(nameof(playerId) + " '" + playerId + "' could not be found. Be sure to call " + nameof(LoadConversation) + " first.");

            return PlayerDialogs[playerId];
        }

        /// <summary>
        /// Removes a player's dialog from the cache.
        /// </summary>
        /// <param name="playerId">The player's unique Id.</param>
        public void RemovePlayerDialog(string playerId)
        {
            var dialog = PlayerDialogs[playerId];
            DialogFilesInUse[dialog.DialogNumber] = false;

            PlayerDialogs.Remove(playerId);
        }

        /// <summary>
        /// Loads a conversation for a player.
        /// </summary>
        /// <param name="player">The player to load the conversation for.</param>
        /// <param name="talkTo">The creature to talk to.</param>
        /// <param name="class">The conversation name to start.</param>
        /// <param name="dialogNumber">The dialog number the player's conversation is tied to.</param>
        public void LoadConversation(uint player, uint talkTo, string @class, int dialogNumber)
        {
            if (string.IsNullOrWhiteSpace(@class)) throw new ArgumentException(nameof(@class), nameof(@class) + " cannot be null, empty, or whitespace.");
            if (dialogNumber != -1 && (dialogNumber < 1 || dialogNumber > NumberOfDialogs)) throw new ArgumentOutOfRangeException(nameof(dialogNumber), nameof(dialogNumber) + " must be between 1 and " + NumberOfDialogs);

            var convo = GetConversation(@class);
            var dialog = convo.SetUp(player);
            var playerId = GetObjectUUID(player);

            if (dialog == null)
            {
                throw new NullReferenceException(nameof(dialog) + " cannot be null.");
            }

            if (dialogNumber > 0)
                dialog.DialogNumber = dialogNumber;

            dialog.ActiveDialogName = @class;
            dialog.DialogTarget = talkTo;
            StorePlayerDialog(playerId, dialog);
        }

        /// <summary>
        /// Stores a player's active dialog into the cache.
        /// </summary>
        /// <param name="playerId">The player's unique Id</param>
        /// <param name="dialog">The dialog to store.</param>
        private void StorePlayerDialog(string playerId, PlayerDialog dialog)
        {
            if (dialog.DialogNumber <= 0)
            {
                for (var x = 1; x <= NumberOfDialogs; x++)
                {
                    var existingDialog = PlayerDialogs.SingleOrDefault(d => d.Value.DialogNumber == x);
                    if (!DialogFilesInUse[x] || existingDialog.Value == null)
                    {
                        DialogFilesInUse[x] = true;
                        dialog.DialogNumber = x;
                        break;
                    }
                }
            }

            // Couldn't find an open dialog file. Throw error.
            if (dialog.DialogNumber <= 0)
            {
                Console.WriteLine("ERROR: Unable to locate a free dialog. Add more dialog files, update their custom tokens, and update Dialog.cs");
                return;
            }

            PlayerDialogs[playerId] = dialog;
        }

        /// <summary>
        /// Ends a conversation and cleans up related cache data.
        /// </summary>
        /// <param name="player">The player to end the conversation for.</param>
        public void EndConversation(uint player)
        {
            var playerId = GetObjectUUID(player);
            var playerDialog = LoadPlayerDialog(playerId);
            playerDialog.IsEnding = true;
            StorePlayerDialog(playerId, playerDialog);
        }

    }
}
