using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista
{
    public partial class frmAltaUsuario : Form
    {
        Usuario usuario;

        public frmAltaUsuario()
        {
            InitializeComponent();
        }

        private void cargarComboBox(ComboBox cbo, DataTable tabla)
        {
            cbo.DataSource = tabla;
            cbo.DisplayMember = tabla.Columns[1].ColumnName;
            cbo.ValueMember = tabla.Columns[0].ColumnName;
            cbo.SelectedIndex = -1;
        }

        private bool emailCorrecto(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                return (Regex.Replace(email, expresion, String.Empty).Length == 0) ? true : false;
            }

            return false;
        }

        private void frmAltaUsuario_Load(object sender, EventArgs e)
        {
            cargarComboBox(cboPerfiles, Perfil.ObtenerPerfiles());
        }

        private void btnVisualizar_MouseDown(object sender, MouseEventArgs e)
        {
            txtClave.UseSystemPasswordChar = false;
        }

        private void btnVisualizar_MouseUp(object sender, MouseEventArgs e)
        {
            txtClave.UseSystemPasswordChar = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void labelInforme(string mensaje, bool estado, bool defecto)
        {
            if (defecto)
            {
                lblInformes.BackColor = System.Drawing.Color.LightGray;
                lblInformes.ForeColor = System.Drawing.Color.Black;
                lblInformes.Text = mensaje;
            }
            else
            {
                if (!estado)
                {
                    lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                    lblInformes.ForeColor = System.Drawing.Color.White;
                    lblInformes.Text = mensaje;
                }
                else
                {
                    lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
                    lblInformes.ForeColor = System.Drawing.Color.White;
                    lblInformes.Text = mensaje;
                }
            }
        }

        private void deshabilitarControles()
        {
            numDNI.Enabled = false;
            btnRegistrar.Enabled = false;
            btnCancelar.Enabled = true;
            btnVisualizar.Enabled = false;
            cboPerfiles.Enabled = false;
            txtNombreUsuario.Enabled = false;
            txtClave.Enabled = false;
            txtEmail.Enabled = false;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (cboPerfiles.SelectedIndex == -1)
            {
                labelInforme("DATO OBLIGATORIO: PERFIL DE USUARIO", false, false);
                cboPerfiles.Focus();

                return;
            }

            if (txtNombreUsuario.Text == "")
            {
                labelInforme("DATO OBLIGATORIO: NOMBRE DE USUARIO", false, false);
                txtNombreUsuario.Focus();

                return;
            }

            if (txtClave.Text == "")
            {
                labelInforme("DATO OBLIGATORIO: CLAVE", false, false);
                txtClave.Focus();

                return;
            }

            if (txtEmail.Text == "")
            {
                labelInforme("DATO OBLIGATORIO: CORREO ELECTRÓNICO", false, false);
                txtEmail.Focus();

                return;
            }

            if (!emailCorrecto(txtEmail.Text))
            {
                labelInforme("FORMATO DE EMAIL INCORRECTO\nFORMATO ADMITIDO: usuario@dominio.com", false, false);
                txtEmail.Focus();

                return;
            }

            string dni = numDNI.Text.ToString();
            Perfil perfil = new Perfil(Perfil.ObtenerPerfilPorNombre(cboPerfiles.Text).Id_perfil, cboPerfiles.Text, false);
            string nombreUsuario = txtNombreUsuario.Text.ToString();
            string clave = txtClave.Text.ToString();
            string email = txtEmail.Text.ToString();

            Usuario usuarioAuxiliar = new Usuario(dni, nombreUsuario, perfil, clave, email, false);
            string error;

            usuario = Usuario.ObtenerUsuario(numDNI.Text);
            if (usuario != null)
            {
                if (usuario.Borrado)
                {
                    error = Usuario.ModificarUsuario(usuarioAuxiliar);
                }
                else
                {
                    labelInforme("EL USUARIO YA SE ENCUENTRA REGISTRADO", false, false);

                    numDNI.Focus();

                    return;
                }
            }
            else
            {
                error = Usuario.AgregarUsuario(usuarioAuxiliar);
            }

            if (error == "")
            {
                labelInforme("USUARIO REGISTRADO CON ÉXITO", true, false);

                deshabilitarControles();
            }
            else
            {
                labelInforme(error, false, false);

                deshabilitarControles();
            }
        }

        private void numDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void numDNI_ValueChanged(object sender, EventArgs e)
        {
            labelInforme("INFORME", false, true);
            cboPerfiles.SelectedIndex = -1;
            txtNombreUsuario.Text = "";
            txtClave.Text = "";
            txtEmail.Text = "";
        }
    }
}
