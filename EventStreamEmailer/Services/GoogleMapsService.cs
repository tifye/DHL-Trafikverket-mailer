using EventStreamEmailer.Model.GoogleMaps;
using Google.Cloud.Firestore;

namespace EventStreamEmailer.Services
{
    public class GoogleMapsService
    {
        private const string _baseUrl = @"https://maps.googleapis.com/maps/api/staticmap?";
        private const string _apiKey = "xxxx";

        private readonly HttpClient _httpClient;

        public GoogleMapsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Stream> GetMapImageAsync(MapSettings mapSettings)
        {
            string queryParamsString = mapSettings.ToQueryString() + $"&key={_apiKey}";
            Uri uri = new(_baseUrl + queryParamsString);

            System.Console.WriteLine($"Requesting map image from: \n\t{uri}");

            HttpRequestMessage request = new(HttpMethod.Get, uri);

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }

            throw new Exception($"Request failed with status code: {response.StatusCode} and reason: {response.ReasonPhrase}");
        }
    }
}