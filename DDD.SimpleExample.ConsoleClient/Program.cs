using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DDD.SimpleExample.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            PostCustomerAsync().Wait();
        }

        static async Task PostCustomerAsync()
        {
            using (var handler = new HttpClientHandler {UseDefaultCredentials = true})
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("http://localhost:45396/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var gizmo = new {Id = Guid.NewGuid().ToString(), Name = "Super"};
                var gizmoJson = JsonConvert.SerializeObject(gizmo, Formatting.Indented);
                var contentPost = new StringContent(gizmoJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/customer", contentPost);
                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.
                    Uri gizmoUrl = response.Headers.Location;
                }
            }
        }
    }
}
