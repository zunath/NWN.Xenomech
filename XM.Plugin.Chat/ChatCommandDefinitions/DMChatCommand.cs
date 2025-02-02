using System;
using System.Collections.Generic;
using Anvil.Services;
using NWN.Core;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.AdminPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Authorization;

namespace XM.Chat.ChatCommandDefinitions
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    public class DMChatCommand : IChatCommandListDefinition
    {
        private readonly ChatCommandBuilder _builder = new ChatCommandBuilder();

        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
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
            _builder.Create("copyitem")
                .Description("Copies the targeted item.")
                .RequiresTarget()
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Action((user, target, location, args) =>
                {
                    if (GetObjectType(target) != ObjectType.Item)
                    {
                        SendMessageToPC(user, "You can only copy items with this command.");
                        return;
                    }

                    CopyItem(target, user, true);
                    SendMessageToPC(user, "Item copied successfully.");
                });
        }

        private void Day()
        {
            _builder.Create("day")
                .Description("Sets the world time to 8 AM.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Action((user, target, location, args) =>
                {
                    SetTime(8, 0, 0, 0);
                });
        }

        private void Night()
        {
            _builder.Create("night")
                .Description("Sets the world time to 8 PM.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Action((user, target, location, args) =>
                {
                    SetTime(20, 0, 0, 0);
                });
        }

        private void GetPlot()
        {
            _builder.Create("getplot")
                .Description("Gets whether an object is marked plot.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Action((user, target, location, args) =>
                {
                    SendMessageToPC(user, GetPlotFlag(target) ? "Target is marked plot." : "Target is NOT marked plot.");
                })
                .RequiresTarget();
        }

        private void Kill()
        {
            _builder.Create("kill")
                .Description("Kills your target.")
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
            _builder.Create("rez")
                .Description("Revives you, heals you to full, and restores all EP.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget(ObjectType.Creature)
                .Action((user, target, location, args) =>
                {
                    if (GetIsDead(target))
                    {
                        ApplyEffectToObject(DurationType.Instant, EffectResurrection(), target);
                    }

                    ApplyEffectToObject(DurationType.Instant, EffectHeal(999), target);
                });
        }

        private void SpawnGold()
        {
            _builder.Create("spawngold")
                .Description("Spawns gold of a specific quantity on your character. Example: /spawngold 33")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return ColorToken.Red("Please specify a quantity. Example: /spawngold 34");
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
            _builder.Create("tpwp")
                .Description("Teleports you to a waypoint with a specified tag.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return "You must specify a waypoint tag. Example: /tpwp MY_WAYPOINT_TAG";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    var tag = args[0];
                    var wp = GetWaypointByTag(tag);

                    if (!GetIsObjectValid(wp))
                    {
                        SendMessageToPC(user, "Invalid waypoint tag. Did you enter the right tag?");
                        return;
                    }

                    AssignCommand(user, () => ActionJumpToLocation(GetLocation(wp)));
                });
        }

        private void GetLocalVariable()
        {
            _builder.Create("getlocalfloat")
                .Description("Gets a local float on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return "Missing arguments. Format should be: /GetLocalFloat Variable_Name. Example: /GetLocalFloat MY_VARIABLE";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
                        target = GetArea(user);
                    }

                    var variableName = Convert.ToString(args[0]);
                    var value = GetLocalFloat(target, variableName);

                    SendMessageToPC(user, variableName + " = " + value);
                });

            _builder.Create("getlocalint")
                .Description("Gets a local integer on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return "Missing arguments. Format should be: /GetLocalInt Variable_Name. Example: /GetLocalInt MY_VARIABLE";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
                        target = GetArea(user);
                    }

                    var variableName = Convert.ToString(args[0]);
                    var value = GetLocalInt(target, variableName);

                    SendMessageToPC(user, variableName + " = " + value);
                });

            _builder.Create("getlocalstring")
                .Description("Gets a local string on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return "Missing arguments. Format should be: /GetLocalString Variable_Name. Example: /GetLocalString MY_VARIABLE";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
                        target = GetArea(user);
                    }

                    var variableName = Convert.ToString(args[0]);
                    var value = GetLocalString(target, variableName);

                    SendMessageToPC(user, variableName + " = " + value);
                });
        }

        private void SetLocalVariable()
        {
            _builder.Create("setlocalfloat")
                .Description("Sets a local float on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 2)
                    {
                        return "Missing arguments. Format should be: /SetLocalFloat Variable_Name <VALUE>. Example: /SetLocalFloat MY_VARIABLE 6.9";
                    }

                    if (!float.TryParse(args[1], out var value))
                    {
                        return "Invalid value entered. Please try again.";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
                        target = GetArea(user);
                    }

                    var variableName = args[0];
                    var value = float.Parse(args[1]);

                    SetLocalFloat(target, variableName, value);

                    SendMessageToPC(user, "Local float set: " + variableName + " = " + value);
                });


            _builder.Create("setlocalint")
                .Description("Sets a local int on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 2)
                    {
                        return "Missing arguments. Format should be: /SetLocalInt Variable_Name <VALUE>. Example: /SetLocalInt MY_VARIABLE 69";
                    }

                    if (!int.TryParse(args[1], out var value))
                    {
                        return "Invalid value entered. Please try again.";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
                        target = GetArea(user);
                    }

                    var variableName = args[0];
                    var value = Convert.ToInt32(args[1]);

                    SetLocalInt(target, variableName, value);

                    SendMessageToPC(user, "Local integer set: " + variableName + " = " + value);
                });

            _builder.Create("setlocalstring")
                .Description("Sets a local string on a target.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return "Missing arguments. Format should be: /SetLocalString Variable_Name <VALUE>. Example: /SetLocalString MY_VARIABLE My Text";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "Target is invalid. Targeting area instead.");
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

                    SendMessageToPC(user, "Local string set: " + variableName + " = " + value);
                });

            _builder.Create("tptag")
                .Description("Sets a local tag on a target Teleport Object placeable.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return "Missing arguments. Format should be: /TPTag <VALUE>. Example: /TPTag DUNGEON_ENTRANCE";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (GetResRef(target) != "tele_obj")
                    {
                        SendMessageToPC(user, "This command can only be used on the Teleport Object placeable.");
                    }
                    else
                    {
                        SetTag(target, args[0]);

                        SendMessageToPC(user, "Tag set to: " + args[0] + ".");
                    }
                });

            _builder.Create("tpdest")
                .Description("Changes the destination of a selected Teleport Object placeable to point toward a given waypoint or placeable tag.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return "Missing arguments. Format should be: /destination <VALUE>. Example: /destination EventEntrance";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (GetResRef(target) != "tele_obj")
                    {
                        SendMessageToPC(user, "Target is invalid. Please target a Teleport Object placeable.");
                    }
                    else
                    {
                        SetLocalString(target, "DESTINATION", args[0]);
                        SendMessageToPC(user, "Destination tag set to " + args[0] + ".");
                    }
                });
        }

        private void SetPortrait()
        {
            _builder.Create("setportrait")
                .Description("Sets portrait of the target player using the string specified. (Remember to add po_ to the portrait)")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return "Please enter the name of the portrait and try again. Example: /SetPortrait po_myportrait";
                    }

                    if (args[0].Length > 16)
                    {
                        return "The portrait you entered is too long. Portrait names should be between 1 and 16 characters.";
                    }

                    return string.Empty;
                })
                .Action((user, target, location, args) =>
                {
                    if (!GetIsObjectValid(target) || GetObjectType(target) != ObjectType.Creature)
                    {
                        SendMessageToPC(user, "Only creatures may be targeted with this command.");
                        return;
                    }

                    SetPortraitResRef(target, args[0]);
                    FloatingTextStringOnCreature("Your portrait has been changed.", target, false);
                });
        }

        private void SpawnItem()
        {
            _builder.Create("spawnitem")
                .Description("Spawns an item of a specific quantity on your character. Example: /spawnitem my_item 3")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return ColorToken.Red("Please specify a resref and optionally a quantity. Example: /spawnitem my_resref 20");
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
                        SendMessageToPC(user, ColorToken.Red("Item not found! Did you enter the correct ResRef?"));
                        return;
                    }

                    SetIdentified(item, true);
                });
        }

        private void PlayVFX()
        {
            _builder.Create("playvfx")
                .Description("Plays a visual effect from visualeffects.2da.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    if (args.Length < 1)
                    {
                        return "Enter the ID from visauleffects.2da. Example: /playvfx 123";
                    }

                    if (!int.TryParse(args[0], out var vfxId))
                    {
                        return "Enter the ID from visauleffects.2da. Example: /playvfx 123";
                    }

                    try
                    {
                        var unused = (VisualEffectType)vfxId;
                    }
                    catch
                    {
                        return "Enter the ID from visauleffects.2da. Example: /playvfx 123";
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
            _builder.Create("restartserver")
                .Description("Restarts the server. Requires CD Key to be entered. Example: /restartserver XXXXYYYY")
                .Permissions(AuthorizationLevel.Admin, AuthorizationLevel.DM)
                .Validate((user, args) =>
                {
                    if (args.Length <= 0)
                    {
                        return "Requires CD Key to be entered. Example: /restartserver XXXXYYYY";
                    }
                    else if (String.IsNullOrWhiteSpace(args[0]))
                    {
                        return "Please enter your public CD Key to confirm the server reset. Use /cdkey to retrieve this. E.G: /restartserver XXXXYYYY";
                    }
                    else if (GetPCPublicCDKey(user) != args[0])
                    {
                        return $"Invalid public CD Key. {args[0]} does not match your CDKey:{GetPCPublicCDKey(user)}. Try again. E.G: /restartserver XXXXYYYY";
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
                        BootPC(player, "The server is restarting.");
                        player = GetNextPC();
                    }
                    AdminPlugin.ShutdownServer();
                });
        }

        private void GetTag()
        {
            _builder.Create("gettag")
                .Description("Gets a target's tag.")
                .Permissions(AuthorizationLevel.Admin, AuthorizationLevel.DM)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Action((user, target, location, args) =>
                {
                    var tag = NWScript.GetTag(target);

                    SendMessageToPC(user, $"Target's tag: {tag}");
                });
        }

        private void SetScale()
        {
            const int MaxAmount = 50;

            _builder.Create("setscale")
                .Description("Sets an object's scale.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Validate((user, args) =>
                {
                    // Missing an amount argument?
                    if (args.Length <= 0)
                    {
                        return "Please specify the object's scale you want to set to. Valid range: 0.1-" + MaxAmount;
                    }

                    // Can't parse the amount?
                    if (!float.TryParse(args[0], out var value))
                    {
                        return "Please specify a value between 0.1 and " + MaxAmount + ".";
                    }

                    // Amount is outside of our allowed range?
                    if (value < 0.1f || value > MaxAmount)
                    {
                        return "Please specify a value between 0.1 and " + MaxAmount + ".";
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

                    SendMessageToPC(user, $"{targetName} scaled to {shownValue}.");
                });
        }

        private void GetScale()
        {
            _builder.Create("getscale")
                .Description("Gets an object's scale.")
                .Permissions(AuthorizationLevel.DM, AuthorizationLevel.Admin)
                .AvailableToAllOnTestEnvironment()
                .RequiresTarget()
                .Action((user, target, location, args) =>
                {
                    var targetScale = GetObjectVisualTransform(target, ObjectVisualTransformType.Scale);
                    var targetName = GetName(target);
                    var shownScale = targetScale.ToString("0.###");

                    SendMessageToPC(user, $"{targetName} has a scale of {shownScale}.");
                });
        }
    }
}
