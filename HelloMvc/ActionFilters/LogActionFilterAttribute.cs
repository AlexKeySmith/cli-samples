using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloMvc.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class LogActionFilterAttribute : Attribute
    {
        //http://blog.ploeh.dk/2014/06/13/passive-attributes/
    }
}
