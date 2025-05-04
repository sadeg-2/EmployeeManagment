
using System.Net.Http.Json;
using BaseLibrary.Responses;
using Client.Library.Helpers;
using Client.Library.Services.Contracts;

namespace Client.Library.Services.Implementaions
{
    public class GenericServiceImplementaion<T>(GetHttpClient getHttpClient) : IGenericServieInterface<T>
    {
        // Create
        public async Task<GeneralResponse> Insert(T item, string baseUrl)
        {
            var httpclient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpclient.PostAsJsonAsync($"{baseUrl}/add",item);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
        // Read All
        public async Task<List<T>> GetAll(string baseUrl)
        {
            var httpclient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpclient.GetFromJsonAsync<List<T>>($"{baseUrl}/all");
            return result!;
        }
        // Read Single {id}
        public async Task<T> GetById(int id, string baseUrl)
        {
            var httpclient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpclient.GetFromJsonAsync<T>($"{baseUrl}/single/{id}");
            return result!;
        }
        // Update {item}
        public async Task<GeneralResponse> Update(T item, string baseUrl)
        {
            var httpclient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpclient.PutAsJsonAsync($"{baseUrl}/update",item);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
        // Delete {id}
        public async Task<GeneralResponse> DeleteById(int id, string baseUrl)
        {
            var httpclient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpclient.DeleteAsync($"{baseUrl}/delete/{id}");
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

      
    }
}
