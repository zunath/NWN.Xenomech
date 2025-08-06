using Anvil.API;
using Anvil.Services;
using XM.Chat.UI.Conversation.ViewModels;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;
using NuiAspect = XM.Shared.API.NUI.NuiAspect;

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
                    .Title(model => model.NpcName)
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
            col.AddRow(BuildMainContent);
            col.AddRow(BuildResponseButtons);
        }

        /// <summary>
        /// Builds the main content area with portrait and dialogue in separate columns.
        /// </summary>
        private void BuildMainContent(NuiRowBuilder<ConversationViewModel> row)
        {
            // Portrait Column
            row.AddColumn(col =>
            {
                col.AddRow(portraitRow =>
                {
                    portraitRow.AddImage(image =>
                    {
                        image.ResRef(model => model.NpcPortrait)
                            .Width(128f)
                            .Height(200f)
                            .Margin(8f)
                            .ImageAspect(NuiAspect.Exact)
                            .HorizontalAlign(NuiHAlign.Center)
                            .VerticalAlign(NuiVAlign.Middle);
                    });
                });
            });

            // Dialogue Column
            row.AddColumn(col =>
            {
                col.AddRow(textRow =>
                {
                    textRow.AddText(text =>
                    {
                        text.Text(model => model.ConversationHeader)
                            .Height(200f)
                            .Margin(8f);
                    });
                });
            });
        }

        /// <summary>
        /// Builds the response buttons section spanning both columns.
        /// </summary>
        private void BuildResponseButtons(NuiRowBuilder<ConversationViewModel> row)
        {
            row.AddColumn(col =>
            {
                col.AddList(list =>
                {
                    list.AddTemplateCell(template =>
                    {
                        template.AddRow(row2 =>
                        {
                            row2.AddButton(button =>
                            {
                                button.Label(model => model.AvailableResponses);
                            });
                        });
                    });

                }, model => model.AvailableResponses);
            });
        }
    }
} 