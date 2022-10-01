using AutoFixture;
using Entities;
using FluentAssertions;

namespace Infrastructure.UnitTests.Data;

public class EfRepositoryAddTests : EfRepositoryTestBase
{
    private readonly Fixture _fixture;

    public EfRepositoryAddTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public async Task AddAsync_ShouldAddNewRecordWithNewId()
    {
        // Arrange
        var repository = GetRepository();
        var item = _fixture.Create<Pubs>();

        // Act
        await repository.AddAsync(item);

        // Arrange
        var newItem = (await repository.ListAsync<Pubs>()).FirstOrDefault();
        newItem.Should().NotBeNull();
        newItem.Should().BeSameAs(item);
        newItem?.Id.ToString().Should().NotBeNull();
    }
}