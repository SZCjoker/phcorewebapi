using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Utility
{
    [AttributeUsage(AttributeTargets.Class,Inherited =false)]
    public class InjectionAttribute:Attribute
    {
        [Description("sortting Index")]
        [DefaultValue(0)]
        public int Index;
        public InjectionAttribute() { }
        public InjectionAttribute(int index)
        {
            Index = index;
        }
    }
}
