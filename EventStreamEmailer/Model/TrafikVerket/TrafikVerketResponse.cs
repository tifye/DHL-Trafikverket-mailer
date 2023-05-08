using System.Text.Json.Serialization;

namespace EventStreamEmailer.Model.TrafikVerket
{
    public class TrafikVerketResponse<T> {
        [JsonPropertyName("RESULT")]
        public T Result { get; set; }

        public TrafikVerketResponse(T result) {
            Result = result;
        }
    }
}