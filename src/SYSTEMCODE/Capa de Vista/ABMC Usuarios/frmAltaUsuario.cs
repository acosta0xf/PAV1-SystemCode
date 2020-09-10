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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (numDNI.Text != "")
            {
                usuario = Usuario.ObtenerUsuario(numDNI.Text);
                if (usuario != null)
                {
                    if (usuario.Dni.ToString() == numDNI.Text && !usuario.Borrado)
                    {
                        lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
                        lblInformes.ForeColor = System.Drawing.Color.White;
                        lblInformes.Text = "EL USUARIO YA SE ENCUENTRA REGISTRADO";

                        btnRegistrar.Enabled = false;
                        btnCancelar.Enabled = true;
                        btnVisualizar.Enabled = false;
                        cboPerfiles.SelectedIndex = usuario.Perfil.Id_perfil - 1;
                        txtNombreUsuario.Text = usuario.NombreUsuario.ToString();
                        txtClave.Text = usuario.Clave.ToString();
                        txtEmail.Text = usuario.Email.ToString();
                        cboPerfiles.Enabled = false;
                        txtNombreUsuario.Enabled = false;
                        txtClave.Enabled = false;
                        txtEmail.Enabled = false;
                    }
                    else if (usuario.Dni.ToString() == numDNI.Text && usuario.Borrado)
                    {
                        lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                        lblInformes.ForeColor = System.Drawing.Color.White;
                        lblInformes.Text = "EL USUARIO FUE BORRADO\n¿DESEA REGISTRARLO NUEVAMENTE?";

                        btnRegistrar.Enabled = true;
                        btnCancelar.Enabled = true;
                        btnVisualizar.Enabled = true;
                        cboPerfiles.SelectedIndex = -1;
                        txtNombreUsuario.Text = "";
                        txtClave.Text = "";
                        txtEmail.Text = "";
                        cboPerfiles.Enabled = true;
                        txtNombreUsuario.Enabled = true;
                        txtClave.Enabled = true;
                        txtEmail.Enabled = true;
                    }
                }
                else
                {
                    lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                    lblInformes.ForeColor = System.Drawing.Color.White;
                    lblInformes.Text = "EL USUARIO NO SE ENCUENTRA REGISTRADO\nREGISTRELO A CONTINUACIÓN";

                    cboPerfiles.SelectedIndex = -1;
                    txtNombreUsuario.Text = "";
                    txtClave.Text = "";
                    txtEmail.Text = "";
                    btnRegistrar.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnVisualizar.Enabled = true;
                    cboPerfiles.Enabled = true;
                    txtNombreUsuario.Enabled = true;
                    txtClave.Enabled = true;
                    txtEmail.Enabled = true;
                }
            }
            else
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = "DATO OBLIGATORIO: D.N.I";
            }
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

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (cboPerfiles.SelectedIndex == -1)
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = "DATO OBLIGATORIO: PERFIL DE USUARIO";
                cboPerfiles.Focus();

                return;
            }

            if (txtNombreUsuario.Text == "")
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = "DATO OBLIGATORIO: NOMBRE DE USUARIO";
                txtNombreUsuario.Focus();

                return;
            }

            if (txtClave.Text == "")
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = "DATO OBLIGATORIO: CLAVE";
                txtClave.Focus();

                return;
            }

            if (txtEmail.Text == "")
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = "DATO OBLIGATORIO: CORREO ELECTRÓNICO";
                txtEmail.Focus();

                return;
            }

            if (!emailCorrecto(txtEmail.Text))
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = "FORMATO DE EMAIL INCORRECTO\nFORMATO ADMITIDO: usuario@dominio.com";
                txtEmail.Focus();

                return;
            }

            string dni = numDNI.Text.ToString();
            Perfil perfil = new Perfil(cboPerfiles.SelectedIndex + 1, cboPerfiles.Text, false);
            string nombreUsuario = txtNombreUsuario.Text.ToString();
            string clave = txtClave.Text.ToString();
            string email = txtEmail.Text.ToString();

            Usuario usuarioAuxiliar = new Usuario(dni, nombreUsuario, perfil, clave, email, false);
            string error;

            if (usuario.Borrado)
            {
                error = Usuario.ModificarUsuario(usuarioAuxiliar);
            }
            else
            {
                error = Usuario.AgregarUsuario(usuarioAuxiliar);
            }

            if (error == "")
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = "USUARIO REGISTRADO CON ÉXITO";

                btnRegistrar.Enabled = false;
                btnCancelar.Enabled = true;
                btnVisualizar.Enabled = false;
                cboPerfiles.Enabled = false;
                txtNombreUsuario.Enabled = false;
                txtClave.Enabled = false;
                txtEmail.Enabled = false;
            }
            else
            {
                lblInformes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
                lblInformes.ForeColor = System.Drawing.Color.White;
                lblInformes.Text = error;

                btnRegistrar.Enabled = false;
                btnCancelar.Enabled = true;
                btnVisualizar.Enabled = false;
                numDNI.Enabled = false;
                btnConsultar.Enabled = false;
                cboPerfiles.Enabled = false;
                txtNombreUsuario.Enabled = false;
                txtClave.Enabled = false;
                txtEmail.Enabled = false;
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
    }
}
