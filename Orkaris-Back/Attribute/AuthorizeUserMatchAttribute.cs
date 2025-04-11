using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Orkaris_Back.Attribute;

public class AuthorizeUserMatchAttribute : System.Attribute, IAuthorizationFilter
{
    private readonly string _routeParamName;

    public AuthorizeUserMatchAttribute(string routeParamName = "id")
    {
        _routeParamName = routeParamName;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userIdFromToken = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdFromToken == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        if (!context.RouteData.Values.TryGetValue(_routeParamName, out var routeId))
        {
            context.Result = new BadRequestObjectResult($"Missing route parameter '{_routeParamName}'");
            return;
        }

        if (!Guid.TryParse(userIdFromToken, out var userIdToken) || !Guid.TryParse(routeId?.ToString(), out var userIdRoute))
        {
            context.Result = new ForbidResult();
            return;
        }

        if (userIdToken != userIdRoute)
        {
            context.Result = new ForbidResult();
        }
    }
}

