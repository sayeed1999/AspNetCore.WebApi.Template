using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.UpsertCategory;
using Application.Categories.Queries.GetCategoriesWithPagination;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;

namespace Web.Controllers;

public class CategoriesController(ICategoryRepository _repository) : BaseController
{
    [EnableQuery(PageSize = 10)]
    [HttpGet]
    public IQueryable<Category> GetCategories()
    {
        return _repository.GetAll();
    }

    [EnableQuery]
    [HttpGet("{id}")]
    public SingleResult<Category> GetCategory([FromODataUri] Guid key)
    {
        return SingleResult.Create(_repository.GetById(key));
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesWithPagination(
        [FromQuery] GetCategoriesWithPaginationQuery query)
    {
        PaginatedList<CategoryDto> res = await Mediator.Send(query);

        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> UpsertCategory(UpsertCategoryCommand command)
    {
        CategoryDto result = await Mediator.Send(command);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        CategoryDto res = await Mediator.Send(new DeleteCategoryCommand(id));

        return Ok(res);
    }
}
