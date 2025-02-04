using Anvil.API;
using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;

namespace XM.Plugin.Item.Market.UI.ExamineItem
{
    [ServiceBinding(typeof(IView))]
    internal class ExamineItemView: IView
    {
        private readonly NuiBuilder<ExamineItemViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new ExamineItemViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0f, 0f, 385f, 379f)
                    .Title(model => model.WindowTitle)
                    
                    .Root(col =>
                    {
                        col.AddRow(row =>
                        {
                            row.AddGroup(group =>
                            {
                                group
                                    .Height(26f)
                                    .Border(true)
                                    .Scrollbars(NuiScrollbars.Auto)
                                    .SetLayout(layout =>
                                    {
                                        layout.AddLabel(label =>
                                        {
                                            label
                                                .Label(LocaleString.Description)
                                                .HorizontalAlign(NuiHAlign.Center)
                                                .VerticalAlign(NuiVAlign.Middle);
                                        });
                                    });
                            });
                        });

                        col.AddRow(row =>
                        {
                            row.AddText(text =>
                            {
                                text
                                    .Text(model => model.Description)
                                    .Height(160f);
                            });
                        });

                        col.AddRow(row =>
                        {
                            row.AddGroup(group =>
                            {
                                group
                                    .Height(26f)
                                    .Border(true)
                                    .Scrollbars(NuiScrollbars.Auto)
                                    .SetLayout(layout => 
                                    {
                                        layout.AddLabel(label =>
                                        {
                                            label
                                                .Label(LocaleString.ItemProperties)
                                                .HorizontalAlign(NuiHAlign.Center)
                                                .VerticalAlign(NuiVAlign.Middle);
                                        });
                                    });
                            });
                        });

                        col.AddRow(row =>
                        {
                            row.AddText(text =>
                            {
                                text
                                    .Text(model => model.ItemProperties)
                                    .Height(105f);
                            });
                        });
                    });

            }).Build();
        }
    }
}
