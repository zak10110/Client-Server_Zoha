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
        public string path { get; set; }
        public Socket socket { get; set; }
        public IPEndPoint iPEndPoint { get; set; }

        public Client(string ipadres, int port, string path)
        {
            this.ipAddr = ipadres;
            this.port = port;
            this.path = path;
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
        public void SendPathToServ()
        {
            this.socket.Send(Encoding.Unicode.GetBytes(this.path));
        }

        public void GetAndSendSizeToServ()
        {
            string size = string.Empty;
            size = File.ReadAllBytes(this.path).Count().ToString();
            this.socket.Send(Encoding.Unicode.GetBytes(size));

        }

        public void SendFileToServ()
        {
            this.socket.Send(File.ReadAllBytes(this.path));

        }

    }
}
