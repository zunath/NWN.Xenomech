using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Anvil.API;
using Anvil.Services;
using Discord;
using Discord.Webhook;
using XM.Shared.Core;
using XM.Shared.Core.Configuration;
using XM.Shared.Core.Localization;
using XM.UI;
using Action = System.Action;
using Color = Discord.Color;

namespace XM.Plugin.Administration.BugReport
{
    internal class BugReportViewModel: ViewModel<BugReportViewModel>
    {
        public const int MaxBugReportLength = 1000;

        [Inject]
        private XMSettingsService Settings { get; set; }

        public string BugReportText
        {
            get => Get<string>();
            set => Set(value);
        }

        public Action OnClickSubmit() => () =>
        {
            if (string.IsNullOrWhiteSpace(BugReportText))
            {
                return;
            }

            var message = BugReportText;

            if (message.Length > 1000)
            {
                SendMessageToPC(Player, LocaleString.YourBugReportMessageWasTooLong.ToLocalizedString(message));
                return;
            }
            var area = GetArea(Player);
            var position = GetPosition(Player);

            var url = Settings.BugWebHookUrl;

            if (string.IsNullOrWhiteSpace(url))
            {
                SendMessageToPC(Player, ColorToken.Red(LocaleString.BugReportMisconfigured.ToLocalizedString()));
                return;
            }

            var authorName = $"{GetName(Player)} ({GetPCPlayerName(Player)}) [{GetPCPublicCDKey(Player)}]";
            var areaName = GetName(area);
            var areaTag = GetTag(area);
            var areaResref = GetResRef(area);
            var positionGroup = $"({position.X}, {position.Y}, {position.X})";
            var dateReported = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss");
            var playerId = GetObjectUUID(Player);
            var nextReportAllowed = DateTime.UtcNow.AddMinutes(1);
            var title = Settings.ServerEnvironment == ServerEnvironmentType.Test
                ? LocaleString.BugReportTestServer.ToLocalizedString()
                : LocaleString.BugReport.ToLocalizedString();

            var nameTitle = LocaleString.AreaName.ToLocalizedString();
            var tagTitle = LocaleString.AreaTag.ToLocalizedString();
            var resrefTitle = LocaleString.AreaResref.ToLocalizedString();
            var positionTitle = LocaleString.Position.ToLocalizedString();
            var dateReportedTitle = LocaleString.DateReported.ToLocalizedString();
            var playerIdTitle = LocaleString.PlayerId.ToLocalizedString();

            Task.Run(async () =>
            {
                using (var client = new DiscordWebhookClient(url))
                {
                    var embed = new EmbedBuilder
                    {
                        Title = title,
                        Description = message,
                        Author = new EmbedAuthorBuilder
                        {
                            Name = authorName
                        },
                        Color = Color.Red,
                        Fields = new List<EmbedFieldBuilder>
                        {
                            new()
                            {
                                IsInline = true,
                                Name = nameTitle,
                                Value = areaName
                            },
                            new()
                            {
                                IsInline = true,
                                Name = tagTitle,
                                Value = areaTag
                            },
                            new()
                            {
                                IsInline = true,
                                Name = resrefTitle,
                                Value = areaResref
                            },
                            new()
                            {
                                IsInline = true,
                                Name = positionTitle,
                                Value = positionGroup
                            },
                            new()
                            {
                                IsInline = true,
                                Name = dateReportedTitle,
                                Value = dateReported,
                            },
                            new()
                            {
                                IsInline = true,
                                Name = playerIdTitle,
                                Value = playerId
                            },
                        }
                    };


                    await client.SendMessageAsync(
                        string.Empty,
                        embeds: new[] { embed.Build() },
                        threadName: title);
                }
            });

            SetLocalString(Player, "BUG_REPORT_LAST_SUBMISSION", nextReportAllowed.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            SendMessageToPC(Player, LocaleString.BugReportSubmittedThankYouForYouReport.ToLocalizedString());
            SendMessageToPC(Player, LocaleString.SubmittedBugReportX.ToLocalizedString(BugReportText));
            
            CloseWindow();
        };

        public Action OnClickCancel() => () =>
        {
            CloseWindow();
        };
        public override void OnOpen()
        {
            BugReportText = string.Empty;
            WatchOnClient(model => model.BugReportText);
        }

        public override void OnClose()
        {
            
        }
    }
}
