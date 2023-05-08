using System.Net.Mail;
using Google.Cloud.Firestore;
using EventStreamEmailer;
using EventStreamEmailer.Services;

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", 
    @"D:\Projects\_test\TrafilVerketMailer\EventStreamEmailer\dhl-warn-traffic-service-649d4544b47e.json");

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddHttpClient();

        services.AddSingleton<IMailingService, MailingService>();
        services.AddSingleton(provider => {
            return new SmtpClient("localhost"){
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
        });

        services.AddSingleton<TrafikVerketService>();
        services.AddSingleton<GoogleMapsService>();
        services.AddSingleton<StorageService>();
    })
    .Build();

host.Run();
