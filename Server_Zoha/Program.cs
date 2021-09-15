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
        static int bytes = 0;
       
        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static string str = String.Empty;
        static string[] arr = null;
        static Dictionary<string, int> Count_Words = new Dictionary<string, int>();
        static Socket socketClient;

        static void Main(string[] args)
        {
            
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            

            Console.WriteLine("Start server...");
            try
            {
                socket.Bind(iPEndPoint);
                socket.Listen(10);

                while (true)
                {

                    byte[] data = new byte[256];
                    StringBuilder stringBuilder = new StringBuilder();
                    socketClient = socket.Accept();

                   
                    
                    
                    string path = GetFileName(socketClient);

                    data = new byte[int.Parse(GetFileSize(socketClient))];
                    do
                    {
                        bytes = socketClient.Receive(data);
                        stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (socketClient.Available > 0);

                    File.WriteAllBytes(path, data);

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






                    for (int i = 0; i < Count_Words.Count; i++)
                    {
                        str += Count_Words.ElementAt(i).Key + " " + Count_Words.ElementAt(i).Value + "\n";
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
        static string GetFileName(Socket clientSoc)
        {

            StringBuilder stringBuilder = new StringBuilder();
            byte[] data = new byte[256];
          
            do
            {
                bytes = clientSoc.Receive(data);
                stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (socketClient.Available > 0);

            return stringBuilder.ToString();
        }


        static string GetFileSize(Socket clientSoc)
        {

            StringBuilder stringBuilder = new StringBuilder();
            byte[] data = new byte[256];

            do
            {
                bytes = clientSoc.Receive(data);
                stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (socketClient.Available > 0);

            return stringBuilder.ToString();
        }


        static string GetFile(Socket clientSoc,int size)
        {

            StringBuilder stringBuilder = new StringBuilder();
            byte[] data = new byte[size];

            do
            {
                bytes = clientSoc.Receive(data);
                stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (socketClient.Available > 0);

            return stringBuilder.ToString();
        }
    }


}
