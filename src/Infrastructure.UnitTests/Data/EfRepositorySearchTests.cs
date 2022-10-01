using AutoFixture;
using Entities;
using FluentAssertions;
using SharedKernel.Extensions;

namespace Infrastructure.UnitTests.Data;

public class EfRepositorySearchTests : EfRepositoryTestBase
{
    private readonly Fixture _fixture;

    public EfRepositorySearchTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnFilteredData_WhenSearchCriteriaIsAvailable()
    {
        // Arrange
        var repository = GetRepository();
        const int rowsWithStarsBeer = 5;
        var pubsWithStarsBeer =
            _fixture.Build<Pubs>().With(x => x.StarsBeer, 1.1).CreateMany(rowsWithStarsBeer).ToList();
        await repository.AddRangeAsync(pubsWithStarsBeer);
        var list = _fixture.CreateMany<Pubs>(20).ToList();
        await repository.AddRangeAsync(list);

        var predicate = PredicateBuilder.True<Pubs>();
        predicate = predicate.And(x => x.StarsBeer.CompareTo(1.1d) == 0);

        // Act
        var result = await repository.SearchAsync(predicate);

        // Assert
        var pubsList = result.ToList();
        pubsList.Should().NotBeNull();
        pubsList.Count.Should().Be(rowsWithStarsBeer);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnAllData_WhenNoSearchCriteriaIsPassed()
    {
        // Arrange
        var repository = GetRepository();
        const int totalRows = 25;
        var list = _fixture.CreateMany<Pubs>(totalRows).ToList();
        await repository.AddRangeAsync(list);
        var predicate = PredicateBuilder.True<Pubs>();

        // Act
        var result = await repository.SearchAsync(predicate);

        // Assert
        var pubsList = result.ToList();
        pubsList.Should().NotBeNull();
        pubsList.Count.Should().Be(totalRows);
    }
}