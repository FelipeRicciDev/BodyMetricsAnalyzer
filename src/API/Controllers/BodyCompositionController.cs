namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BodyCompositionController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("compare")]
    public async Task<IActionResult> Compare(
    [FromForm] CompareBodyCompositionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}