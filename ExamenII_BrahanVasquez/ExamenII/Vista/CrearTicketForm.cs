using Datos;
using Entidades;
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
    public partial class CrearTicketForm : Form
    {
        public CrearTicketForm()
        {
            InitializeComponent();
        }
        TipoSoporteDatos soporteDatos = new TipoSoporteDatos();
        Ticket ticket;
        TicketDatos datos;

        decimal subTotal = 0;
        decimal isv = 0;
        decimal total = 0;
        decimal descuento = 0;

        private void CrearTicketForm_Load(object sender, EventArgs e)
        {
            LlenarSoporte();
            txtUsuario.Text = VariableGlobal.UsuarioLogin;
            txtUsuario.ReadOnly = true;
            
        }

        private async void LlenarSoporte()
        {
            dgvTipoSoporte.DataSource = await soporteDatos.DevolverListaAsync();
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTipoSoporte_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtImpuesto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                errorProvider1.SetError(txtCodigo, "Ingrese un codigo");
                txtCodigo.Focus();
                return;
            }
            errorProvider1.Clear();

            if (string.IsNullOrEmpty(txtCliente.Text))
            {
                errorProvider1.SetError(txtCliente, "Ingrese un cliente");
                txtCliente.Focus();
                return;
            }
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtRespuesta.Text))
            {
                errorProvider1.SetError(txtRespuesta, "Ingrese una respuesta");
                txtRespuesta.Focus();
                return;
            }
            errorProvider1.Clear();

            if (dgvTipoSoporte.SelectedRows.Count > 0)
            {
                txtTipoSoporte.Text = dgvTipoSoporte.CurrentRow.Cells["Nombre"].Value.ToString();
                txtPrecio.Text = dgvTipoSoporte.CurrentRow.Cells["Precio"].Value.ToString();           
                txtDescripcion.Text = dgvTipoSoporte.CurrentRow.Cells["Descripcion"].Value.ToString();
                
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescuento.Text))
            {
                errorProvider1.SetError(txtDescuento, "Ingrese un descuento");
                txtDescuento.Focus();
                return;
            }
            errorProvider1.Clear();
            descuento = Convert.ToDecimal(txtDescuento.Text) / 100;
            subTotal = Convert.ToDecimal(txtPrecio.Text)-(Convert.ToDecimal(txtPrecio.Text) * descuento);
            isv = Convert.ToDecimal(txtImpuesto.Text) / 100;
            
            txtSubTotal.Text = subTotal.ToString();
            total = (Convert.ToDecimal(txtSubTotal.Text) * isv) + Convert.ToDecimal(txtSubTotal.Text);
            txtTotal.Text = total.ToString();

            btnGuardar.Enabled = true;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            ticket = new Ticket();
            ticket.Codigo = Convert.ToInt32(txtCodigo.Text);
            ticket.Fecha = dtpFecha.Value;
            ticket.CodigoUsuario = txtUsuario.Text;
            ticket.NombreCliente = txtCliente.Text;
            ticket.TipoSoporte = txtTipoSoporte.Text;
            ticket.Descripcion = txtDescripcion.Text;
            ticket.DescripcionRespuesta = txtRespuesta.Text;
            ticket.Precio = Convert.ToDecimal(txtPrecio.Text);
            ticket.Descuento = Convert.ToDecimal(txtDescuento.Text);
            ticket.Impuesto = Convert.ToDecimal(txtImpuesto.Text);
            ticket.SubTotal = Convert.ToDecimal(txtSubTotal.Text);
            ticket.Total = Convert.ToDecimal(txtTotal.Text);
            

            datos = new TicketDatos();
            bool inserto = await datos.InsertarAsync(ticket);
            if (inserto)
            {
                MessageBox.Show("Ticket guardado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCliente.Clear();
                txtTipoSoporte.Clear();
                txtPrecio.Clear();
                txtDescripcion.Clear();
                txtRespuesta.Clear();
                txtDescuento.Clear();
                txtSubTotal.Clear();
                txtTotal.Clear();
                btnGuardar.Enabled = false;
            }
            else
            {
                MessageBox.Show("Ticket no se pudo guardar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
