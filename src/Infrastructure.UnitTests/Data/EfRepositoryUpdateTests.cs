using AutoFixture;
using Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitTests.Data;

public class EfRepositoryUpdateTests : EfRepositoryTestBase
{
    private readonly Fixture _fixture;

    public EfRepositoryUpdateTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingRecord()
    {
        // Arrange
        var repository = GetRepository();
        const string name = "LeedsPub";
        var item = _fixture.Build<Pubs>().With(x => x.Name, name).Create();
        await repository.AddAsync(item);
        DbContext.Entry(item).State = EntityState.Detached;
        var newItem = (await repository.ListAsync<Pubs>())
            .FirstOrDefault(x => x.Name == name);
        newItem.Should().NotBeNull();
        newItem.Should().NotBeSameAs(item);
        const string newName = "LeedsUnitedPub";
        newItem.Name = newName;

        // Act
        await repository.UpdateAsync(newItem);

        // Assert
        var updatedItem = (await repository.GetByIdAsync<Pubs>(newItem.Id));

        updatedItem.Should().NotBeNull();
        updatedItem?.Name.Should().NotBeSameAs(item.Name);
        updatedItem?.Id.Should().Be(newItem.Id);
    }
}