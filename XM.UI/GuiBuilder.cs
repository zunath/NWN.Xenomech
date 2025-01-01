using System;
using Anvil.API;

namespace XM.UI
{
    public class GuiBuilder<TViewModel>
        where TViewModel: IViewModel
    {
        private NuiColumn _root;
        private NuiElement _activeElement;

        public GuiBuilder()
        {
            
        }

        public GuiBuilder(NuiColumn col)
        {
            _root = col;
        }

        public GuiBuilder<TViewModel> CreateWindow()
        {
            _root = new NuiColumn();
            _activeElement = _root;

            return this;
        }

        public GuiBuilder<TViewModel> AddColumn(Action<GuiBuilder<TViewModel>> configure)
        {
            var col = new NuiColumn();
            configure(new GuiBuilder<TViewModel>(col));
            var element = new ModelElement<NuiColumn, TViewModel>(col);

            element.Set(model => model.Id, "test");
            

            return this;
        }

    }
}
