using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace catalogo.Control
{
    public class ConexionBD
    {
        
        public SqlConnection Conectar()
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.ConnectionString = "data source= localhost; initial catalog=CATALOGO_DB; integrated security=true";
                comando.Connection = conexion;
                conexion.Open();
                return conexion;
            }catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

    }
}
