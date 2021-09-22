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
        static int[,] pole = new int[3, 3] { {-1,-1,-1}, { -1, -1, -1 }, { -1, -1, -1 } };
        static char[,] poleGui = new char[3, 3];
        static int win = 0;
        static string pos = " ";
        static string[] pos2;
        static int X = 0;
        static int Y = 0;
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

                    

                    while (win==0)
                    {
                        win = CheckWin(pole, win);
                        pos = GetPosition_X(socketClient).ToString();
                        pos2 = pos.Split(',');
                        poleGui[int.Parse(pos2[0]),int.Parse(pos2[1])] = 'X';
                        Console.Clear();
                        DrawField(poleGui);
                        pole[int.Parse(pos2[0]), int.Parse(pos2[1])] = 1;
                        Console.WriteLine("Enter Posicion X:");
                        X = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Posicion Y:");
                        Y = int.Parse(Console.ReadLine());
                        poleGui[X, Y] = '0';
                        pole[X, Y] = 0;
                        Console.Clear();
                        DrawField(poleGui);
                        win = CheckWin(pole, win);
                        socketClient.Send(Encoding.Unicode.GetBytes($"{win.ToString()},{X.ToString()},{Y.ToString()}"));
                       
                    }

                   

                    
                    






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

        static StringBuilder GetPosition_X(Socket clientSoc)
        {

            StringBuilder stringBuilder = new StringBuilder();
            byte[] data = new byte[256];

            do
            {
                bytes = clientSoc.Receive(data);
                stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (socketClient.Available > 0);
            
            return stringBuilder;

        }


        static int CheckWin(int[,] pole,int win)
        {

            if (pole[0, 0] == 1 && pole[0, 1] == 1 && pole[0, 2] == 1)
            {

                win = 1;

            }
            else if (pole[1, 0] == 1 && pole[1, 1] == 1 && pole[1, 2] == 1)
            {

                win = 1;

            }
            else if (pole[2, 0] == 1 && pole[2, 1] == 1 && pole[2, 2] == 1)
            {

                win = 1;

            }
            else if (pole[0, 0] == 0 && pole[0, 1] == 0 && pole[0, 2] == 0)
            {

                win = 2;

            }
            else if (pole[1, 0] == 0 && pole[1, 1] == 0 && pole[1, 2] == 0)
            {

                win = 2;

            }
            else if (pole[2, 0] == 0 && pole[2, 1] == 0 && pole[2, 2] == 0)
            {

                win = 2;

            }
            else if (pole[0, 0] == 0 && pole[1, 1] == 0 && pole[2, 2] == 0)
            {

                win = 2;

            }
            else if (pole[0, 2] == 0 && pole[1, 1] == 0 && pole[2, 0] == 0)
            {

                win = 2;

            }
            else if (pole[0, 0] == 1 && pole[1, 1] == 1 && pole[2, 2] == 1)
            {

                win = 1;

            }
            else if (pole[0, 2] == 1 && pole[1, 1] == 1 && pole[2, 0] == 1)
            {

                win = 1;

            }
            else
            {
                win = 0;
            }

            return win;
        }

        static void DrawField(char[,] field)
        {

            Console.WriteLine($"{field[0, 0]} | {field[0, 1]} | {field[0, 2]}");
            Console.WriteLine("---------");
            Console.WriteLine($"{field[1, 0]} | {field[1, 1]} | {field[1, 2]}");
            Console.WriteLine("---------");
            Console.WriteLine($"{field[2, 0]} | {field[2, 1]} | {field[2, 2]}");


        }

    }


}
