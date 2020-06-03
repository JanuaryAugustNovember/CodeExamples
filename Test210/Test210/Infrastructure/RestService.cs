
using Newtonsoft.Json;

using RestSharp;

using System;
using System.Collections.Generic;
using System.Net;

namespace Test210.Infrastructure
{
    public class RestService
    {
        private RestClient _client;

        public RestService(string url)
        {
            _client = new RestClient(url);
        }

        public U SendRequest<T, U>(T request, Method httpMethod)
        {
            U response;

            var restRequest = new RestRequest(httpMethod);

            restRequest.AddJsonBody(request);
            restRequest.RequestFormat = DataFormat.Json;

            var restResponse = _client.Execute(restRequest);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = restResponse.Content;

                response = JsonConvert.DeserializeObject<U>(responseBody);

                return response;
            }
            else
            {
                throw new Exception(restResponse.Content.ToString());
            }
        }

        public U SendRequest<U>(Method httpMethod)
        {
            U response;

            var restRequest = new RestRequest(httpMethod);

            var restResponse = _client.Get(restRequest);

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = restResponse.Content;

                response = JsonConvert.DeserializeObject<U>(responseBody);

                return response;
            }
            else
            {
                throw new Exception(restResponse.Content.ToString());
            }
        }

        //public List<U> SendRequest<U>(Method httpMethod)
        //{
        //    List<U> response;

        //    var restRequest = new RestRequest(httpMethod);

        //    var restResponse = _client.Get(restRequest);

        //    if (restResponse.StatusCode == HttpStatusCode.OK)
        //    {
        //        var responseBody = restResponse.Content;

        //        response = JsonConvert.DeserializeObject<List<U>>(responseBody);

        //        return response;
        //    }
        //    else
        //    {
        //        throw new Exception(restResponse.Content.ToString());
        //    }
        //}
    }
}