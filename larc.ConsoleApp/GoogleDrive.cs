using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using larc.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace larc.ConsoleApp
{
    public class GoogleDrive
    {

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Drive API .NET Quickstart";

        public static void GetPictures()
        {
            UserCredential credential;

            using (var stream =
                new FileStream((Directory.GetCurrentDirectory() + "//Google Data/GDrive/credentials.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = (Directory.GetCurrentDirectory() + "//Google Data/GDrive/token.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Retrieve the existing parents folder
            var folderRequest = service.Files.List();
            folderRequest.Q = "name='Podcasts'";
            folderRequest.Spaces = "drive";
            folderRequest.Fields = "nextPageToken, files(id, name)";
            var folterResult = folderRequest.Execute();
            if (folterResult.Files.Count != 1) return;
            var folder = folterResult.Files[0];

            string pageToken = null;
            do
            {
                var request = service.Files.List();
                request.Q = "'" + folder.Id + "' in parents";
                request.Spaces = "drive";
                request.Fields = "nextPageToken, files(id, name, properties)";
                request.PageToken = pageToken;
                var result = request.Execute();
                foreach (var file in result.Files)
                {

                    Console.WriteLine(String.Format(
                            "Found file: {0} ({1})", file.Name, file.Id));

                }
                pageToken = result.NextPageToken;
            } while (pageToken != null);

            Console.Read();
        }

        public static List<Photo> GetAudioFilebyId(string fileId)
        {

            var pictures = new List<Photo>();
            UserCredential credential;

            using (var stream =
                new FileStream((Directory.GetCurrentDirectory() + "//Google Data/GDrive/credentials.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = (Directory.GetCurrentDirectory() + "//Google Data/GDrive/token.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Retrieve the existing parents folder
            var getRequest = service.Files.Get(fileId);

            var folderRequest = service.Files.List();
            folderRequest.Q = "id='" + fileId + "'";
            folderRequest.Spaces = "drive";
            folderRequest.Fields = "nextPageToken, files(id, name)";
            var folterResult = folderRequest.Execute();
            if (folterResult.Files.Count != 1) return pictures;
            var folder = folterResult.Files[0];

            return pictures;

        }

    }
}
