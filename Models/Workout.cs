using System.Text.Json.Serialization;

namespace FitnessApp.Models
{
    public class Workout
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("type")]
        public string Type { get; set; } = "";

        [JsonPropertyName("muscle")]
        public string Muscle { get; set; } = "";

        [JsonPropertyName("equipment")]
        public string Equipment { get; set; } = "";

        [JsonPropertyName("difficulty")]
        public string Difficulty { get; set; } = "";

        [JsonPropertyName("instructions")]
        public string Instructions { get; set; } = "";
    }
}
