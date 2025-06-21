﻿using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AutoLot.Services.ApiWrapper.Base;

public abstract class ApiServiceWrapperBase<TEntity> : IApiServiceWrapperBase<TEntity> where TEntity : BaseEntity, new()
{
    protected readonly HttpClient Client;
    private readonly string _endPoint;
    protected readonly ApiServiceSettings ApiSettings;
    protected readonly string ApiVersion;

    protected ApiServiceWrapperBase(
        HttpClient client,
        IOptionsMonitor<ApiServiceSettings> apiSettingsMonitor,
        string endPoint)
    {
        Client = client;
        _endPoint = endPoint;
        ApiSettings = apiSettingsMonitor.CurrentValue;
        ApiVersion = ApiSettings.ApiVersion;
        Client.BaseAddress = new Uri(ApiSettings.Uri);
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var authToken =
            Convert.ToBase64String(Encoding.UTF8.GetBytes(
                $"{apiSettingsMonitor.CurrentValue.UserName}:{apiSettingsMonitor.CurrentValue.Password}"));
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", authToken);
    }

    internal async Task<HttpResponseMessage> PostAsJsonAsync(string uri, string json)
    {
        return await Client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
    }

    internal async Task<HttpResponseMessage> PutAsJsonAsync(string uri, string json)
    {
        return await Client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
    }

    internal async Task<HttpResponseMessage> DeleteAsJsonAsync(string uri, string json)
    {
        var request = new HttpRequestMessage
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
            Method = HttpMethod.Delete,
            RequestUri = new Uri(uri)
        };

        return await Client.SendAsync(request);
    }

    public async Task<IList<TEntity>> GetAllEntitiesAsync()
    {
        var response = await Client.GetAsync($"{ApiSettings.Uri}{_endPoint}?v={ApiVersion}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IList<TEntity>>();
        return result;
    }

    public async Task<TEntity> GetEntityAsync(int id)
    {
        var response = await Client.GetAsync($"{ApiSettings.Uri}{_endPoint}/{id}?v={ApiVersion}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<TEntity>();
        return result;
    }

    public async Task<TEntity> AddEntityAsync(TEntity entity)
    {
        var response = await PostAsJsonAsync($"{ApiSettings.Uri}{_endPoint}?v={ApiVersion}", JsonSerializer.Serialize(entity));

        if (response is null)
        {
            throw new Exception("Unable to communicate with the service");
        }

        var result = await response.Content.ReadFromJsonAsync<TEntity>() ?? await GetEntityAsync(entity.Id);
        return result;
    }

    public async Task<TEntity> UpdateEntityAsync(TEntity entity)
    {
        var response = await PutAsJsonAsync($"{ApiSettings.Uri}{_endPoint}/{entity.Id}?v={ApiVersion}", JsonSerializer.Serialize(entity));
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TEntity>() ?? await GetEntityAsync(entity.Id);
        return result;
    }

    public async Task DeleteEntityAsync(TEntity entity)
    {
        var response = await DeleteAsJsonAsync($"{ApiSettings.Uri}{_endPoint}/{entity.Id}?v={ApiVersion}", JsonSerializer.Serialize(entity));
        response.EnsureSuccessStatusCode();
    }
}
