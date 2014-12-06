using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace facturacion_norte
{
    public partial class formMantenedorProductos : Form
    {
        clSentencias sentencias = new clSentencias();
        clConexion conexion = new clConexion();

        DataTable dtListaProductos = new DataTable();

        DataColumn dcProductoID = new DataColumn("ID");
        DataColumn dcProducto = new DataColumn("Descripción");
        DataColumn dcUnidades = new DataColumn("Unidades disponibles");
        DataColumn dcValor = new DataColumn("Valor unidad");


        DataTable dtListaPre = new DataTable();

        public formMantenedorProductos()
        {
            InitializeComponent();

            dtListaProductos.Columns.Add(dcProductoID);
            dtListaProductos.Columns.Add(dcProducto);
            dtListaProductos.Columns.Add(dcUnidades);
            dtListaProductos.Columns.Add(dcValor);

            dtListaPre = sentencias.dtListaProductosTodo();

            for (Int32 i = 0; i < dtListaPre.Rows.Count; i++)
            {
                DataRow drProducto = dtListaProductos.NewRow();
                drProducto[dcProductoID] = dtListaPre.Rows[i][0].ToString();
                drProducto[dcProducto] = dtListaPre.Rows[i][1].ToString();
                drProducto[dcUnidades] = dtListaPre.Rows[i][2].ToString();
                drProducto[dcValor] = dtListaPre.Rows[i][3].ToString();
                dtListaProductos.Rows.Add(drProducto);
            }

            dgvListaProductos.DataSource = dtListaProductos;
        }

        private void dgvListaProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodProd.Text = dgvListaProductos.CurrentRow.Cells[0].Value.ToString();
            txtDescrpcionProd.Text = dgvListaProductos.CurrentRow.Cells[1].Value.ToString();
            txtUnitsProd.Text = dgvListaProductos.CurrentRow.Cells[2].Value.ToString();
            txtValorProd.Text = dgvListaProductos.CurrentRow.Cells[3].Value.ToString();
        }

        private void formMantenedorProductos_Load(object sender, EventArgs e)
        {

        }

        private void dgvListaProductos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                txtCodProd.Text = dgvListaProductos.CurrentRow.Cells[0].Value.ToString();
                txtDescrpcionProd.Text = dgvListaProductos.CurrentRow.Cells[1].Value.ToString();
                txtUnitsProd.Text = dgvListaProductos.CurrentRow.Cells[2].Value.ToString();
                txtValorProd.Text = dgvListaProductos.CurrentRow.Cells[3].Value.ToString();
            }
        }

        private void dgvListaProductos_KeyDown(object sender, KeyEventArgs e)
        {
            //int firstRow = 0;
            DataGridViewRow cursorArriba = dgvListaProductos.CurrentRow;
            int cursorFilaArriba = cursorArriba.Index;

            if (e.KeyCode == Keys.Up)
            {
                if (cursorFilaArriba <= 0)
                {
                    MessageBox.Show("No más filas");
                }
                else
                {
                    dgvListaProductos.CurrentRow.Selected = true;
                    Int32 sel = dgvListaProductos.CurrentRow.Index;
                    txtCodProd.Text = dgvListaProductos.Rows[sel - 1].Cells[0].Value.ToString();
                    //txtCodProd.Text = dgvListaProductos.CurrentRow.Cells[0].Value.ToString();
                    txtDescrpcionProd.Text = dgvListaProductos.Rows[sel - 1].Cells[1].Value.ToString();
                    txtUnitsProd.Text = dgvListaProductos.Rows[sel - 1].Cells[2].Value.ToString();
                    txtValorProd.Text = dgvListaProductos.Rows[sel - 1].Cells[3].Value.ToString();
                }
            }

            int lastRow = dgvListaProductos.Rows.Count - 1;
            DataGridViewRow cursorAbajo = dgvListaProductos.CurrentRow;
            int cursorFilaAbajo = cursorAbajo.Index;

            if (e.KeyCode == Keys.Down)
            {
                if (cursorFilaAbajo >= lastRow)
                {
                    MessageBox.Show("No mas filas");
                }
                else
                {
                    dgvListaProductos.CurrentRow.Selected = true;
                    Int32 sel = dgvListaProductos.CurrentRow.Index;
                    txtCodProd.Text = dgvListaProductos.Rows[sel + 1].Cells[0].Value.ToString();
                    //txtCodProd.Text = dgvListaProductos.CurrentRow.Cells[0].Value.ToString();
                    txtDescrpcionProd.Text = dgvListaProductos.Rows[sel + 1].Cells[1].Value.ToString();
                    txtUnitsProd.Text = dgvListaProductos.Rows[sel + 1].Cells[2].Value.ToString();
                    txtValorProd.Text = dgvListaProductos.Rows[sel + 1].Cells[3].Value.ToString();
                }
            }

            //if (e.KeyCode == Keys.Up)
            //{
            //    int lastRowNum = dgvListaProductos.Rows.Count - 1;
            //    DataGridViewRow cursorDGV = dgvListaProductos.CurrentRow;
            //    int cursorFila = cursorDGV.Index;
            //    if (cursorFila <= 0)
            //    {
            //        MessageBox.Show("No más filas");
            //    }
            //    else
            //    {
            //        DataGridViewRow siguienteFila = dgvListaProductos.Rows[cursorFila];
            //        dgvListaProductos.CurrentCell = siguienteFila.Cells[0];
            //        txtCodProd.Text = siguienteFila.Cells[0].Value.ToString();
            //        siguienteFila.Selected = true;
            //    }
            //}
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            dgvListaProductos.Enabled = false;
            dgvListaProductos.CurrentRow.Selected = false;
            btnNuevo.Enabled = false;
            btnEliminar.Enabled = false;

            txtCodProd.Text = "";
            txtDescrpcionProd.Text = "";
            txtUnitsProd.Text = "";
            txtValorProd.Text = "";

            txtDescrpcionProd.ReadOnly = false;
            txtUnitsProd.ReadOnly = false;
            txtValorProd.ReadOnly = false;
            txtDescrpcionProd.Focus();

            btnModificar.Text = "CLICK AQUÍ para guardar nuevo producto";

        }
    }
}
