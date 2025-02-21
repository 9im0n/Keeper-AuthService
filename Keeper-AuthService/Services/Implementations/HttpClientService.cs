using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;


namespace Keeper_AuthService.Services.Implementations
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<ServiceResponse<T?>> GetAsync<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                return await ProcessResponse<T>(response);
            }
            catch (Exception ex)
            {
                return ServiceResponse<T?>.Fail(default, 500, ex.Message);
            }
        }


        public async Task<ServiceResponse<TResponse?>> PostAsync<TRequest, TResponse>(string url, TRequest data)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, data);
                return await ProcessResponse<TResponse>(response);
            }
            catch (Exception ex)
            {
                return ServiceResponse<TResponse?>.Fail(default, 500, ex.Message);
            }
        }


        public async Task<ServiceResponse<TResponse?>> PutAsync<TRequest, TResponse>(string url, TRequest data)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync(url, data);
                return await ProcessResponse<TResponse>(response);
            }
            catch (Exception ex)
            {
                return ServiceResponse<TResponse?>.Fail(default, 500, ex.Message);
            }
        }


        public async Task<ServiceResponse<T?>> DeleteAsync<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(url);
                return await ProcessResponse<T>(response);
            }
            catch (Exception ex)
            {
                return ServiceResponse<T?>.Fail(default, 500, ex.Message);
            }
        }


        private async Task<ServiceResponse<T?>> ProcessResponse<T>(HttpResponseMessage response)
        {
            try
            {
                string rawJson = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(rawJson);
                JsonElement root = doc.RootElement;

                if (!root.TryGetProperty("message", out JsonElement message))
                    return ServiceResponse<T?>.Fail(default, 400, "response doesn't have a message field.");

                if (!response.IsSuccessStatusCode)
                    return ServiceResponse<T?>.Fail(default, (int)response.StatusCode, message.GetString());

                if (!root.TryGetProperty("data", out JsonElement dataElement))
                    return ServiceResponse<T?>.Fail(default, 400, "response doesn't have a data field.");

                T? data = JsonSerializer.Deserialize<T>(dataElement.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                return ServiceResponse<T?>.Success(data, (int)response.StatusCode, message.GetString());
            }
            catch (Exception ex)
            {
                return ServiceResponse<T?>.Fail(default, 500, $"Auth Service: {ex.Message}");
            }
           
        }

    }
}

