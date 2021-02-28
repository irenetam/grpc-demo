using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest { Name = "Tim" };
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(input);
            //Console.WriteLine(reply.Message);

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var input = new HelloRequest { Name = "Tim" };
            var client1 = new Greeter.GreeterClient(channel);
            var reply = await client1.SayHelloAsync(input);
            Console.WriteLine(reply.Message);
            var customerClient = new Customer.CustomerClient(channel);
            var clientRequested = new CustomerLookupModel { UserId = 1 };
            var customer = await customerClient.GetCustomerInfoAsync(clientRequested);
            Console.WriteLine($"{customer.FirstName} {customer.LastName}");

            Console.WriteLine("New customer list");
            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while(await call.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                {
                    var currenyCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"{currenyCustomer.FirstName} {currenyCustomer.LastName}: { currenyCustomer.EmailAddress}");
                }
            }
            
            Console.ReadLine();
        }
    }
}
