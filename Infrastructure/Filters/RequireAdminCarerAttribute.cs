using System.Security.Claims;
using BelshefaaApp.Core.Entities;
using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BelshefaaApp.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RequireAdminCarerAttribute : ActionFilterAttribute
    {
        public const string IdHeaderName = "X-Carer-Id";
        public const string PasscodeHeaderName = "X-Carer-Passcode";

        public bool AllowWhenNoCarers { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var accountIdClaim = context.HttpContext.User.FindFirstValue("sub");
            if (!int.TryParse(accountIdClaim, out var accountId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var carerRepository = context.HttpContext.RequestServices.GetRequiredService<ICarerRepository>();

            if (AllowWhenNoCarers
                && !await carerRepository.AnyAsync(c => c.AccountId == accountId, context.HttpContext.RequestAborted))
            {
                await next();
                return;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue(IdHeaderName, out var rawCarerId)
                || !int.TryParse(rawCarerId, out var carerId))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = $"The {IdHeaderName} header is required and must be a valid carer id.",
                });
                return;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue(PasscodeHeaderName, out var rawPasscode)
                || string.IsNullOrEmpty(rawPasscode))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = $"The {PasscodeHeaderName} header is required.",
                });
                return;
            }

            var carer = await carerRepository.FindOwnedAsync(accountId, carerId, context.HttpContext.RequestAborted);

            if (carer is null || carer.Relationship != CarerRelationship.Admin)
            {
                context.Result = new ObjectResult(new
                {
                    message = "Only an admin carer can perform this operation.",
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                };
                return;
            }

            if (!PasswordHasher.Verify(rawPasscode!, carer.PasscodeHash))
            {
                context.Result = new ObjectResult(new
                {
                    message = "Invalid carer passcode.",
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                };
                return;
            }

            await next();
        }
    }
}
