using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

//can place this is a separate library to reuse
namespace App.Repository.webApi.ApiClient
{
    //helper class
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string baseUrl;
        private readonly HttpClient httpClient;
        public WebApiExecuter(string baseUrl, HttpClient httpClient)
        {
            this.baseUrl = baseUrl;
            this.httpClient = httpClient;

            //initialize
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> InvokeGet<T>(string uri)
        {
            //gets content from reponse and returns it in type T
            return await httpClient.GetFromJsonAsync<T>(GetUrl(uri));
        }

        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            var response = await httpClient.PostAsJsonAsync(GetUrl(uri), obj);

            //will throw exception if response is not successful
            //response.EnsureSuccessStatusCode();

            //handle exception to show error message thrown back to caller
            await HandleError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokePut<T>(string uri, T obj)
        {
            var response = await httpClient.PutAsJsonAsync(GetUrl(uri), obj);
            await HandleError(response);

        }

        public async Task InvokeDelete(string uri)
        {
            var response = await httpClient.DeleteAsync(GetUrl(uri));
            await HandleError(response);

        }


        private string GetUrl(string uri)
        {
            return $"{baseUrl}/{uri}";
        }

        //will include a detailed error message in the exception thrown back
        private async Task HandleError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }
        }






    }
}
