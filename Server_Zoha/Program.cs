﻿using System;
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

                    socketClient.Send(Encoding.Unicode.GetBytes("Welcome on server!"));


                    StringBuilder stringBuilder = new StringBuilder();

                    int bytes = 0;
                    byte[] data = new byte[256];
                    int res = 0;
                    do
                    {
                        bytes = socketClient.Receive(data);
                        stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        foreach (var z in stringBuilder.ToString().Split(','))
                        {
                            res = res + int.Parse(z);

                        }
                    } while (socketClient.Available > 0);

                    Console.WriteLine($"MSG: {stringBuilder.ToString()}");
                    socketClient.Send(Encoding.Unicode.GetBytes($"{res}"));


                    socketClient.Shutdown(SocketShutdown.Both);
                    socketClient.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
