using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteFinal
{
    public partial class Chat : Form
    {
        private readonly Sala sala;
        private readonly Cliente cliente;
        public Chat()
        {
            InitializeComponent();
            sala = new Sala();
            cliente = new Cliente();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            using (cliente.cliente)
            {
                sala.conexion();
                sala.UnirseASala(cliente.cliente);

                while (true)
                {
                    string mensaje = textBox1.Text;
                    sala.EnviarMensaje(mensaje);

                    textBox1.Clear();
                }
            }
        }
    }
}
