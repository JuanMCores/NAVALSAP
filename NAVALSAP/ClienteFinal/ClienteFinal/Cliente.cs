using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ClienteFinal
{
    internal class Cliente
    {
       
        
        public Socket cliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public IPEndPoint direc = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

        public void enviarMensaje(string str)
        {
            byte[] txtAEnviar = Encoding.Default.GetBytes(str);
            cliente.Send(txtAEnviar,0,txtAEnviar.Length,0);
        }

    }
}
