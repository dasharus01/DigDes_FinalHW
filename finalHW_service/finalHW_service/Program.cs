using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
//using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace finalHW_service
{
    class Program
    {
        private static int BytsSize = 256000000;
        

        static void Main(string[] args)
        {
            
            var uris = new Uri[1];
            string address = "net.tcp://localhost:7061/CounterService";
            uris[0] = new Uri(address);
            ICounterService icounterservice = new CounterService();
            ServiceHost servicehost = new ServiceHost(icounterservice, uris);
            var binding = new NetTcpBinding(SecurityMode.None);
            //стандартное время устраивает для 256 Мб
            binding.MaxReceivedMessageSize = BytsSize;
            binding.MaxBufferSize = BytsSize;
            servicehost.AddServiceEndpoint(typeof(ICounterService), binding, "");
            servicehost.Opened += Host_Opened;
            servicehost.Open();
            Console.ReadLine();
        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Service start");
        }
    }
}


