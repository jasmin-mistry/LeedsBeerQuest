using System.Linq.Expressions;
using Core.Interfaces;
using Core.Models;
using Entities;
using Geolocation;
using SharedKernel.Extensions;
using SharedKernel.Interfaces;

namespace Core.Services;

public class PubsSearchService : IPubsSearchService
{
    private readonly IRepository _repository;

    public PubsSearchService(IRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Pubs>> SearchPubs(PubsSearchParameters parameters)
    {
        var predicate = PredicateBuilder.True<Pubs>();

        predicate = AddLocationCriteria(parameters, predicate);

        if (!string.IsNullOrEmpty(parameters.Address))
            predicate = predicate.And(x => x.Address.Contains(parameters.Address));

        if (parameters.BeerStars is not null) predicate = predicate.And(x => x.StarsBeer >= parameters.BeerStars);

        if (parameters.AtmosphereStars is not null)
            predicate = predicate.And(x => x.StarsAtmosphere >= parameters.AtmosphereStars);

        if (parameters.AmenitiesStars is not null)
            predicate = predicate.And(x => x.StarsAmenities >= parameters.AmenitiesStars);

        if (parameters.ValueStars is not null) predicate = predicate.And(x => x.StarsValue >= parameters.ValueStars);

        if (!string.IsNullOrEmpty(parameters.Tags)) predicate = predicate.And(x => x.Tags.Contains(parameters.Tags));

        return _repository.SearchAsync(predicate);
    }

    private static Expression<Func<Pubs, bool>> AddLocationCriteria(PubsSearchParameters parameters,
        Expression<Func<Pubs, bool>> predicate)
    {
        if (parameters.Latitude is null || parameters.Longitude is null || parameters.RadiusInMeters is null)
            return predicate;

        var boundaries = new CoordinateBoundaries(parameters.Latitude.Value, parameters.Longitude.Value,
            parameters.RadiusInMeters.Value, DistanceUnit.Meters);

        var minLatitude = boundaries.MinLatitude;
        var maxLatitude = boundaries.MaxLatitude;
        var minLongitude = boundaries.MinLongitude;
        var maxLongitude = boundaries.MaxLongitude;

        predicate = predicate.And(x => x.Latitude >= minLatitude && x.Latitude <= maxLatitude);
        predicate = predicate.And(x => x.Longitude >= minLongitude && x.Longitude <= maxLongitude);

        return predicate;
    }
}