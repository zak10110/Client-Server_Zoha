using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server_Zoha
{
    class Program
    {
        static int port = 8000;
        
        static void Main(string[] args)
        {
            string str = String.Empty;
            string[] arr = null;
            Dictionary<string, int> Count_Words = new Dictionary<string, int>();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Start server...");
            try
            {
                socket.Bind(iPEndPoint);
                socket.Listen(10);

                while (true)
                {
                    Socket socketClient = socket.Accept();

                   


                    StringBuilder stringBuilder = new StringBuilder();

                    int bytes = 0;
                    byte[] data = new byte[256];
                  
                    
                    do
                    {
                        bytes = socketClient.Receive(data);
                        stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (socketClient.Available > 0) ;

                    File.WriteAllBytes($"{DateTime.Now.ToString().Replace(' ', '_').Replace('.', '_').Replace(':', '_')}.txt", data);
                    arr = stringBuilder.ToString().Split('.');

                    foreach (var z in arr)
                    {
                        if (Count_Words.ContainsKey(z) == false)
                        {
                            Count_Words.Add(z, 1);
                        }
                        else
                        {
                            Count_Words[z]++;

                        }
                    }

                    

                       
                    

                    for (int i=0;i<Count_Words.Count;i++)
                    {
                        str += Count_Words.ElementAt(i).Key + " " + Count_Words.ElementAt(i).Value+"\n";
                    }

                    data = Encoding.Unicode.GetBytes(str);
                    socketClient.Send(data);
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
}
    }
}
