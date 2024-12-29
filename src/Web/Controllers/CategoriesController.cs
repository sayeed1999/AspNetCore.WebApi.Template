using Application.Categories.Commands.UpsertCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Queries.GetCategoriesWithPagination;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

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
    public async Task<IActionResult> UpsertCategory(UpsertCategoryCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var res = await Mediator.Send(new DeleteCategoryCommand(id));

        return Ok(res);
    }
}
