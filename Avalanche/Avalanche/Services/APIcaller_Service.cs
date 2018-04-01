using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace Avalanche.Services
{
    public class APIcaller_Service
    {
        public void GetRequest(string endnode)
        {
            var client = new RestClient(endnode);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "71ac3e8e-7c4b-4731-a203-47fd29b4bd31");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);
        }


        public void PostRequest(string endnode, string post)
        {
            var client = new RestClient(endnode);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "cf2a0a17-df25-4585-9a57-cc6ec09eca5 2");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddParameter("undefined", post, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }
    }
}
