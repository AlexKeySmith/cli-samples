using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloMvc.Queries.Implementations
{
    using HelloMvc.Models;
    using HelloMvc.Services;

    public class HelloQuery : IHelloQuery
    {
        private readonly IHelloService helloService;

        public HelloQuery(IHelloService helloService)
        {
            this.helloService = helloService;
        }

        public HelloModel Get()
        {
            return new HelloModel { Message = this.helloService.Hello() };
        }
    }
}
