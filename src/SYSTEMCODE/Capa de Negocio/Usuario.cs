﻿using SYSTEMCODE.Capa_de_Datos;

namespace SYSTEMCODE.Capa_de_Negocio
{
    public class Usuario
    {
        private int id_usuario;
        private string dni;
        private string nombreUsuario;
        private Perfil perfil;
        private string clave;
        private string email;
        private bool borrado;

        public Usuario()
        {

        }

        public Usuario(string dni)
        {
            this.Dni = dni;
        }

        public Usuario(string dni, string nombreUsuario, Perfil perfil, string clave, string email)
        {
            this.Dni = dni;
            this.NombreUsuario = nombreUsuario;
            this.Perfil = perfil;
            this.Clave = clave;
            this.Email = email;
        }

        public Usuario(string dni, string nombreUsuario, Perfil perfil, string clave, string email, bool borrado)
        {
            this.Dni = dni;
            this.NombreUsuario = nombreUsuario;
            this.Perfil = perfil;
            this.Clave = clave;
            this.Email = email;
            this.Borrado = borrado;
        }

        public Usuario(int id_usuario, string dni, string nombreUsuario, Perfil perfil, string clave, string email, bool borrado)
        {
            this.Id_usuario = id_usuario;
            this.Dni = dni;
            this.NombreUsuario = nombreUsuario;
            this.Perfil = perfil;
            this.Clave = clave;
            this.Email = email;
            this.Borrado = borrado;
        }

        public int Id_usuario { get => id_usuario; set => id_usuario = value; }
        public string Dni { get => dni; set => dni = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public Perfil Perfil { get => perfil; set => perfil = value; }
        public string Clave { get => clave; set => clave = value; }
        public string Email { get => email; set => email = value; }
        public bool Borrado { get => borrado; set => borrado = value; }

        public static Usuario ValidarUsuario(string nombreUsuario, string clave)
        {
            Usuario usuario = UsuarioDatos.ConsultarUsuarioPorNombreUsuario(nombreUsuario);
            
            if (usuario != null)
            {
                if (usuario.Clave.Equals(clave) && !usuario.Borrado)
                {
                    return usuario;
                }

                return null;
            }

            return null;
        }

        public static string ObtenerPerfil(Usuario usuario)
        {
            return usuario.Perfil.Nombre.ToString();
        }

        public static Usuario ObtenerUsuario(string dni)
        {
            return UsuarioDatos.ConsultarUsuarioPorDNI(dni);
        }

        public static string AgregarUsuario(Usuario usuario)
        {
            return UsuarioDatos.InsertarUsuario(usuario);
        }

        public static string ModificarUsuario(Usuario usuario)
        {
            return UsuarioDatos.ModificarUsuario(usuario);
        }

        public static string EliminarUsuario(Usuario usuario)
        {
            return UsuarioDatos.EliminarUsuario(usuario);
        }
    }
}