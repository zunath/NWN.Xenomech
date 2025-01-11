using System;
using XM.Shared.Core.Localization;

namespace XM.UI
{
    public abstract partial class ViewModel<TViewModel>
        where TViewModel: IViewModel
    {

        public LocaleString ModalPromptText
        {
            get => Get<LocaleString>();
            private set => Set(value);
        }

        public LocaleString ModalConfirmButtonText
        {
            get => Get<LocaleString>();
            private set => Set(value);
        }

        public LocaleString ModalCancelButtonText
        {
            get => Get<LocaleString>();
            private set => Set(value);
        }

        private Action _callerConfirmAction;
        private Action _callerCancelAction;

        public Action OnModalConfirm() => () =>
        {
            ChangePartialView(IViewModel.MainViewElementId, IViewModel.UserPartialId);

            if (_callerConfirmAction != null)
                _callerConfirmAction();
        };

        public Action OnModalCancel() => () =>
        {
            ChangePartialView(IViewModel.MainViewElementId, IViewModel.UserPartialId);

            if (_callerCancelAction != null)
                _callerCancelAction();
        };

        protected void ShowModal(
            LocaleString prompt,
            Action confirmAction,
            Action cancelAction = null,
            LocaleString confirmText = LocaleString.Yes,
            LocaleString cancelText = LocaleString.No)
        {
            ModalPromptText = prompt;
            ModalConfirmButtonText = confirmText;
            ModalCancelButtonText = cancelText;
            _callerConfirmAction = confirmAction;
            _callerCancelAction = cancelAction;

            ChangePartialView(IViewModel.MainViewElementId, IViewModel.ModalPartialId);
        }

    }
}
