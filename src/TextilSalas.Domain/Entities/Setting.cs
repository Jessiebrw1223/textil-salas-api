using System.Text.Json;
namespace TextilSalas.Domain.Entities;
public sealed class Setting
{
    public string Key { get; set; } = string.Empty;
    public JsonDocument Value { get; set; } = JsonDocument.Parse("{}");
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
