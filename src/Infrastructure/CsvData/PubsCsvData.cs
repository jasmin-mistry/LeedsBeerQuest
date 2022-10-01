using CsvHelper.Configuration.Attributes;
using Entities.Interfaces;

namespace Infrastructure.CsvData
{
    public class PubsCsvData : IPubData
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public string Excerpt { get; set; }
        public string Thumbnail { get; set; }
        [Name("lat")]
        public double Latitude { get; set; }
        [Name("lng")]
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Twitter { get; set; }

        [Name("Stars_beer")]
        public double StarsBeer { get; set; }
        [Name("Stars_atmosphere")]
        public double StarsAtmosphere { get; set; }
        [Name("Stars_amenities")]
        public double StarsAmenities { get; set; }
        [Name("Stars_value")]
        public double StarsValue { get; set; }
        public string Tags { get; set; }
    }
}