using Core.Models;
using Entities;

namespace Core.Interfaces;

public interface IPubsSearchService
{
    Task<IEnumerable<Pubs>> SearchPubs(PubsSearchParameters parameters);
}