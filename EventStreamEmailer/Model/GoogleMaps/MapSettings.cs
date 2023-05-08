using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace EventStreamEmailer.Model.GoogleMaps
{
    public class MapSettings
    {
        public int Zoom { get; set; }
        public Vector2 Center { get; set; }
        public string MarkerColor { get; set; } = "red";
        public uint ImageWidth { get; set; }
        public uint ImageHeight { get; set; }
        public string MapType { get; set; } = "hybrid";

        public string ToQueryString()
        {
            string xString = Center.X.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
            string yString = Center.Y.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
            return $"zoom={Zoom}&size={ImageWidth}x{ImageHeight}&maptype={MapType}&markers=color:{MarkerColor}|{xString},{yString}";
        }
    }
}