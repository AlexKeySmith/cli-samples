using HelloMvc.Models;
using HelloMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloMvc
{
    using HelloMvc.ActionFilters;
    using HelloMvc.Queries;

    public class HomeController : Controller
    {
        private readonly IHelloQuery helloQuery;

        public HomeController(IHelloQuery helloQuery)
        {
            this.helloQuery = helloQuery;
        }

        [HttpGet("/")]
        [LogActionFilter]
        [YouSmellActionFilter]
        public IActionResult Index()
        {
            var model = this.helloQuery.Get();
            return View(model);
        }
    }
}