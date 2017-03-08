using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace XamarinRestAPI.Services
{
    public abstract class RestClient
    {
        protected string BaseUrl { get; set; }

        protected RestClient(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<string> GetData()
        {
            //realiza chama assincrona a url solicitada
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetStringAsync(BaseUrl).ConfigureAwait(false);
            }
        }

        protected async Task<IEnumerable<T>> GetAsJson<T>()
            where T : class
        {
            var result = Enumerable.Empty<T>();

            using (var httpClient = new HttpClient())
            {
                //adiona header na requisição
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
                //realiza chama assincrona a url solicitada
                var response = await httpClient.GetAsync(BaseUrl).ConfigureAwait(false);

                //verificar o status da resposta
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        //deserealiza o objeto json no tipo da classe
                        result = await Task.Run(() =>
                        {
                            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
                        }).ConfigureAwait(false);
                    }
                }
            }

            //retorna a objeto preenchido
            return result;
        }

        protected string FromUrl(string baseUrl, string resource)
        {
            return string.Join("/", baseUrl, resource);
        }
    }
}
