using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;


namespace catalogo
{
    public partial class Menu : Form
    {
        Control.Control_Articulos control = new Control.Control_Articulos();
        public Menu()
        {
            InitializeComponent();

        }
        private void Menu_Load(object sender, EventArgs e)
        {
            dgvPrincipal.DataSource = control.listar();
        }

        private void dgvPrincipal_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvPrincipal.CurrentRow.DataBoundItem;
            try
            {
                picPrincipal.Load(seleccionado.imagenUrl);
            }
            catch (Exception ex)
            {
                picPrincipal.Load("https://cdn.pixabay.com/photo/2017/01/25/17/35/picture-2008484_960_720.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Vistas.Agregar agregar = new Vistas.Agregar();
            agregar.ShowDialog();
            dgvPrincipal.DataSource = control.listar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvPrincipal.CurrentRow.DataBoundItem;
            Control.Control_Articulos objctrlArticulos = new Control.Control_Articulos();
            objctrlArticulos.eliminarArticulo(seleccionado.Id);
            dgvPrincipal.DataSource = control.listar();
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvPrincipal.CurrentRow.DataBoundItem;
            Vistas.Modificar fmrmodificar = new Vistas.Modificar(seleccionado);
            fmrmodificar.ShowDialog();
            dgvPrincipal.DataSource = control.listar();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvPrincipal.CurrentRow.DataBoundItem;
            Vistas.verDetalle fmrDetalle = new Vistas.verDetalle(seleccionado);
            fmrDetalle.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            string filtro = txtBusqueda.Text;
            List<Articulo> lista_filtrada = control.listar();
           

            if(filtro == null)
            {
                dgvPrincipal = null;
                dgvPrincipal.DataSource = control.listar();
                
            }
            else
            {
                List<Articulo> listaFiltrada = lista_filtrada.FindAll(W => W.Descripcion.ToUpper().Contains(filtro.ToUpper()) || W.Nombre.ToUpper().Contains(filtro.ToUpper()) || W.Categoria.ToUpper().Contains(filtro.ToUpper()));
                dgvPrincipal.DataSource = null;
                dgvPrincipal.DataSource = listaFiltrada;
            }
        }

        private void categoríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vistas.Agregar_Categorias agregar_Categorias = new Vistas.Agregar_Categorias();

            agregar_Categorias.ShowDialog();
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vistas.Agragar_Marcas agragar_Marcas = new Vistas.Agragar_Marcas();

            agragar_Marcas.ShowDialog();
        }
    }
}
