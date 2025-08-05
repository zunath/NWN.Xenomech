using Anvil.API;
using Anvil.Services;
using XM.Chat.UI.Conversation.ViewModels;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;

namespace XM.Chat.UI.Conversation.Views
{
    /// <summary>
    /// View for the conversation UI that displays NPC dialogue and player response options.
    /// </summary>
    [ServiceBinding(typeof(IView))]
    internal class ConversationView : IView
    {
        private readonly NuiBuilder<ConversationViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new ConversationViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window.InitialGeometry(0, 0, 400, 300)
                    .Title(LocaleString.Conversation)
                    .IsClosable(true)
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .IsTransparent(false)
                    .Border(true)
                    .AcceptsInput(true)
                    .Root(BuildWindow);
            }).Build();
        }

        private void BuildWindow(NuiColumnBuilder<ConversationViewModel> col)
        {
            col.AddRow(BuildHeader);
            col.AddRow(BuildMainContent);
            col.AddRow(BuildResponseButtons);
        }

        /// <summary>
        /// Builds the header section with NPC portrait and name.
        /// </summary>
        private void BuildHeader(NuiRowBuilder<ConversationViewModel> row)
        {
            // NPC Portrait
            row.AddColumn(col =>
            {
                col.AddRow(portraitRow =>
                {
                    portraitRow.AddImage(image =>
                    {
                        image.ResRef(model => model.NpcPortrait)
                            .Width(80f)
                            .Height(80f)
                            .Margin(8f)
                            .HorizontalAlign(NuiHAlign.Center)
                            .VerticalAlign(NuiVAlign.Middle);
                    });
                });
            });

            // NPC Name
            row.AddColumn(col =>
            {
                col.AddRow(nameRow =>
                {
                    nameRow.AddLabel(label =>
                    {
                        label.Label(model => model.NpcName)
                            .Height(30f)
                            .HorizontalAlign(NuiHAlign.Left)
                            .VerticalAlign(NuiVAlign.Middle);
                    });
                });
            });

            // Close button
            row.AddColumn(col =>
            {
                col.AddRow(closeRow =>
                {
                    closeRow.AddButton(button =>
                    {
                        button.Label(LocaleString.X)
                            .Width(30f)
                            .Height(30f)
                            .Margin(4f)
                            .OnClick(model => model.CloseConversation());
                    });
                });
            });
        }

        /// <summary>
        /// Builds the main content area with NPC dialogue text.
        /// </summary>
        private void BuildMainContent(NuiRowBuilder<ConversationViewModel> row)
        {
            row.AddColumn(col =>
            {
                col.AddRow(textRow =>
                {
                    textRow.AddText(text =>
                    {
                        text.Text(model => model.ConversationHeader)
                            .Height(120f)
                            .Margin(8f);
                    });
                });
            });
        }

        /// <summary>
        /// Builds the response buttons section.
        /// </summary>
        private void BuildResponseButtons(NuiRowBuilder<ConversationViewModel> row)
        {
            row.AddColumn(col =>
            {
                col.AddRow(responseRow =>
                {
                    responseRow.AddLabel(label =>
                    {
                        label.Label(LocaleString.Empty)
                            .Height(100f)
                            .Margin(8f);
                    });
                });
            });
        }
    }
} 