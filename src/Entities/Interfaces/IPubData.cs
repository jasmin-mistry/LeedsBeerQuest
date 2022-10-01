namespace Entities.Interfaces;

public interface IPubData
{
    string Name { get; set; }
    string Category { get; set; }
    string Url { get; set; }
    DateTime Date { get; set; }
    string Excerpt { get; set; }
    string Thumbnail { get; set; }
    double Latitude { get; set; }
    double Longitude { get; set; }
    string Address { get; set; }
    string Phone { get; set; }
    string Twitter { get; set; }
    double StarsBeer { get; set; }
    double StarsAtmosphere { get; set; }
    double StarsAmenities { get; set; }
    double StarsValue { get; set; }
    string Tags { get; set; }
}