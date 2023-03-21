using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using System.IO;

namespace CSHttpClientSample
{
    static class FakeApiService
    {

        static async void MakeRequest()
        {
            var client = new HttpClient();


            // Request headers

            client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");

            client.DefaultRequestHeaders.Add("NBB-CBSO-Subscription-Key", "fa89275760f1437bb21f33ed9448afd3");
            var uri = "https://ws.uat2.cbso.nbb.be/authentic/deposit/{referenceNumber}/reference";

            var response = await client.GetAsync(uri);

        }
    }
}

