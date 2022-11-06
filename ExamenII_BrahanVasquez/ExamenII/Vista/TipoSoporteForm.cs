using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Entidades;

namespace Vista
{
    public partial class TipoSoporteForm : Form
    {
        public TipoSoporteForm()
        {
            InitializeComponent();
        }

        TipoSoporteDatos soporteDatos = new TipoSoporteDatos();
        TipoSoporte tipoSoporte;
        string tipoOperacion = string.Empty;

        private async void LlenarProductos()
        {
            dgvSoporte.DataSource = await soporteDatos.DevolverListaAsync();
        }

        private void HabilitarControles()
        {
            txtCodigo.Enabled = true;
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;
            txtPrecio.Enabled = true;
        }

        private void DeshabilitarControles()
        {
            txtCodigo.Enabled = false;
            txtNombre.Enabled = false;
            txtDescripcion.Enabled = false;
            txtPrecio.Enabled = false;
        }

        private void LimpiarControles()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtCodigo.ReadOnly = false;
            tipoOperacion = "Nuevo";
            HabilitarControles();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            tipoSoporte = new TipoSoporte();

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                errorProvider1.SetError(txtCodigo, "Ingrese el código");
                txtCodigo.Focus();
                return;
            }
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                errorProvider1.SetError(txtNombre, "Ingrese un nombre");
                txtNombre.Focus();
                return;
            }
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                errorProvider1.SetError(txtDescripcion, "Ingrese una descripción");
                txtDescripcion.Focus();
                return;
            }
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                errorProvider1.SetError(txtPrecio, "Ingrese un precio");
                txtPrecio.Focus();
                return;
            }
            errorProvider1.Clear();



            tipoSoporte.Codigo = Convert.ToInt32(txtCodigo.Text);
            tipoSoporte.Nombre = txtNombre.Text;
            tipoSoporte.Descripcion = txtDescripcion.Text;
            tipoSoporte.Precio = Convert.ToDecimal(txtPrecio.Text);

            if (tipoOperacion == "Nuevo")
            {
                bool inserto = await soporteDatos.InsertarAsync(tipoSoporte);
                if (inserto)
                {
                    LlenarProductos();
                    LimpiarControles();
                    DeshabilitarControles();
                    MessageBox.Show("Tipo de soporte guardado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tipo de soporte no se pudo guardar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (tipoOperacion == "Modificar")
            {
                bool modifico = await soporteDatos.ActualizarAsync(tipoSoporte);
                if (modifico)
                {
                    LlenarProductos();
                    LimpiarControles();
                    DeshabilitarControles();
                    MessageBox.Show("Tipo de soporte guardado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tipo de soporte no se pudo guardar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TipoSoporteForm_Load(object sender, EventArgs e)
        {
            LlenarProductos();
            DeshabilitarControles();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvSoporte.SelectedRows.Count > 0)
            {
                tipoOperacion = "Modificar";
                HabilitarControles();
                txtCodigo.ReadOnly = true;
                txtCodigo.Text = dgvSoporte.CurrentRow.Cells["Codigo"].Value.ToString();
                txtNombre.Text = dgvSoporte.CurrentRow.Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = dgvSoporte.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtPrecio.Text = dgvSoporte.CurrentRow.Cells["Precio"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvSoporte.SelectedRows.Count > 0)
            {
                bool elimino = await soporteDatos.EliminarAsync(dgvSoporte.CurrentRow.Cells["Codigo"].Value.ToString());

                if (elimino)
                {
                    LlenarProductos();
                    MessageBox.Show("Producto Eliminado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Producto no se pudo eliminar", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarControles();
            LimpiarControles();
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
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
