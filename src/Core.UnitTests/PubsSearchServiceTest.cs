using System.Linq.Expressions;
using AutoFixture;
using Core.Models;
using Core.Services;
using Entities;
using FluentAssertions;
using Moq;
using SharedKernel.Interfaces;

namespace Core.UnitTests;

public class PubsSearchServiceTest
{
    private readonly Mock<IRepository> _mockEfRepository;
    private readonly Fixture _fixture;

    public PubsSearchServiceTest()
    {
        _mockEfRepository = new Mock<IRepository>();
        _fixture = new Fixture();
    }

    [Fact]
    public async Task SearchPubs_ShouldReturnExpectedData()
    {
        // Arrange
        var searchCriteria = new PubsSearchParameters
        {
            Address = "Leeds",
            BeerStars = 1d,
            AtmosphereStars = 2d,
            AmenitiesStars = 2.2d,
            ValueStars = 1.1d,
            Tags = "food"
        };
        var list = _fixture.CreateMany<Pubs>(5).ToList();
        _mockEfRepository.Setup(x => x.SearchAsync<Pubs>(It.IsAny<Expression<Func<Pubs, bool>>>())).ReturnsAsync(list);

        var sut = new PubsSearchService(_mockEfRepository.Object);

        // Act
        var result = await sut.SearchPubs(searchCriteria);

        // Assert
        var pubsEnumerable = result.ToList();
        pubsEnumerable.Should().NotBeNull();
        pubsEnumerable.Count().Should().Be(list.Count);
        _mockEfRepository.Verify(x => x.SearchAsync<Pubs>(It.IsAny<Expression<Func<Pubs, bool>>>()),
            Times.AtLeastOnce());
    }
}