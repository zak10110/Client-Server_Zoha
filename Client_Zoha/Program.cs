using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client_Zoha
{
    class Program
    {
        static string ipAddr = "127.0.0.1";
        static int port = 8000;
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ipAddr), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(iPEndPoint);

                int bytes = 0;
                string size = string.Empty;
                string size_path = string.Empty;
                byte[] data = new byte[250];
                StringBuilder stringBuilder = new StringBuilder();

               


                Console.Write("Enter Path:");
                string sms = Console.ReadLine();
                size_path= sms.Count().ToString();
                socket.Send(Encoding.Unicode.GetBytes(size_path));
                socket.Send(Encoding.Unicode.GetBytes(sms));
                size = File.ReadAllBytes(sms).Count().ToString();
                socket.Send(Encoding.Unicode.GetBytes(size));
                socket.Send(File.ReadAllBytes(sms));


                Console.WriteLine($"Sms \"{sms}\" send to SERVER [{ipAddr}]!");

                do
                {
                    bytes = socket.Receive(data);
                    stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                Console.WriteLine(stringBuilder.ToString());



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
