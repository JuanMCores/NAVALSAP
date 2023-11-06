namespace ClienteFinal
{
    public partial class Inicio : Form
    {
        Cliente c;
        public Inicio()
        {
            InitializeComponent();
            Cliente c = new Cliente();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                c.cliente.Connect(c.direc);
                c.enviarMensaje("1");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CrearSala crearSala = new CrearSala();
            crearSala.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cliente c = new Cliente();
            try
            {
                c.cliente.Connect(c.direc);
                c.enviarMensaje("2");
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