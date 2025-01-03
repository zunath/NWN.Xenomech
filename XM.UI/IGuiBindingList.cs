using System.ComponentModel;

namespace XM.UI
{
    public interface IGuiBindingList: IBindingList
    {
        string PropertyName { get; set; }
    }
}
