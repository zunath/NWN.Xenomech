using Anvil.Services;
using NLog;
using XM.Shared.API.Constants;

namespace XM.Core.EventManagement.Registration
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
            _event.RegisterEvent<ModuleEvent.OnHeartbeat>(EventScript.OnModuleHeartbeatScript);
            _event.RegisterEvent<ModuleEvent.OnUserDefined>(EventScript.OnModuleUserDefinedEventScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerEnter>(EventScript.OnModuleOnClientEnterScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerLeave>(EventScript.OnModuleOnClientExitScript);
            _event.RegisterEvent<ModuleEvent.OnActivateItem>(EventScript.OnModuleActivateItemScript);
            _event.RegisterEvent<ModuleEvent.OnAcquireItem>(EventScript.OnModuleAcquireItemScript);
            _event.RegisterEvent<ModuleEvent.OnUnacquireItem>(EventScript.OnModuleUnacquireItemScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerDeath>(EventScript.OnModulePlayerDeathScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerDying>(EventScript.OnModulePlayerDyingScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerRespawn>(EventScript.OnModuleRespawnScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerRest>(EventScript.OnModulePlayerRestScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerLevelUp>(EventScript.OnModulePlayerLevelUpScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerCancelCutscene>(EventScript.OnModulePlayerCancelCutsceneScript);
            _event.RegisterEvent<ModuleEvent.OnEquipItem>(EventScript.OnModuleEquipItemScript);
            _event.RegisterEvent<ModuleEvent.OnUnequipItem>(EventScript.OnModuleUnequipItemScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerChat>(EventScript.OnModulePlayerChatScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerTarget>(EventScript.OnModulePlayerTargetScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerGui>(EventScript.OnModulePlayerGuiScript);
            _event.RegisterEvent<ModuleEvent.OnPlayerTile>(EventScript.OnModulePlayerTileScript);
            _event.RegisterEvent<ModuleEvent.OnNuiEvent>(EventScript.OnModuleNuiEventScript);

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
