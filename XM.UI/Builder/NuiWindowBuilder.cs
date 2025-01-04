using Anvil.API;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using XM.UI.Builder.Layout;

namespace XM.UI.Builder
{
    public class NuiWindowBuilder<TViewModel>: NuiBindable<TViewModel>
        where TViewModel: IViewModel
    {

        private readonly NuiWindow _window;
        private NuiRect _defaultGeometry;
        private readonly Dictionary<string, NuiLayout> _partialViews;

        public NuiWindowBuilder(NuiLayout root, string title, NuiEventCollection eventCollection) 
            : base(eventCollection)
        {
            _window = new NuiWindow(root, title);
            _window.Id = typeof(TViewModel).FullName;

            _window.Geometry = new NuiBind<NuiRect>(nameof(IViewModel.Geometry));
            _partialViews = new Dictionary<string, NuiLayout>();
        }

        public NuiWindowBuilder<TViewModel> DefinePartialView(string id, Action<NuiGroupBuilder<TViewModel>> configure)
        {
            var groupBuilder = new NuiGroupBuilder<TViewModel>(RegisteredEvents);
            configure(groupBuilder);

            _partialViews[id] = groupBuilder.Build();
            return this;
        }

        public NuiWindowBuilder<TViewModel> Border(bool border)
        {
            _window.Border = border;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsClosable(bool closable)
        {
            _window.Closable = closable;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsCollapsed(bool? collapsed)
        {
            _window.Collapsed = collapsed;
            return this;
        }

        public NuiWindowBuilder<TViewModel> InitialGeometry(float x, float y, float width, float height)
        {
            _defaultGeometry = new NuiRect(x, y, width, height);
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsResizable(bool resizable)
        {
            _window.Resizable = resizable;
            return this;
        }

        public NuiWindowBuilder<TViewModel> SetRoot(NuiLayout root)
        {
            _partialViews[IViewModel.UserPartialId] = root;
            var group = new NuiGroup
            {
                Id = IViewModel.MainViewElementId,
                Scrollbars = NuiScrollbars.None,
                Border = false
            };

            _window.Root = group;
            return this;
        }

        public NuiWindowBuilder<TViewModel> Title(string title)
        {
            _window.Title = title;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsTransparent(bool transparent)
        {
            _window.Transparent = transparent;
            return this;
        }

        public NuiWindowBuilder<TViewModel> AcceptsInput(bool acceptsInput)
        {
            _window.AcceptsInput = acceptsInput;
            return this;
        }

        public NuiWindowBuilder<TViewModel> Border(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.Border = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsClosable(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.Closable = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsCollapsed(Expression<Func<TViewModel, bool?>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.Collapsed = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsResizable(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.Resizable = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> Title(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            _window.Title = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsTransparent(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.Transparent = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> AcceptsInput(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.AcceptsInput = bind;
            return this;
        }

        private void BuildModalPartial()
        {
            DefinePartialView(IViewModel.ModalPartialId, group =>
            {
                group.SetBorder(false);
                group.SetScrollbars(NuiScrollbars.None);
                group.SetLayout(col =>
                {
                    col.AddRow(row =>
                    {
                        row.AddText(text =>
                        {
                            text.Text(model => model.ModalPromptText);
                        });
                    });
                    col.AddRow(row =>
                    {
                        row.AddSpacer();

                        row.AddButton(button =>
                        {
                            button.Label(model => model.ModalConfirmButtonText);
                            button.OnClick(model => model.OnModalConfirm());
                        });

                        row.AddButton(button =>
                        {
                            button.Label(model => model.ModalCancelButtonText);
                            button.OnClick(model => model.OnModalCancel());
                        });

                        row.AddSpacer();
                    });
                });
            });
        }

        internal NuiBuiltWindow Build()
        {
            BuildModalPartial();

            var serializedPartials = new Dictionary<string, Json>();
            foreach (var (partialId, layout) in _partialViews)
            {
                var json = JsonUtility.ToJson(layout);
                serializedPartials[partialId] = JsonParse(json);
            }

            return new NuiBuiltWindow(
                _window, 
                _defaultGeometry,
                serializedPartials);
        }
    }
}
