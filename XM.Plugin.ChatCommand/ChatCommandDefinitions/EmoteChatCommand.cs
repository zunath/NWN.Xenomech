using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Authorization;

namespace XM.ChatCommand.ChatCommandDefinitions
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    public class EmoteChatCommand: IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            var builder = new ChatCommandBuilder();

            builder.Create("bored")
                .Description("Plays a bored animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetPauseBored)
                .IsEmote();
            builder.Create("bow")
                .Description("Plays a bow animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetBow)
                .IsEmote(); 
            builder.Create("deadback")
                .Description("Plays a dead back animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingDeadBack)
                .IsEmote();
            builder.Create("deadfront")
                .Description("Plays a dead front animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingDeadFront)
                .IsEmote();
            builder.Create("drink")
                .Description("Plays a drinking animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.FireForgetDrink)
                .IsEmote();
            builder.Create("drunk")
                .Description("Plays a drunk animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingPauseDrunk)
                .IsEmote();
            builder.Create("duck")
                .Description("Plays a duck animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetDodgeDuck)
                .IsEmote();
            builder.Create("greet")
                .Description("Plays a greet animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetGreeting)
                .IsEmote();
            builder.Create("interact")
                .Description("Plays an interact animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingGetMid)
                .IsEmote();
            builder.Create("meditate")
                .Description("Plays a meditate animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingMeditate)
                .IsEmote();
            builder.Create("laughing")
                .Description("Plays a laughing animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingTalkLaughing)
                .IsEmote();
            builder.Create("listen")
                .Description("Plays a listen animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingListen)
                .IsEmote();
            builder.Create("look")
                .Description("Plays a look far animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingLookFar)
                .IsEmote();
            builder.Create("pickup")
                .Description("Plays a pickup animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingGetLow)
                .IsEmote();
            builder.Create("read")
                .Description("Plays a read animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetRead)
                .IsEmote();
            builder.Create("salute")
                .Description("Plays a salute animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetSalute)
                .IsEmote();
            builder.Create("scratchhead")
                .Description("Plays a scratch head animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetPauseScratchHead)
                .IsEmote();
            builder.Create("sidestep")
                .Description("Plays a side-step animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetDodgeSide)
                .IsEmote();
            builder.Create("sit")
                .Description("Makes your character sit down.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingSitCross)
                .IsEmote();
            builder.Create("spasm")
                .Description("Plays a spasm animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingSpasm)
                .IsEmote();
            builder.Create("taunt")
                .Description("Plays a taunt animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetTaunt)
                .IsEmote();
            builder.Create("tired")
                .Description("Plays a tired animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingPauseTired)
                .IsEmote();
            builder.Create("victory1")
                .Description("Plays a victory 1 animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetVictory1)
                .IsEmote();
            builder.Create("victory2")
                .Description("Plays a victory 2 animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetVictory2)
                .IsEmote();
            builder.Create("victory3")
                .Description("Plays a victory 3 animation.")
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetVictory3)
                .IsEmote();

            return builder.Build();
        }
    }
}
