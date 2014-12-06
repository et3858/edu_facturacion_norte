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
    public partial class formDetalleFactura : Form
    {

        private Int32? idBusqueda = null;

        clSentencias sentencias = new clSentencias();

        DataTable dtDetalles = new DataTable();

        DataColumn colProductoID = new DataColumn("ID");
        DataColumn colProducto = new DataColumn("Producto");
        DataColumn colValorProducto = new DataColumn("Valor unitario");
        DataColumn colCantidad = new DataColumn("Cantidad");
        DataColumn colTotalPorCantidad = new DataColumn("Total por cantidad");

        DataTable dtDetallesPre = new DataTable();  //este datatable solo se va a encargar de los productos como detalles incluidos en una factura
        DataTable dtFacturaYCliente = new DataTable();

        public formDetalleFactura()
        {
            InitializeComponent();
            dtDetalles.Columns.Add(colProductoID);
            dtDetalles.Columns.Add(colProducto);
            dtDetalles.Columns.Add(colValorProducto);
            dtDetalles.Columns.Add(colCantidad);
            dtDetalles.Columns.Add(colTotalPorCantidad);
            dgvListaDetalle.DataSource = dtDetalles;

            
        }

        public formDetalleFactura(Int32 idBusqueda) : this()
        {
            this.idBusqueda = idBusqueda;
        }

        private void formDetalleFactura_Load(object sender, EventArgs e)
        {
            if (idBusqueda.HasValue)
            {
                txtCodFactura.Text = Convert.ToString(idBusqueda);
                Int32 codigoFactura = Convert.ToInt32(idBusqueda);
                dtFacturaYCliente = sentencias.dtBuscarDatosFacturaYCliente(codigoFactura);

                String nombreAPosicionar = "";
                if (dtFacturaYCliente.Rows[0][0].ToString() == "")
                {
                    nombreAPosicionar = dtFacturaYCliente.Rows[0][12].ToString();
                }
                else
                {
                    nombreAPosicionar = dtFacturaYCliente.Rows[0][0].ToString();
                }
                txtNombreCliente.Text = nombreAPosicionar;
                String rutComp = dtFacturaYCliente.Rows[0][1].ToString() + "-" + dtFacturaYCliente.Rows[0][2].ToString();
                mtxtRut.Text = rutComp;
                txtGiro.Text = dtFacturaYCliente.Rows[0][13].ToString();

                txtDireccion.Text = dtFacturaYCliente.Rows[0][3].ToString();
                txtComuna.Text = dtFacturaYCliente.Rows[0][4].ToString();
                if (dtFacturaYCliente.Rows[0][5].ToString() == "")
                {
                    txtCiudad.Text = "(No tiene ciudad)";
                }
                else
                {
                    txtCiudad.Text = dtFacturaYCliente.Rows[0][5].ToString();
                }
                

                String[] fecha = dtFacturaYCliente.Rows[0][6].ToString().Split('-');
                mtxtFecha.Text = fecha[2] + "-" + fecha[1] + "-" + fecha[0];
                mtxtHora.Text = dtFacturaYCliente.Rows[0][7].ToString();

                txtSubTotal.Text = dtFacturaYCliente.Rows[0][8].ToString();
                txtIva.Text = dtFacturaYCliente.Rows[0][9].ToString();
                txtTotal.Text = dtFacturaYCliente.Rows[0][10].ToString();
                txtMedioPago.Text = dtFacturaYCliente.Rows[0][11].ToString();

                dtDetallesPre = sentencias.dtListaDetalleFacturaSegunCodigo(codigoFactura);

                for (int i = 0; i < dtDetallesPre.Rows.Count; i++)
                {
                    DataRow drDetalle = dtDetalles.NewRow();
                    drDetalle[colProductoID] = dtDetallesPre.Rows[i][0].ToString();
                    if (dtDetallesPre.Rows[i][5].ToString() == null || dtDetallesPre.Rows[i][5].ToString() == "")
                    {
                        drDetalle[colProducto] = dtDetallesPre.Rows[i][1].ToString();
                    }
                    else
                    {
                        drDetalle[colProducto] = dtDetallesPre.Rows[i][5].ToString();
                    }
                    //drDetalle[colProducto] = dtDetallesPre.Rows[i][1].ToString();
                    if (dtDetallesPre.Rows[i][6].ToString() == null || dtDetallesPre.Rows[i][6].ToString() == "")
                    {
                        drDetalle[colValorProducto] = dtDetallesPre.Rows[i][2].ToString();
                    }
                    else
                    {
                        drDetalle[colValorProducto] = dtDetallesPre.Rows[i][6].ToString();
                    }
                    //drDetalle[colValorProducto] = dtDetallesPre.Rows[i][2].ToString();
                    drDetalle[colCantidad] = dtDetallesPre.Rows[i][3].ToString();
                    drDetalle[colTotalPorCantidad] = dtDetallesPre.Rows[i][4].ToString();

                    dtDetalles.Rows.Add(drDetalle);
                }

                dgvListaDetalle.DataSource = dtDetalles;
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            DialogResult dialogoAnular = MessageBox.Show(
                "Usted está apunto de anular una factura existente. ¿Desea aplicar esta anulación?", 
                "Factura", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Exclamation, 
                MessageBoxDefaultButton.Button2
                );

            DialogResult dialogoAnularInsiste = MessageBox.Show(
                "¿De veras está seguro de que desea aplicar esta anulación a esta factura?",
                "Factura",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
                );
            if (dialogoAnular == DialogResult.Yes)
            {
                if (dialogoAnularInsiste == DialogResult.Yes)
                {
                    Int32 codFactura = Convert.ToInt32(txtCodFactura.Text);
                    sentencias.anularFactura(codFactura);
                    MessageBox.Show("Factura anulada");
                    this.Close();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
