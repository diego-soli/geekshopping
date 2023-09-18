using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace GeekShopping.Web.Utils
{
    /// <summary>
    /// Extension criada para fazer requisição assicrona a API.
    /// </summary>
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) throw new ApplicationException($"Somenthing went wrong calling the API: " +
                $"{response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //TODO verificar pois pode ser que retorne null, deve testar.
            return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
        }


        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PostAsync(url, content);
        }   
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PostAsync(url, content);
        }


    }



}
