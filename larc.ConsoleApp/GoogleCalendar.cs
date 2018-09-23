using FluentDateTime;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace larc.ConsoleApp
{
    public class GoogleCalendar
    {

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";


        public static void GetCalendarEvents()
        {

            UserCredential credential;

            using (var stream =
                new FileStream((Directory.GetCurrentDirectory() + "//Google Data/GCalendar/credentials.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = (Directory.GetCurrentDirectory() + "//Google Data/GCalendar/token.json");
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
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
            Console.Read();

        }

        public static new List<Model.Event> GetCalendarEventsWithPodcasts()
        {

            var events = new List<Model.Event>();
            UserCredential credential;

            using (var stream =
                new FileStream((Directory.GetCurrentDirectory() + "//Google Data/GCalendar/credentials.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = (Directory.GetCurrentDirectory() + "//Google Data/GCalendar/token.json");
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
                    if (eventItem.Attachments.Count == 0) continue;
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

    }
}
