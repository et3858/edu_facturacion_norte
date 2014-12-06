namespace facturacion_norte
{
    partial class formNuevaFactura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDetalleFactura = new System.Windows.Forms.DataGridView();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.txtUnidades = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.txtIVA = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnLimpiarTabla = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.cbClientes = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAgregarCliente = new System.Windows.Forms.Button();
            this.btnNuevaTransaccion = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.btnPruebaCertX509 = new System.Windows.Forms.Button();
            this.cbMediosPago = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNumeroPrueba = new System.Windows.Forms.TextBox();
            this.btnNumeroNombrePrueba = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleFactura)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDetalleFactura
            // 
            this.dgvDetalleFactura.AllowUserToAddRows = false;
            this.dgvDetalleFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleFactura.Location = new System.Drawing.Point(48, 32);
            this.dgvDetalleFactura.Name = "dgvDetalleFactura";
            this.dgvDetalleFactura.ReadOnly = true;
            this.dgvDetalleFactura.Size = new System.Drawing.Size(552, 150);
            this.dgvDetalleFactura.TabIndex = 0;
            // 
            // cbProducto
            // 
            this.cbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProducto.FormattingEnabled = true;
            this.cbProducto.Location = new System.Drawing.Point(101, 210);
            this.cbProducto.Name = "cbProducto";
            this.cbProducto.Size = new System.Drawing.Size(268, 21);
            this.cbProducto.TabIndex = 1;
            this.cbProducto.SelectedIndexChanged += new System.EventHandler(this.cbProducto_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Producto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cantidad";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(278, 289);
            this.txtCantidad.MaxLength = 3;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.ReadOnly = true;
            this.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCantidad.Size = new System.Drawing.Size(91, 20);
            this.txtCantidad.TabIndex = 3;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Valor";
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(278, 237);
            this.txtValor.Name = "txtValor";
            this.txtValor.ReadOnly = true;
            this.txtValor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtValor.Size = new System.Drawing.Size(91, 20);
            this.txtValor.TabIndex = 2;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Enabled = false;
            this.btnAgregar.Location = new System.Drawing.Point(219, 315);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(150, 31);
            this.btnAgregar.TabIndex = 4;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(375, 208);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(225, 23);
            this.btnConsultar.TabIndex = 6;
            this.btnConsultar.Text = "Consultar producto";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // txtUnidades
            // 
            this.txtUnidades.Location = new System.Drawing.Point(278, 263);
            this.txtUnidades.Name = "txtUnidades";
            this.txtUnidades.ReadOnly = true;
            this.txtUnidades.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtUnidades.Size = new System.Drawing.Size(91, 20);
            this.txtUnidades.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Unidades disponibles";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(655, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Sub total";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(680, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "IVA";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(662, 266);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "TOTAL";
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.Location = new System.Drawing.Point(710, 211);
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.ReadOnly = true;
            this.txtSubTotal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSubTotal.Size = new System.Drawing.Size(160, 20);
            this.txtSubTotal.TabIndex = 12;
            this.txtSubTotal.TextChanged += new System.EventHandler(this.txtSubTotal_TextChanged);
            // 
            // txtIVA
            // 
            this.txtIVA.Location = new System.Drawing.Point(710, 236);
            this.txtIVA.Name = "txtIVA";
            this.txtIVA.ReadOnly = true;
            this.txtIVA.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtIVA.Size = new System.Drawing.Size(160, 20);
            this.txtIVA.TabIndex = 13;
            this.txtIVA.TextChanged += new System.EventHandler(this.txtIVA_TextChanged);
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(710, 263);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotal.Size = new System.Drawing.Size(160, 20);
            this.txtTotal.TabIndex = 14;
            // 
            // btnLimpiarTabla
            // 
            this.btnLimpiarTabla.Location = new System.Drawing.Point(778, 32);
            this.btnLimpiarTabla.Name = "btnLimpiarTabla";
            this.btnLimpiarTabla.Size = new System.Drawing.Size(92, 62);
            this.btnLimpiarTabla.TabIndex = 15;
            this.btnLimpiarTabla.Text = "Remover todo";
            this.btnLimpiarTabla.UseVisualStyleBackColor = true;
            this.btnLimpiarTabla.Click += new System.EventHandler(this.btnLimpiarTabla_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.Location = new System.Drawing.Point(658, 32);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(114, 62);
            this.btnRemover.TabIndex = 16;
            this.btnRemover.Text = "Remover artículo";
            this.btnRemover.UseVisualStyleBackColor = true;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // cbClientes
            // 
            this.cbClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClientes.FormattingEnabled = true;
            this.cbClientes.Location = new System.Drawing.Point(426, 263);
            this.cbClientes.Name = "cbClientes";
            this.cbClientes.Size = new System.Drawing.Size(174, 21);
            this.cbClientes.TabIndex = 17;
            this.cbClientes.Tag = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(423, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Cliente";
            // 
            // btnAgregarCliente
            // 
            this.btnAgregarCliente.Location = new System.Drawing.Point(426, 290);
            this.btnAgregarCliente.Name = "btnAgregarCliente";
            this.btnAgregarCliente.Size = new System.Drawing.Size(174, 38);
            this.btnAgregarCliente.TabIndex = 19;
            this.btnAgregarCliente.Text = "Si no existe cliente, entonces agréguelo aquí";
            this.btnAgregarCliente.UseVisualStyleBackColor = true;
            this.btnAgregarCliente.Visible = false;
            // 
            // btnNuevaTransaccion
            // 
            this.btnNuevaTransaccion.Location = new System.Drawing.Point(710, 342);
            this.btnNuevaTransaccion.Name = "btnNuevaTransaccion";
            this.btnNuevaTransaccion.Size = new System.Drawing.Size(160, 49);
            this.btnNuevaTransaccion.TabIndex = 20;
            this.btnNuevaTransaccion.Text = "REGISTRAR TRANSACCIÓN";
            this.btnNuevaTransaccion.UseVisualStyleBackColor = true;
            this.btnNuevaTransaccion.Click += new System.EventHandler(this.btnNuevaTransaccion_Click);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(59, 263);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(35, 13);
            this.lblFecha.TabIndex = 21;
            this.lblFecha.Text = "label9";
            this.lblFecha.Visible = false;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Location = new System.Drawing.Point(59, 292);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(35, 13);
            this.lblHora.TabIndex = 22;
            this.lblHora.Text = "label9";
            this.lblHora.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Location = new System.Drawing.Point(440, 347);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(160, 44);
            this.btnImprimir.TabIndex = 23;
            this.btnImprimir.Text = "Imprimir vista previa";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // pd
            // 
            this.pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage);
            // 
            // btnPruebaCertX509
            // 
            this.btnPruebaCertX509.Location = new System.Drawing.Point(39, 316);
            this.btnPruebaCertX509.Name = "btnPruebaCertX509";
            this.btnPruebaCertX509.Size = new System.Drawing.Size(150, 29);
            this.btnPruebaCertX509.TabIndex = 24;
            this.btnPruebaCertX509.Text = "Prueba cetificacion x509";
            this.btnPruebaCertX509.UseVisualStyleBackColor = true;
            this.btnPruebaCertX509.Visible = false;
            this.btnPruebaCertX509.Click += new System.EventHandler(this.btnPruebaCertX509_Click);
            // 
            // cbMediosPago
            // 
            this.cbMediosPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMediosPago.FormattingEnabled = true;
            this.cbMediosPago.Location = new System.Drawing.Point(710, 292);
            this.cbMediosPago.Name = "cbMediosPago";
            this.cbMediosPago.Size = new System.Drawing.Size(160, 21);
            this.cbMediosPago.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(626, 295);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Medio de pago";
            // 
            // txtNumeroPrueba
            // 
            this.txtNumeroPrueba.Location = new System.Drawing.Point(48, 371);
            this.txtNumeroPrueba.MaxLength = 9;
            this.txtNumeroPrueba.Name = "txtNumeroPrueba";
            this.txtNumeroPrueba.Size = new System.Drawing.Size(141, 20);
            this.txtNumeroPrueba.TabIndex = 27;
            this.txtNumeroPrueba.Visible = false;
            this.txtNumeroPrueba.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroPrueba_KeyPress);
            // 
            // btnNumeroNombrePrueba
            // 
            this.btnNumeroNombrePrueba.Location = new System.Drawing.Point(219, 369);
            this.btnNumeroNombrePrueba.Name = "btnNumeroNombrePrueba";
            this.btnNumeroNombrePrueba.Size = new System.Drawing.Size(141, 23);
            this.btnNumeroNombrePrueba.TabIndex = 28;
            this.btnNumeroNombrePrueba.Text = "Numero a Nombre";
            this.btnNumeroNombrePrueba.UseVisualStyleBackColor = true;
            this.btnNumeroNombrePrueba.Visible = false;
            this.btnNumeroNombrePrueba.Click += new System.EventHandler(this.btnNumeroNombrePrueba_Click);
            // 
            // formNuevaFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 423);
            this.Controls.Add(this.btnNumeroNombrePrueba);
            this.Controls.Add(this.txtNumeroPrueba);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbMediosPago);
            this.Controls.Add(this.btnPruebaCertX509);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.lblHora);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.btnNuevaTransaccion);
            this.Controls.Add(this.btnAgregarCliente);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbClientes);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.btnLimpiarTabla);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtIVA);
            this.Controls.Add(this.txtSubTotal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUnidades);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbProducto);
            this.Controls.Add(this.dgvDetalleFactura);
            this.Name = "formNuevaFactura";
            this.Text = "Nueva factura";
            this.Load += new System.EventHandler(this.formNuevaFactura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleFactura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDetalleFactura;
        private System.Windows.Forms.ComboBox cbProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.TextBox txtUnidades;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.TextBox txtIVA;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnLimpiarTabla;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.ComboBox cbClientes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAgregarCliente;
        private System.Windows.Forms.Button btnNuevaTransaccion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Button btnImprimir;
        private System.Drawing.Printing.PrintDocument pd;
        private System.Windows.Forms.Button btnPruebaCertX509;
        private System.Windows.Forms.ComboBox cbMediosPago;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNumeroPrueba;
        private System.Windows.Forms.Button btnNumeroNombrePrueba;
    }
}