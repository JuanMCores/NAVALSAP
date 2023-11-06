using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections;


class Servidor
{
    public Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    IPEndPoint direccion = new IPEndPoint(IPAddress.Any, 1234);
    public ArrayList socketsArray = new ArrayList();
    Dictionary<string, Socket> sockets = new Dictionary<string, Socket>();
    private object lockObject = new object();

    public static void Main(string[] args)
    {
        Servidor servidor = new Servidor();
        servidor.Start();
    }

    public void Start()
    {
        
        server.Bind(direccion);
        
        

        while (true)
        {
            server.Listen(1000);
            Socket cliente = server.Accept();
            Thread clientThread = new Thread(() => HandleClient(cliente));
            clientThread.Start();
        }
    }

    public void HandleClient(Socket cliente)
    {
        try
        {
            while (true)
            {
                string texto = recibirTexto(cliente);

                switch (texto)
                {
                    case "1":
                        // Crear una nueva sala para el cliente
                        texto = recibirTexto(cliente);
                        CrearSala(cliente, Convert.ToInt32(texto));
                        break;

                    case "2":
                        // Redireccionar al cliente a una sala existente
                        texto = recibirTexto(cliente);
                        RedireccionarClienteASala(cliente);
                        break;

                    default:
                        // Enviar el mensaje a todos los clientes en la sala del cliente
                        if (sockets.ContainsKey(texto))
                        {
                            // Obtener el socket de la sala
                            Socket sala = sockets[texto];

                            
                        }
                        break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error en el cliente " + cliente.RemoteEndPoint + ": " + e.ToString());
        }
    }

    private void CrearSala(Socket cliente, int puerto)
    {
        // Crear un nuevo socket para la sala
        IPEndPoint nuevaDireccion = new IPEndPoint(IPAddress.Any, puerto);
        Socket nuevaSala = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Agregar el nuevo socket a la lista de salas
        sockets[Convert.ToString(puerto)] = nuevaSala;

    }
    private void RedireccionarClienteASala(Socket cliente)
    {
        // Recibir el puerto de la sala a la que se desea redireccionar al cliente
        string puertoStr = recibirTexto(cliente);

        // Verificar si la sala existe
        if (sockets.ContainsKey(puertoStr))
        {
            // Obtener el socket de la sala
            Socket sala = sockets[puertoStr];

            // Redireccionar al cliente a la sala
            cliente.Connect(sala.RemoteEndPoint);

            // Cerrar el socket del cliente original
            cliente.Close();
        }
        else
        {
            // La sala no existe
            // Aquí puedes manejar el caso en el que la sala no existe
        }
    }
    private string GenerarNuevoPuerto()
    {
        Random random = new Random();
        string puertoStr = random.Next(10000, 20000).ToString();
        string nuevoPuerto = puertoStr;

        // Verificar si el puerto ya está en uso
        while (sockets.ContainsKey(nuevoPuerto))
        {
            puertoStr = random.Next(10000, 20000).ToString();
            nuevoPuerto = puertoStr;
        }

        return nuevoPuerto;
    }
    public string recibirTexto(Socket s)
    {
        string texto;
        byte[] byRec = new byte[256];
        int enteroRecibido = s.Receive(byRec, 0, byRec.Length, 0);
        Array.Resize(ref byRec, enteroRecibido);
        texto= Encoding.Default.GetString(byRec);
        return texto;
    }
    
}