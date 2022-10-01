using AutoMapper;
using Entities;
using Infrastructure.CsvData;

namespace Infrastructure.Mappers
{
    public class PubsMapProfile : Profile
    {
        public PubsMapProfile()
        {
            CreateMap<PubsCsvData, Pubs>();
        }
    }
}
