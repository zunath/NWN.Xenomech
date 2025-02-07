using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XM.UI
{
    public class NuiEventDetail
    {
        public MethodInfo Method { get; }
        public IReadOnlyList<KeyValuePair<Type, object>> Arguments { get; }

        public NuiEventDetail(MethodInfo method, IEnumerable<KeyValuePair<Type, object>> arguments)
        {
            Method = method;
            Arguments = arguments.ToList().AsReadOnly();
        }
    }
}
