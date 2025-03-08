using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Progression.Craft.UI
{
    [ServiceBinding(typeof(IView))]
    internal class SelectCraftView: IView
    {
        private readonly NuiBuilder<SelectCraftViewModel> _builder = new();
        public IViewModel CreateViewModel()
        {
            return new SelectCraftViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsCollapsible(WindowCollapsibleType.Disabled)
                    .IsResizable(true)
                    .IsTransparent(false)
                    .Title(LocaleString.SelectCraftSkill)
                    .InitialGeometry(0, 0, 400f, 300f)
                    .Root(root =>
                    {
                        root.AddRow(row =>
                        {
                            row.AddText(text =>
                            {
                                text
                                    .Text(model => model.Message);
                            });
                        });

                        root.AddRow(row =>
                        {
                            row.AddSpacer();

                            row.AddButton(button =>
                            {
                                button
                                    .Height(32f)
                                    .Label(LocaleString.Confirm)
                                    .OnClick(model => model.OnClickConfirm());
                            });

                            row.AddButton(button =>
                            {
                                button
                                    .Height(32f)
                                    .Label(LocaleString.Cancel)
                                    .OnClick(model => model.OnClickCancel());
                            });

                            row.AddSpacer();
                        });
                    });
            }).Build();
        }
    }
}
