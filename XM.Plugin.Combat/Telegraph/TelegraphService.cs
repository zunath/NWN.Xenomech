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
        private const string TelegraphsVariable = "TELEGRAPHS";

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

            var telegraphData = JsonObject();
            telegraphData = JsonObjectSet(telegraphData, "start", JsonInt(start));
            telegraphData = JsonObjectSet(telegraphData, "end", JsonInt(end));
            telegraphData = JsonObjectSet(telegraphData, "packed", JsonString(packed));

            var telegraphIds = GetLocalJson(area, TelegraphsVariable);
            telegraphIds = JsonGetType(telegraphIds) == JsonType.Null ? JsonArray() : telegraphIds;
            telegraphIds = JsonArrayInsert(telegraphIds, JsonString(telegraphId));

            SetLocalString(area, telegraphId, data.OnUpdateScript);
            SetLocalJson(area, telegraphId, telegraphData);
            SetLocalJson(area, TelegraphsVariable, telegraphIds);
        }

        private void OnRemoved(uint telegrapher, Effect effect)
        {
            var area = GetArea(telegrapher);
            var telegraphId = GetEffectLinkId(effect);
            var telegraphIds = GetLocalJson(area, TelegraphsVariable);
            telegraphIds = JsonArrayDel(telegraphIds, JsonGetInt(JsonFind(telegraphIds, JsonString(telegraphId))));

            DeleteLocalString(area, telegraphId);
            DeleteLocalJson(area, telegraphId);
            SetLocalJson(area, TelegraphsVariable, telegraphIds);
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
            var telegraphs = GetLocalJson(area, TelegraphsVariable);
            var telegraphCount = JsonGetLength(telegraphs);
            var telegraphCountToRender = telegraphCount > 8 ? 8 : telegraphCount;
            var telegraphCountToReset = 8 - telegraphCountToRender;

            for (var i = 0; i < telegraphCountToRender; ++i)
            {
                var telegraphId = JsonGetString(JsonArrayGet(telegraphs, i));
                var telegraphJson = GetLocalJson(area, telegraphId);

                var start = JsonGetInt(JsonObjectGet(telegraphJson, "start"));
                var end = JsonGetInt(JsonObjectGet(telegraphJson, "end"));
                var packed = JsonGetString(JsonObjectGet(telegraphJson, "packed"));
                var unpacked = XMJsonUtility.Deserialize<TelegraphData>(packed);

                SetShaderUniformInt(player, ShaderUniformType.Uniform1 + i, (int)unpacked.Shape);
                SetShaderUniformVec(player, ShaderUniformType.Uniform1 + (i * 2) + 0, unpacked.X, unpacked.Y, unpacked.Z, unpacked.Rotation);
                SetShaderUniformVec(player, ShaderUniformType.Uniform1 + (i * 2) + 1, start, end, unpacked.SizeX, unpacked.SizeY);
            }

            for (var i = 0; i < telegraphCountToReset; ++i)
            {
                var uniformIndex = ShaderUniformType.Uniform1 + telegraphCountToRender + i;
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
                SizeX = 10f,
                SizeY = 10f,
                Duration = 2.5f,

            });
        }
    }
}
