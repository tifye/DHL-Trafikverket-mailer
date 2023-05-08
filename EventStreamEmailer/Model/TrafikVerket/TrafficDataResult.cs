using System.Text.Json.Serialization;

namespace EventStreamEmailer.Model.TrafikVerket
{
    public class TrafficDataResult {
        [JsonPropertyName("Situation")]
        public List<TrafficSituation> Situations { get; set; } = new List<TrafficSituation>();
    }
}