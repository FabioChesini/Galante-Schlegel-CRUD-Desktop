using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;

namespace catalogo.Control
{
    public class Control_Marcas
    {

        public List<string> lstmarcas = new List<string>();

        public List<string> listar()
        {

            try
            {
                lstmarcas = new List<string>();

                ConexionBD objconexion = new ConexionBD();

                SqlConnection con = objconexion.Conectar()
                    ;
                SqlCommand comando = new SqlCommand();

                SqlDataReader lector;

                string query = "Select Descripcion from MARCAS";
                comando.Connection = con;

                comando.CommandText = query;

                comando.CommandType = System.Data.CommandType.Text;

                lector = comando.ExecuteReader();
                Marca objmarca = new Marca();
                while (lector.Read())
                {
                    
                    objmarca.Descripcion = lector.GetString(0);
                    
                    lstmarcas.Add(objmarca.Descripcion);
                }
                con.Close();
                return lstmarcas;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return null;
            }

        }
        public void Agregar_Marca(string marca)
        {
            try
            {
                string query = "insert into MARCAS(Descripcion) values ('" +marca + "');";

                Control.ConexionBD conexionBD = new Control.ConexionBD();
                SqlConnection sqlConnection = conexionBD.Conectar();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = query;

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                MessageBox.Show("Marca agregada");         
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
