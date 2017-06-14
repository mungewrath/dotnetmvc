using System;
using System.Threading.Tasks;
using dotnetmvc.Helper;
using Newtonsoft.Json;
using RestSharp;

public class RestSharpClient {
    // Private Services box
    private IRestClient client = new RestClient("http://jsonplaceholder.typicode.com/posts");

    public T Get<T>(string url, params object[] args) {
        IRestRequest request = new RestRequest(RouteHelper.Format(url, args), Method.GET);
        return JsonConvert.DeserializeObject<T>(client.Get(request).Content);
    }

    public T Post<T>(string url, object requestBody) {
        IRestRequest request = new RestRequest(url, Method.POST);
        request.AddJsonBody(requestBody);
        var result = client.Post(request);
        Console.Write(result.Content);
        return JsonConvert.DeserializeObject<T>(result.Content);
    }
}