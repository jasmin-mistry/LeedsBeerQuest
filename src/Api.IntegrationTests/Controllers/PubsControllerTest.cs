using System.Net;
using Core.Models;
using FluentAssertions;
using Newtonsoft.Json;

namespace Api.IntegrationTests.Controllers;

public class PubsControllerTest : IntegrationTest
{
    private const string ApiUrl = "/api/pubs";

    public PubsControllerTest(IntegrationTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task PostSearch_ReturnsNoContent()
    {
        // Arrange
        var searchCriteria = new PubsSearchParameters { BeerStars = 100000 };

        // Act
        var response = await PostSearch(searchCriteria);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task PostSearch_ReturnsPubsWithinTheRadiusOfLocation()
    {
        // Arrange
        var searchCriteria = new PubsSearchParameters
        {
            Latitude = 53.799468,
            Longitude = -1.545427,
            RadiusInMeters = 200
        };

        // Act
        var response = await PostSearch(searchCriteria);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostSearch_ReturnsBadRequest()
    {
        // Arrange
        // Act
        var response = await PostJson(ApiUrl, "");

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private async Task<HttpResponseMessage> PostSearch(PubsSearchParameters body)
    {
        var payload = JsonConvert.SerializeObject(body);
        var response = await PostJson(ApiUrl, payload);
        return response;
    }
}