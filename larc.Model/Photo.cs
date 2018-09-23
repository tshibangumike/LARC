using System;

namespace larc.Model
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string GoogleDriveLink { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AddedOn { get; set; }
    }
}
