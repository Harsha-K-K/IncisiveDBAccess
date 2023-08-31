using System;
using System.ServiceModel;

namespace IncisiveDBAccessor
{
    internal class Program
    {
        private const string Address = "net.tcp://localhost:6565/IncisiveAccessor";

        static void Main(string[] args)
        {
            var uris = new Uri(Address);
            var accessor = new IncisiveAccessor();
            var bindings = new NetTcpBinding(SecurityMode.None);
            bindings.MaxReceivedMessageSize = int.MaxValue;

            var host = new ServiceHost(accessor, uris);
            host.AddServiceEndpoint(typeof(IIncisiveAccessor.IIncisiveAccessor), bindings, "");
            host.Opened += Host_Opened;
            host.Open();

            Console.ReadLine();
        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("IncisiveDBAccessor service started");
        }
    }
}
