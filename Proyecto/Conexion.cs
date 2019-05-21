using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Proyecto
{
    class Conexion
    {
        private static String cadenaConexion = "Data Source=localhost; Initial Catalog=ElectivaIVFinal; Integrated Security=True";
        private SqlConnection conexion = new SqlConnection(cadenaConexion);

        public SqlConnection AbrirConexion()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
            return conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
            return conexion;
        }
    }
}
