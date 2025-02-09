using System;
using System.Collections.Generic;
using System.Numerics;
using Anvil.API;
using Anvil.Services;
using XM.Plugin.Combat.Event;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;
using CreatureType = XM.Shared.API.Constants.CreatureType;

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

        public void CreateTelegraphSphere(
            uint creator, 
            Vector3 position, 
            float rotation, 
            Vector2 size, 
            float duration,
            ApplyTelegraphEffect action)
        {
            var data = new TelegraphData
            {
                Creator = creator,
                Shape = TelegraphType.Sphere,
                Position = position,
                Rotation = rotation,
                Size = size,
                Duration = duration,
                Action = action
            };
            RunTelegraphEffect(creator, data);
        }
        public void CreateTelegraphCone(
            uint creator, 
            Vector3 position, 
            float rotation, 
            Vector2 size, 
            float duration,
            ApplyTelegraphEffect action)
        {
            var data = new TelegraphData
            {
                Creator = creator,
                Shape = TelegraphType.Cone,
                Position = position,
                Rotation = rotation,
                Size = size,
                Duration = duration,
                Action = action
            };
            RunTelegraphEffect(creator, data);
        }
        public void CreateTelegraphLine(
            uint creator,
            Vector3 position,
            float rotation,
            Vector2 size,
            float duration,
            ApplyTelegraphEffect action)
        {
            var data = new TelegraphData
            {
                Creator = creator,
                Shape = TelegraphType.Line,
                Position = position,
                Rotation = rotation,
                Size = size,
                Duration = duration,
                Action = action
            };
            RunTelegraphEffect(creator, data);
        }

        private void RunTelegraphEffect(uint telegrapher, TelegraphData data)
        {
            var area = GetArea(telegrapher);
            if (!_telegraphsByArea.ContainsKey(area))
                _telegraphsByArea[area] = new Dictionary<string, ActiveTelegraph>();

            var effect = EffectRunScript(
                CombatEventScript.TelegraphEffectScript,
                CombatEventScript.TelegraphEffectScript,
                string.Empty);
            OnApply(telegrapher, data, effect);
            ApplyEffectToObject(DurationType.Temporary, effect, telegrapher, data.Duration);
        }

        private void OnRunTelegraphEffect(uint telegrapher)
        {
            var effect = GetLastRunScriptEffect();
            var @event = GetLastRunScriptEffectScriptType();
            
            if (@event == RunScriptEffectScriptType.OnApplied)
            {
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

        private void OnApply(uint telegrapher, TelegraphData data, Effect effect)
        {
            var area = GetArea(telegrapher);
            var telegraphId = GetEffectLinkId(effect);

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

            if (!_telegraphsByArea[area].ContainsKey(telegraphId))
                return;

            RunTelegraphAction(area, _telegraphsByArea[area][telegraphId].Data);

            _telegraphsByArea[area].Remove(telegraphId);
        }

        private void RunTelegraphAction(uint area, TelegraphData data)
        {
            var action = data.Action;
            if (action != null)
            {
                var location = Location(area, data.Position, data.Rotation);
                var maxDistance = CalculateMaxCreatureDistance(data.Shape, data.Size);
                var creatureList = new List<uint>();

                var nth = 1;
                var nearest = GetNearestCreatureToLocation((int)CreatureType.IsAlive, 1, location, nth);
                while (GetIsObjectValid(nearest) &&
                       GetDistanceBetweenLocations(location, GetLocation(nearest)) <= maxDistance)
                {
                    if (IsInTelegraph(nearest, data))
                    {
                        creatureList.Add(nearest);
                    }

                    nth++;
                    nearest = GetNearestCreatureToLocation((int)CreatureType.IsAlive, 1, location, nth);
                }

                action(data.Creator, creatureList);
            }
        }

        private float CalculateMaxCreatureDistance(TelegraphType shape, Vector2 size)
        {
            switch (shape)
            {
                case TelegraphType.None:
                    return 0f;
                case TelegraphType.Sphere:
                    return size.X; // Sphere radius
                case TelegraphType.Cone:
                    return size.X; // Cone length
                case TelegraphType.Line:
                    return size.X; // Line length
                default:
                    throw new ArgumentOutOfRangeException(nameof(shape), shape, null);
            }
        }

        private bool IsInTelegraph(uint creature, TelegraphData data)
        {
            switch (data.Shape)
            {
                case TelegraphType.Sphere:
                    return IsCreatureInSphere(creature, data);
                case TelegraphType.Cone:
                    return IsCreatureInCone(creature, data);
                case TelegraphType.Line:
                    return IsCreatureInLine(creature, data);
                default:
                    return false;
            }
        }

        private static bool IsCreatureInSphere(uint creature, TelegraphData data)
        {
            var position = GetPosition(creature);
            var radius = data.Size.X;
            return Vector3.Distance(position, data.Position) <= radius;
        }

        private static bool IsCreatureInCone(uint creature, TelegraphData data)
        {
            var position = GetPosition(creature);

            var direction = new Vector3(cos(data.Rotation), sin(data.Rotation), 0);
            var toPoint = (position - data.Position);
            var distance = toPoint.Length();

            // Compute the actual cone angle dynamically
            var halfAngle = atan((data.Size.Y * 0.5f) / data.Size.X);

            // Angle between the direction and the point
            var angleBetween = acos(Vector3.Dot(Vector3.Normalize(toPoint), direction));

            return (distance <= data.Size.X) && (angleBetween <= halfAngle);
        }

        private static bool IsCreatureInLine(uint creature, TelegraphData data)
        {
            var position = GetPosition(creature);
            var toPoint = position - data.Position;

            // Compute rotated position relative to the telegraph's orientation
            var rotatedPos = new Vector2(
                toPoint.X * cos(-data.Rotation) - toPoint.Y * sin(-data.Rotation),
                toPoint.X * sin(-data.Rotation) + toPoint.Y * cos(-data.Rotation)
            );

            var distAlongLine = rotatedPos.X;
            var distFromCenter = MathF.Abs(rotatedPos.Y);

            return (distAlongLine >= 0f && distAlongLine <= data.Size.X) // Within length
                   && (distFromCenter <= data.Size.Y * 0.5f); // Within width
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
                var data = telegraph.Data;
                var position = data.Position;
                var size = data.Size;

                SetShaderUniformInt(player, ShaderUniformType.Uniform1 + i, (int)telegraph.Data.Shape);
                SetShaderUniformVec(player, ShaderUniformType.Uniform1 + (i * 2) + 0, position.X, position.Y, position.Z, telegraph.Data.Rotation);
                SetShaderUniformVec(player, ShaderUniformType.Uniform1 + (i * 2) + 1, telegraph.Start, telegraph.End, size.X, size.Y);

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

            CreateTelegraphCone(
                player, 
                position, 
                rotation, 
                new Vector2(8f, 2f), 
                2.5f,
                ((telegrapher, creatures) =>
                {
                    foreach (var creature in creatures)
                    {
                        ApplyEffectToObject(DurationType.Instant, EffectDamage(1), creature);
                    }
                }));
        }
    }
}
