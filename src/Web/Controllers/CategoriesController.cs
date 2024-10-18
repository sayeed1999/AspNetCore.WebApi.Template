using AspNetCore.WebApi.Template.Application.Categories.Commands.CreateCategory;
using AspNetCore.WebApi.Template.Application.Categories.Commands.DeleteCategory;
using AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;
using AspNetCore.WebApi.Template.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.WebApi.Template.Web.Controllers;

public class CategoriesController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetCategoriesWithPagination(
        [FromQuery] GetCategoriesWithPaginationQuery query)
    {
        var res = await Mediator.Send(query);

        if (res is null)
        {
            return BadRequest(res);
        }

        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> UpsertCategory(CreateCategoryCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var res = await Mediator.Send(new DeleteCategoryCommand(id));

        return Ok(res);
    }
}
