using gRPC.Core.Server;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace gRPC.Core.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //await CheckDefault(channel);
            //await GetEmployee(channel);
            await GetEmployeeList(channel);
        }

        private static async Task CheckDefault(GrpcChannel channel)
        {
            var client = new Greeter.GreeterClient(channel);

            var message = "";
            Console.WriteLine("Type your name: ");
            message = Console.ReadLine();
            
            while (true)
            { 
                var reply = await client.SayHelloAsync(
                             new HelloRequest { Name = $"{message}" });
                Console.WriteLine("Greeting: " + reply.Message);
                Console.WriteLine("Press 'exit' to exit... or type your name : ");
                message = Console.ReadLine();
                if(message.ToLower() == "exit")
                {
                    break;
                }
            } 
        }


        private static async Task GetEmployee(GrpcChannel channel)
        {
            int id = 0;
            Console.WriteLine("Enter customer id (must be number): ");
            int.TryParse(Console.ReadLine(), out id);

            var client = new Emp.EmpClient(channel);

            var requestModel = new EmployeeRequest { Id = id };
            var employee = await client.GetEmployeeAsync(requestModel);

            if (employee is null) Console.WriteLine($"No employee with id {id}");
            else
            {
                Console.WriteLine($"Employee Detail with id {id}");
                Console.WriteLine($"Name : {employee.FirstName} {employee.LastName}");
                Console.WriteLine($"Date : {employee.Date}"); 
            }

            Console.ReadKey();
        }
        private static async Task GetEmployeeList(GrpcChannel channel)
        {
            var client = new Emp.EmpClient(channel);
            using (var call = client.GetEmployeeList(new EmployeeRequest()))
            {
                while(await call.ResponseStream.MoveNext())
                {
                    var employee = call.ResponseStream.Current;
                    Console.WriteLine($"Employee Detail with id {employee.Id}");
                    Console.WriteLine($"Name : {employee.FirstName} {employee.LastName}");
                    Console.WriteLine($"----------------------------------------------------");
                }
            }

            Console.ReadKey();
        }

    }
}
