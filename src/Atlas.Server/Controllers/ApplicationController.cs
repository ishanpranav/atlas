using Microsoft.AspNetCore.Mvc;

namespace Atlas.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
    private readonly ILogger<ApplicationController> _logger;
    private readonly AtlasDbContext _context;

    public ApplicationController(ILogger<ApplicationController> logger, AtlasDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetApplication")]
    public RichTextResponse Get()
    {
        return new RichTextResponse()
        {
            Value = "Welcome to Arcane Dominion! What is your username, brave adventurer?"
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
                Value = $"Welcome, {request.Username}! Please choose a password, so I know it's you next time.",
                IsNextPassword = true
            });
        }

        if (user.Hash == null)
        {
            user.Hash = request.Password;

            await _context.SaveChangesAsync();

            return Ok(new RichTextResponse()
            {
                Value = "A worthy password! Say anything to continue..."
            });
        }

        if (request.Password == null)
        {
            return Ok(new RichTextResponse()
            {
                Value = $"Welcome back, {user.Username}! Please remind me of your password, so I know it's you.",
                IsNextPassword = true
            });
        }

        if (request.Password != user.Hash)
        {
            return Ok(new RichTextResponse()
            {
                Value = $"Confound you knavish impostor! That's not {user.Username}'s password!",
            });
        }

        return Ok(new RichTextResponse()
        {
            Value = $"Your name is {user.Username} and you have {user.Credits} credits. The top 10 characters are: {string.Join(", ", _context.Users.OrderByDescending(x => x.Id).Take(10).Select(x => x.Username))}. That's all you can do for now, sorry."
        });
    }
}
