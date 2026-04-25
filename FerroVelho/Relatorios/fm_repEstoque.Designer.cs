namespace FerroVelho.Relatorios
{
    partial class fm_repEstoque
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_repEstoque));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id_prod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc_prod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Peso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt_imprimir = new System.Windows.Forms.Button();
            this.tb_impressoraBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bt_relImp = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dt_inicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dt_fim = new System.Windows.Forms.DateTimePicker();
            this.bt_pesquisa = new System.Windows.Forms.Button();
            this.txt_codProd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_impressoraBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_prod,
            this.desc_prod,
            this.Inicio,
            this.Peso,
            this.Total,
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(15, 77);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(757, 469);
            this.dataGridView1.TabIndex = 42;
            // 
            // id_prod
            // 
            this.id_prod.DataPropertyName = "Codigo";
            this.id_prod.HeaderText = "Codigo";
            this.id_prod.Name = "id_prod";
            this.id_prod.ReadOnly = true;
            this.id_prod.Width = 60;
            // 
            // desc_prod
            // 
            this.desc_prod.DataPropertyName = "Descrição";
            this.desc_prod.HeaderText = "Descrição";
            this.desc_prod.Name = "desc_prod";
            this.desc_prod.ReadOnly = true;
            this.desc_prod.Width = 250;
            // 
            // Inicio
            // 
            this.Inicio.DataPropertyName = "Inicio";
            this.Inicio.HeaderText = "Inicio";
            this.Inicio.Name = "Inicio";
            this.Inicio.ReadOnly = true;
            // 
            // Peso
            // 
            this.Peso.DataPropertyName = "Entrada";
            dataGridViewCellStyle5.Format = "N3";
            dataGridViewCellStyle5.NullValue = null;
            this.Peso.DefaultCellStyle = dataGridViewCellStyle5;
            this.Peso.HeaderText = "Entrada";
            this.Peso.Name = "Peso";
            this.Peso.ReadOnly = true;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Saida";
            dataGridViewCellStyle6.Format = "N3";
            dataGridViewCellStyle6.NullValue = null;
            this.Total.DefaultCellStyle = dataGridViewCellStyle6;
            this.Total.HeaderText = "Saida";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Saldo";
            this.Column1.HeaderText = "Saldo";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // bt_imprimir
            // 
            this.bt_imprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_imprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_imprimir.Location = new System.Drawing.Point(391, 27);
            this.bt_imprimir.Name = "bt_imprimir";
            this.bt_imprimir.Size = new System.Drawing.Size(166, 45);
            this.bt_imprimir.TabIndex = 41;
            this.bt_imprimir.Text = "Imprimir";
            this.bt_imprimir.UseVisualStyleBackColor = true;
            this.bt_imprimir.Click += new System.EventHandler(this.bt_imprimir_Click);
            // 
            // tb_impressoraBindingSource
            // 
            this.tb_impressoraBindingSource.DataSource = typeof(FerroVelhoDAO.tb_impressora);
            // 
            // bt_relImp
            // 
            this.bt_relImp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_relImp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_relImp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_relImp.Location = new System.Drawing.Point(563, 27);
            this.bt_relImp.Name = "bt_relImp";
            this.bt_relImp.Size = new System.Drawing.Size(209, 45);
            this.bt_relImp.TabIndex = 43;
            this.bt_relImp.Text = "Pagina de impressão";
            this.bt_relImp.UseVisualStyleBackColor = true;
            this.bt_relImp.Click += new System.EventHandler(this.bt_relImp_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dt_inicio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dt_fim);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 63);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Periodo";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Data Inicio:";
            // 
            // dt_inicio
            // 
            this.dt_inicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dt_inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_inicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_inicio.Location = new System.Drawing.Point(6, 33);
            this.dt_inicio.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dt_inicio.Name = "dt_inicio";
            this.dt_inicio.Size = new System.Drawing.Size(109, 26);
            this.dt_inicio.TabIndex = 51;
            this.dt_inicio.Value = new System.DateTime(2021, 6, 15, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "Data Fim:";
            // 
            // dt_fim
            // 
            this.dt_fim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dt_fim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_fim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_fim.Location = new System.Drawing.Point(123, 33);
            this.dt_fim.Name = "dt_fim";
            this.dt_fim.Size = new System.Drawing.Size(105, 26);
            this.dt_fim.TabIndex = 53;
            this.dt_fim.Value = new System.DateTime(2021, 6, 15, 0, 0, 0, 0);
            // 
            // bt_pesquisa
            // 
            this.bt_pesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_pesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_pesquisa.Image = ((System.Drawing.Image)(resources.GetObject("bt_pesquisa.Image")));
            this.bt_pesquisa.Location = new System.Drawing.Point(336, 27);
            this.bt_pesquisa.Margin = new System.Windows.Forms.Padding(0);
            this.bt_pesquisa.Name = "bt_pesquisa";
            this.bt_pesquisa.Size = new System.Drawing.Size(43, 44);
            this.bt_pesquisa.TabIndex = 52;
            this.bt_pesquisa.UseVisualStyleBackColor = true;
            this.bt_pesquisa.Click += new System.EventHandler(this.bt_pesquisa_Click);
            // 
            // txt_codProd
            // 
            this.txt_codProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_codProd.Location = new System.Drawing.Point(253, 27);
            this.txt_codProd.Name = "txt_codProd";
            this.txt_codProd.Size = new System.Drawing.Size(80, 44);
            this.txt_codProd.TabIndex = 1;
            this.txt_codProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_codProd_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "Codigo Produto:";
            // 
            // fm_repEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 558);
            this.Controls.Add(this.txt_codProd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_relImp);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.bt_pesquisa);
            this.Controls.Add(this.bt_imprimir);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_repEstoque";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatorio de estoque";
            this.Load += new System.EventHandler(this.fm_repEstoque_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_impressoraBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button bt_imprimir;
        private System.Windows.Forms.BindingSource tb_impressoraBindingSource;
        private System.Windows.Forms.Button bt_relImp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dt_inicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_pesquisa;
        private System.Windows.Forms.DateTimePicker dt_fim;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_prod;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc_prod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Inicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Peso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.TextBox txt_codProd;
        private System.Windows.Forms.Label label3;
    }
}