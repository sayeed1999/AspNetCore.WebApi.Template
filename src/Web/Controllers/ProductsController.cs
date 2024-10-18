using AspNetCore.WebApi.Template.Application.Common.Models;
using AspNetCore.WebApi.Template.Application.Products.Commands.CreateProduct;
using AspNetCore.WebApi.Template.Application.Products.Commands.DeleteProduct;
using AspNetCore.WebApi.Template.Application.Products.Commands.UpdateProduct;
using AspNetCore.WebApi.Template.Application.Products.Queries.GetProductsWithPagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.WebApi.Template.Web.Controllers;

public class ProductController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetProductsWithPagination(
        [FromQuery] GetProductsWithPaginationQuery query)
    {
        var res = await Mediator.Send(query);

        if (res is null)
        {
            return BadRequest(res);
        }

        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<int>> UpdateProduct(int id, UpdateProductCommand command)
    {
        if (id != command.Id) return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteProduct(int id)
    {
        var res = await Mediator.Send(new DeleteProductCommand(id));

        return res;
    }
}
