using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client_Zoha
{
    public class Client
    {
        public string ipAddr { get; set; }
        public int port { get; set; }
        public string posicion { get; set; }
        public Socket socket { get; set; }
        public IPEndPoint iPEndPoint { get; set; }

        public Client(string ipadres, int port, string posicion)
        {
            this.ipAddr = ipadres;
            this.port = port;
            this.posicion = posicion;
            this.socket = socket;

        }



        public void CreateSocet()
        { 
         this.socket= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void CreateIPEndPoint()
        { 
        
            this.iPEndPoint= new IPEndPoint(IPAddress.Parse(this.ipAddr), this.port);

        }
        public void SendposicionToServ(string pos)
        {
            this.socket.Send(Encoding.Unicode.GetBytes(pos.ToString()));
        }

        public void GetAndSendSizeToServ()
        {
            string size = string.Empty;
            size = File.ReadAllBytes(this.posicion).Count().ToString();
            this.socket.Send(Encoding.Unicode.GetBytes(size));

        }

        public void SendFileToServ()
        {
            this.socket.Send(File.ReadAllBytes(this.posicion));

        }

        public void Conect()
        {

            this.CreateIPEndPoint();
            this.CreateSocet();
        
        }

        public string GetMSGFromServ()
        {

            int bytes = 0;

            byte[] data = new byte[250];

            StringBuilder stringBuilder = new StringBuilder();

            do
            {
                bytes = this.socket.Receive(data);
                stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (this.socket.Available > 0);
            return stringBuilder.ToString();


        }

    }
}
