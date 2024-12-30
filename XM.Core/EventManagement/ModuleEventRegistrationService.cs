using System;
using System.Collections.Generic;
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


        [Inject]
        public IList<IOnModuleAcquireItem> OnModuleAcquireItemSubscriptions { get; set; }

        [Inject]
        public IList<IOnModuleActivateItem> OnModuleActivateItemSubscriptions { get; set; }

        [Inject]
        public IList<IOnModuleEnter> OnModuleOnClientEnterSubscriptions { get; set; }

        [Inject]
        public IList<IOnModuleOnClientExit> OnModuleOnClientExitSubscriptions { get; set; }

        [Inject]
        public IList<IOnModulePlayerCancelCutscene> OnModulePlayerCancelCutsceneSubscriptions { get; set; }

        [Inject]
        public IList<IOnModuleHeartbeat> OnModuleHeartbeatSubscriptions { get; set; }

        [Inject]
        public IList<IOnModuleLoad> OnModuleLoadSubscriptions { get; set; }

        [Inject]
        public IList<IOnModulePlayerChat> OnModulePlayerChatSubscriptions { get; set; }

        [Inject]
        public IList<IOnModulePlayerDying> OnModulePlayerDyingSubscriptions { get; set; }

        [Inject]
        public IList<IOnModulePlayerDeath> OnModulePlayerDeathSubscriptions { get; set; }

        [Inject]
        public IList<IOnModuleEquipItem> OnModuleEquipItemSubscriptions { get; set; }

        [Inject]
        public IList<IOnModulePlayerLevelUp> OnModulePlayerLevelUpSubscriptions { get; set; }

        [Inject]
        public IList<IOnModuleRespawn> OnModuleRespawnSubscriptions { get; set; }

        [Inject]
        public IList<IOnModulePlayerRest> OnModulePlayerRestSubscriptions { get; set; }

        [Inject]
        public IList<IOnModuleUnequipItem> OnModuleUnequipItemSubscriptions { get; set; }

        [Inject]
        public IList<IOnModuleUnacquireItem> OnModuleUnacquireItemSubscriptions { get; set; }

        [Inject]
        public IList<IOnModuleUserDefinedEvent> OnModuleUserDefinedEventSubscriptions { get; set; }

        [Inject]
        public IList<IOnModulePlayerTarget> OnModulePlayerTargetSubscriptions { get; set; }

        [Inject]
        public IList<IOnModulePlayerGui> OnModulePlayerGuiSubscriptions { get; set; }

        [Inject]
        public IList<IOnModulePlayerTile> OnModulePlayerTileSubscriptions { get; set; }

        [Inject]
        public IList<IOnModuleNuiEvent> OnModuleNuiEventSubscriptions { get; set; }

        public ModuleEventRegistrationService()
        {
            HookModuleEvents();
        }

        private void HookModuleEvents()
        {
            Console.WriteLine($"Registering module events...");
            var module = GetModule();

            SetEventScript(module, EventScriptType.ModuleOnAcquireItem, EventScript.OnModuleAcquireItemScript);
            SetEventScript(module, EventScriptType.ModuleOnActivateItem, EventScript.OnModuleActivateItemScript);
            SetEventScript(module, EventScriptType.ModuleOnClientEnter, EventScript.OnModuleOnClientEnterScript);
            SetEventScript(module, EventScriptType.ModuleOnClientExit, EventScript.OnModuleOnClientExitScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerCancelCutscene, EventScript.OnModulePlayerCancelCutsceneScript);
            SetEventScript(module, EventScriptType.ModuleOnHeartbeat, EventScript.OnModuleHeartbeatScript);
            SetEventScript(module, EventScriptType.ModuleOnModuleLoad, EventScript.OnModuleLoadScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerChat, EventScript.OnModulePlayerChatScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerDying, EventScript.OnModulePlayerDyingScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerDeath, EventScript.OnModulePlayerDeathScript);
            SetEventScript(module, EventScriptType.ModuleOnEquipItem, EventScript.OnModuleEquipItemScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerLevelUp, EventScript.OnModulePlayerLevelUpScript);
            SetEventScript(module, EventScriptType.ModuleOnRespawnButtonPressed, EventScript.OnModuleRespawnScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerRest, EventScript.OnModulePlayerRestScript);
            SetEventScript(module, EventScriptType.ModuleOnUnequipItem, EventScript.OnModuleUnequipItemScript);
            SetEventScript(module, EventScriptType.ModuleOnLoseItem, EventScript.OnModuleUnacquireItemScript);
            SetEventScript(module, EventScriptType.ModuleOnUserDefinedEvent, EventScript.OnModuleUserDefinedEventScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerTarget, EventScript.OnModulePlayerTargetScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerGuiEvent, EventScript.OnModulePlayerGuiScript);
            SetEventScript(module, EventScriptType.ModuleOnPlayerTileAction, EventScript.OnModulePlayerTileScript);
            SetEventScript(module, EventScriptType.ModuleOnNuiEvent, EventScript.OnModuleNuiEventScript);
        }


        [ScriptHandler(EventScript.OnModuleAcquireItemScript)]
        public void HandleOnModuleAcquireItem()
        {
            foreach (var handler in OnModuleAcquireItemSubscriptions)
            {
                try
                {
                    handler.OnModuleAcquireItem();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModuleActivateItemScript)]
        public void HandleOnModuleActivateItem()
        {
            foreach (var handler in OnModuleActivateItemSubscriptions)
            {
                try
                {
                    handler.OnModuleActivateItem();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModuleOnClientEnterScript)]
        public void HandleOnModuleOnClientEnter()
        {
            foreach (var handler in OnModuleOnClientEnterSubscriptions)
            {
                try
                {
                    handler.OnModuleOnClientEnter();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModuleOnClientExitScript)]
        public void HandleOnModuleOnClientExit()
        {
            foreach (var handler in OnModuleOnClientExitSubscriptions)
            {
                try
                {
                    handler.OnModuleOnClientExit();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModulePlayerCancelCutsceneScript)]
        public void HandleOnModulePlayerCancelCutscene()
        {
            foreach (var handler in OnModulePlayerCancelCutsceneSubscriptions)
            {
                try
                {
                    handler.OnModulePlayerCancelCutscene();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModuleHeartbeatScript)]
        public void HandleOnModuleHeartbeat()
        {
            foreach (var handler in OnModuleHeartbeatSubscriptions)
            {
                try
                {
                    handler.OnModuleHeartbeat();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModuleLoadScript)]
        public void HandleOnModuleLoad()
        {
            foreach (var handler in OnModuleLoadSubscriptions)
            {
                try
                {
                    handler.OnModuleLoad();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModulePlayerChatScript)]
        public void HandleOnModulePlayerChat()
        {
            foreach (var handler in OnModulePlayerChatSubscriptions)
            {
                try
                {
                    handler.OnModulePlayerChat();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModulePlayerDyingScript)]
        public void HandleOnModulePlayerDying()
        {
            foreach (var handler in OnModulePlayerDyingSubscriptions)
            {
                try
                {
                    handler.OnModulePlayerDying();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModulePlayerDeathScript)]
        public void HandleOnModulePlayerDeath()
        {
            foreach (var handler in OnModulePlayerDeathSubscriptions)
            {
                try
                {
                    handler.OnModulePlayerDeath();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModuleEquipItemScript)]
        public void HandleOnModuleEquipItem()
        {
            foreach (var handler in OnModuleEquipItemSubscriptions)
            {
                try
                {
                    handler.OnModuleEquipItem();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModulePlayerLevelUpScript)]
        public void HandleOnModulePlayerLevelUp()
        {
            foreach (var handler in OnModulePlayerLevelUpSubscriptions)
            {
                try
                {
                    handler.OnModulePlayerLevelUp();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModuleRespawnScript)]
        public void HandleOnModuleRespawn()
        {
            foreach (var handler in OnModuleRespawnSubscriptions)
            {
                try
                {
                    handler.OnModuleRespawn();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModulePlayerRestScript)]
        public void HandleOnModulePlayerRest()
        {
            foreach (var handler in OnModulePlayerRestSubscriptions)
            {
                try
                {
                    handler.OnModulePlayerRest();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModuleUnequipItemScript)]
        public void HandleOnModuleUnequipItem()
        {
            foreach (var handler in OnModuleUnequipItemSubscriptions)
            {
                try
                {
                    handler.OnModuleUnequipItem();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModuleUnacquireItemScript)]
        public void HandleOnModuleUnacquireItem()
        {
            foreach (var handler in OnModuleUnacquireItemSubscriptions)
            {
                try
                {
                    handler.OnModuleUnacquireItem();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModuleUserDefinedEventScript)]
        public void HandleOnModuleUserDefinedEvent()
        {
            foreach (var handler in OnModuleUserDefinedEventSubscriptions)
            {
                try
                {
                    handler.OnModuleUserDefinedEvent();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModulePlayerTargetScript)]
        public void HandleOnModulePlayerTarget()
        {
            foreach (var handler in OnModulePlayerTargetSubscriptions)
            {
                try
                {
                    handler.OnModulePlayerTarget();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModulePlayerGuiScript)]
        public void HandleOnModulePlayerGui()
        {
            foreach (var handler in OnModulePlayerGuiSubscriptions)
            {
                try
                {
                    handler.OnModulePlayerGui();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModulePlayerTileScript)]
        public void HandleOnModulePlayerTile()
        {
            foreach (var handler in OnModulePlayerTileSubscriptions)
            {
                try
                {
                    handler.OnModulePlayerTile();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnModuleNuiEventScript)]
        public void HandleOnModuleNuiEvent()
        {
            foreach (var handler in OnModuleNuiEventSubscriptions)
            {
                try
                {
                    handler.OnModuleNuiEvent();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }
    }
}
