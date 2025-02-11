using Keeper_ApiGateWay.Models.Services;
using Keeper_ApiGateWay.Services.Interfaces;
using System.Text.Json;


namespace Keeper_ApiGateWay.Services.Implemitations
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
            string rawJson = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var data = JsonSerializer.Deserialize<T>(rawJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return ServiceResponse<T?>.Success(data, (int)response.StatusCode);
                }
                catch (Exception)
                {
                    return ServiceResponse<T?>.Fail(default, (int)response.StatusCode, "Ошибка десериализации ответа");
                }
            }
            return ServiceResponse<T?>.Fail(default, (int)response.StatusCode, rawJson);
        }
    }
}

