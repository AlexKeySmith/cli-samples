namespace HelloMvc.ActionFilters
{
    using System;
    using System.Reflection;

    #region Using Directives

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc.Controllers;
    #endregion

    public class LogActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<LogActionFilter> logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            this.logger = logger;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            var logFilterAttribute = controllerActionDescriptor?.MethodInfo.GetCustomAttribute<LogActionFilterAttribute>();

            if (logFilterAttribute == null)
            {
                return next();
            }

            return next()
                .ContinueWith(
                    c =>
                    {
                        this.logger.LogWarning("Yay");
                        return c.Result;
                    });
        }

    }
}