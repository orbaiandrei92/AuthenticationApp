using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataMarket.Infrastructure
{
    public class ApiCaller : IApiCaller
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

        public async Task<TReturn> PostJsonWithAuthorization2<TRequest, TReturn>(string baseUrl, string method, int userId, TRequest request, string token)
          where TRequest : class
          where TReturn : class
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

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

        public async Task PostJsonWithAuthorization<TRequest>(string baseUrl, string method, TRequest request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                response = await client.PostAsync(method, request, new NoCharSetJsonMediaTypeFormatter());
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiClientException(client.BaseAddress, method, response.StatusCode);
                }
            }
        }


        //public async Task<TReturn> PostWithAuthorization<TRequest>(string baseUrl, string method, int userId, TRequest request, string token = "")
        //   where TRequest : class
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage response = new HttpResponseMessage();
        //        client.BaseAddress = new Uri(baseUrl);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

        //        response = await client.PostAsync(method, request, new NoCharSetJsonMediaTypeFormatter());
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            throw new ApiClientException(client.BaseAddress, method, response.StatusCode);
        //        }
        //    }
        //}




        public async Task<TReturn> Get<TReturn>(string baseUrl, string method)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                response = await client.GetAsync(method);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiClientException(client.BaseAddress, method, response.StatusCode);
                }
                string bodyMessage = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TReturn>(bodyMessage);
            }
        }

        public async Task<TReturn> GetWithAuthorization<TReturn>(string baseUrl, string method, string token = "")
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

        public T GetWithAuthorizationn<T>(string baseUrl, string method, string token = "")
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);

                response = client.GetAsync(method).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiClientException(client.BaseAddress, method, response.StatusCode);
                }
                string bodyMessage = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(bodyMessage);
            }
        }

        public Task PostWithAuthorization<TRequest>(string baseUrl, string method, int userId, TRequest request, string token = "") where TRequest : class
        {
            throw new NotImplementedException();
        }

        public async Task<T> PostWithAuthorization2<TRequest, T>(string baseUrl, string method, int userId, TRequest request, string token = "")
            where TRequest : class         
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);


                response = await client.PostAsync(method, request, new NoCharSetJsonMediaTypeFormatter());

                string bodyMessage = await response.Content.ReadAsStringAsync();
                string requestBody = JsonConvert.SerializeObject(request);
                var x = new Response()
                {
                    CountValue = int.Parse(bodyMessage)
                };

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiClientException(client.BaseAddress, response.StatusCode, method, requestBody, bodyMessage);
                }

                return JsonConvert.DeserializeObject<T>(bodyMessage);
            }
        }

        public class Response
        {
            public int CountValue { get; set; }
        }

        public Task<TReturn> PostWithAuthorization<TRequest, TReturn>(string baseUrl, string method, int userId, TRequest request, string token = "")
            where TRequest : class
            where TReturn : class
        {
            throw new NotImplementedException();
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