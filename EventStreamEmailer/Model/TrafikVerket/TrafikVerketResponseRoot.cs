using System.Text.Json.Serialization;

namespace EventStreamEmailer.Model.TrafikVerket
{
    public class TrafikVerketResponseRoot<T> {
        [JsonPropertyName("RESPONSE")]
        public TrafikVerketResponse<T> Response { get; set; }

        public TrafikVerketResponseRoot(TrafikVerketResponse<T> response) {
            Response = response;
        }
    }
}