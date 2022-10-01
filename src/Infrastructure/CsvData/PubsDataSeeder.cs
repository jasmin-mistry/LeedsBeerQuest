using AutoMapper;
using Entities;
using Infrastructure.Data;
using SharedKernel.Interfaces;

namespace Infrastructure.CsvData;

public class PubsDataSeeder: IPubsDataSeeder
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PubsDataSeeder(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void SeedData()
    {
        var csvData = BeerQuestCsv.GetData();

        var pubs = _mapper.Map<IEnumerable<Pubs>>(csvData);

        _context.AddRange(pubs);

        _context.SaveChanges();
    }
}
