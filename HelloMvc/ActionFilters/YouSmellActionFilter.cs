namespace HelloMvc.ActionFilters
{
    #region Using Directives

    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Reflection;

    using HelloMvc.Models;

    using Microsoft.AspNetCore.Mvc;
    #endregion

    public class YouSmellActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var result = (ViewResult)context.Result;
            var model = result.Model as HelloModel;

            model.Message = $"{model.Message}  you smell";

            context.Result = result;
        }


        //No need to override, looking at the implementation it just calls OnResultExecuted anyway.

        //public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        //{
        //    var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

        //    var youSmellResultFilter = controllerActionDescriptor?.MethodInfo.GetCustomAttribute<YouSmellResultFilterAttribute>();

        //    if (youSmellResultFilter == null)
        //    {
        //        return next();
        //    }

        //    return next()
        //        .ContinueWith(
        //            c =>
        //            {
        //                var result = (ViewResult)c.Result.Result;
        //                var model = result.Model as HelloModel;

        //                model.Message = $"{model.Message}  you smell";

        //                return result;
        //            });

        //}
    }
}