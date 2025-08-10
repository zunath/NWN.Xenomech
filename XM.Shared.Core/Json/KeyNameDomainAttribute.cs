using System;

namespace XM.Shared.Core.Json
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class KeyNameDomainAttribute : Attribute
    {
        public string Domain { get; }
        public KeyNameDomainAttribute(string domain)
        {
            Domain = domain;
        }
    }
}


