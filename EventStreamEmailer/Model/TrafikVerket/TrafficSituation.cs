using System.Text.Json.Serialization;

namespace EventStreamEmailer.Model.TrafikVerket
{
    public class TrafficSituation {
        [JsonPropertyName("Deviation")]
        public List<TrafficDeviation> Deviations { get; set; } = new List<TrafficDeviation>();
    }
}