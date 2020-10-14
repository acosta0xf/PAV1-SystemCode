using System;
using System.Windows.Forms;
using SYSTEMCODE.Capa_de_Negocio;
using SYSTEMCODE.Capa_de_Vista;
using SYSTEMCODE.Capa_de_Vista.ABMC;

namespace SYSTEMCODE
{
    public partial class FrmSystemCode : Form
    {
        public FrmSystemCode()
        {
            InitializeComponent();
        }

        public static Usuario UsuarioActual { get; set; } = null;

        private void HabilitarMenu(string condicion)
        {
            menuUsuarios.Visible = condicion.Equals("Encargado General") || condicion.Equals("Testing");
            menuClientes.Visible = condicion.Equals("Encargado de Administración") || condicion.Equals("Testing");
            menuBarrios.Visible = condicion.Equals("Encargado de Administración") || condicion.Equals("Testing");
            menuProyectos.Visible = condicion.Equals("Encargado de Administración") || condicion.Equals("Testing");
            menuVentas.Visible = condicion.Equals("Encargado de Ventas") || condicion.Equals("Testing");
            menuInformes.Visible = condicion.Equals("Encargado de Administración") || condicion.Equals("Testing");
        }

        private void FrmSystemCode_Load(object sender, EventArgs e)
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
                    HabilitarMenu("Encargado General");
                    break;

                case "Encargado de Administración":
                    HabilitarMenu("Encargado de Administración");
                    break;

                case "Encargado de Ventas":
                    HabilitarMenu("Encargado de Ventas");
                    break;

                case "Testing":
                    HabilitarMenu("Testing");
                    break;
                    
                default:
                    HabilitarMenu("Defecto");
                    break;
            }
        }

        private void MenuVentas_Click(object sender, EventArgs e)
        {
            FrmGestionarVentas ventas = new FrmGestionarVentas(UsuarioActual.NombreUsuario)
            {
                Text = "Venta [Usuario logueado: " + UsuarioActual.NombreUsuario + "]"
            };
            ventas.ShowDialog();
        }

        private void UsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuarios usuarios = new FrmUsuarios
            {
                Text = "Usuarios [Usuario logueado: " + UsuarioActual.NombreUsuario + "]"
            };
            usuarios.ShowDialog();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmClientes clientes = new FrmClientes
            {
                Text = "Clientes [Usuario logueado: " + UsuarioActual.NombreUsuario + "]"
            };
            clientes.ShowDialog();
        }

        private void barriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBarrios barrios = new FrmBarrios
            {
                Text = "Barrios [Usuario logueado: " + UsuarioActual.NombreUsuario + "]"
            };
            barrios.ShowDialog();
        }

        private void proyectosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProyectos proyectos = new FrmProyectos
            {
                Text = "Proyectos [Usuario logueado: " + UsuarioActual.NombreUsuario + "]"
            };
            proyectos.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConfirmacion confirmacion = new FrmConfirmacion();
            confirmacion.ShowDialog();

            if (confirmacion.BtnSiPresionado)
            {
                Close();
                return;
            }
        }
    }
}