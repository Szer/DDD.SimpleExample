using System;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace DDD.SimpleExample.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //PostCustomerAsync().Wait();
            var guid = new Guid("352d0f45-4f5d-4bc5-8690-165879877a6d");
            PostProjectAsync(guid).Wait();
            //MakeCustomerInactiveAsync(guid).Wait();
            //RenameProjectAsync(new Guid("a163e89c-685d-482b-9063-a41874cf204f"), "FunnyOne").Wait();
        }

        static async Task PostCustomerAsync()
        {
            var restClient = new RestClient("http://localhost:45396/")
            {
                Authenticator = new NtlmAuthenticator()
            };
            var request = new RestRequest("api/customer", Method.POST);
            request.AddParameter("Id", Guid.NewGuid());
            request.AddParameter("Name", "NewOne");
            var response = await restClient.ExecuteTaskAsync(request);
            Console.WriteLine(response.Content);
            Console.ReadKey();
        }

        static async Task PostProjectAsync(Guid customerId)
        {
            var restClient = new RestClient("http://localhost:45396/")
            {
                Authenticator = new NtlmAuthenticator()
            };
            var request = new RestRequest("api/project", Method.POST);
            request.AddParameter("ProjectId", Guid.NewGuid());
            request.AddParameter("CustomerId", customerId);
            request.AddParameter("Name", "NewProject");
            var response = await restClient.ExecuteTaskAsync(request);
            Console.WriteLine(response.Content);
            Console.ReadKey();
        }

        static async Task RenameCustomerAsync(Guid id, string newName)
        {
            var restClient = new RestClient("http://localhost:45396/")
            {
                Authenticator = new NtlmAuthenticator()
            };
            var request = new RestRequest($"api/customer/{id.ToString()}/name", Method.POST);
            request.AddParameter("NewName", newName);
            var response = await restClient.ExecuteTaskAsync(request);
            Console.WriteLine(response.Content);
            Console.ReadKey();
        }

        static async Task RenameProjectAsync(Guid id, string newName)
        {
            var restClient = new RestClient("http://localhost:45396/")
            {
                Authenticator = new NtlmAuthenticator()
            };
            var request = new RestRequest($"api/project/{id.ToString()}/name", Method.POST);
            request.AddParameter("NewName", newName);
            var response = await restClient.ExecuteTaskAsync(request);
            Console.WriteLine(response.Content);
            Console.ReadKey();
        }

        static async Task MakeCustomerInactiveAsync(Guid id)
        {
            var restClient = new RestClient("http://localhost:45396/")
            {
                Authenticator = new NtlmAuthenticator()
            };
            var request = new RestRequest($"api/customer/{id.ToString()}/makeinactive", Method.POST);
            var response = await restClient.ExecuteTaskAsync(request);
            Console.WriteLine(response.Content);
            Console.ReadKey();
        }

        static async Task MakeProjectInactiveAsync(Guid id)
        {
            var restClient = new RestClient("http://localhost:45396/")
            {
                Authenticator = new NtlmAuthenticator()
            };
            var request = new RestRequest($"api/project/{id.ToString()}/makeinactive", Method.POST);
            var response = await restClient.ExecuteTaskAsync(request);
            Console.WriteLine(response.Content);
            Console.ReadKey();
        }
    }
}