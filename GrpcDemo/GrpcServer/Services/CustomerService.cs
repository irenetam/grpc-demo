using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            if (request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel> {
                new CustomerModel
                {
                    FirstName = "Jamie",
                    LastName = "Smith",
                    EmailAddress = "tim@jamietimcorey.com",
                    Age = 41,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Sue",
                    LastName = "Storn",
                    EmailAddress = "sue@stony.net",
                    Age = 22,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Bilbo",
                    LastName = "Smith",
                    EmailAddress = "bill@jamietimcorey.com",
                    Age = 99,
                    IsAlive = false
                }
            };

            foreach (var cust in customers)
            {
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
