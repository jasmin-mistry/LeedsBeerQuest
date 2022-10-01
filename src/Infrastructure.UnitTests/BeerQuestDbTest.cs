using FluentAssertions;
using Infrastructure.CsvData;

namespace Infrastructure.UnitTests;

public class BeerQuestDbTest
{
    [Fact]
    public void GetAll_ShouldReturnAllData()
    {
        // Arrange

        // Act
        var result = BeerQuestCsv.GetData();

        // Assert
        result.Should().NotBeNullOrEmpty();
    }
}