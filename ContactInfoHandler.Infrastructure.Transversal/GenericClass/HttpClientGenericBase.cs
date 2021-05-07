using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ContactInfoHandler.Application.Dto.Base;
using ContactInfoHandler.Infrastructure.Transversal.Configurator;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ContactInfoHandler.Infrastructure.Transversal.GenericClass
{
    public abstract class HttpClientGenericBase<T> : IHttpClientGenericBase<T> where T : DtoBase
    {
        public abstract  string Controller { get; }
        
        private readonly HttpClient _cliente;
        private readonly string baseUrl;

        public HttpClientGenericBase(HttpClient client, IOptions<HttpClientSettings> settings)
        {
            _cliente = client;
            if (settings.Value.GetServiceUrl() == null)
                throw new UriFormatException("La URI no se ha podido establecer");
            baseUrl = settings.Value.GetServiceUrl().ToString();
        }

        public void ValidateNoNullPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("La ruta no es valida");
        }

        public void UnathorizedException(HttpResponseMessage respuesta)
        {
            if ((respuesta.StatusCode.ToString()).ToUpper() == "UNAUTHORIZED")
            {
                throw new Exception("Autorizacion denegada");
            }
        }

        public async Task<IEnumerable<T>> Get(string action)
        {
            ValidateNoNullPath(action);
            var response = await _cliente.GetAsync($"{baseUrl}/{Controller}/{action}").ConfigureAwait(false);
            UnathorizedException(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false));
        }

        public async Task<bool> Post(T request, string action)
        {
            ValidateNoNullPath(action);
            var requestToString = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "aplication/json");
            _cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
            var response = await _cliente.PostAsync($"{baseUrl}/{Controller}/{action}", requestToString).ConfigureAwait(false);
            UnathorizedException(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        public async Task<bool> Put(T request, string action)
        {
            ValidateNoNullPath(action);
            var requestString = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "aplication/json");
            _cliente.DefaultRequestHeaders.Accept.Add((new MediaTypeWithQualityHeaderValue("aplication/json")));
            var response = await _cliente.PutAsync($"{baseUrl}/{Controller}/{action}", requestString).ConfigureAwait(false);
            UnathorizedException(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false));
        }

        public async Task<bool> Patch(T request, string action)
        {
            ValidateNoNullPath(action);
            var stringRequest =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "aplication/json");
            _cliente.DefaultRequestHeaders.Accept.Add((new MediaTypeWithQualityHeaderValue("aplication/json")));
            var response = await _cliente.PatchAsync($"{baseUrl}/{Controller}/{action}", stringRequest).ConfigureAwait(false);
            UnathorizedException(response);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        public Task<bool> Delete(T request, string action)
        {
            throw new System.NotImplementedException();
        }
    }
}