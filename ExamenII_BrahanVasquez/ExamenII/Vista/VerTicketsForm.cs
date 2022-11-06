using Datos;
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
    public partial class VerTicketsForm : Form
    {
        public VerTicketsForm()
        {
            InitializeComponent();
        }
        TicketDatos ticketDatos = new TicketDatos();
        private async void LlenarTickets()
        {
            dgvTickets.DataSource = await ticketDatos.DevolverListaAsync();
        }

        private void VerTicketsForm_Load(object sender, EventArgs e)
        {
            LlenarTickets();
        }
    }
}
