using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloMvc.Models
{
    public class HelloModel : IHasMessage
    {
        public string Message { get; set; }
    }
}
