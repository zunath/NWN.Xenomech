using Anvil.Services;
using NLog;
using XM.API.Constants;
using XM.Core.EventManagement.ModuleEvent;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(ModuleEventRegistrationService))]
    internal class ModuleEventRegistrationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly XMEventService _event;

        public ModuleEventRegistrationService(XMEventService @event)
        {
            _event = @event;

            _logger.Info($"Registering module events...");
            RegisterEvents();
            HookModuleEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<ModuleOnHeartbeatEvent>(EventScript.OnModuleHeartbeatScript);
            _event.RegisterEvent<ModuleOnUserDefinedEvent>(EventScript.OnModuleUserDefinedEventScript);
            _event.RegisterEvent<ModuleOnPlayerEnterEvent>(EventScript.OnModuleOnClientEnterScript);
            _event.RegisterEvent<ModuleOnPlayerLeaveEvent>(EventScript.OnModuleOnClientExitScript);
            _event.RegisterEvent<ModuleOnActivateItemEvent>(EventScript.OnModuleActivateItemScript);
            _event.RegisterEvent<ModuleOnAcquireItemEvent>(EventScript.OnModuleAcquireItemScript);
            _event.RegisterEvent<ModuleOnUnacquireItemEvent>(EventScript.OnModuleUnacquireItemScript);
            _event.RegisterEvent<ModuleOnPlayerDeathEvent>(EventScript.OnModulePlayerDeathScript);
            _event.RegisterEvent<ModuleOnPlayerDyingEvent>(EventScript.OnModulePlayerDyingScript);
            _event.RegisterEvent<ModuleOnPlayerRespawnEvent>(EventScript.OnModuleRespawnScript);
            _event.RegisterEvent<ModuleOnPlayerRestEvent>(EventScript.OnModulePlayerRestScript);
            _event.RegisterEvent<ModuleOnPlayerLevelUpEvent>(EventScript.OnModulePlayerLevelUpScript);
            _event.RegisterEvent<ModuleOnCancelCutsceneEvent>(EventScript.OnModulePlayerCancelCutsceneScript);
            _event.RegisterEvent<ModuleOnEquipEvent>(EventScript.OnModuleEquipItemScript);
            _event.RegisterEvent<ModuleOnUnequipItemEvent>(EventScript.OnModuleUnequipItemScript);
            _event.RegisterEvent<ModuleOnChatEvent>(EventScript.OnModulePlayerChatScript);
            _event.RegisterEvent<ModuleOnPlayerTargetEvent>(EventScript.OnModulePlayerTargetScript);
            _event.RegisterEvent<ModuleOnPlayerGuiEvent>(EventScript.OnModulePlayerGuiScript);
            _event.RegisterEvent<ModuleOnPlayerTileEvent>(EventScript.OnModulePlayerTileScript);
            _event.RegisterEvent<ModuleOnNuiEvent>(EventScript.OnModuleNuiEventScript);

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
    }
}
