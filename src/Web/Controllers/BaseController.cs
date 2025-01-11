#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ODataController
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
