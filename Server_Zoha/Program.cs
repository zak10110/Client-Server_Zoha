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

                    socketClient = socket.Accept();
                    string path = GetFileName(socketClient);
                    WriteFile(path);






                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static bool WriteFile(string path)
        {
            try
            {
                byte[] data = new byte[int.Parse(GetFileSize(socketClient))];
                StringBuilder stringBuilder = new StringBuilder();
                do
                {
                    bytes = socketClient.Receive(data);
                    stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socketClient.Available > 0);

                File.WriteAllBytes(Path.GetFileName(path), data);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
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



    }


}
