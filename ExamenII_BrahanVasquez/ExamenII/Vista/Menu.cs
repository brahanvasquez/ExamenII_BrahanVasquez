using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Hide();
            Login.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tipoSoporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoSoporteForm Soporte = new TipoSoporteForm();
            
            Soporte.Show();
        }

        private void crearTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CrearTicketForm Crear = new CrearTicketForm();

            Crear.Show();
        }

        private void verTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerTicketsForm verTicketsForm = new VerTicketsForm();

            verTicketsForm.Show();
        }
    }
}
