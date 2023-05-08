using System.Net;
using System.Net.Mail;
using EventStreamEmailer.Model.GoogleMaps;
using EventStreamEmailer.Model.TrafikVerket;
using EventStreamEmailer.Services;

namespace EventStreamEmailer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMailingService _mailingService;
    private readonly TrafikVerketService _trafikVerketService;
    private readonly GoogleMapsService _googleMapsService;
    private readonly StorageService _storageService;


    public Worker(ILogger<Worker> logger,
                  IMailingService mailingService,
                  TrafikVerketService trafikVerketService,
                  GoogleMapsService googleMapsService,
                  StorageService storageService)
    {
        _logger = logger;
        _mailingService = mailingService;
        _trafikVerketService = trafikVerketService;
        _googleMapsService = googleMapsService;
        _storageService = storageService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try {
            List<TrafficDataResult> results = await _trafikVerketService.MakeRequest<List<TrafficDataResult>>();

            // notify first deviation
            TrafficDeviation deviation = results.First().Situations.First().Deviations.First();
            if (deviation != null)
                await NotifyDeviation(deviation);

            /* foreach (TrafficSituation situation in results.First().Situations)
            {
                foreach (TrafficDeviation deviation in situation.Deviations)
                {
                    await NotifyDeviation(deviation);
                }
            } */


        } catch (Exception e) {
            _logger.LogError(e, "Error in TrafikVerketService");
            System.Console.WriteLine(e);
        }
        // _mailingService.SendEmail("joel.kontio@hotmail.com", "Ayy", "Lmao");

        while (!stoppingToken.IsCancellationRequested)
        {
            // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task NotifyDeviation(TrafficDeviation deviation)
    {
        MapSettings mapSettings = CreateMapSettings(deviation);
        Stream mapImageStream = await _googleMapsService.GetMapImageAsync(mapSettings);
        string url = await _storageService.StoreMapImageAsync(mapImageStream, deviation.CreationTime.ToLongTimeString());
        
        string emailBody = BuildEmailHtmlString(deviation, url);
        _mailingService.SendEmail("stacey.dematas@gmail.com", "Traffic Situation", emailBody, true);
        // _mailingService.SendEmail("dematasjoshua@hotmail.com", "Traffic Situation", emailBody, true);
    }

    private static string BuildEmailHtmlString(TrafficDeviation deviation, string mapImageUrl) 
    {
        return $"<p>⚠ {deviation.MessageCode} ⚠</p>" +
        $"<p>{deviation.RoadName} {deviation.RoadNumber}</p>" +
        $"<p>{deviation.AffectedDirection}</p>" +
        $"<p>{deviation.SeverityText}</p>" +
        $"<img src=\"{mapImageUrl}\" alt=\"Map image\"/>" +
        $"<p>{deviation.LocationDescriptor}</p>";
    }

    private static MapSettings CreateMapSettings(TrafficDeviation deviation)
    {
        return new() {
            Zoom = 14,
            Center = new() {
                X = (float)deviation.Geometry.Point.WGS84.Y, // Yes swapped
                Y = (float)deviation.Geometry.Point.WGS84.X,
            },
            ImageWidth = 400,
            ImageHeight = 400,
        };
    }

    private static string FormatDeviation(TrafficDeviation deviation) 
    {
        return $"⚠ {deviation.MessageCode} ⚠" +
        $"\nRoad: {deviation.RoadName} {deviation.RoadNumber}" +
        $"\nAffected direction: {deviation.AffectedDirection}" +
        $"\nSeverity: {deviation.SeverityCode}";
    }
}   
