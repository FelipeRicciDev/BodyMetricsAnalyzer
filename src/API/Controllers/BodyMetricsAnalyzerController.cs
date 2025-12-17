namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BodyMetricsAnalyzerController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> SendPdf(
        [FromForm] ProcessBodyMetricsFilesCommand command,
        CancellationToken cancellationToken)
    {
        if (command.PdfFiles == null || command.PdfFiles.Count == 0)
            return BadRequest("Pelo menos um arquivo PDF é necessário.");

        await _mediator.Send(command, cancellationToken);

        return Ok("PDF recebido e processado.");
    }
}