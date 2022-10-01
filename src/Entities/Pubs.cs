using Entities.Interfaces;
using SharedKernel;

namespace Entities;

public class Pubs : BaseEntity, IPubData
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string Url { get; set; }
    public DateTime Date { get; set; }
    public string Excerpt { get; set; }
    public string Thumbnail { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Twitter { get; set; }
    public double StarsBeer { get; set; }
    public double StarsAtmosphere { get; set; }
    public double StarsAmenities { get; set; }
    public double StarsValue { get; set; }
    public string Tags { get; set; }
}