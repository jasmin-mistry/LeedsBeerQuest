using AutoFixture;
using Entities;
using FluentAssertions;

namespace Infrastructure.UnitTests.Data;

public class EfRepositoryDelete : EfRepositoryTestBase
{
    private readonly Fixture _fixture;

    public EfRepositoryDelete()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteExistingRecord()
    {
        // Arrange
        var repository = GetRepository();
        var item = _fixture.Create<Pubs>();
        await repository.AddAsync(item);
        var paymentId = item.Id;

        // Act
        await repository.DeleteAsync(item);

        // Assert
        var payments = await repository.ListAsync<Pubs>();
        payments.Should().NotContain(x => x.Id == paymentId);
    }
}