using AspNetCore.WebApi.Template.Application.Common.Models;
using AspNetCore.WebApi.Template.Application.TodoItems.Commands.CreateTodoItem;
using AspNetCore.WebApi.Template.Application.TodoItems.Commands.DeleteTodoItem;
using AspNetCore.WebApi.Template.Application.TodoItems.Commands.UpdateTodoItem;
using AspNetCore.WebApi.Template.Application.TodoItems.Commands.UpdateTodoItemDetail;
using AspNetCore.WebApi.Template.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.WebApi.Template.Web.Controllers;

public class TodoItemController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<TodoItemBriefDto>>> GetTodoItemsWithPagination(
        [FromQuery] GetTodoItemsWithPaginationQuery query)
    {
        var res = await Mediator.Send(query);

        if (res is null)
        {
            return BadRequest(res);
        }

        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateTodoItem(CreateTodoItemCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTodoItem(int id, UpdateTodoItemCommand command)
    {
        if (id != command.Id) return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTodoItem(int id)
    {
        await Mediator.Send(new DeleteTodoItemCommand(id));

        return NoContent();
    }
}
