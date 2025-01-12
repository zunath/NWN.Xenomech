using System.ComponentModel;

namespace XM.Shared.Core
{
    public interface IXMBindingList: IBindingList
    {
        string PropertyName { get; set; }
    }
}
