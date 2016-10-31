using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LoginLibrary.Helpers
{
    public class ApiCaller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="baseUrl"></param>
        /// <param name="method"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TReturn> PostJson<TRequest, TReturn>(string baseUrl, string method, TRequest request)
            where TRequest : class
            where TReturn : class
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                response = await client.PostAsync(method, request, new NoCharSetJsonMediaTypeFormatter());

                string bodyMessage = await response.Content.ReadAsStringAsync();
                string requestBody = JsonConvert.SerializeObject(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiClientException(client.BaseAddress, response.StatusCode, method, requestBody, bodyMessage);
                }

                return JsonConvert.DeserializeObject<TReturn>(bodyMessage);
            }
        }

        public async Task<TReturn> PostJsonToken<TReturn>(string baseUrl, string userName, string password)
            where TReturn: class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                // We want the response to be JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Build up the data to POST.
                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("grant_type", "password"));
                postData.Add(new KeyValuePair<string, string>("username", userName));
                postData.Add(new KeyValuePair<string, string>("password", password));

                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

                // Post to the Server and parse the response.
                HttpResponseMessage response = await client.PostAsync("token", content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiClientException(client.BaseAddress, "token", response.StatusCode);
                }
                string jsonString = await response.Content.ReadAsStringAsync();
                object responseData = JsonConvert.DeserializeObject(jsonString);

                // return the Access Token.
                return ((dynamic)responseData).access_token;
            }
        }

        public async Task PostJson<TRequest>(string baseUrl, string method, TRequest request)
            where TRequest : class
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                response = await client.PostAsync(method, request, new NoCharSetJsonMediaTypeFormatter());
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiClientException(client.BaseAddress, method, response.StatusCode);
                }
            }
        }

        public async Task<TReturn> Get<TReturn>(string baseUrl, string method, string token="")
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                response = await client.GetAsync(method);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiClientException(client.BaseAddress, method, response.StatusCode);
                }
                string bodyMessage = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TReturn>(bodyMessage);
            }
        }

        [Serializable]
        public class ApiClientException : Exception
        {
            public ApiClientException(Uri uri, string method, HttpStatusCode code)
                : base(string.Format("Non-successful status code returned from {0}, method {1}: {2} - {3}", uri.AbsoluteUri, method, (int)code, code))
            {
                this.Uri = uri;
                this.Code = code;
            }

            public ApiClientException(Uri uri, HttpStatusCode code, string method, string requestBody, string responseBody)
                : base(string.Format("Non-successful status code returned from {0}, method {1}: {2} - {3}", uri.AbsoluteUri, method, (int)code, code))
            {
                this.Uri = uri;
                this.Code = code;
                this.RequestBody = requestBody;
                this.ResponseBody = responseBody;
            }

            public Uri Uri { get; private set; }
            public HttpStatusCode Code { get; private set; }
            public string RequestBody { get; set; }
            public string ResponseBody { get; set; }
        }
    }
}
