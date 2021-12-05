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
    public class Control_Categorias
    {
        public List<string> lstcategorias = new List<string>();
        public List<string> listar()
        {

            try
            {
                lstcategorias = new List<string>();

                ConexionBD objconexion = new ConexionBD();

                SqlConnection con = objconexion.Conectar();

                SqlCommand comando = new SqlCommand();

                SqlDataReader lector;

                string query = "Select CATEGORIAS.Descripcion from CATEGORIAS";
                comando.Connection = con;

                comando.CommandText = query;

                comando.CommandType = System.Data.CommandType.Text;

                lector = comando.ExecuteReader();
                Categoria objcategoria = new Categoria();
                while (lector.Read())
                {
                    objcategoria.Descripcion = lector.GetString(0);

                    lstcategorias.Add(objcategoria.Descripcion);
                }
                con.Close();
                return lstcategorias;
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void Agregar_Categoria(string categoria)
        {
            try
            {
                string query = "insert into CATEGORIAS(Descripcion) values ('" + categoria + "');";

                Control.ConexionBD conexionBD = new Control.ConexionBD();
                SqlConnection sqlConnection = conexionBD.Conectar();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = query;

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                MessageBox.Show("Categoria agregada");

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}

