using System.ComponentModel;

namespace XM.Shared.Core
{
    public class XMBindingList<T>: BindingList<T>, IXMBindingList
    {
        public string PropertyName { get; set; }

        public XMBindingList()
        {
            PropertyName = string.Empty;
        }
    }
}
