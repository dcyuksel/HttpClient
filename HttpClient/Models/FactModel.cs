using System.Text.Json.Serialization;

namespace HttpClient.Models;

public record FactModel
{
    [JsonPropertyName("_id")]
    public string Id { get; init; } = null!;
    public string Type { get; init; } = null!;
    public string Text { get; init; } = null!;
    public string Source { get; init; } = null!;
    public string User { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
