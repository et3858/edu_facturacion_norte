namespace facturacion_norte
{
    partial class formMantenedorProductos
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
            this.dgvListaProductos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCodProd = new System.Windows.Forms.TextBox();
            this.txtDescrpcionProd = new System.Windows.Forms.TextBox();
            this.txtUnitsProd = new System.Windows.Forms.TextBox();
            this.txtValorProd = new System.Windows.Forms.TextBox();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvListaProductos
            // 
            this.dgvListaProductos.AllowUserToAddRows = false;
            this.dgvListaProductos.AllowUserToDeleteRows = false;
            this.dgvListaProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaProductos.Location = new System.Drawing.Point(42, 45);
            this.dgvListaProductos.Name = "dgvListaProductos";
            this.dgvListaProductos.ReadOnly = true;
            this.dgvListaProductos.Size = new System.Drawing.Size(583, 273);
            this.dgvListaProductos.StandardTab = true;
            this.dgvListaProductos.TabIndex = 1;
            this.dgvListaProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListaProductos_CellClick);
            this.dgvListaProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvListaProductos_KeyDown);
            this.dgvListaProductos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvListaProductos_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(660, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID Producto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(661, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Descripción";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(661, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Unidades disponibles";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(661, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Valor unidad";
            // 
            // txtCodProd
            // 
            this.txtCodProd.Location = new System.Drawing.Point(774, 80);
            this.txtCodProd.Name = "txtCodProd";
            this.txtCodProd.ReadOnly = true;
            this.txtCodProd.Size = new System.Drawing.Size(206, 20);
            this.txtCodProd.TabIndex = 23;
            this.txtCodProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDescrpcionProd
            // 
            this.txtDescrpcionProd.Location = new System.Drawing.Point(774, 106);
            this.txtDescrpcionProd.MaxLength = 30;
            this.txtDescrpcionProd.Name = "txtDescrpcionProd";
            this.txtDescrpcionProd.ReadOnly = true;
            this.txtDescrpcionProd.Size = new System.Drawing.Size(206, 20);
            this.txtDescrpcionProd.TabIndex = 24;
            this.txtDescrpcionProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUnitsProd
            // 
            this.txtUnitsProd.Location = new System.Drawing.Point(774, 132);
            this.txtUnitsProd.MaxLength = 3;
            this.txtUnitsProd.Name = "txtUnitsProd";
            this.txtUnitsProd.ReadOnly = true;
            this.txtUnitsProd.Size = new System.Drawing.Size(206, 20);
            this.txtUnitsProd.TabIndex = 25;
            this.txtUnitsProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtValorProd
            // 
            this.txtValorProd.Location = new System.Drawing.Point(774, 158);
            this.txtValorProd.MaxLength = 8;
            this.txtValorProd.Name = "txtValorProd";
            this.txtValorProd.ReadOnly = true;
            this.txtValorProd.Size = new System.Drawing.Size(206, 20);
            this.txtValorProd.TabIndex = 26;
            this.txtValorProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(664, 263);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(104, 55);
            this.btnNuevo.TabIndex = 27;
            this.btnNuevo.Text = "Nuevo producto";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(774, 263);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(125, 55);
            this.btnModificar.TabIndex = 28;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(905, 263);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 55);
            this.btnEliminar.TabIndex = 29;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(896, 354);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(84, 34);
            this.btnSalir.TabIndex = 30;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // formMantenedorProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 400);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.txtValorProd);
            this.Controls.Add(this.txtUnitsProd);
            this.Controls.Add(this.txtDescrpcionProd);
            this.Controls.Add(this.txtCodProd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvListaProductos);
            this.Name = "formMantenedorProductos";
            this.Text = "Mantenedor de productos";
            this.Load += new System.EventHandler(this.formMantenedorProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListaProductos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCodProd;
        private System.Windows.Forms.TextBox txtDescrpcionProd;
        private System.Windows.Forms.TextBox txtUnitsProd;
        private System.Windows.Forms.TextBox txtValorProd;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnSalir;
    }
}