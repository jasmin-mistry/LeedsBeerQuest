namespace Core.Models;

public class PubsSearchParameters
{
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public double? RadiusInMeters { get; init; }
    public string? Address { get; init; }
    public double? BeerStars { get; init; }
    public double? AtmosphereStars { get; init; }
    public double? AmenitiesStars { get; init; }
    public double? ValueStars { get; init; }
    public string? Tags { get; init; }
}