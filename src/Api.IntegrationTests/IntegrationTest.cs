using System.Text;
using AutoFixture;
using Newtonsoft.Json;

namespace Api.IntegrationTests;

[Collection("Api")]
public abstract class IntegrationTest
{
    private const string ApplicationJsonContentType = "application/json";
    private readonly HttpClient _client;

    protected IntegrationTest(IntegrationTestFixture fixture)
    {
        Factory = fixture.Factory;
        _client = Factory.CreateClient();
        Services = Factory.Services;
    }

    protected Fixture Fixture { get; } = new();

    protected ApiWebApplicationFactory<Startup> Factory { get; set; }
    protected IServiceProvider Services { get; set; }

    protected async Task<HttpResponseMessage> Get(string url)
    {
        return await _client.GetAsync(url);
    }

    protected Task<HttpResponseMessage> Delete(string url)
    {
        return _client.DeleteAsync(url);
    }

    protected async Task<string> GetJson(string url)
    {
        var response = await _client.GetAsync(url);
        var body = await response.Content.ReadAsStringAsync();
        return body;
    }

    protected async Task<T> GetJsonAs<T>(string url)
    {
        var body = await GetJson(url);
        var result = JsonConvert.DeserializeObject<T>(body);
        return result;
    }

    protected async Task<HttpResponseMessage> PostJson(string url, dynamic jToken)
    {
        var message = GetPayloadString(jToken.ToString());
        var response = await _client.PostAsync(url, message);
        return response;
    }

    protected async Task<T> GetContentAs<T>(HttpResponseMessage response)
    {
        var body = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(body, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto
        });
        return result;
    }

    protected async Task<HttpResponseMessage> PostJson(string url)
    {
        var response = await _client.PostAsync(url, new StringContent(string.Empty));
        return response;
    }

    protected async Task<HttpResponseMessage> PutJson(string url, dynamic jToken)
    {
        var message = GetPayloadString(jToken);
        var response = await _client.PutAsync(url, message);
        return response;
    }

    private static StringContent GetPayloadString(string payload)
    {
        return new StringContent(payload, Encoding.UTF8, ApplicationJsonContentType);
    }

    protected async Task<HttpResponseMessage> PatchJson(string url, dynamic jToken)
    {
        return await PatchJson(url, jToken.ToString());
    }

    protected async Task<HttpResponseMessage> PatchJson(string url, string payload)
    {
        var message = GetPayloadString(payload);
        var response = await _client.PatchAsync(url, message);
        return response;
    }
}