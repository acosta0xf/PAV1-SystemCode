using System;
using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Datos
{
    public static class ClienteDatos
    {
        private static Cliente DiseniarCliente(int posicion, DataTable tabla)
        {
            int id_cliente = Convert.ToInt32(tabla.Rows[posicion]["id_cliente"].ToString());
            string cuit = tabla.Rows[posicion]["cuit"].ToString();
            string razon_social = tabla.Rows[posicion]["razon_social"].ToString();
            string calle = tabla.Rows[posicion]["calle"].ToString();
            int numero = Convert.ToInt32(tabla.Rows[posicion]["numero"].ToString());
            DateTime fecha_alta = Convert.ToDateTime(tabla.Rows[posicion]["fecha_alta"]);
            int id_barrio = Convert.ToInt32(tabla.Rows[posicion]["id_barrio"].ToString());
            int id_contacto = (tabla.Rows[posicion]["id_contacto"].ToString() == "") ? -1 : Convert.ToInt32(tabla.Rows[posicion]["id_contacto"].ToString());
            bool borrado = Convert.ToBoolean(tabla.Rows[posicion]["borrado"]);

            Contacto contacto = null;
            Barrio barrio = null;

            if (!borrado)
            {
                barrio = BarrioDatos.ConsultarBarrio(id_barrio)[0];

                if (id_contacto != -1)
                {
                    contacto = ContactoDatos.ConsultarContacto(cuit)[0];
                }
            }
            
            return new Cliente(id_cliente, cuit, razon_social, borrado, calle, numero, fecha_alta, barrio, contacto);
        }

        public static IList<Cliente> ConsultarTablaClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            DataTable tablaClientes = GestorBD.ConsultarTabla("Clientes");
            for (int i = 0; i < tablaClientes.Rows.Count; i++)
            {
                listaClientes.Add(DiseniarCliente(i, tablaClientes));
            }

            return listaClientes;
        }

        public static DataTable ConsultarTablaClientesComboBox()
        {
            string SQL = "SELECT clientes.* " +
                        "FROM Clientes clientes " +
                        "WHERE borrado = 0";

            return GestorBD.Consultar(SQL);
        }

        public static IList<Cliente> ConsultarTablaClientesFiltro(string filtro)
        {
            string SQL = "SELECT clientes.* " +
                         "FROM Clientes clientes " +
                         "WHERE cuit LIKE '" + filtro + "%'";

            List<Cliente> listaClientes = new List<Cliente>();

            DataTable tabla = GestorBD.Consultar(SQL);
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                listaClientes.Add(DiseniarCliente(i, tabla));
            }

            return listaClientes;
        }

        public static Cliente ConsultarClientePorCUIT(string CUIT)
        {
            string SQL = "SELECT clientes.* " +
                         "FROM Clientes clientes " +
                         "WHERE cuit = '" + CUIT.ToString() + "'";

            DataTable tabla = GestorBD.Consultar(SQL);

            return (tabla.Rows.Count > 0) ? DiseniarCliente(0, tabla) : null;
        }

        public static Cliente ConsultarClientePorID(int idCliente)
        {
            string SQL = "SELECT clientes.* " +
                         "FROM Clientes clientes " +
                         "WHERE " +
                            "id_cliente = " + idCliente.ToString() + " AND " +
                            "borrado = 0";

            DataTable tabla = GestorBD.Consultar(SQL);

            return (tabla.Rows.Count > 0) ? DiseniarCliente(0, tabla) : null;
        }

        public static string InsertarCliente(Cliente cliente)
        {
            string error;

            string SQL = "INSERT INTO Clientes " +
                         "VALUES " +
                         "('" +
                            cliente.Cuit.ToString() + "', '" +
                            cliente.Razon_social.ToString() + "', '" +
                            "0', '" + 
                            cliente.Calle.ToString() + "', '" +
                            cliente.Numero.ToString() + "', " +
                            "CAST(N'" + cliente.Fecha_alta.ToString("yyyy-MM-dd") + "' AS Date), " +
                            cliente.BarrioAsociado.Id_barrio + ", " +
                            "null" +
                         ")";

            error = GestorBD.Ejecutar(SQL);
            if (error == "")
            {
                error = ContactoDatos.InsertarContacto(cliente.ContactoAsociado);
            }

            return error;
        }

        public static string ModificarCliente(Cliente cliente)
        {
            string error;

            string SQL = "UPDATE Clientes " +
                         "SET " +
                            "razon_social = '" + cliente.Razon_social.ToString() + "', " +
                            "borrado = " + Convert.ToInt32(cliente.Borrado) + ", " +
                            "calle = '" + cliente.Calle.ToString() + "', " +
                            "numero = '" + cliente.Numero.ToString() + "', " +
                            "fecha_alta = CAST(N'" + cliente.Fecha_alta.ToString("yyyy-MM-dd") + "' AS Date), " +
                            "id_barrio = " + cliente.BarrioAsociado.Id_barrio.ToString() + " " +
                         "WHERE cuit = '" + cliente.Cuit.ToString() + "'";

            error = GestorBD.Ejecutar(SQL);
            if (error == "")
            {
                error = ContactoDatos.ModificarContacto(cliente.ContactoAsociado);
            }

            return error;
        }

        public static string EliminarCliente(Cliente cliente)
        {
            string error;

            string SQL = "UPDATE Clientes " +
                         "SET " +
                            "borrado = 1 " +
                         "WHERE cuit = '" + cliente.Cuit.ToString() + "'";

            error = GestorBD.Ejecutar(SQL);
            if (error == "")
            {
                error = ContactoDatos.EliminarContacto(cliente.ContactoAsociado);
            }
            
            return error;
        }
    }
}