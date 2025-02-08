using System.Collections;
using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using XM.Plugin.Combat.Event;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Json;

namespace XM.Plugin.Combat.Telegraph
{
    [ServiceBinding(typeof(TelegraphService))]
    [ServiceBinding(typeof(IInitializable))]
    internal class TelegraphService: IInitializable
    {
        private const float TargetFPS = 30f;
        private const int MaxRenderCount = 8;

        private readonly Dictionary<uint, Dictionary<string, ActiveTelegraph>> _telegraphsByArea = new();

        private readonly XMEventService _event;

        public TelegraphService(
            XMEventService @event)
        {
            _event = @event;

            RegisterEvents();
            SubscribeEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<TelegraphEvent.RunTelegraphEffect>(CombatEventScript.TelegraphEffectScript);
            _event.RegisterEvent<TelegraphEvent.TelegraphApplied>(CombatEventScript.TelegraphApplied);
            _event.RegisterEvent<TelegraphEvent.TelegraphTicked>(CombatEventScript.TelegraphTicked);
            _event.RegisterEvent<TelegraphEvent.TelegraphRemoved>(CombatEventScript.TelegraphRemoved);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<TelegraphEvent.RunTelegraphEffect>(OnRunTelegraphEffect);
        }

        public void CreateTelegraph(uint creator, TelegraphData data)
        {
            var json = XMJsonUtility.Serialize(data);
            var effect = EffectRunScript(
                CombatEventScript.TelegraphEffectScript,
                CombatEventScript.TelegraphEffectScript,
                CombatEventScript.TelegraphEffectScript,
                data.UpdateInterval,
                json
            );
            ApplyEffectToObject(DurationType.Temporary, effect, creator, data.Duration);
        }

        private void OnRunTelegraphEffect(uint telegrapher)
        {
            var effect = GetLastRunScriptEffect();
            var @event = GetLastRunScriptEffectScriptType();
            
            if (@event == RunScriptEffectScriptType.OnApplied)
            {
                OnApply(telegrapher, effect);
                UpdateShadersForAllPlayers();
                _event.PublishEvent<TelegraphEvent.TelegraphApplied>(telegrapher);
            }
            else if (@event == RunScriptEffectScriptType.OnInterval)
            {
                _event.PublishEvent<TelegraphEvent.TelegraphTicked>(telegrapher);
            }
            else if (@event == RunScriptEffectScriptType.OnRemoved)
            {
                OnRemoved(telegrapher, effect);
                UpdateShadersForAllPlayers();
                _event.PublishEvent<TelegraphEvent.TelegraphRemoved>(telegrapher);
            }
        }

        private void OnApply(uint telegrapher, Effect effect)
        {
            var area = GetArea(telegrapher);
            var telegraphId = GetEffectLinkId(effect);
            var packed = GetEffectString(effect, 0);
            var data = XMJsonUtility.Deserialize<TelegraphData>(packed);

            var start = GetMicrosecondCounter();
            var end = (int)(start + data.Duration * 1000 * 1000);
            var telegraph = new ActiveTelegraph(start, end, data);

            if (!_telegraphsByArea.ContainsKey(area))
                _telegraphsByArea[area] = new Dictionary<string, ActiveTelegraph>();

            _telegraphsByArea[area][telegraphId] = telegraph;
        }

        private void OnRemoved(uint telegrapher, Effect effect)
        {
            var area = GetArea(telegrapher);
            var telegraphId = GetEffectLinkId(effect);

            if (!_telegraphsByArea.ContainsKey(area))
                return;

            _telegraphsByArea[area].Remove(telegraphId);
        }

        private void UpdateShadersForAllPlayers()
        {
            for (var player = GetFirstPC(); GetIsObjectValid(player); player = GetNextPC())
            {
                UpdateShaderForPlayer(player);
            }
        }

        private void UpdateShaderForPlayer(uint player)
        {
            var area = GetArea(player);
            if (!_telegraphsByArea.ContainsKey(area))
                return;

            var telegraphs = _telegraphsByArea[area];
            var telegraphCountToRender = telegraphs.Count > MaxRenderCount 
                ? MaxRenderCount 
                : telegraphs.Count;
            var telegraphCountToReset = MaxRenderCount - telegraphCountToRender;

            var i = 0;
            foreach (var (_, telegraph) in telegraphs)
            {
                SetShaderUniformInt(player, ShaderUniformType.Uniform1 + i, (int)telegraph.Data.Shape);
                SetShaderUniformVec(player, ShaderUniformType.Uniform1 + (i * 2) + 0, telegraph.Data.X, telegraph.Data.Y, telegraph.Data.Z, telegraph.Data.Rotation);
                SetShaderUniformVec(player, ShaderUniformType.Uniform1 + (i * 2) + 1, telegraph.Start, telegraph.End, telegraph.Data.SizeX, telegraph.Data.SizeY);

                i++;
            }

            for (var x = 0; x < telegraphCountToReset; ++x)
            {
                var uniformIndex = ShaderUniformType.Uniform1 + telegraphCountToRender + x;
                SetShaderUniformInt(player, uniformIndex, (int)TelegraphType.None);
            }
        }

        public void Init()
        {
            UpdateShaderLerpTimer();
        }

        private void UpdateShaderLerpTimer()
        {
            var counter = GetMicrosecondCounter();

            for (var player = GetFirstPC(); GetIsObjectValid(player); player = GetNextPC())
            {
                SetShaderUniformInt(player, ShaderUniformType.Uniform16, counter);
            }

            DelayCommand(1.0f / TargetFPS, UpdateShaderLerpTimer);
        }

        [ScriptHandler("bread_test6")]
        public void BreadTest6()
        {
            var player = GetLastUsedBy();
            var position = GetPosition(player);
            var rotation = GetFacing(player);

            CreateTelegraph(player, new TelegraphData
            {
                Shape = TelegraphType.Sphere,
                X = position.X,
                Y = position.Y,
                Z = position.Z,
                Rotation = rotation,
                SizeX = 4f,
                SizeY = 4f,
                Duration = 2.5f,

            });
        }
    }
}
