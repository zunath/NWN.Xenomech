namespace XM.App.Database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Starting redis socket server.");
            var socketServer = new RedisSocketServer("redis");
            socketServer.Start();

            Console.WriteLine($"Redis socket server shutting down...");
        }
    }
}
