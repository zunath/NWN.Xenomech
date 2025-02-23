using System;
using System.Collections.Generic;
using Anvil.Services;
using NWN.Core;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.AdminPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.ChatCommand;
using XM.Shared.Core.Localization;

namespace XM.Chat.ChatCommand
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    public class DMChatCommand : IChatCommandListDefinition
    {
        private readonly ChatCommandBuilder _builder = new();
        private readonly StatService _stat;
        public DMChatCommand(StatService stat)
        {
            _stat = stat;
        }

        public Dictionary<LocaleString, ChatCommandDetail> BuildChatCommands()
        {
            CopyTargetItem();
            Day();
            Night();
            GetPlot();
            Kill();
            Resurrect();
            SpawnGold();
            TeleportWaypoint();
            GetLocalVariable();
            SetLocalVariable();
            SetPortrait();
            SpawnItem();
            PlayVFX();
            RestartServer();
            GetTag();
            SetScale();
            GetScale();

            return _builder.Build();
        }

        private void CopyTargetItem()
        {
            _builder.Create(LocaleString.copyitem)
                .Description(LocaleString.CopiesTheTargetedItem)
                .RequiresTarget()
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Action((user, target, location, args) =>
                {
                    if (GetObjectType(target) != ObjectType.Item)
                    {
                        SendMessageToPC(user, LocaleString.YouCanOnlyCopyItemsWithThisCommand.ToLocalizedString());
                        return;
                    }

                    CopyItem(target, user, true);
                    SendMessageToPC(user, "Item copied successfully.");
                });
        }

        private void Day()
        {
            _builder.Create(LocaleString.day)
                .Description(LocaleString.SetsTheWorldTimeTo8AM)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Action((user, target, location, args) =>
                {
                    SetTime(8, 0, 0, 0);
                });
        }

        private void Night()
        {
            _builder.Create(LocaleString.night)
                .Description(LocaleString.SetsTheWorldTimeTo8PM)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Action((user, target, location, args) =>
                {
                    SetTime(20, 0, 0, 0);
                });
        }

        private void GetPlot()
        {
            _builder.Create(LocaleString.getplot)
                .Description(LocaleString.GetsWhetherAnObjectIsMarkedPlot)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Action((user, target, location, args) =>
                {
                    var message = GetPlotFlag(target) 
                        ? LocaleString.TargetIsMarkedPlot 
                        : LocaleString.TargetIsNOTMarkedPlot;
                    SendMessageToPC(user, message.ToLocalizedString());
                })
                .RequiresTarget();
        }

        private void Kill()
        {
            _builder.Create(LocaleString.kill)
                .Description(LocaleString.KillsYourTarget)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Action((user, target, location, args) =>
                {
                    var amount = GetMaxHitPoints(target) + 11;
                    var damage = EffectDamage(amount);
                    ApplyEffectToObject(DurationType.Instant, damage, target);
                })
                .RequiresTarget();
        }

        private void Resurrect()
        {
            _builder.Create(LocaleString.rez)
                .Description(LocaleString.RevivesYouHealsYouToFullAndRestoresAllEP)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget(ObjectType.Creature)
                .Action((user, target, location, args) =>
                {
                    if (GetIsDead(target))
                    {
                        ApplyEffectToObject(DurationType.Instant, EffectResurrection(), target);
                    }

                    ApplyEffectToObject(DurationType.Instant, EffectHeal(9999), target);
                    _stat.RestoreEP(target, 9999);
                });
        }

        private void SpawnGold()
        {
            _builder.Create(LocaleString.spawngold)
                .Description(LocaleString.SpawnsGoldOfASpecificQuantityOnYourCharacter)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return ColorToken.Red(LocaleString.PleaseSpecifyAQuantityExampleSpawnGold34.ToLocalizedString());
                    }
                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    var quantity = 1;

                    if (args.Length >= 1)
                    {
                        if (!int.TryParse(args[0], out quantity))
                        {
                            return;
                        }
                    }

                    GiveGoldToCreature(user, quantity);
                });
        }

        private void TeleportWaypoint()
        {
            _builder.Create(LocaleString.tpwp)
                .Description(LocaleString.TeleportsYouToAWaypointWithASpecifiedTag)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return LocaleString.YouMustSpecifyAWaypointTag.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    var tag = args[0];
                    var wp = GetWaypointByTag(tag);

                    if (!GetIsObjectValid(wp))
                    {
                        SendMessageToPC(user, LocaleString.InvalidWaypointTag.ToLocalizedString());
                        return;
                    }

                    AssignCommand(user, () => ActionJumpToLocation(GetLocation(wp)));
                });
        }

        private void GetLocalVariable()
        {
            _builder.Create(LocaleString.getlocalfloat)
                .Description(LocaleString.GetsALocalFloatOnATarget)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return LocaleString.InvalidArgumentsGetFloat.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, LocaleString.TargetIsInvalidTargetingAreaInstead.ToLocalizedString());
                        target = GetArea(user);
                    }

                    var variableName = Convert.ToString(args[0]);
                    var value = GetLocalFloat(target, variableName);

                    SendMessageToPC(user, variableName + " = " + value);
                });

            _builder.Create(LocaleString.getlocalint)
                .Description(LocaleString.GetsALocalIntegerOnATarget)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return LocaleString.InvalidArgumentsGetInt.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, LocaleString.TargetIsInvalidTargetingAreaInstead.ToLocalizedString());
                        target = GetArea(user);
                    }

                    var variableName = Convert.ToString(args[0]);
                    var value = GetLocalInt(target, variableName);

                    SendMessageToPC(user, variableName + " = " + value);
                });

            _builder.Create(LocaleString.getlocalstring)
                .Description(LocaleString.GetsALocalStringOnATarget)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return LocaleString.InvalidArgumentsGetString.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, LocaleString.TargetIsInvalidTargetingAreaInstead.ToLocalizedString());
                        target = GetArea(user);
                    }

                    var variableName = Convert.ToString(args[0]);
                    var value = GetLocalString(target, variableName);

                    SendMessageToPC(user, variableName + " = " + value);
                });
        }

        private void SetLocalVariable()
        {
            _builder.Create(LocaleString.setlocalfloat)
                .Description(LocaleString.SetsALocalFloatOnATarget)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 2)
                    {
                        return LocaleString.InvalidArgumentsSetFloat.ToLocalizedString();
                    }

                    if (!float.TryParse(args[1], out var value))
                    {
                        return LocaleString.InvalidValueEnteredPleaseTryAgain.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, LocaleString.TargetIsInvalidTargetingAreaInstead.ToLocalizedString());
                        target = GetArea(user);
                    }

                    var variableName = args[0];
                    var value = float.Parse(args[1]);

                    SetLocalFloat(target, variableName, value);

                    SendMessageToPC(user, LocaleString.LocalFloatSetXEqualsY.ToLocalizedString(variableName, value));
                });


            _builder.Create(LocaleString.setlocalint)
                .Description(LocaleString.SetsALocalIntOnATarget)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 2)
                    {
                        return LocaleString.InvalidArgumentsSetInt.ToLocalizedString();
                    }

                    if (!int.TryParse(args[1], out var value))
                    {
                        return LocaleString.InvalidValueEnteredPleaseTryAgain.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, LocaleString.TargetIsInvalidTargetingAreaInstead.ToLocalizedString());
                        target = GetArea(user);
                    }

                    var variableName = args[0];
                    var value = Convert.ToInt32(args[1]);

                    SetLocalInt(target, variableName, value);

                    SendMessageToPC(user, LocaleString.LocalIntSetXEqualsY.ToLocalizedString(variableName, value));
                });

            _builder.Create(LocaleString.setlocalstring)
                .Description(LocaleString.SetsALocalStringOnATarget)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return LocaleString.InvalidArgumentsSetString.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, LocaleString.TargetIsInvalidTargetingAreaInstead.ToLocalizedString());
                        target = GetArea(user);
                    }

                    var variableName = Convert.ToString(args[0]);
                    var value = string.Empty;

                    for (var x = 1; x < args.Length; x++)
                    {
                        value += " " + args[x];
                    }

                    value = value.Trim();

                    SetLocalString(target, variableName, value);

                    SendMessageToPC(user, LocaleString.LocalStringSetXEqualsY.ToLocalizedString(variableName, value));
                });
        }

        private void SetPortrait()
        {
            _builder.Create(LocaleString.setportrait)
                .Description(LocaleString.SetsPortraitOfTheTargetPlayerUsingTheStringSpecified)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return LocaleString.PleaseEnterTheNameOfThePortrait.ToLocalizedString();
                    }

                    if (args[0].Length > 16)
                    {
                        return LocaleString.ThePortraitYouEnteredIsTooLong.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target) || GetObjectType(target) != ObjectType.Creature)
                    {
                        SendMessageToPC(user, LocaleString.OnlyCreaturesMayBeTargetedWithThisCommand.ToLocalizedString());
                        return;
                    }

                    SetPortraitResRef(target, args[0]);
                    FloatingTextStringOnCreature(LocaleString.YourPortraitHasBeenChanged.ToLocalizedString(), target, false);
                });
        }

        private void SpawnItem()
        {
            _builder.Create(LocaleString.spawnitem)
                .Description(LocaleString.SpawnsAnItemOfASpecificQuantityOnYourCharacter)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return ColorToken.Red(LocaleString.PleaseSpecifyAResrefAndOptionallyAQuantity.ToLocalizedString());
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    var resref = args[0];
                    var quantity = 1;

                    if (args.Length > 1)
                    {
                        if (!int.TryParse(args[1], out quantity))
                        {
                            return;
                        }
                    }

                    var item = CreateItemOnObject(resref, user, quantity);

                    if (!GetIsObjectValid(item))
                    {
                        SendMessageToPC(user, ColorToken.Red(LocaleString.ItemNotFoundDidYouEnterTheCorrectResref.ToLocalizedString()));
                        return;
                    }

                    SetIdentified(item, true);
                });
        }

        private void PlayVFX()
        {
            _builder.Create(LocaleString.playvfx)
                .Description(LocaleString.PlaysAVisualEffectFromVisualEffects2DA)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return LocaleString.EnterTheIDFromVisualEffects2DA.ToLocalizedString();
                    }

                    if (!int.TryParse(args[0], out var vfxId))
                    {
                        return LocaleString.EnterTheIDFromVisualEffects2DA.ToLocalizedString();
                    }

                    try
                    {
                        var unused = (VisualEffectType)vfxId;
                    }
                    catch
                    {
                        return LocaleString.EnterTheIDFromVisualEffects2DA.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    var vfxId = Convert.ToInt32(args[0]);
                    var vfx = (VisualEffectType)vfxId;
                    var effect = EffectVisualEffect(vfx);
                    ApplyEffectToObject(DurationType.Instant, effect, target);
                });
        }

        private void RestartServer()
        {
            _builder.Create(LocaleString.restartserver)
                .Description(LocaleString.RestartsTheServerRequiresCDKeyToBeEntered)
                .Permissions(AuthorizationLevel.Admin, AuthorizationLevel.DM)
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return LocaleString.RequiresCDKeyToBeEntered.ToLocalizedString();
                    }
                    else if (string.IsNullOrWhiteSpace(args[0]))
                    {
                        return LocaleString.PleaseEnterYourPublicCDKey.ToLocalizedString();
                    }
                    else if (GetPCPublicCDKey(user) != args[0])
                    {
                        return LocaleString.InvalidPublicCDKey.ToLocalizedString(args[0], GetPCPublicCDKey(user));
                    }
                    else
                    {
                        return string.Empty;
                    }
                })
                .Action((user, target, location, args) =>
                {
                    uint player = GetFirstPC();
                    while (player != OBJECT_INVALID)
                    {
                        BootPC(player, LocaleString.TheServerIsRestarting.ToLocalizedString());
                        player = GetNextPC();
                    }
                    AdminPlugin.ShutdownServer();
                });
        }

        private void GetTag()
        {
            _builder.Create(LocaleString.gettag)
                .Description(LocaleString.GetsATargetsTag)
                .Permissions(AuthorizationLevel.Admin, AuthorizationLevel.DM)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Action((user, target, location, args) =>
                {
                    var tag = NWScript.GetTag(target);
                    SendMessageToPC(user, LocaleString.TargetsTagX.ToLocalizedString(tag));
                });
        }

        private void SetScale()
        {
            const int MaxAmount = 50;

            _builder.Create(LocaleString.setscale)
                .Description(LocaleString.SetsAnObjectsScale)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    // Missing an amount argument?
                    if (args.Length <= 0)
                    {
                        return LocaleString.PleaseSpecifyTheObjectsScale.ToLocalizedString(MaxAmount);
                    }

                    // Can't parse the amount?
                    if (!float.TryParse(args[0], out var value))
                    {
                        return LocaleString.PleaseSpecifyAValueBetween01AndX.ToLocalizedString(MaxAmount);
                    }

                    // Amount is outside of our allowed range?
                    if (value < 0.1f || value > MaxAmount)
                    {
                        return LocaleString.PleaseSpecifyAValueBetween01AndX.ToLocalizedString(MaxAmount);
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    // Allows the scale value to be a decimal number.
                    var finalValue = float.TryParse(args[0], out var value) ? value : 1f;

                    SetObjectVisualTransform(target, ObjectVisualTransformType.Scale, finalValue);

                    // Lets the DM know what he set the scale to, but round it to the third decimal place.
                    var targetName = GetName(target);
                    var shownValue = finalValue.ToString("0.###");

                    SendMessageToPC(user, LocaleString.XScaledToY.ToLocalizedString(targetName, shownValue));
                });
        }

        private void GetScale()
        {
            _builder.Create(LocaleString.getscale)
                .Description(LocaleString.GetsAnObjectsScale)
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Action((user, target, location, args) =>
                {
                    var targetScale = GetObjectVisualTransform(target, ObjectVisualTransformType.Scale);
                    var targetName = GetName(target);
                    var shownScale = targetScale.ToString("0.###");

                    SendMessageToPC(user, LocaleString.XHasAScaleOfY.ToLocalizedString(targetName, shownScale));
                });
        }
    }
}
