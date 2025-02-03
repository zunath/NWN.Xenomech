using System.Collections.Generic;
using Anvil.API;
using XM.Shared.Core.Localization;
using Action = System.Action;

namespace XM.UI
{
    public interface IViewModel
    {
        internal const string MainViewElementId = "%%XM_WINDOW_MAIN_VIEW_ELEMENT_ID%%";
        internal const string ModalPartialId = "%%XM_WINDOW_MODAL_PARTIAL%%";
        internal const string UserPartialId = "%%XM_WINDOW_USER_PARTIAL%%";

        public NuiRect Geometry { get; protected set; }

        internal Dictionary<string, Json> PartialViews { get; set; }

        internal void Bind(
            uint player, 
            int windowToken,
            NuiRect geometry,
            Dictionary<string, Json> partialViews,
            object initialData,
            uint tetherObject);

        internal void Unbind();

        internal void OnOpenInternal();
        public void OnOpen();

        public void OnClose();

        string ModalPromptText { get; }

        string ModalConfirmButtonText { get; }

        string ModalCancelButtonText { get; }

        public Action OnModalConfirm();
        public Action OnModalCancel();

    }
}
