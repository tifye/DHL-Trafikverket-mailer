using System.Text.Json.Serialization;

namespace EventStreamEmailer.Model.TrafikVerket
{
    public class TrafficDeviation {
        public string AffectedDirection { get; set; } = string.Empty;

        [JsonPropertyName("CountyNo")]
        public List<int> CountyNumbers { get; set; } = new List<int>();
        
        public DateTime CreationTime { get; set; } = DateTime.MinValue;
        
        public DateTime EndTime { get; set; } = DateTime.MinValue;
        
        public string MessageCode { get; set; } = string.Empty;
        public string LocationDescriptor { get; set; } = string.Empty;
        
        public string RoadNumber { get; set; } = string.Empty;
        
        public int SeverityCode { get; set; }
        public string SeverityText { get; set; } = string.Empty;
        
        public string RoadName { get; set; } = string.Empty;
        
        public Geometry Geometry { get; set; } = new Geometry();

        public override string ToString()
        {
            return $"Road: {RoadName} {RoadNumber}\nAffected direction: {AffectedDirection}\nSeverity: {SeverityCode}\nMessage code: {MessageCode}";
        }
    }
}