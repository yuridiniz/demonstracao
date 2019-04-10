using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Transporte.Repository.Base.Http
{
    public abstract class BaseHttpAccess
    {
        protected readonly HttpClient http;

        public BaseHttpAccess(HttpClient http)
        {

        }

        protected async Task<T> Get<T>(string url, Dictionary<string, string> header = null)
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, http.BaseAddress + url);

            SetHeader(msg, header);

            using (var result = await http.GetAsync(url))
            {
                result.EnsureSuccessStatusCode();

                var strResult = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(strResult);
            }
        }

        protected async Task<TResult> Post<TResult>(string url, object content, Dictionary<string, string> header = null)
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, http.BaseAddress + url);
            msg.Content = new StringContent(JsonConvert.SerializeObject(content));
            SetHeader(msg, header);

            using (var result = await http.GetAsync(url))
            {
                result.EnsureSuccessStatusCode();

                var strResult = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(strResult);
            }
        }

        protected async Task<TResult> Post<TResult>(string url, string content, Dictionary<string, string> header = null)
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, http.BaseAddress + url);
            msg.Content = new StringContent(content);
            SetHeader(msg, header);

            using (var result = await http.GetAsync(url))
            {
                result.EnsureSuccessStatusCode();

                var strResult = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(strResult);
            }
        }

        private void SetHeader(HttpRequestMessage mensagem, Dictionary<string, string> header)
        {
            if (header != null)
            {
                foreach (var item in header)
                {
                    mensagem.Headers.TryAddWithoutValidation(item.Key, item.Value);
                }
            }
        }
    }
}
