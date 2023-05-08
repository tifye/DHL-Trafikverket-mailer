using System.Text;
using System.Text.Json;
using EventStreamEmailer.Model.TrafikVerket;

namespace EventStreamEmailer.Services
{
    public class TrafikVerketService
    {
        private const string _baseUrl = "https://api.trafikinfo.trafikverket.se/v2/data.json";
        private const string _apiKey = "xxxx";

        private readonly HttpClient _httpClient;

        public TrafikVerketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> MakeRequest<T>()
        {
            TrafikVerketQuery query = new(GetDefaultQuery());
            TrafikVerketRequest trafikVerketRequest = new(_apiKey, query);

            HttpRequestMessage request = new(HttpMethod.Post, _baseUrl);
            request.Headers.Add("Accept", "application/json");
            request.Content = new StringContent(trafikVerketRequest.ToXml(), Encoding.UTF8, "application/xml");

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) {
                throw new Exception($"Error in TrafikVerketService: {response.StatusCode}");
            }

            string responseString = await response.Content.ReadAsStringAsync();
            TrafikVerketResponseRoot<T>? responseRoot = JsonSerializer.Deserialize<TrafikVerketResponseRoot<T>>(responseString) 
                ?? throw new Exception("Error in TrafikVerketService: responseRoot is null");

            return responseRoot.Response.Result;
        }

        private static string GetDefaultQuery() 
        {
            return @"
            <QUERY objecttype=""Situation"" schemaversion=""1.5"" includedeletedobjects=""true""
                changeid=""0"">
                <FILTER>
                    <GT name=""Deviation.CreationTime"" value='$dateadd(-0.2:00:00)'/>
                    <GTE name=""Deviation.SeverityCode"" value=""4""/>
                    <EQ name=""CountryCode"" value=""SE""/> 
                    <OR>
                        <EQ name=""Deviation.RoadNumber"" value=""E4""/>
                        <EQ name=""Deviation.RoadNumber"" value=""E6""/>
                        <EQ name=""Deviation.RoadNumber"" value=""E14""/>
                        <EQ name=""Deviation.RoadNumber"" value=""E16""/>
                        <EQ name=""Deviation.RoadNumber"" value=""E18""/>
                        <EQ name=""Deviation.RoadNumber"" value=""E20""/>
                        <EQ name=""Deviation.RoadNumber"" value=""E22""/>
                        <EQ name=""Deviation.RoadNumber"" value=""E45""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 15""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 21""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 23""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 25""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 26""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 27""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 30""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 31""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 32""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 35""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 37""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 40""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 41""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 42""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 44""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 46""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 47""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 49""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 50""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 51""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 52""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 53""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 55""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 56""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 66""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 68""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 70""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 72""/>
                        <EQ name=""Deviation.RoadNumber"" value=""Väg 75""/>
                    </OR>
                </FILTER> 
                <INCLUDE>Deviation.CountyNo</INCLUDE>
                <INCLUDE>Deviation.MessageCode</INCLUDE>
                <INCLUDE>Deviation.SeverityCode</INCLUDE>
                <INCLUDE>Deviation.Schedule.StartOfPeriod</INCLUDE>
                <INCLUDE>Deviation.CreationTime</INCLUDE>
                <INCLUDE>Deviation.Schedule.EndOfPeriod</INCLUDE>
                <INCLUDE>Deviation.EndTime</INCLUDE>
                <INCLUDE>Deviation.RoadName</INCLUDE>
                <INCLUDE>Deviation.RoadNumber</INCLUDE>
                <INCLUDE>Deviation.AffectedDirection</INCLUDE>
                
                <INCLUDE>Deviation.SeverityText</INCLUDE>
                <INCLUDE>Deviation.LocationDescriptor</INCLUDE>
                <INCLUDE>Deviation.Geometry.Point.WGS84</INCLUDE>
            </QUERY>
            ";
        }
    }
}