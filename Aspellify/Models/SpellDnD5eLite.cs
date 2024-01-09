using System.Text.Json.Serialization;

namespace Aspellify.Models
{
    public class SpellListResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("results")]
        public List<SpellDnD5e> Results { get; set; } = new();
    }
}
