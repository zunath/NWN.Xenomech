using Anvil.Services;
using System.Collections.Generic;
using XM.API.Constants;
using XM.Core.EventManagement.ModuleEvent;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(ModuleEventRegistrationService))]
    internal class ModuleEventRegistrationService: EventRegistrationServiceBase
    {
        public ModuleEventRegistrationService()
        {
            HookModuleEvents();
        }

        private void HookModuleEvents()
        {
            var module = GetModule();

            SetEventScript(module, EventScriptType.ModuleOnHeartbeat, EventScript.OnModuleHeartbeatScript);
            SetEventScript(module, EventScriptType.ModuleOnUserDefinedEvent, EventScript.OnModuleUserDefinedEventScript);
            SetEventScript(module, EventScriptType.ModuleOnModuleLoad, EventScript.OnModuleLoadScript);
            SetEventScript(module, EventScriptType.ModuleOnClientEnter, EventScript.OnModuleOnClientEnterScript);
            SetEventScript(module, EventScriptType.ModuleOnClientExit, EventScript.OnModuleOnClientExitScript);
            SetEventScript(module, EventScriptType.ModuleOnActivateItem, EventScript.OnModuleActivateItemScript);
            SetEventScript(module, EventScriptType.ModuleOnAcquireItem, EventScript.OnModuleAcquireItemScript);
            SetEventScript(module, EventScriptType.ModuleOnLoseItem, EventScript.OnModuleUnacquireItemScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerDeath, EventScript.OnModulePlayerDeathScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerDying, EventScript.OnModulePlayerDyingScript);
            SetEventScript(module, EventScriptType.ModuleOnRespawnButtonPressed, EventScript.OnModuleRespawnScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerRest, EventScript.OnModulePlayerRestScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerLevelUp, EventScript.OnModulePlayerLevelUpScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerCancelCutscene, EventScript.OnModulePlayerCancelCutsceneScript);
            SetEventScript(module, EventScriptType.ModuleOnEquipItem, EventScript.OnModuleEquipItemScript);
            SetEventScript(module, EventScriptType.ModuleOnUnequipItem, EventScript.OnModuleUnequipItemScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerChat, EventScript.OnModulePlayerChatScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerTarget, EventScript.OnModulePlayerTargetScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerGuiEvent, EventScript.OnModulePlayerGuiScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerTileAction, EventScript.OnModulePlayerTileScript);
            SetEventScript(module, EventScriptType.ModuleOnNuiEvent, EventScript.OnModuleNuiEventScript);
        }

        [Inject]
        public IList<IModuleOnHeartbeatEvent> OnModuleHeartbeatSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleHeartbeatScript)]
        public void HandleModuleHeartbeat() => HandleEvent(OnModuleHeartbeatSubscriptions, (subscription) => subscription.OnModuleHeartbeat());

        [Inject]
        public IList<IModuleOnUserDefinedEvent> OnModuleUserDefinedEventSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleUserDefinedEventScript)]
        public void HandleModuleUserDefinedEvent() => HandleEvent(OnModuleUserDefinedEventSubscriptions, (subscription) => subscription.OnModuleUserDefined());

        [Inject]
        public IList<IModuleOnLoadEvent> OnModuleLoadSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleLoadScript)]
        public void HandleModuleLoad() => HandleEvent(OnModuleLoadSubscriptions, (subscription) => subscription.OnModuleLoad());

        [Inject]
        public IList<IModuleOnPlayerEnterEvent> OnModuleClientEnterSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleOnClientEnterScript)]
        public void HandleModuleClientEnter() => HandleEvent(OnModuleClientEnterSubscriptions, (subscription) => subscription.OnModuleClientEnter());

        [Inject]
        public IList<IModuleOnPlayerLeaveEvent> OnModuleClientExitSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleOnClientExitScript)]
        public void HandleModuleClientExit() => HandleEvent(OnModuleClientExitSubscriptions, (subscription) => subscription.OnModuleClientExit());

        [Inject]
        public IList<IModuleOnActivateItemEvent> OnModuleActivateItemSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleActivateItemScript)]
        public void HandleModuleActivateItem() => HandleEvent(OnModuleActivateItemSubscriptions, (subscription) => subscription.OnModuleActivateItem());

        [Inject]
        public IList<IModuleOnAcquireItemEvent> OnModuleAcquireItemSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleAcquireItemScript)]
        public void HandleModuleAcquireItem() => HandleEvent(OnModuleAcquireItemSubscriptions, (subscription) => subscription.OnModuleAcquireItem());

        [Inject]
        public IList<IModuleOnUnacquireItemEvent> OnModuleLoseItemSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleUnacquireItemScript)]
        public void HandleModuleLoseItem() => HandleEvent(OnModuleLoseItemSubscriptions, (subscription) => subscription.OnModuleLoseItem());

        [Inject]
        public IList<IModuleOnPlayerDeathEvent> OnModulePlayerDeathSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModulePlayerDeathScript)]
        public void HandleModulePlayerDeath() => HandleEvent(OnModulePlayerDeathSubscriptions, (subscription) => subscription.OnModulePlayerDeath());

        [Inject]
        public IList<IModuleOnPlayerDyingEvent> OnModulePlayerDyingSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModulePlayerDyingScript)]
        public void HandleModulePlayerDying() => HandleEvent(OnModulePlayerDyingSubscriptions, (subscription) => subscription.OnModulePlayerDying());

        [Inject]
        public IList<IModuleOnPlayerRespawnEvent> OnModuleRespawnButtonPressedSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleRespawnScript)]
        public void HandleModuleRespawnButtonPressed() => HandleEvent(OnModuleRespawnButtonPressedSubscriptions, (subscription) => subscription.OnModuleRespawnButtonPressed());

        [Inject]
        public IList<IModuleOnPlayerRestEvent> OnModulePlayerRestSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModulePlayerRestScript)]
        public void HandleModulePlayerRest() => HandleEvent(OnModulePlayerRestSubscriptions, (subscription) => subscription.OnModulePlayerRest());

        [Inject]
        public IList<IModuleOnPlayerLevelUpEvent> OnModulePlayerLevelUpSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModulePlayerLevelUpScript)]
        public void HandleModulePlayerLevelUp() => HandleEvent(OnModulePlayerLevelUpSubscriptions, (subscription) => subscription.OnModulePlayerLevelUp());

        [Inject]
        public IList<IModuleOnCancelCutsceneEvent> OnModulePlayerCancelCutsceneSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModulePlayerCancelCutsceneScript)]
        public void HandleModulePlayerCancelCutscene() => HandleEvent(OnModulePlayerCancelCutsceneSubscriptions, (subscription) => subscription.OnModulePlayerCancelCutscene());

        [Inject]
        public IList<IModuleOnEquipEvent> OnModuleEquipItemSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleEquipItemScript)]
        public void HandleModuleEquipItem() => HandleEvent(OnModuleEquipItemSubscriptions, (subscription) => subscription.OnModuleEquipItem());

        [Inject]
        public IList<IModuleOnUnequipItemEvent> OnModuleUnequipItemSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleUnequipItemScript)]
        public void HandleModuleUnequipItem() => HandleEvent(OnModuleUnequipItemSubscriptions, (subscription) => subscription.OnModuleUnequipItem());

        [Inject]
        public IList<IModuleOnChatEvent> OnModulePlayerChatSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModulePlayerChatScript)]
        public void HandleModulePlayerChat() => HandleEvent(OnModulePlayerChatSubscriptions, (subscription) => subscription.OnModulePlayerChat());

        [Inject]
        public IList<IModuleOnPlayerTargetEvent> OnModulePlayerTargetSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModulePlayerTargetScript)]
        public void HandleModulePlayerTarget() => HandleEvent(OnModulePlayerTargetSubscriptions, (subscription) => subscription.OnModulePlayerTarget());

        [Inject]
        public IList<IModuleOnPlayerGuiEvent> OnModulePlayerGuiEventSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModulePlayerGuiScript)]
        public void HandleModulePlayerGuiEvent() => HandleEvent(OnModulePlayerGuiEventSubscriptions, (subscription) => subscription.OnModulePlayerGuiEvent());

        [Inject]
        public IList<IModuleOnPlayerTileEvent> OnModulePlayerTileActionSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModulePlayerTileScript)]
        public void HandleModulePlayerTileAction() => HandleEvent(OnModulePlayerTileActionSubscriptions, (subscription) => subscription.OnModulePlayerTileAction());

        [Inject]
        public IList<IModuleOnNuiEvent> OnModuleNuiEventSubscriptions { get; set; }

        [ScriptHandler(EventScript.OnModuleNuiEventScript)]
        public void HandleModuleNuiEvent() => HandleEvent(OnModuleNuiEventSubscriptions, (subscription) => subscription.OnModuleNuiEvent());

    }
}
