using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Atlas;

[Table(nameof(User))]
[Index(nameof(Username), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? Hash { get; set; }

    [DefaultValue(1000)]
    public decimal Credits { get; set; } = 1000;
}
