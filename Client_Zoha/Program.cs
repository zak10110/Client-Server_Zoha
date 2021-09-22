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
        static int win = 0;
        static int X = 0;
        static int Y = 0;
        static int tmp = 0;
        static string pos = " ";
        static string[] pos2;
        static char[,] poleGui = new char[3, 3];

        static void Main(string[] args)
        {
            try
            {


                Client client = new Client("127.0.0.1", 8000, "0");
                client.Conect();

                client.socket.Connect(client.iPEndPoint);

                while (win == 0)
                {
                    if (tmp > 0)
                    {

                        poleGui[X, Y] = '0';
                        Console.Clear();
                        DrawField(poleGui);
                    }

                    Console.WriteLine("Enter Posicion(Like that:0,0):");
                    pos = Console.ReadLine();
                    pos2 = pos.Split(',');
                    poleGui[int.Parse(pos2[0]), int.Parse(pos2[1])] = 'X';
                    Console.Clear();
                    DrawField(poleGui);
                    client.SendposicionToServ(pos);
                    Console.Clear();
                    poleGui[int.Parse(pos2[0]), int.Parse(pos2[1])] = 'X';
                    DrawField(poleGui);
                    pos = client.GetMSGFromServ();
                    pos2 = pos.Split(',');
                    win = int.Parse(pos2[0]);
                    X = int.Parse(pos2[1]);
                    Y = int.Parse(pos2[2]);
                    Console.Clear();
                    DrawField(poleGui);
                    tmp += 1;
                }
                if (win == 1)
                {

                    Console.WriteLine("Player X Wins!");

                }
                else if (win == 2)
                {

                    Console.WriteLine("Player 0 Wins!");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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
