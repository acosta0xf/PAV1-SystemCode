using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Vista
{
    public partial class frmModificarUsuario : Form
    {
        Usuario usuario;
        private int dni;

        public frmModificarUsuario(int dni)
        {
            this.dni = dni;
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

        private void labelInforme(string mensaje, bool estado)
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

        private void deshabilitarControles()
        {
            btnModificar.Enabled = false;
            btnCancelar.Enabled = true;
            btnVisualizar.Enabled = false;
            cboPerfiles.Enabled = false;
            txtNombreUsuario.Enabled = false;
            txtClave.Enabled = false;
            txtEmail.Enabled = false;
        }

        private void frmModificarUsuario_Load(object sender, EventArgs e)
        {
            cargarComboBox(cboPerfiles, Perfil.ObtenerPerfiles());

            usuario = Usuario.ObtenerUsuario(dni.ToString());

            numDNI.Text = usuario.Dni.ToString();

            DataTable tablaPerfiles = Perfil.ObtenerPerfiles();
            for (int i = 0; i < tablaPerfiles.Rows.Count; i++)
            {
                if (tablaPerfiles.Rows[i]["nombre"].ToString() == usuario.Perfil.Nombre.ToString())
                {
                    cboPerfiles.SelectedIndex = i;
                    break;
                }
            }
            
            txtNombreUsuario.Text = usuario.NombreUsuario.ToString();
            txtClave.Text = usuario.Clave.ToString();
            txtEmail.Text = usuario.Email.ToString();
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (cboPerfiles.SelectedIndex == -1)
            {
                labelInforme("DATO OBLIGATORIO: PERFIL DE USUARIO", false);
                cboPerfiles.Focus();

                return;
            }

            if (txtNombreUsuario.Text == "")
            {
                labelInforme("DATO OBLIGATORIO: NOMBRE DE USUARIO", false);
                txtNombreUsuario.Focus();

                return;
            }

            if (txtClave.Text == "")
            {
                labelInforme("DATO OBLIGATORIO: CLAVE", false);
                txtClave.Focus();

                return;
            }

            if (txtEmail.Text == "")
            {
                labelInforme("DATO OBLIGATORIO: CORREO ELECTRÓNICO", false);
                txtEmail.Focus();

                return;
            }

            if (!emailCorrecto(txtEmail.Text))
            {
                labelInforme("FORMATO DE EMAIL INCORRECTO\nFORMATO ADMITIDO: usuario@dominio.com", false);
                txtEmail.Focus();

                return;
            }

            string dni = numDNI.Text.ToString();
            Perfil perfil = new Perfil(cboPerfiles.SelectedIndex + 1, cboPerfiles.Text, false);
            string nombreUsuario = txtNombreUsuario.Text.ToString();
            string clave = txtClave.Text.ToString();
            string email = txtEmail.Text.ToString();

            Usuario usuarioAuxiliar = new Usuario(dni, nombreUsuario, perfil, clave, email);
            string error = Usuario.ModificarUsuario(usuarioAuxiliar);

            if (error == "")
            {
                labelInforme("USUARIO MODIFICADO CON ÉXITO", true);

                deshabilitarControles();
            }
            else
            {
                labelInforme(error, false);

                deshabilitarControles();
            }
        }
    }
}
