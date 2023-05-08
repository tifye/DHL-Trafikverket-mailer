using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace EventStreamEmailer.Model.TrafikVerket
{
    public class Geometry
    {
        public Point Point { get; set; } = new Point();
    }

    public partial class Point
    {
        private string _wgs84String { get; set; } = string.Empty;

        [JsonPropertyName("WGS84")]
        public string WGS84String {
            get => _wgs84String;
            set {
                _wgs84String = value;

                Regex regex = PointRegex();
                var match = regex.Match(value);

                if (match.Success)
                {
                    WGS84 = new System.Numerics.Vector2(
                        float.Parse(match.Groups[1].Value.Replace('.', ',')),
                        float.Parse(match.Groups[2].Value.Replace('.', ','))
                    );
                }
            }
        }

        [JsonIgnore]
        public System.Numerics.Vector2 WGS84 { get; private set; }

        [GeneratedRegex("POINT \\((\\d+\\.\\d+) (\\d+\\.\\d+)\\)")]
        private static partial Regex PointRegex();
    }
}