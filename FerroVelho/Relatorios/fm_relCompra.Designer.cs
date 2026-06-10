namespace FerroVelho.Relatorios
{
    partial class fm_relCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_relCompra));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dt_fim = new System.Windows.Forms.DateTimePicker();
            this.bt_pesquisa = new System.Windows.Forms.Button();
            this.dt_inicio = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_imprimir = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id_prod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc_prod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Peso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_inicial = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_entrada = new System.Windows.Forms.Label();
            this.lb_saldo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lb_compras = new System.Windows.Forms.Label();
            this.tb_impressoraBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lb_total = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_impressoraBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "De:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Até:";
            // 
            // dt_fim
            // 
            this.dt_fim.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dt_fim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_fim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_fim.Location = new System.Drawing.Point(129, 32);
            this.dt_fim.Name = "dt_fim";
            this.dt_fim.Size = new System.Drawing.Size(110, 26);
            this.dt_fim.TabIndex = 34;
            this.dt_fim.Value = new System.DateTime(2021, 6, 15, 0, 0, 0, 0);
            // 
            // bt_pesquisa
            // 
            this.bt_pesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_pesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_pesquisa.Image = ((System.Drawing.Image)(resources.GetObject("bt_pesquisa.Image")));
            this.bt_pesquisa.Location = new System.Drawing.Point(244, 16);
            this.bt_pesquisa.Margin = new System.Windows.Forms.Padding(0);
            this.bt_pesquisa.Name = "bt_pesquisa";
            this.bt_pesquisa.Size = new System.Drawing.Size(44, 42);
            this.bt_pesquisa.TabIndex = 33;
            this.bt_pesquisa.UseVisualStyleBackColor = true;
            this.bt_pesquisa.Click += new System.EventHandler(this.bt_pesquisa_Click);
            // 
            // dt_inicio
            // 
            this.dt_inicio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dt_inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_inicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_inicio.Location = new System.Drawing.Point(13, 32);
            this.dt_inicio.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dt_inicio.Name = "dt_inicio";
            this.dt_inicio.Size = new System.Drawing.Size(110, 26);
            this.dt_inicio.TabIndex = 32;
            this.dt_inicio.Value = new System.DateTime(2021, 6, 15, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.bt_pesquisa);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dt_fim);
            this.groupBox1.Controls.Add(this.dt_inicio);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 66);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Periodo";
            // 
            // bt_imprimir
            // 
            this.bt_imprimir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_imprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_imprimir.Location = new System.Drawing.Point(314, 12);
            this.bt_imprimir.Name = "bt_imprimir";
            this.bt_imprimir.Size = new System.Drawing.Size(204, 66);
            this.bt_imprimir.TabIndex = 38;
            this.bt_imprimir.Text = "Imprimir";
            this.bt_imprimir.UseVisualStyleBackColor = true;
            this.bt_imprimir.Click += new System.EventHandler(this.bt_imprimir_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_prod,
            this.desc_prod,
            this.Peso,
            this.Total});
            this.dataGridView1.Location = new System.Drawing.Point(15, 94);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(503, 245);
            this.dataGridView1.TabIndex = 39;
            // 
            // id_prod
            // 
            this.id_prod.DataPropertyName = "id_prod";
            this.id_prod.HeaderText = "Codigo";
            this.id_prod.Name = "id_prod";
            this.id_prod.ReadOnly = true;
            this.id_prod.Width = 60;
            // 
            // desc_prod
            // 
            this.desc_prod.DataPropertyName = "desc_prod";
            this.desc_prod.HeaderText = "Descrição";
            this.desc_prod.Name = "desc_prod";
            this.desc_prod.ReadOnly = true;
            this.desc_prod.Width = 200;
            // 
            // Peso
            // 
            this.Peso.DataPropertyName = "Peso";
            dataGridViewCellStyle2.Format = "N3";
            dataGridViewCellStyle2.NullValue = null;
            this.Peso.DefaultCellStyle = dataGridViewCellStyle2;
            this.Peso.HeaderText = "Peso";
            this.Peso.Name = "Peso";
            this.Peso.ReadOnly = true;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.Total.DefaultCellStyle = dataGridViewCellStyle3;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Entrada em caixa no periodo:";
            // 
            // lb_inicial
            // 
            this.lb_inicial.AutoSize = true;
            this.lb_inicial.Location = new System.Drawing.Point(159, 25);
            this.lb_inicial.Name = "lb_inicial";
            this.lb_inicial.Size = new System.Drawing.Size(35, 13);
            this.lb_inicial.TabIndex = 41;
            this.lb_inicial.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Saldo em caixa no periodo:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Saldo inicial no periodo:";
            // 
            // lb_entrada
            // 
            this.lb_entrada.AutoSize = true;
            this.lb_entrada.Location = new System.Drawing.Point(159, 49);
            this.lb_entrada.Name = "lb_entrada";
            this.lb_entrada.Size = new System.Drawing.Size(35, 13);
            this.lb_entrada.TabIndex = 44;
            this.lb_entrada.Text = "label4";
            // 
            // lb_saldo
            // 
            this.lb_saldo.AutoSize = true;
            this.lb_saldo.Location = new System.Drawing.Point(159, 99);
            this.lb_saldo.Name = "lb_saldo";
            this.lb_saldo.Size = new System.Drawing.Size(35, 13);
            this.lb_saldo.TabIndex = 45;
            this.lb_saldo.Text = "label4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "Compras no periodo:";
            // 
            // lb_compras
            // 
            this.lb_compras.AutoSize = true;
            this.lb_compras.Location = new System.Drawing.Point(159, 75);
            this.lb_compras.Name = "lb_compras";
            this.lb_compras.Size = new System.Drawing.Size(35, 13);
            this.lb_compras.TabIndex = 47;
            this.lb_compras.Text = "label4";
            // 
            // tb_impressoraBindingSource
            // 
            this.tb_impressoraBindingSource.DataSource = typeof(FerroVelhoDAO.tb_impressora);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lb_compras);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lb_inicial);
            this.groupBox2.Controls.Add(this.lb_saldo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lb_entrada);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(15, 345);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 124);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controle do caixa:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.lb_total);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(280, 345);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(238, 69);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Total de compra:";
            // 
            // lb_total
            // 
            this.lb_total.AutoSize = true;
            this.lb_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_total.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lb_total.Location = new System.Drawing.Point(6, 21);
            this.lb_total.Name = "lb_total";
            this.lb_total.Size = new System.Drawing.Size(87, 31);
            this.lb_total.TabIndex = 50;
            this.lb_total.Text = "00,00";
            this.lb_total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fm_relCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 472);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.bt_imprimir);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_relCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatorio de Compra";
            this.Load += new System.EventHandler(this.fm_relCompra_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_impressoraBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dt_fim;
        private System.Windows.Forms.Button bt_pesquisa;
        private System.Windows.Forms.DateTimePicker dt_inicio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_imprimir;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource tb_impressoraBindingSource;
        private System.Windows.Forms.Label lb_inicial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_entrada;
        private System.Windows.Forms.Label lb_saldo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lb_compras;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lb_total;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_prod;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc_prod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Peso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
    }
}