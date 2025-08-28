using Microsoft.AspNetCore.Mvc;

namespace Atlas.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
    private readonly ILogger<ApplicationController> _logger;
    private readonly AtlasDbContext _context;
    private readonly FormattedStrings _strings;

    public ApplicationController(ILogger<ApplicationController> logger, AtlasDbContext context, FormattedStrings strings)
    {
        _logger = logger;
        _context = context;
        _strings = strings;
    }

    [HttpGet(Name = "GetApplication")]
    public RichTextResponse Get()
    {
        return new RichTextResponse()
        {
            Value = _strings.WelcomeText
        };
    }

    [HttpPost(Name = "PostApplication")]
    public async Task<IActionResult> PostAsync([FromBody] RichTextRequest request)
    {
        //await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        User? user = _context.Users
            .SingleOrDefault(x => x.Username.ToUpper() == request.Username.ToUpper());

        if (user == null)
        {
            user = new User()
            {
                Username = request.Username
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok(new RichTextResponse()
            {
                Value = _strings.GetCreatingPasswordText(request.Username),
                IsNextPassword = true
            });
        }

        if (user.Hash == null)
        {
            user.Hash = request.Password;

            await _context.SaveChangesAsync();

            return Ok(new RichTextResponse()
            {
                Value = _strings.CreatedPasswordText
            });
        }

        if (request.Password == null)
        {
            return Ok(new RichTextResponse()
            {
                Value = _strings.GetAuthenticatingText(user.Username),
                IsNextPassword = true
            });
        }

        if (request.Password != user.Hash)
        {
            return Ok(new RichTextResponse()
            {
                Value = _strings.GetUnauthenticatedText(user.Username)
            });
        }

        return Ok(new RichTextResponse()
        {
            Value = $"Your name is {user.Username} and you have {user.Credits:n0} credits. The top 10 characters are: {string.Join(", ", _context.Users.OrderByDescending(x => x.Id).Take(10).Select(x => x.Username))}. That's all you can do for now, sorry."
        });
    }
}
