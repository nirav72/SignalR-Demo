using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static HubConnection _hub;
        static IHubProxy _hubProxy;
        static SynchronizationContext _context;

        static void Main(string[] args)
        {
            StartSignalR();

            Console.WriteLine("Hello from Console");

            Console.ReadLine();

            Broadcast();

            Console.ReadLine();

        }

        private static async void Broadcast()
        {
            await _hubProxy.Invoke("Hello", new object[] { "Hello from Console! Yeah you heard me!!" });

        }

        private static async void StartSignalR()
        {
            _hub = new HubConnection("http://localhost:23377");
            _hubProxy = _hub.CreateHubProxy("MessageHub");
            _context = SynchronizationContext.Current;

            await _hub.Start();

            _hubProxy.On<string>("hello", (msg) =>
                Console.WriteLine(msg)
                );

        }
    }
}
