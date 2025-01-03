﻿using System.Linq;
using XM.API.BaseTypes;
using XM.API.NUI;

namespace XM.UI.Component
{
    public class GuiGroup<T> : GuiExpandableComponent<T>
        where T: IGuiViewModel
    {
        private bool ShowBorder { get; set; }
        private NuiScrollbars Scrollbars { get; set; }

        public GuiGroup()
        {
            ShowBorder = true;
            Scrollbars = NuiScrollbars.Auto;
        }

        public GuiGroup<T> SetShowBorder(bool showBorder)
        {
            ShowBorder = showBorder;

            return this;
        }

        public GuiGroup<T> SetScrollbars(NuiScrollbars scrollBars)
        {
            Scrollbars = scrollBars;
            return this;
        }


        public override Json BuildElement()
        {
            if (Elements.Count <= 0)
            {
                Elements.Add(new GuiSpacer<T>());
            }

            var child = Elements.ElementAt(0).ToJson();
            return Nui.Group(child, ShowBorder, Scrollbars);
        }
    }
}
