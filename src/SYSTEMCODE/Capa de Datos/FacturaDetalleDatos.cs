using System;
using System.Collections.Generic;
using System.Data;
using SYSTEMCODE.Capa_de_Negocio;

namespace SYSTEMCODE.Capa_de_Datos
{
    public class FacturaDetalleDatos
    {
        private static FacturaDetalle DiseniarFacturaDetalle(int posicion, DataTable tabla)
        {
            string numeroFactura = tabla.Rows[posicion]["numero_factura"].ToString();
            int idProyectoAsociado = Convert.ToInt32(tabla.Rows[posicion]["id_proyecto"].ToString());
            int cantidadLicencias = Convert.ToInt32(tabla.Rows[posicion]["cantidad_licencias"].ToString());
            int precio = Convert.ToInt32(tabla.Rows[posicion]["precio"]);
            bool borrado = Convert.ToBoolean(tabla.Rows[posicion]["borrado"]);

            Proyecto proyectoAsociado = Proyecto.ObtenerProyectoPorID(idProyectoAsociado);

            return new FacturaDetalle(numeroFactura, proyectoAsociado, cantidadLicencias, precio, borrado);
        }

        public static IList<FacturaDetalle> ConsultarListaFacturaDetalle(string numeroFactura)
        {
            string SQL = "SELECT facturasDetalle.* " +
                         "FROM FacturasDetalle facturasDetalle " +
                         "WHERE " +
                            "borrado = 0 AND " +
                            "numero_factura = '" + numeroFactura.ToString() + "'";

            DataTable tabla = GestorBD.Consultar(SQL);

            IList<FacturaDetalle> listaFacturaDetalle = new List<FacturaDetalle>();

            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                listaFacturaDetalle.Add(DiseniarFacturaDetalle(i, tabla));
            }

            return listaFacturaDetalle;
        }
    }
}
