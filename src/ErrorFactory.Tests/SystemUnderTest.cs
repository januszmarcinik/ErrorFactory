using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ErrorFactory.Domain;
using Newtonsoft.Json;

namespace ErrorFactory.Tests
{
    public class SystemUnderTest : IDisposable
    {
        private readonly HttpClient _httpClient;
        public ISubjectsRepository SubjectsRepository { get; }

        public SystemUnderTest(HttpClient httpClient, ISubjectsRepository subjectsRepository)
        {
            _httpClient = httpClient;
            SubjectsRepository = subjectsRepository;
        }

        public async Task<ApiResult<T>> Get<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            return await GetApiResult<T>(response);
        }
        
        public async Task<ApiResult> Post(string url, object command = null)
        {
            var body = CreateBody(command);
            var response = await _httpClient.PostAsync(url, body);
            return await GetApiResult(response);
        }
        
        public async Task<ApiResult> Delete(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            return await GetApiResult(response);
        }

        private static async Task<ApiResult> GetApiResult(HttpResponseMessage response)
        {
            var stringContent = await response.Content.ReadAsStringAsync();
            return new ApiResult(response.StatusCode, stringContent);
        }

        private static async Task<ApiResult<T>> GetApiResult<T>(HttpResponseMessage response)
        {
            var stringContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                return new ApiResult<T>(response.StatusCode, stringContent, default);
            }
            
            var value = JsonConvert.DeserializeObject<T>(stringContent);
            return new ApiResult<T>(response.StatusCode, "", value);
        }
        
        private static StringContent CreateBody(object command)
        {
            if (command == null)
            {
                return new StringContent("", Encoding.UTF8, "application/json");
            }
            
            var json = JsonConvert.SerializeObject(command);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}