
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
namespace MVCPrac.CustomeFilter
{
    public class CustomActionFilter : IActionFilter
    {
        private readonly ILogger<CustomActionFilter> _logger;

        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Before the action method executes.");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("After the action method executes.");
        }
    }
}
