using Microsoft.AspNetCore.Mvc;

namespace Atlas.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
    private readonly ILogger<ApplicationController> _logger;

    public ApplicationController(ILogger<ApplicationController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetApplication")]
    public RichTextResult Get()
    {
        return new RichTextResult()
        {
            Value = "Welcome to Arcane Dominion! To start, please enter a username."
        };
     }

    [HttpPost(Name = "PostApplication")]
    public IActionResult Post([FromBody] RichTextRequest request)
    {
        return Ok(new RichTextResult()
        {
            Value = $"Ok {request.Username}, what's your password?",
            IsNextPassword = true
        });
    }
}
