using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

public class PermissionAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _requiredPermission;

    public PermissionAuthorizeAttribute(string requiredPermission)
    {
        _requiredPermission = requiredPermission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user == null || !user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var claims = user.Claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();

        if (!claims.Contains(_requiredPermission))
        {
            context.Result = new ForbidResult();
        }
    }
}
