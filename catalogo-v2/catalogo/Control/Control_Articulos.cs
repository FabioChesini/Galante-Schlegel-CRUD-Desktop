using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Modelo;
using System.Windows.Forms;
namespace catalogo.Control
{
    public class Control_Articulos
    {
        public List<Articulo> listaArticulos;
        public List<Articulo> listar(){
            ConexionBD objconexion = new ConexionBD();
            SqlConnection con = objconexion.Conectar(); ;
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            con = objconexion.Conectar();
            try
            {
                listaArticulos = new List<Articulo>();
                string query = "select ARTICULOS.Id, ARTICULOS.Codigo, ARTICULOS.Nombre, ARTICULOS.ImagenUrl, ARTICULOS.Precio,ARTICULOS.Descripcion, MARCAS.Descripcion, CATEGORIAS.Descripcion from ARTICULOS inner join MARCAS  on IdMarca =  MARCAS.Id Inner join CATEGORIAS on CATEGORIAS.Id = ARTICULOS.IdCategoria";
                comando.Connection = con;
                comando.CommandText = query;
                comando.CommandType = System.Data.CommandType.Text;
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo objarticulo = new Articulo();
                    objarticulo.Id = lector.GetInt32(0);
                    objarticulo.Codigo = lector.GetString(1);
                    objarticulo.Nombre = lector.GetString(2);
                    objarticulo.imagenUrl = lector.GetString(3);
                    objarticulo.Precio = lector.GetDecimal(4);
                    objarticulo.Descripcion = lector.GetString(5);
                    objarticulo.Marca = lector.GetString(6);
                    objarticulo.Categoria = lector.GetString(7);
                    listaArticulos.Add(objarticulo);
                }
                con.Close();
                return listaArticulos;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
                return null;
            }
        }

        public int obtenerIdCategoria(Articulo objarticulo)
        {
            ConexionBD objconexion = new ConexionBD();
            SqlConnection con = objconexion.Conectar(); ;
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            con = objconexion.Conectar(); 
            try
            {
                comando.CommandText = "select CATEGORIAS.id from CATEGORIAS where CATEGORIAS.Descripcion = @descCat";
                comando.Parameters.AddWithValue("@descCat", objarticulo.Categoria);
                comando.Connection = con;
                comando.CommandType = System.Data.CommandType.Text;
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    return lector.GetInt32(0);
                }
                lector.Close();
                con.Close();
            }
            catch (SqlException ex) {
                MessageBox.Show(ex.Message);
                return 0;
            }
            lector.Close();
            con.Close();
            return 0;
        }
        public int obtenerIdMarca(Articulo objarticulo)
        {
            ConexionBD objconexion = new ConexionBD();
            SqlConnection con = objconexion.Conectar(); ;
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            con = objconexion.Conectar();
            try
            {
                comando.CommandText = "select MARCAS.id from MARCAS where MARCAS.Descripcion = @descMarca";
                comando.Parameters.AddWithValue("@descMarca", objarticulo.Marca);
                comando.Connection = con;
                comando.CommandType = System.Data.CommandType.Text;
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    return lector.GetInt32(0);
                }
                lector.Close();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            con.Close();
            return 0;
        }
        public void insertarArticulo(Articulo objarticulo)
        {
            ConexionBD objconexion = new ConexionBD();
            SqlConnection con = objconexion.Conectar(); ;
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.CommandText = "INSERT INTO ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)";
                comando.Parameters.AddWithValue("@Codigo", objarticulo.Codigo);
                comando.Parameters.AddWithValue("@Nombre", objarticulo.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", objarticulo.Descripcion);
                comando.Parameters.AddWithValue("@IdMarca", obtenerIdMarca(objarticulo));
                comando.Parameters.AddWithValue("@IdCategoria", obtenerIdCategoria(objarticulo));
                comando.Parameters.AddWithValue("@ImagenUrl", objarticulo.imagenUrl);
                comando.Parameters.AddWithValue("@Precio", objarticulo.Precio);
                comando.Connection = con;
                comando.CommandType = System.Data.CommandType.Text;
                comando.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void eliminarArticulo(int id)
        {
            ConexionBD objconexion = new ConexionBD();
            SqlConnection con = objconexion.Conectar(); ;
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = con;
                comando.CommandText = "DELETE FROM ARTICULOS WHERE ARTICULOS.Id = @id";
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Artículo eliminado correctamente");
            }catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void modificarArticulo(Articulo articulo)
        {
            ConexionBD objconexion = new ConexionBD();
            SqlConnection con = objconexion.Conectar(); ;
            SqlCommand comando;
            try
            {
                int idMarca = Convert.ToInt32(obtenerIdMarca(articulo));
                int idCategoria = Convert.ToInt32(obtenerIdCategoria(articulo));
                string query = "update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdMarca, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id";
                comando = new SqlCommand(query, con);
                comando.Parameters.AddWithValue("@Codigo", articulo.Codigo);
                comando.Parameters.AddWithValue("@Nombre", articulo.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", articulo.Descripcion);
                comando.Parameters.AddWithValue("@IdMarca", obtenerIdMarca(articulo));
                comando.Parameters.AddWithValue("@IdCategoria", obtenerIdCategoria(articulo));
                comando.Parameters.AddWithValue("@ImagenUrl", articulo.imagenUrl);
                comando.Parameters.AddWithValue("@Precio", articulo.Precio);
                comando.Parameters.AddWithValue("@Id", articulo.Id);
                comando.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
     
    }
   
}
