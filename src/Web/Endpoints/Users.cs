using AspNetCore.WebApi.Template.Infrastructure.Identity;

namespace AspNetCore.WebApi.Template.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }
}
