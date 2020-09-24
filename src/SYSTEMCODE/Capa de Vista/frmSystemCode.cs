using System;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;
using SYSTEMCODE.Capa_de_Vista;
using SYSTEMCODE.Capa_de_Vista.ABMC_Perfiles;
using SYSTEMCODE.Capa_de_Vista.ABMC_Usuarios;

namespace SYSTEMCODE
{
    public partial class frmSystemCode : Form
    {
        public frmSystemCode()
        {
            InitializeComponent();
        }

        public static Usuario UsuarioActual { get; set; } = null;

        private void frmSystemCode_Load(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
            login.ShowDialog();

            if (login.Cerrado)
            {
                Close();
                return;
            }

            UsuarioActual = FrmLogin.UsuarioActual;

            lblBienvenida.Text = "¡Bienvenido, " + UsuarioActual.NombreUsuario + "!";

            switch (Usuario.ObtenerPerfil(UsuarioActual))
            {
                case "Encargado General":
                    menuUsuarios.Enabled = true;
                    menuPerfiles.Enabled = true;
                    menuClientes.Enabled = true;
                    menuBarrios.Enabled = true;
                    menuContactos.Enabled = true;
                    menuProyectos.Enabled = true;
                    menuVentas.Enabled = true;
                    menuInformes.Enabled = true;
                    break;

                case "Encargado de Administración":
                    menuUsuarios.Enabled = false;
                    menuPerfiles.Enabled = false;
                    menuClientes.Enabled = true;
                    menuBarrios.Enabled = true;
                    menuContactos.Enabled = true;
                    menuProyectos.Enabled = true;
                    menuVentas.Enabled = false;
                    menuInformes.Enabled = true;
                    break;

                case "Encargado de Ventas":
                    menuUsuarios.Enabled = false;
                    menuPerfiles.Enabled = false;
                    menuClientes.Enabled = false;
                    menuBarrios.Enabled = false;
                    menuContactos.Enabled = false;
                    menuProyectos.Enabled = false;
                    menuVentas.Enabled = true;
                    menuInformes.Enabled = false;
                    break;
            }
        }

        private void menuSalir_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            FrmUsuarios usuarios = new FrmUsuarios();
            usuarios.ShowDialog();
        }

        private void menuPerfiles_Click(object sender, EventArgs e)
        {
            frmPerfiles perfiles = new frmPerfiles();
            perfiles.ShowDialog();
        }
    }
}
