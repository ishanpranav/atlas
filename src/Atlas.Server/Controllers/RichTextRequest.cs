namespace Atlas.Server.Controllers;

public class RichTextRequest
{
    public string Username { get; set; } = string.Empty;
    public string? Password { get; set; }
    public string? Value { get; set; }
}
