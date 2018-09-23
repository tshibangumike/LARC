using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using FluentDateTime;

namespace larc.Service.Google_Services
{
    public class CalendarGoogleService
    {

        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        public static new List<Model.Event> GetCalendarEvents()
        {

            var events = new List<Model.Event>();

            try
            {
                
                UserCredential credential;

                using (var stream =
                    new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/GoogleCalendar/credentials.json"), FileMode.Open, FileAccess.Read))
                {
                    string credPath = HttpContext.Current.Server.MapPath("~/App_Data/GoogleCalendar/token.json");
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define parameters of request.
                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = DateTime.Now.Previous(DayOfWeek.Monday);
                request.TimeMax = DateTime.Now.Next(DayOfWeek.Monday);
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                // List events.
                Events googleEvents = request.Execute();
                Console.WriteLine("Upcoming events:");
                if (googleEvents.Items != null && googleEvents.Items.Count > 0)
                {
                    foreach (var eventItem in googleEvents.Items)
                    {
                        events.Add(new Model.Event()
                        {
                            Title = eventItem.Summary,
                            Date = eventItem.Start.Date,
                            StartTime = eventItem.Start.DateTime.Value,
                            EndTime = eventItem.End.DateTime.Value,
                            Description = eventItem.Description,
                            Location = eventItem.Location
                        });
                    }
                }
                return events;
            }
            catch (Exception ex)
            {
                // Append line to the file.
                using (StreamWriter writer =
                    new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Error/log.text"), true))
                {
                    writer.WriteLine(ex);
                }

                return events;

            }
        }

        public static new List<Model.Event> GetCalendarEventsWithPodcasts()
        {

            var events = new List<Model.Event>();

            try
            {
                
                UserCredential credential;

                using (var stream =
                    new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/GoogleCalendar/credentials.json"), FileMode.Open, FileAccess.Read))
                {
                    string credPath = HttpContext.Current.Server.MapPath("~/App_Data/GoogleCalendar/token.json");
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define parameters of request.
                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = DateTime.Now.AddDays(-30).Previous(DayOfWeek.Monday);
                request.TimeMax = DateTime.Now.Previous(DayOfWeek.Monday);
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                // List events.
                Events googleEvents = request.Execute();
                Console.WriteLine("Upcoming events:");
                if (googleEvents.Items != null && googleEvents.Items.Count > 0)
                {
                    foreach (var eventItem in googleEvents.Items)
                    {
                        if (eventItem.Attachments == null || eventItem.Attachments.Count == 0) continue;
                        events.Add(new Model.Event()
                        {
                            Title = eventItem.Summary,
                            Date = eventItem.Start.Date,
                            StartTime = eventItem.Start.DateTime.Value,
                            EndTime = eventItem.End.DateTime.Value,
                            Description = eventItem.Description,
                            Location = eventItem.Location,
                            GoogleAttachmentId = eventItem.Attachments[0].FileId
                        });
                    }
                }
                return events;
            }
            catch (Exception ex)
            {
                // Append line to the file.
                using (StreamWriter writer =
                    new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Error/log.text"), true))
                {
                    writer.WriteLine(ex);
                }

                return events;

            }
        }

    }
}
