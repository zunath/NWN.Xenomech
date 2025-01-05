using System.ComponentModel;

namespace XM.UI
{
    public class GuiBindingList<T>: BindingList<T>, IGuiBindingList
    {
        public string PropertyName { get; set; }

        public GuiBindingList()
        {
            PropertyName = string.Empty;
        }
    }
}
