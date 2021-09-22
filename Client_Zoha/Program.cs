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
 
        static void Main(string[] args)
        {
            try
            {
                
                Console.Write("Enter Path:");
                string path = Console.ReadLine();
                Client client = new Client("127.0.0.1", 8000, path);
                client.Conect();
                



                client.socket.Connect(client.iPEndPoint);

                int bytes = 0;
                
                byte[] data = new byte[250];
               
                StringBuilder stringBuilder = new StringBuilder();




                client.SendPathToServ();
                client.GetAndSendSizeToServ();
                client.SendFileToServ();


                Console.WriteLine($"Sms \"{path}\" send to SERVER [{client.ipAddr}]!");

                do
                {
                    bytes = client.socket.Receive(data);
                    stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (client.socket.Available > 0);
                Console.WriteLine(stringBuilder.ToString());



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
