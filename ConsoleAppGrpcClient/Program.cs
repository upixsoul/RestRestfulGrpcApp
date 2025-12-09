using Grpc.Net.Client;
using GrpcService1;

namespace ConsoleAppGrpcClient;

class Program
{
    static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5169");

        var client = new Greeter.GreeterClient(channel);
        var reply = await client.SayHelloAsync(new HelloRequest
        {
            Name = "David"
        });

        Console.WriteLine("Respuesta del servidor: " + reply.Message);
    }
}