using HelloMvc.Models;
using HelloMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloMvc
{
    using HelloMvc.ActionFilters;

    public class HomeController : Controller
    {
        private readonly IHelloService helloService;

        public HomeController(IHelloService helloService)
        {
            this.helloService = helloService;
        }

        [HttpGet("/")]
        [LogActionFilter]
        public IActionResult Index()
        {
            return View(new HelloModel { Message = this.helloService.Hello()});
        }
    }
}