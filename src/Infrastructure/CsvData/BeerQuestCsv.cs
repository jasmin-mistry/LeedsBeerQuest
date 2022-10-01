using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Entities;

namespace Infrastructure.CsvData
{
    public static class BeerQuestCsv
    {
        public static IEnumerable<PubsCsvData> GetData()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = x => x.Header.ToLower(),
            };
            var csvFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CsvData/leedsbeerquest.csv");

            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, config);
            var result = csv.GetRecords<PubsCsvData>();
            return result.ToList();
        }

    }
}