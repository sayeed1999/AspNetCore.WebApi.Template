using Web.Workers;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class BackgroundWorkersController(
    PeriodicTrashCleaner periodicTrashCleanerService
) : BaseController
{
    [HttpGet]
    [Route("periodic-trash-cleaner")]
    public IActionResult Get()
    {
        return Ok(new PeriodicTrashCleanerState(periodicTrashCleanerService.IsEnabled));
    }

    [HttpPatch]
    [Route("periodic-trash-cleaner")]
    public IActionResult Update([FromBody] PeriodicTrashCleanerState state)
    {
        periodicTrashCleanerService.IsEnabled = state.IsEnabled;
        return Ok(new PeriodicTrashCleanerState(periodicTrashCleanerService.IsEnabled));
    }
}

public record PeriodicTrashCleanerState(bool IsEnabled);
