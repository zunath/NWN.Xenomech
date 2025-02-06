using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.ChatCommand;
using XM.Shared.Core.Localization;

namespace XM.Chat.ChatCommand
{
    [ServiceBinding(typeof(IChatCommandListDefinition))]
    public class EmoteChatCommand : IChatCommandListDefinition
    {
        public Dictionary<LocaleString, ChatCommandDetail> BuildChatCommands()
        {
            var builder = new ChatCommandBuilder();
            builder.Create(LocaleString.bored)
                .Description(LocaleString.PlaysABoredAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetPauseBored)
                .IsEmote();
            builder.Create(LocaleString.bow)
                .Description(LocaleString.PlaysABowAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetBow)
                .IsEmote();
            builder.Create(LocaleString.deadback)
                .Description(LocaleString.PlaysADeadBackAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingDeadBack)
                .IsEmote();
            builder.Create(LocaleString.deadfront)
                .Description(LocaleString.PlaysADeadFrontAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingDeadFront)
                .IsEmote();
            builder.Create(LocaleString.drink)
                .Description(LocaleString.PlaysADrinkingAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.FireForgetDrink)
                .IsEmote();
            builder.Create(LocaleString.drunk)
                .Description(LocaleString.PlaysADrunkAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingPauseDrunk)
                .IsEmote();
            builder.Create(LocaleString.duck)
                .Description(LocaleString.PlaysADuckAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetDodgeDuck)
                .IsEmote();
            builder.Create(LocaleString.greet)
                .Description(LocaleString.PlaysAGreetAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetGreeting)
                .IsEmote();
            builder.Create(LocaleString.interact)
                .Description(LocaleString.PlaysAnInteractAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingGetMid)
                .IsEmote();
            builder.Create(LocaleString.meditate)
                .Description(LocaleString.PlaysAMeditateAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingMeditate)
                .IsEmote();
            builder.Create(LocaleString.laughing)
                .Description(LocaleString.PlaysALaughingAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingTalkLaughing)
                .IsEmote();
            builder.Create(LocaleString.listen)
                .Description(LocaleString.PlaysAListenAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingListen)
                .IsEmote();
            builder.Create(LocaleString.look)
                .Description(LocaleString.PlaysALookFarAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingLookFar)
                .IsEmote();
            builder.Create(LocaleString.pickup)
                .Description(LocaleString.PlaysAPickupAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingGetLow)
                .IsEmote();
            builder.Create(LocaleString.read)
                .Description(LocaleString.PlaysAReadAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetRead)
                .IsEmote();
            builder.Create(LocaleString.salute)
                .Description(LocaleString.PlaysASaluteAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetSalute)
                .IsEmote();
            builder.Create(LocaleString.scratchhead)
                .Description(LocaleString.PlaysAScratchHeadAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetPauseScratchHead)
                .IsEmote();
            builder.Create(LocaleString.sidestep)
                .Description(LocaleString.PlaysASideStepAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetDodgeSide)
                .IsEmote();
            builder.Create(LocaleString.sit)
                .Description(LocaleString.MakesYourCharacterSitDown)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingSitCross)
                .IsEmote();
            builder.Create(LocaleString.spasm)
                .Description(LocaleString.PlaysASpasmAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationLoopingAction(AnimationType.LoopingSpasm)
                .IsEmote();
            builder.Create(LocaleString.taunt)
                .Description(LocaleString.PlaysATauntAnimation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetTaunt)
                .IsEmote();
            builder.Create(LocaleString.victory1)
                .Description(LocaleString.PlaysAVictory1Animation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetVictory1)
                .IsEmote();
            builder.Create(LocaleString.victory2)
                .Description(LocaleString.PlaysAVictory2Animation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetVictory2)
                .IsEmote();
            builder.Create(LocaleString.victory3)
                .Description(LocaleString.PlaysAVictory3Animation)
                .Permissions(AuthorizationLevel.All)
                .AnimationAction(AnimationType.FireForgetVictory3)
                .IsEmote();


            return builder.Build();
        }
    }
}
