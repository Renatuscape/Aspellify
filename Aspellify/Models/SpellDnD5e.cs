using System.Text.Json.Serialization;

namespace Aspellify.Models
{
    public class SpellDnD5e
    {
        [JsonPropertyName("index")]
        public string Index { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("desc")]
        public List<string> Description { get; set; } = new();

        [JsonPropertyName("range")]
        public string Range { get; set; } = string.Empty;

        [JsonPropertyName("components")]
        public List<string> Components { get; set; } = new();

        [JsonPropertyName("duration")]
        public string Duration { get; set; } = string.Empty;

        [JsonPropertyName("casting_time")]
        public string CastingTime { get; set; } = string.Empty;

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("damage")]
        public SpellDnD5eDamage Damage { get; set; } = new();

        // Add other properties as needed
    }

    public class SpellDnD5eDamage
    {
        [JsonPropertyName("damage_type")]
        public SpellDnD5eDamageType DamageType { get; set; } = new();

        [JsonPropertyName("damage_at_slot_level")]
        public Dictionary<string, string> DamageAtSlotLevel { get; set; } = new();
    }

    public class SpellDnD5eDamageType
    {
        [JsonPropertyName("index")]
        public string Index { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}
