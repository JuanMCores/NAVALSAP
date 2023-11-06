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
    public partial class CrearSala : Form
    {
        public CrearSala()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cliente c = new Cliente();
            try {
                c.cliente.Connect(c.direc);
                c.enviarMensaje(textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UnirseASala unirseASala = new UnirseASala();
            unirseASala.Show();
            this.Hide();
        }
    }
}
