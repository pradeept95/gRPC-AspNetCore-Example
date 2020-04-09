using gRPC.Core.Server;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Client.SharedProto
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
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
                if (message.ToLower() == "exit")
                {
                    break;
                }
            }
        }
    }
}
