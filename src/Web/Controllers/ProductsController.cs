using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Domain.Entities;
using Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;

namespace Web.Controllers;

public class ProductsController(IProductRepository _repository) : BaseController
{
    [EnableQuery]
    [HttpGet]
    public IQueryable<Product> GetProducts()
    {
        return _repository.GetAll();
    }

    // Note: **The naming is very bug prone! When I rename GetProduct to something else, it doesn't work! :(
    [EnableQuery]
    [HttpGet("{id}")]
    public SingleResult<Product> GetProduct([FromODataUri] Guid key)
    {
        return SingleResult.Create(_repository.GetById(key));
    }

    // Note: This conflicts with odata queries when both has same route='/products'
    // [HttpGet]
    // public async Task<IActionResult> GetProductsWithPagination(
    //     [FromQuery] GetProductsWithPaginationQuery query)
    // {
    //     var res = await Mediator.Send(query);

    //     return Ok(res);
    // }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;

        var res = await Mediator.Send(command);

        return Ok(res);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        var res = await Mediator.Send(new DeleteProductCommand(id));

        return Ok(res);
    }
}
