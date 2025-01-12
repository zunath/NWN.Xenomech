using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Anvil.API;
using XM.Shared.API.NUI;
using XM.Shared.Core.Localization;
using XM.UI.Builder.Component;
using XM.UI.Builder.Layout;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;

namespace XM.UI.Builder
{
    public class NuiWindowBuilder<TViewModel> : NuiBindable<TViewModel>
        where TViewModel : IViewModel
    {
        private INuiComponentBuilder _root;
        private LocaleString _title;
        private string _titleBind;

        private bool _resizable;
        private string _resizableBind;

        private WindowCollapsibleType _collapsed;
        private string _collapsedBind;

        private bool _closable;
        private string _closableBind;

        private bool _transparent;
        private string _transparentBind;

        private bool _border;
        private string _borderBind;

        private bool _acceptsInput = true;

        private NuiRect _defaultGeometry;
        private readonly string _geometryBind = nameof(IViewModel.Geometry);
        private readonly Dictionary<string, NuiGroupBuilder<TViewModel>> _partialViews = new();

        public NuiWindowBuilder(NuiEventCollection registeredEvents) 
            : base(registeredEvents)
        {
            _title = LocaleString.NewWindow;
            _resizable = true;
            _collapsed = WindowCollapsibleType.UserCollapsible;
            _closable = true;
            _transparent = false;
            _border = true;
        }

        public NuiWindowBuilder<TViewModel> DefinePartialView(string id, Action<NuiGroupBuilder<TViewModel>> configure)
        {
            var groupBuilder = new NuiGroupBuilder<TViewModel>(RegisteredEvents);
            configure(groupBuilder);

            _partialViews[id] = groupBuilder;
            return this;
        }

        public NuiWindowBuilder<TViewModel> Root(Action<NuiColumnBuilder<TViewModel>> root)
        {
            // The root gets wrapped in a group and then placed in the partial views, to be swapped out later when the window opens
            var rootBuilder = new NuiColumnBuilder<TViewModel>(RegisteredEvents);
            root(rootBuilder);

            var rootWrapperPartial = new NuiGroupBuilder<TViewModel>(RegisteredEvents);
            rootWrapperPartial
                .SetLayout(rootBuilder)
                .Border(false)
                .Scrollbars(NuiScrollbars.None);

            _partialViews[IViewModel.UserPartialId] = rootWrapperPartial;

            // A dummy group is shown initially before the view model opens up the real one upon window open.
            var groupBuilder = new NuiGroupBuilder<TViewModel>(RegisteredEvents)
                .Id(IViewModel.MainViewElementId)
                .Scrollbars(NuiScrollbars.None)
                .Border(false)
                .SetLayout(col =>
                {
                });

            _root = groupBuilder;
            return this;
        }

        public NuiWindowBuilder<TViewModel> Title(LocaleString title)
        {
            _title = title;
            return this;
        }

        public NuiWindowBuilder<TViewModel> Title(Expression<Func<TViewModel, LocaleString>> expression)
        {
            _titleBind = GetBindName(expression);
            return this;
        }

        public NuiWindowBuilder<TViewModel> InitialGeometry(float x, float y, float w, float h)
        {
            _defaultGeometry = new NuiRect(x, y, w, h);
            return this;
        }

        public NuiWindowBuilder<TViewModel> InitialGeometry(NuiRect geometry)
        {
            _defaultGeometry = geometry;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsResizable(bool resizable)
        {
            _resizable = resizable;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsResizable(Expression<Func<TViewModel, bool>> expression)
        {
            _resizableBind = GetBindName(expression);
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsCollapsible(WindowCollapsibleType collapsed)
        {
            _collapsed = collapsed;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsCollapsible(Expression<Func<TViewModel, bool>> expression)
        {
            _collapsedBind = GetBindName(expression);
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsClosable(bool closable)
        {
            _closable = closable;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsClosable(Expression<Func<TViewModel, bool>> expression)
        {
            _closableBind = GetBindName(expression);
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsTransparent(bool transparent)
        {
            _transparent = transparent;
            return this;
        }

        public NuiWindowBuilder<TViewModel> IsTransparent(Expression<Func<TViewModel, bool>> expression)
        {
            _transparentBind = GetBindName(expression);
            return this;
        }

        public NuiWindowBuilder<TViewModel> Border(bool border)
        {
            _border = border;
            return this;
        }

        public NuiWindowBuilder<TViewModel> Border(Expression<Func<TViewModel, bool>> expression)
        {
            _borderBind = GetBindName(expression);
            return this;
        }

        public NuiWindowBuilder<TViewModel> AcceptsInput(bool acceptsInput)
        {
            _acceptsInput = acceptsInput;
            return this;
        }
        private void BuildModalPartial()
        {
            DefinePartialView(IViewModel.ModalPartialId, group =>
            {
                group.Border(false);
                group.Scrollbars(NuiScrollbars.None);
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

        public NuiBuiltWindow BuildWindow()
        {
            if (_root == null)
                throw new InvalidOperationException("Root element must be set for a NuiWindow.");

            BuildModalPartial();

            var root = _root.Build();
            
            var title = string.IsNullOrWhiteSpace(_titleBind) 
                ? JsonString(Locale.GetString(_title)) 
                : Nui.Bind(_titleBind);

            var geometry = Nui.Bind(_geometryBind);

            var resizable = string.IsNullOrWhiteSpace(_resizableBind) 
                ? JsonBool(_resizable) 
                : Nui.Bind(_resizableBind);

            Json collapsedValue;
            switch (_collapsed)
            {
                case WindowCollapsibleType.CollapsedInitially:
                    collapsedValue = JsonBool(true);
                    break;
                case WindowCollapsibleType.UserCollapsible:
                    collapsedValue = JsonNull();
                    break;
                case WindowCollapsibleType.Disabled:
                    collapsedValue = JsonBool(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var collapsed = string.IsNullOrWhiteSpace(_collapsedBind) 
                ? collapsedValue 
                : Nui.Bind(_collapsedBind);

            var closable = string.IsNullOrWhiteSpace(_closableBind) 
                ? JsonBool(_closable) 
                : Nui.Bind(_closableBind);

            var transparent = string.IsNullOrWhiteSpace(_transparentBind)
                ? JsonBool(_transparent)
                : Nui.Bind(_transparentBind);

            var border = string.IsNullOrWhiteSpace(_borderBind) 
                ? JsonBool(_border) 
                : Nui.Bind(_borderBind);

            var acceptsInput = JsonBool(_acceptsInput);

            var partialViews = new Dictionary<string, Json>();

            foreach (var (partialId, builder) in _partialViews)
            {
                partialViews[partialId] = builder.Build();
            }

            var window = Nui.Window(
                root,
                title,
                geometry,
                resizable,
                collapsed,
                closable,
                transparent,
                border,
                acceptsInput);

            var id = typeof(TViewModel).FullName;
            window = Nui.Id(window, id);

            return new NuiBuiltWindow(id, window, _defaultGeometry, partialViews);
        }
    }
}
