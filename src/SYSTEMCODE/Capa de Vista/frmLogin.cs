using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista
{
    public partial class frmLogin : Form
    {
        private bool cerrado = false;
        private bool btnIngresarPresionado = false;
        private static Usuario usuarioActual;
        private int temporizador = 3;

        public frmLogin()
        {
            InitializeComponent();
        }

        public bool Cerrado { get => cerrado; set => cerrado = value; }
        public static Usuario UsuarioActual { get => usuarioActual; set => usuarioActual = value; }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            btnIngresarPresionado = true;

            if (txtUsuario.Text.Length == 0)
            {
                lblEstadoLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblEstadoLogin.ForeColor = System.Drawing.Color.White;
                lblEstadoLogin.Text = "DATO OBLIGATORIO: USUARIO";
                txtUsuario.Focus();

                return;
            }

            if (txtClave.Text.Length == 0)
            {
                lblEstadoLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblEstadoLogin.ForeColor = System.Drawing.Color.White;
                lblEstadoLogin.Text = "DATO OBLIGATORIO: CLAVE";
                txtClave.Focus();

                return;
            }

            try
            {
                UsuarioActual = Usuario.ValidarUsuario(txtUsuario.Text, txtClave.Text);
                if (UsuarioActual != null)
                {
                    btnIngresar.Enabled = false;
                    temporizadorAcceso.Enabled = true;
                    txtUsuario.Enabled = false;
                    txtClave.Enabled = false;
                }
                else
                {
                    lblEstadoLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                    lblEstadoLogin.ForeColor = System.Drawing.Color.White;
                    lblEstadoLogin.Text = "ACCESO DENEGADO - DATOS INCORRECTOS";

                    txtClave.Text = "";
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                }
            }
            catch (SqlException)
            {
                lblEstadoLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblEstadoLogin.ForeColor = System.Drawing.Color.White;
                lblEstadoLogin.Text = "ERROR DE CONEXIÓN - BASE DE DATOS";

                txtUsuario.Enabled = false;
                txtClave.Enabled = false;
                btnIngresar.Enabled = false;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            cerrado = true;
            Close();
        }

        private void temporizadorAcceso_Tick(object sender, EventArgs e)
        {
            lblEstadoLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            lblEstadoLogin.ForeColor = System.Drawing.Color.White;
            lblEstadoLogin.Text = "ACCESO CORRECTO [" + temporizador.ToString() + "]";
            temporizador--;

            if (temporizador == -1)
            {
                temporizadorAcceso.Enabled = false;
                Close();
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!btnIngresarPresionado)
            {
                cerrado = true;
            }
        }
    }
}
