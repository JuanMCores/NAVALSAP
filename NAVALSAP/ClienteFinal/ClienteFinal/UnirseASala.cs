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
    public partial class UnirseASala : Form
    {
        public UnirseASala()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int puertoSala;

            if (!int.TryParse(textBox1.Text, out puertoSala))
            {
                MessageBox.Show("El puerto de la sala no es válido.");
                return;
            }

            Cliente c = new Cliente();
            Sala s = new Sala();
            c.direc.Port = puertoSala;

            try
            {
                c.cliente.Bind(c.direc);
                s.salaChat.Port = puertoSala;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                c.cliente.Close();
            }

            Chat chat = new Chat();
            chat.Show();
            this.Hide();
        }
    }
}
