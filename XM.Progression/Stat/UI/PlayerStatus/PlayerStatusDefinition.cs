﻿using Anvil.Services;
using XM.UI;
using XM.UI.UI;

namespace XM.Progression.Stat.UI.PlayerStatus
{
    [ServiceBinding(typeof(PlayerStatusDefinition))]
    [ServiceBinding(typeof(IGuiWindowDefinition))]
    internal class PlayerStatusDefinition : IGuiWindowDefinition
    {
        private readonly GuiWindowBuilder<PlayerStatusViewModel> _builder;

        public PlayerStatusDefinition(GuiWindowBuilder<PlayerStatusViewModel> builder)
        {
            _builder = builder;
        }


        public GuiConstructedWindow BuildWindow()
        {
            _builder.CreateWindow(GuiWindowType.PlayerStatus)
                .SetInitialGeometry(0, 0, 180f, 70f)
                .SetTitle(null)
                .SetIsClosable(false)
                .SetIsResizable(false)
                .SetIsCollapsible(false)
                .SetIsTransparent(false)
                .SetShowBorder(true)
                .SetAcceptsInput(false)
                .AddColumn(col =>
                {
                    col.AddRow(row =>
                    {
                        row.AddProgressBar()
                            .BindValue(model => model.Bar1Progress)
                            .BindColor(model => model.Bar1Color)
                            .SetHeight(20f)
                            .AddDrawList(drawList =>
                            {
                                drawList.AddText(text =>
                                {
                                    text.BindText(model => model.Bar1Label);
                                    text.SetBounds(5, 2, 110f, 50f);
                                    text.SetColor(255, 255, 255);
                                });

                                drawList.AddText(text =>
                                {
                                    text.BindText(model => model.Bar1Value);
                                    text.BindBounds(model => model.RelativeValuePosition);
                                    text.SetColor(255, 255, 255);
                                });
                            });
                    });

                    col.AddRow(row =>
                    {
                        row.AddProgressBar()
                            .BindValue(model => model.Bar3Progress)
                            .BindColor(model => model.Bar3Color)
                            .SetHeight(20f)
                            .AddDrawList(drawList =>
                            {
                                drawList.AddText(text =>
                                {
                                    text.BindText(model => model.Bar3Label);
                                    text.SetBounds(5, 2, 110f, 50f);
                                    text.SetColor(255, 255, 255);
                                });

                                drawList.AddText(text =>
                                {
                                    text.BindText(model => model.Bar3Value);
                                    text.BindBounds(model => model.RelativeValuePosition);
                                    text.SetColor(255, 255, 255);
                                });
                            });
                    });
                });

            return _builder.Build();
        }
    }
}
