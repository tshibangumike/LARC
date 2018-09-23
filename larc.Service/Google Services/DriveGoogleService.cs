using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using larc.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;

namespace larc.Service
{
    public class DriveGoogleService
    {

        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Drive API .NET Quickstart";

        public static List<Photo> GetPicturesFromGoogleDrive()
        {

            var pictures = new List<Photo>();

            try
            {
                
                UserCredential credential;

                using (var stream =
                    new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/GoogleDrive/credentials.json"), FileMode.Open, FileAccess.Read))
                {
                    string credPath = HttpContext.Current.Server.MapPath("~/App_Data/GoogleDrive/token.json");
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
                var folderRequest = service.Files.List();
                folderRequest.Q = "name='Pictures'";
                folderRequest.Spaces = "drive";
                folderRequest.Fields = "nextPageToken, files(id, name)";
                var folterResult = folderRequest.Execute();
                if (folterResult.Files.Count != 1) return pictures;
                var folder = folterResult.Files[0];

                string pageToken = null;
                do
                {
                    var request = service.Files.List();
                    request.Q = "mimeType='image/jpeg' and '" + folder.Id + "' in parents";
                    request.Spaces = "drive";
                    request.Fields = "nextPageToken, files(id, name, imageMediaMetadata)";
                    request.PageToken = pageToken;
                    var result = request.Execute();
                    foreach (var file in result.Files)
                    {

                        pictures.Add(new Photo()
                        {
                            GoogleDriveLink = file.Id,
                            AddedOn = file.ImageMediaMetadata.Time
                        });

                    }
                    pageToken = result.NextPageToken;
                } while (pageToken != null);

                return pictures;

            }
            catch (Exception ex)
            {
                
                // Append line to the file.
                using (StreamWriter writer =
                    new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Error/log.text"), true))
                {
                    writer.WriteLine(ex);
                }

                return pictures;

            }

        }

    }
}
