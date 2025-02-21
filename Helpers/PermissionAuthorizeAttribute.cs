using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

public class PermissionAuthorizeAttribute : TypeFilterAttribute
{
    public PermissionAuthorizeAttribute(string permission) : base(typeof(PermissionAuthorizeFilter))
    {
        Arguments = new object[] { permission };
    }
}

public class PermissionAuthorizeFilter : IAuthorizationFilter
{
    private readonly string _permission;

    public PermissionAuthorizeFilter(string permission)
    {
        _permission = permission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var hasClaim = user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == _permission);
        if (!hasClaim)
        {
            context.Result = new ForbidResult();
        }
    }
}