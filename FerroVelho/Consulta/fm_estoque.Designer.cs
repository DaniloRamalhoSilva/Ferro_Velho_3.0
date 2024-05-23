namespace FerroVelho
{
    partial class fm_estoque
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_estoque));
            this.label3 = new System.Windows.Forms.Label();
            this.txt_codProd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dg_produtos = new System.Windows.Forms.DataGridView();
            this.idprodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descprodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valprodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_produtosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txt_desc = new System.Windows.Forms.TextBox();
            this.bt_corrigir = new System.Windows.Forms.Button();
            this.lb_texto = new System.Windows.Forms.Label();
            this.txt_novoSaldo = new System.Windows.Forms.TextBox();
            this.bt_confirmar = new System.Windows.Forms.Button();
            this.bt_cancelar = new System.Windows.Forms.Button();
            this.bt_imprimir = new System.Windows.Forms.Button();
            this.bt_visuImpre = new System.Windows.Forms.Button();
            this.tb_impressoraBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dg_produtos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_produtosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_impressoraBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(100, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Descrição Produto:";
            // 
            // txt_codProd
            // 
            this.txt_codProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_codProd.Location = new System.Drawing.Point(15, 24);
            this.txt_codProd.Name = "txt_codProd";
            this.txt_codProd.Size = new System.Drawing.Size(80, 44);
            this.txt_codProd.TabIndex = 30;
            this.txt_codProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_codProd_KeyPress);
            this.txt_codProd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_codProd_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Codigo Produto:";
            // 
            // dg_produtos
            // 
            this.dg_produtos.AllowUserToAddRows = false;
            this.dg_produtos.AllowUserToDeleteRows = false;
            this.dg_produtos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg_produtos.AutoGenerateColumns = false;
            this.dg_produtos.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_produtos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_produtos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_produtos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idprodDataGridViewTextBoxColumn,
            this.descprodDataGridViewTextBoxColumn,
            this.Column1,
            this.valprodDataGridViewTextBoxColumn});
            this.dg_produtos.DataSource = this.tb_produtosBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_produtos.DefaultCellStyle = dataGridViewCellStyle3;
            this.dg_produtos.EnableHeadersVisualStyles = false;
            this.dg_produtos.Location = new System.Drawing.Point(15, 74);
            this.dg_produtos.Name = "dg_produtos";
            this.dg_produtos.ReadOnly = true;
            this.dg_produtos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_produtos.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dg_produtos.RowHeadersWidth = 20;
            this.dg_produtos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_produtos.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dg_produtos.RowTemplate.Height = 35;
            this.dg_produtos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_produtos.Size = new System.Drawing.Size(594, 429);
            this.dg_produtos.TabIndex = 44;
            // 
            // idprodDataGridViewTextBoxColumn
            // 
            this.idprodDataGridViewTextBoxColumn.DataPropertyName = "id_prod";
            this.idprodDataGridViewTextBoxColumn.HeaderText = "Codigo";
            this.idprodDataGridViewTextBoxColumn.Name = "idprodDataGridViewTextBoxColumn";
            this.idprodDataGridViewTextBoxColumn.ReadOnly = true;
            this.idprodDataGridViewTextBoxColumn.Width = 70;
            // 
            // descprodDataGridViewTextBoxColumn
            // 
            this.descprodDataGridViewTextBoxColumn.DataPropertyName = "desc_prod";
            this.descprodDataGridViewTextBoxColumn.HeaderText = "Descrição";
            this.descprodDataGridViewTextBoxColumn.Name = "descprodDataGridViewTextBoxColumn";
            this.descprodDataGridViewTextBoxColumn.ReadOnly = true;
            this.descprodDataGridViewTextBoxColumn.Width = 350;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Format = "N3";
            dataGridViewCellStyle2.NullValue = null;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Saldo";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // valprodDataGridViewTextBoxColumn
            // 
            this.valprodDataGridViewTextBoxColumn.DataPropertyName = "val_prod";
            this.valprodDataGridViewTextBoxColumn.HeaderText = "val_prod";
            this.valprodDataGridViewTextBoxColumn.Name = "valprodDataGridViewTextBoxColumn";
            this.valprodDataGridViewTextBoxColumn.ReadOnly = true;
            this.valprodDataGridViewTextBoxColumn.Visible = false;
            // 
            // tb_produtosBindingSource
            // 
            this.tb_produtosBindingSource.DataSource = typeof(FerroVelhoDAO.tb_produtos);
            // 
            // txt_desc
            // 
            this.txt_desc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_desc.Location = new System.Drawing.Point(103, 24);
            this.txt_desc.Name = "txt_desc";
            this.txt_desc.Size = new System.Drawing.Size(506, 44);
            this.txt_desc.TabIndex = 45;
            this.txt_desc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_desc_KeyUp);
            // 
            // bt_corrigir
            // 
            this.bt_corrigir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_corrigir.Location = new System.Drawing.Point(15, 517);
            this.bt_corrigir.Name = "bt_corrigir";
            this.bt_corrigir.Size = new System.Drawing.Size(83, 42);
            this.bt_corrigir.TabIndex = 46;
            this.bt_corrigir.Text = "Corrigir Saldo";
            this.bt_corrigir.UseVisualStyleBackColor = true;
            this.bt_corrigir.Click += new System.EventHandler(this.bt_corrigir_Click);
            // 
            // lb_texto
            // 
            this.lb_texto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_texto.AutoSize = true;
            this.lb_texto.Location = new System.Drawing.Point(104, 517);
            this.lb_texto.Name = "lb_texto";
            this.lb_texto.Size = new System.Drawing.Size(94, 13);
            this.lb_texto.TabIndex = 47;
            this.lb_texto.Text = "Corrigir saldo para:";
            this.lb_texto.Visible = false;
            // 
            // txt_novoSaldo
            // 
            this.txt_novoSaldo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_novoSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_novoSaldo.Location = new System.Drawing.Point(107, 533);
            this.txt_novoSaldo.Name = "txt_novoSaldo";
            this.txt_novoSaldo.Size = new System.Drawing.Size(100, 26);
            this.txt_novoSaldo.TabIndex = 48;
            this.txt_novoSaldo.Visible = false;
            this.txt_novoSaldo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_novoSaldo_KeyPress);
            this.txt_novoSaldo.Leave += new System.EventHandler(this.txt_novoSaldo_Leave);
            // 
            // bt_confirmar
            // 
            this.bt_confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_confirmar.Location = new System.Drawing.Point(213, 533);
            this.bt_confirmar.Name = "bt_confirmar";
            this.bt_confirmar.Size = new System.Drawing.Size(83, 26);
            this.bt_confirmar.TabIndex = 49;
            this.bt_confirmar.Text = "Confirmar";
            this.bt_confirmar.UseVisualStyleBackColor = true;
            this.bt_confirmar.Visible = false;
            this.bt_confirmar.Click += new System.EventHandler(this.bt_confirmar_Click);
            // 
            // bt_cancelar
            // 
            this.bt_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_cancelar.Location = new System.Drawing.Point(302, 533);
            this.bt_cancelar.Name = "bt_cancelar";
            this.bt_cancelar.Size = new System.Drawing.Size(83, 26);
            this.bt_cancelar.TabIndex = 50;
            this.bt_cancelar.Text = "Cancelar";
            this.bt_cancelar.UseVisualStyleBackColor = true;
            this.bt_cancelar.Visible = false;
            this.bt_cancelar.Click += new System.EventHandler(this.bt_cancelar_Click);
            // 
            // bt_imprimir
            // 
            this.bt_imprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_imprimir.Location = new System.Drawing.Point(502, 517);
            this.bt_imprimir.Name = "bt_imprimir";
            this.bt_imprimir.Size = new System.Drawing.Size(108, 42);
            this.bt_imprimir.TabIndex = 51;
            this.bt_imprimir.Text = "Imprimir";
            this.bt_imprimir.UseVisualStyleBackColor = true;
            this.bt_imprimir.Click += new System.EventHandler(this.bt_imprimir_Click);
            // 
            // bt_visuImpre
            // 
            this.bt_visuImpre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_visuImpre.Location = new System.Drawing.Point(391, 517);
            this.bt_visuImpre.Name = "bt_visuImpre";
            this.bt_visuImpre.Size = new System.Drawing.Size(108, 42);
            this.bt_visuImpre.TabIndex = 52;
            this.bt_visuImpre.Text = "Pag. Impressão";
            this.bt_visuImpre.UseVisualStyleBackColor = true;
            this.bt_visuImpre.Click += new System.EventHandler(this.bt_visuImpre_Click);
            // 
            // tb_impressoraBindingSource
            // 
            this.tb_impressoraBindingSource.DataSource = typeof(FerroVelhoDAO.tb_impressora);
            // 
            // fm_estoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 571);
            this.Controls.Add(this.bt_visuImpre);
            this.Controls.Add(this.bt_imprimir);
            this.Controls.Add(this.bt_cancelar);
            this.Controls.Add(this.bt_confirmar);
            this.Controls.Add(this.txt_novoSaldo);
            this.Controls.Add(this.lb_texto);
            this.Controls.Add(this.bt_corrigir);
            this.Controls.Add(this.txt_desc);
            this.Controls.Add(this.dg_produtos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_codProd);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_estoque";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Saldo Estoque";
            this.Load += new System.EventHandler(this.fm_estoque_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_produtos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_produtosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_impressoraBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_codProd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dg_produtos;
        private System.Windows.Forms.BindingSource tb_produtosBindingSource;
        private System.Windows.Forms.TextBox txt_desc;
        private System.Windows.Forms.Button bt_corrigir;
        private System.Windows.Forms.Label lb_texto;
        private System.Windows.Forms.TextBox txt_novoSaldo;
        private System.Windows.Forms.Button bt_confirmar;
        private System.Windows.Forms.DataGridViewTextBoxColumn idprodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descprodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn valprodDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button bt_cancelar;
        private System.Windows.Forms.Button bt_imprimir;
        private System.Windows.Forms.Button bt_visuImpre;
        private System.Windows.Forms.BindingSource tb_impressoraBindingSource;
    }
}