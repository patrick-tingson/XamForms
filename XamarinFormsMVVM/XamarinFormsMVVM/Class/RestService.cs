using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsMVVM.Class
{
    public class RestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient(); 
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<RestResponse> Transact(HttpVerb httpVerb, string endpoint, string param = "")
        {
            if (endpoint.Length == 0)
                throw new ArgumentException("Invalid Endpoint");  

            if ((httpVerb == HttpVerb.POST) && (param == ""))
                throw new ArgumentException("Invalid Param");


            RestResponse result = new RestResponse();
            HttpResponseMessage response;

            try
            {
                switch (httpVerb)
                {
                    case HttpVerb.GET:
                        response = await client.GetAsync(new Uri(endpoint));
                        break;

                    case HttpVerb.POST:
                        var content = new StringContent(param, Encoding.UTF8, "application/json");
                        response = await client.PostAsync(new Uri(endpoint), content);
                        break;

                    default:
                        throw new ArgumentException("Invalid HttpVerb");
                }

                if (response.IsSuccessStatusCode)
                { 
                    result.Content = await response.Content.ReadAsStringAsync(); 
                }

                result.StatusCode = response.StatusCode.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }

    public enum HttpVerb
    {
        GET,
        POST
    }

    public class RestResponse
    {
        public string StatusCode { get; set; }
        public string Content { get; set; }
    }
}
