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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == String.Empty)
            {
                errorProvider1.SetError(txtCodigo, "Ingrese un código de usuario");
                txtCodigo.Focus();
                return;
            }
            errorProvider1.Clear();
            if (txtClave.Text == String.Empty)
            {
                errorProvider1.SetError(txtClave, "Ingrese una clave");
                txtClave.Focus();
                return;
            }
            errorProvider1.Clear();

            UsuarioDatos userDatos = new UsuarioDatos();

            bool valido = await userDatos.LoginAsync(txtCodigo.Text, txtClave.Text);

            if (valido)
            {
                Menu formulario = new Menu();
                Hide();
                VariableGlobal.UsuarioLogin = txtCodigo.Text;
                formulario.Show();
            }
            else
            {
                MessageBox.Show("Datos de usuario incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtClave.Clear();
            txtCodigo.Clear();
        }
    }
}
