using System;
using XM.Shared.Core.Localization;

namespace XM.UI
{
    public abstract partial class ViewModel<TViewModel>
        where TViewModel: IViewModel
    {

        public string ModalPromptText
        {
            get => Get<string>();
            private set => Set(value);
        }

        public string ModalConfirmButtonText
        {
            get => Get<string>();
            private set => Set(value);
        }

        public string ModalCancelButtonText
        {
            get => Get<string>();
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
            string prompt,
            Action confirmAction,
            Action cancelAction = null,
            LocaleString confirmText = LocaleString.Yes,
            LocaleString cancelText = LocaleString.No)
        {
            ModalPromptText = prompt;
            ModalConfirmButtonText = Locale.GetString(confirmText);
            ModalCancelButtonText = Locale.GetString(cancelText);
            _callerConfirmAction = confirmAction;
            _callerCancelAction = cancelAction;

            ChangePartialView(IViewModel.MainViewElementId, IViewModel.ModalPartialId);
        }

    }
}
