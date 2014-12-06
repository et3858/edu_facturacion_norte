using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace facturacion_norte
{
    public partial class formPrincipal : Form
    {

        clConexion claseConexion = new clConexion();

        clSentencias sentencias = new clSentencias();

        DataTable dtListaFacturas = new DataTable();

        DataColumn dcFactID = new DataColumn("ID factura");
        DataColumn dcNombreCliente = new DataColumn("Nombre cliente");
        DataColumn dcFactSubTotal = new DataColumn("Subtotal");
        DataColumn dcFactIva = new DataColumn("IVA");
        DataColumn dcFactTotal = new DataColumn("Total");
        DataColumn dcFactMedioPago = new DataColumn("Medio de pago");
        DataColumn dcFactFecha = new DataColumn("Fecha de factura");
        DataColumn dcFactHora = new DataColumn("Hora de factura");
        DataColumn dcFactCancelar = new DataColumn("Anuladas");
        //DataColumn dcFactVinculoDetalle = new DataColumn("Detalles");


        DataTable dtPre = new DataTable();
        //DataRow drDetalle;

        DataGrid dg = new DataGrid();

        public void listafacturas() {
            try {
                DataTable dtListaTodasFacturas = new DataTable();
                MySqlConnection conexion = new MySqlConnection(claseConexion.cadena);
                conexion.Open();
                dtListaTodasFacturas = sentencias.dtListaFacturasPorCliente();
                conexion.Close();
            }catch(MySqlException ex){
                MessageBox.Show("No se puede visualizar las facturas: " + ex.Message);
            }
        }



        public void buscarResultados()
        {
            //MessageBox.Show("Está bien. Ahora a hacer las consultas");
            String buscador = txtBuscar.Text + "%";
            DataTable dtConsulta = new DataTable();
            dtConsulta = sentencias.dtConsultarFacturasPorCliente(buscador);
            //MessageBox.Show("Respuestas encontradas: " + dtConsulta.Rows.Count);         
            if (dtConsulta.Rows.Count > 0)
            {
                dgvListaFacturasPorCliente.DataSource = null;
                int contadorFilas = dgvListaFacturasPorCliente.RowCount - 1;
                for (int i = -1; i < contadorFilas; i++)
                {
                    dgvListaFacturasPorCliente.Rows.Remove(dgvListaFacturasPorCliente.CurrentRow);
                }
                dtListaFacturas.Clear();
                DataTable dtConsulta2 = new DataTable();



                for (Int32 k = 0; k < dtConsulta.Rows.Count; k++)
                {
                    DataRow drConsulta2 = dtListaFacturas.NewRow();
                    drConsulta2[dcFactID] = dtConsulta.Rows[k][0].ToString();
                    if (dtConsulta.Rows[k][1].ToString() == "")
                    {
                        drConsulta2[dcNombreCliente] = dtConsulta.Rows[k][9].ToString();
                    }
                    else
                    {
                        drConsulta2[dcNombreCliente] = dtConsulta.Rows[k][1].ToString();
                    }
                    //drConsulta2[dcNombreCliente] = dtConsulta.Rows[k][1].ToString();
                    drConsulta2[dcFactSubTotal] = dtConsulta.Rows[k][2].ToString();
                    drConsulta2[dcFactIva] = dtConsulta.Rows[k][3].ToString();
                    drConsulta2[dcFactTotal] = dtConsulta.Rows[k][4].ToString();
                    drConsulta2[dcFactMedioPago] = dtConsulta.Rows[k][5].ToString();
                    drConsulta2[dcFactFecha] = dtConsulta.Rows[k][6].ToString();
                    drConsulta2[dcFactHora] = dtConsulta.Rows[k][7].ToString();
                    drConsulta2[dcFactCancelar] = dtConsulta.Rows[k][8];

                    dtListaFacturas.Rows.Add(drConsulta2);
                }

                dgvListaFacturasPorCliente.DataSource = dtListaFacturas;
                if (dgvListaFacturasPorCliente.Rows.Count == 1)
                {
                    lblResultados.Text = "Se encontró " + Convert.ToString(dgvListaFacturasPorCliente.Rows.Count) + " resultado";
                }
                else if (dgvListaFacturasPorCliente.Rows.Count > 1)
                {
                    lblResultados.Text = "Se encontraron " + Convert.ToString(dgvListaFacturasPorCliente.Rows.Count) + " resultados";
                }
                lblResultados.Visible = true;
                //lblResultados.Text = Convert.ToString(dtConsulta.Rows.Count);
            }
            else
            {
                MessageBox.Show("No se encontraron resultados");
                txtBuscar.Focus();
                txtBuscar.SelectAll();
            }
        }
        
        


        public formPrincipal()
        {
            InitializeComponent();        
            //claseConexion.iniciarConexion();

            //dg.DataSource = sentencias.dtListaFacturasPorCliente();
            //dgvListaFacturasPorCliente.DataSource = dg;


            //dtListaFacturas = sentencias.dtListaFacturasPorCliente();
            dtListaFacturas.Columns.Add(dcFactID);
            dtListaFacturas.Columns.Add(dcNombreCliente);
            dtListaFacturas.Columns.Add(dcFactSubTotal);
            dtListaFacturas.Columns.Add(dcFactIva);
            dtListaFacturas.Columns.Add(dcFactTotal);
            dtListaFacturas.Columns.Add(dcFactMedioPago);
            dtListaFacturas.Columns.Add(dcFactFecha);
            dtListaFacturas.Columns.Add(dcFactHora);
            dtListaFacturas.Columns.Add(dcFactCancelar);
            //dtListaFacturas.Columns.Add(dcFactVinculoDetalle);
            
            dtPre = sentencias.dtListaFacturasPorCliente();

            //dtListaFacturas = sentencias.dtListaFacturasPorCliente();
            

            for (Int32 i = 0; i < dtPre.Rows.Count; i++)
            {
                DataRow drDetalle = dtListaFacturas.NewRow();
                drDetalle[dcFactID] = dtPre.Rows[i][0].ToString();
                if (dtPre.Rows[i][1].ToString() == "")
                {
                    drDetalle[dcNombreCliente] = dtPre.Rows[i][9].ToString();
                }
                else
                {
                    drDetalle[dcNombreCliente] = dtPre.Rows[i][1].ToString();
                }
                drDetalle[dcFactSubTotal] = dtPre.Rows[i][2].ToString();
                drDetalle[dcFactIva] = dtPre.Rows[i][3].ToString();
                drDetalle[dcFactTotal] = dtPre.Rows[i][4].ToString();
                drDetalle[dcFactMedioPago] = dtPre.Rows[i][5].ToString();
                drDetalle[dcFactFecha] = dtPre.Rows[i][6].ToString();
                drDetalle[dcFactHora] = dtPre.Rows[i][7].ToString();
                drDetalle[dcFactCancelar] = dtPre.Rows[i][8];
                
                //Button btnDetalleFactura = new Button();
                //btnDetalleFactura.Text = "Ver Detalle";
                //drDetalle[dcFactVinculoDetalle] = btnDetalleFactura;
                
                

                dtListaFacturas.Rows.Add(drDetalle);
            }
            //sentencias.dtListaFacturasPorCliente();

            DataGridViewButtonColumn btnDetallefactura = new DataGridViewButtonColumn();
            dgvListaFacturasPorCliente.Columns.Add(btnDetallefactura);
            btnDetallefactura.HeaderText = "Detalles";
            btnDetallefactura.Text = "Ver Detalle";
            btnDetallefactura.Name = "btnDetalles";
            btnDetallefactura.UseColumnTextForButtonValue = true;

            //DataGridViewLinkColumn linkDetalleFactura = new DataGridViewLinkColumn();
            //dgvListaFacturasPorCliente.Columns.Add(linkDetalleFactura);
            //linkDetalleFactura.HeaderText = "Links";
            //linkDetalleFactura.Text = "Ver detalle";
            //linkDetalleFactura.Name = "link";
            //linkDetalleFactura.UseColumnTextForLinkValue = true;

            dgvListaFacturasPorCliente.DataSource = dtListaFacturas;

            

        }

        private void formPrincipal_Load(object sender, EventArgs e)
        {
            
        }

        private void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            formNuevaFactura frmNewFact = new formNuevaFactura();
            frmNewFact.Show();
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            formAgregarCliente frmNewCliente = new formAgregarCliente();
            frmNewCliente.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Length > 0)
            {                
                buscarResultados();
            }
            else
            {
                MessageBox.Show("Por favor escriba el código de factura, nombre cliente ó medio de pago a buscar");
            }
        }

        private void dgvListaFacturasPorCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                Int32 codigoFactura = Convert.ToInt32(dgvListaFacturasPorCliente.CurrentRow.Cells[e.ColumnIndex + 1].Value);
                formDetalleFactura formDetFact = new formDetalleFactura(codigoFactura);
                formDetFact.Show();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int lastRowNum = dgvListaFacturasPorCliente.Rows.Count - 1;
            DataGridViewRow cursorDGV = dgvListaFacturasPorCliente.CurrentRow;
            int cursorFila = cursorDGV.Index;
            if (cursorFila >= lastRowNum)
            {
                MessageBox.Show("No más filas");
            }
            else
            {
                DataGridViewRow siguienteFila = dgvListaFacturasPorCliente.Rows[cursorFila + 1];
                dgvListaFacturasPorCliente.CurrentCell = siguienteFila.Cells[0];
                siguienteFila.Selected = true;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int lastRowNum = dgvListaFacturasPorCliente.Rows.Count - 1;
            DataGridViewRow cursorDGV = dgvListaFacturasPorCliente.CurrentRow;
            int cursorFila = cursorDGV.Index;
            if (cursorFila <= 0)
            {
                MessageBox.Show("No más filas");
            }
            else
            {
                DataGridViewRow siguienteFila = dgvListaFacturasPorCliente.Rows[cursorFila - 1];
                dgvListaFacturasPorCliente.CurrentCell = siguienteFila.Cells[0];
                siguienteFila.Selected = true;
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int cursorFila = dgvListaFacturasPorCliente.Rows.Count - 1;
            dgvListaFacturasPorCliente.FirstDisplayedScrollingRowIndex = dgvListaFacturasPorCliente.Rows.Count - 1;
            dgvListaFacturasPorCliente.CurrentCell = dgvListaFacturasPorCliente.Rows[cursorFila].Cells[0];
            dgvListaFacturasPorCliente.Rows[cursorFila].Selected = true;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            int cursorFila = 0;
            dgvListaFacturasPorCliente.FirstDisplayedScrollingRowIndex = 0;
            dgvListaFacturasPorCliente.CurrentCell = dgvListaFacturasPorCliente.Rows[0].Cells[0];
            dgvListaFacturasPorCliente.Rows[cursorFila].Selected = true;
        }

        private void btnReestablecer_Click(object sender, EventArgs e)
        {
            dgvListaFacturasPorCliente.DataSource = null;
            int contadorFilas = dgvListaFacturasPorCliente.RowCount - 1;
            for (int i = -1; i < contadorFilas; i++)
            {
                dgvListaFacturasPorCliente.Rows.Remove(dgvListaFacturasPorCliente.CurrentRow);
            }
            dtListaFacturas.Clear();
            
            if (lblResultados.Visible == true){
                lblResultados.Visible = false;
            }
            txtBuscar.Text = "";
            
            dtPre = sentencias.dtListaFacturasPorCliente();

            for (Int32 i = 0; i < dtPre.Rows.Count; i++)
            {
                DataRow drDetalle = dtListaFacturas.NewRow();
                drDetalle[dcFactID] = dtPre.Rows[i][0].ToString();
                drDetalle[dcNombreCliente] = dtPre.Rows[i][1].ToString();
                drDetalle[dcFactSubTotal] = dtPre.Rows[i][2].ToString();
                drDetalle[dcFactIva] = dtPre.Rows[i][3].ToString();
                drDetalle[dcFactTotal] = dtPre.Rows[i][4].ToString();
                drDetalle[dcFactMedioPago] = dtPre.Rows[i][5].ToString();
                drDetalle[dcFactFecha] = dtPre.Rows[i][6].ToString();
                drDetalle[dcFactHora] = dtPre.Rows[i][7].ToString();
                drDetalle[dcFactCancelar] = dtPre.Rows[i][8];


                dtListaFacturas.Rows.Add(drDetalle);
            }
            dgvListaFacturasPorCliente.DataSource = dtListaFacturas;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                //MessageBox.Show("Hola mundo");
                if (txtBuscar.Text.Length > 0)
                {                    
                    buscarResultados();
                }
                else
                {
                    MessageBox.Show("Por favor escriba el código de factura, nombre cliente ó medio de pago a buscar");
                }
            }
        }

        private void dgvListaFacturasPorCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                e.SuppressKeyPress = true;
                dgvListaFacturasPorCliente.CurrentRow.Selected = true;
                //MessageBox.Show(dgvListaFacturasPorCliente.CurrentRow.Cells[1].Value.ToString());
                Int32 codigoFactura = Convert.ToInt32(dgvListaFacturasPorCliente.CurrentRow.Cells[1].Value);
                formDetalleFactura formDetFact = new formDetalleFactura(codigoFactura);
                formDetFact.Show();
            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            formMantenedorProductos mantenedorProductos = new formMantenedorProductos();
            mantenedorProductos.Show();
        }
    }
}
