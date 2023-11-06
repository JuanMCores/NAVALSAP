using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClienteFinal
{
    internal class Sala
    {
        public Socket sala = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public IPEndPoint salaChat = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
        ArrayList clientesEnLaSala = new ArrayList();


        public void conexion()
        {


            try
            {
                sala.Bind(salaChat);
                sala.Listen(1000);

                while (true)
                {
                    Socket cliente = sala.Accept();
                    Thread clientThread = new Thread(() => HandleClient(cliente));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void HandleClient(Socket cliente)
        {
            try
            {
                while (true)
                {
                    byte[] byRec = new byte[256];
                    int enteroRecibido = cliente.Receive(byRec, 0, byRec.Length, 0);
                    Array.Resize(ref byRec, enteroRecibido);
                    string texto = Encoding.Default.GetString(byRec);

                    EnviarMensaje(texto); // Enviar el mensaje a todos los clientes en la sala de chat
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en el cliente " + cliente.RemoteEndPoint + ": " + e.ToString());
            }
        }
        public void EnviarMensaje(string mensaje)
        {
            foreach (Socket cliente in clientesEnLaSala)
            {
                cliente.Send(Encoding.Default.GetBytes(mensaje));
            }
        }
        public void UnirseASala(Socket cliente)
        {
            clientesEnLaSala.Add(cliente);
        }




    }
}
