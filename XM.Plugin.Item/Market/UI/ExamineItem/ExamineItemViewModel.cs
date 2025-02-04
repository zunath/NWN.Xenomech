using XM.UI;

namespace XM.Plugin.Item.Market.UI.ExamineItem
{
    internal class ExamineItemViewModel: ViewModel<ExamineItemViewModel>
    {
        public string WindowTitle
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Description
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ItemProperties
        {
            get => Get<string>();
            set => Set(value);
        }

        public override void OnOpen()
        {
            var payload = GetInitialData<ExamineItemPayload>();

            WindowTitle = payload.ItemName;
            Description = payload.Description;
            ItemProperties = payload.ItemProperties;
        }

        public override void OnClose()
        {
            
        }
    }
}
