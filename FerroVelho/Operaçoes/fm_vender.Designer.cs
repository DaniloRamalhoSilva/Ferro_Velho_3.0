namespace FerroVelho
{
    partial class fm_vender
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
            System.Windows.Forms.Label id_vendaLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_vender));
            this.txt_codProd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_subTot = new System.Windows.Forms.TextBox();
            this.txt_valProd = new System.Windows.Forms.TextBox();
            this.cb_desProd = new System.Windows.Forms.ComboBox();
            this.bt_excluir = new System.Windows.Forms.Button();
            this.bt_novoItem = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_quant = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_nNota = new System.Windows.Forms.TextBox();
            this.tb_vendaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTotal = new System.Windows.Forms.Label();
            this.bt_finalCompra = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dg_venda = new System.Windows.Forms.DataGridView();
            this.tb_itemvBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lb_saldo = new System.Windows.Forms.Label();
            this.tb_produtosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tb_impressoraBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bd_ferroVelhoDataSet = new FerroVelho.bd_ferroVelhoDataSet();
            this.vendaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vendaTableAdapter = new FerroVelho.bd_ferroVelhoDataSetTableAdapters.VendaTableAdapter();
            this.tableAdapterManager = new FerroVelho.bd_ferroVelhoDataSetTableAdapters.TableAdapterManager();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tbprodutosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valritemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantitemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subTotitemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iditemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idprodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idvendaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbvendaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            id_vendaLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tb_vendaBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_venda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_itemvBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_produtosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_impressoraBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bd_ferroVelhoDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vendaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // id_vendaLabel
            // 
            id_vendaLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            id_vendaLabel.AutoSize = true;
            id_vendaLabel.Location = new System.Drawing.Point(603, 324);
            id_vendaLabel.Name = "id_vendaLabel";
            id_vendaLabel.Size = new System.Drawing.Size(63, 13);
            id_vendaLabel.TabIndex = 40;
            id_vendaLabel.Text = "Nº da Nota:";
            // 
            // txt_codProd
            // 
            this.txt_codProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_codProd.Location = new System.Drawing.Point(15, 24);
            this.txt_codProd.Name = "txt_codProd";
            this.txt_codProd.Size = new System.Drawing.Size(80, 44);
            this.txt_codProd.TabIndex = 1;
            this.txt_codProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_codProd_KeyPress);
            this.txt_codProd.Leave += new System.EventHandler(this.txt_codProd_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Codigo Produto:";
            // 
            // txt_subTot
            // 
            this.txt_subTot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_subTot.Enabled = false;
            this.txt_subTot.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_subTot.Location = new System.Drawing.Point(754, 26);
            this.txt_subTot.Name = "txt_subTot";
            this.txt_subTot.Size = new System.Drawing.Size(136, 44);
            this.txt_subTot.TabIndex = 5;
            // 
            // txt_valProd
            // 
            this.txt_valProd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_valProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_valProd.Location = new System.Drawing.Point(475, 25);
            this.txt_valProd.Name = "txt_valProd";
            this.txt_valProd.Size = new System.Drawing.Size(119, 44);
            this.txt_valProd.TabIndex = 3;
            this.txt_valProd.Text = "0";
            this.txt_valProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_valProd_KeyPress);
            this.txt_valProd.Leave += new System.EventHandler(this.txt_valProd_Leave);
            // 
            // cb_desProd
            // 
            this.cb_desProd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_desProd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_desProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_desProd.FormattingEnabled = true;
            this.cb_desProd.Location = new System.Drawing.Point(101, 24);
            this.cb_desProd.Name = "cb_desProd";
            this.cb_desProd.Size = new System.Drawing.Size(368, 45);
            this.cb_desProd.TabIndex = 2;
            this.cb_desProd.Leave += new System.EventHandler(this.cb_desProd_Leave);
            // 
            // bt_excluir
            // 
            this.bt_excluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_excluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_excluir.Location = new System.Drawing.Point(1024, 25);
            this.bt_excluir.Name = "bt_excluir";
            this.bt_excluir.Size = new System.Drawing.Size(99, 44);
            this.bt_excluir.TabIndex = 32;
            this.bt_excluir.Text = "Excluir Item";
            this.bt_excluir.UseVisualStyleBackColor = true;
            this.bt_excluir.Click += new System.EventHandler(this.bt_excluir_Click);
            // 
            // bt_novoItem
            // 
            this.bt_novoItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_novoItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_novoItem.Location = new System.Drawing.Point(894, 25);
            this.bt_novoItem.Name = "bt_novoItem";
            this.bt_novoItem.Size = new System.Drawing.Size(122, 44);
            this.bt_novoItem.TabIndex = 6;
            this.bt_novoItem.Text = "Adicionar Item";
            this.bt_novoItem.UseVisualStyleBackColor = true;
            this.bt_novoItem.Click += new System.EventHandler(this.bt_novoItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Descrição Produto:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(751, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Sub Total:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(472, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Valor Unitario:";
            // 
            // txt_quant
            // 
            this.txt_quant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_quant.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_quant.Location = new System.Drawing.Point(600, 25);
            this.txt_quant.Name = "txt_quant";
            this.txt_quant.Size = new System.Drawing.Size(148, 44);
            this.txt_quant.TabIndex = 4;
            this.txt_quant.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_quant_KeyPress);
            this.txt_quant.Leave += new System.EventHandler(this.txt_quant_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(597, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Quantidade/Peso:";
            // 
            // txt_nNota
            // 
            this.txt_nNota.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_nNota.Enabled = false;
            this.txt_nNota.Location = new System.Drawing.Point(672, 321);
            this.txt_nNota.Name = "txt_nNota";
            this.txt_nNota.Size = new System.Drawing.Size(103, 20);
            this.txt_nNota.TabIndex = 41;
            // 
            // tb_vendaBindingSource
            // 
            this.tb_vendaBindingSource.DataSource = typeof(FerroVelhoDAO.tb_venda);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelTotal);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(896, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 74);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Total a Receber:";
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 31F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelTotal.Location = new System.Drawing.Point(6, 17);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(129, 48);
            this.labelTotal.TabIndex = 14;
            this.labelTotal.Text = "00,00";
            this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_finalCompra
            // 
            this.bt_finalCompra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_finalCompra.Enabled = false;
            this.bt_finalCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_finalCompra.Location = new System.Drawing.Point(606, 345);
            this.bt_finalCompra.Name = "bt_finalCompra";
            this.bt_finalCompra.Size = new System.Drawing.Size(284, 51);
            this.bt_finalCompra.TabIndex = 7;
            this.bt_finalCompra.Text = "Finalizar Venda";
            this.bt_finalCompra.UseVisualStyleBackColor = true;
            this.bt_finalCompra.Click += new System.EventHandler(this.bt_finalCompra_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(304, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Saldo disponivel em estoque referente ao produto selecionado:";
            // 
            // dg_venda
            // 
            this.dg_venda.AllowUserToAddRows = false;
            this.dg_venda.AllowUserToDeleteRows = false;
            this.dg_venda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg_venda.AutoGenerateColumns = false;
            this.dg_venda.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_venda.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_venda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_venda.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tbprodutosDataGridViewTextBoxColumn,
            this.valritemDataGridViewTextBoxColumn,
            this.quantitemDataGridViewTextBoxColumn,
            this.subTotitemDataGridViewTextBoxColumn,
            this.iditemDataGridViewTextBoxColumn,
            this.idprodDataGridViewTextBoxColumn,
            this.idvendaDataGridViewTextBoxColumn,
            this.tbvendaDataGridViewTextBoxColumn});
            this.dg_venda.DataSource = this.tb_itemvBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_venda.DefaultCellStyle = dataGridViewCellStyle5;
            this.dg_venda.EnableHeadersVisualStyles = false;
            this.dg_venda.Location = new System.Drawing.Point(15, 76);
            this.dg_venda.Name = "dg_venda";
            this.dg_venda.ReadOnly = true;
            this.dg_venda.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_venda.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dg_venda.RowHeadersWidth = 20;
            this.dg_venda.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dg_venda.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dg_venda.RowTemplate.Height = 35;
            this.dg_venda.Size = new System.Drawing.Size(1108, 240);
            this.dg_venda.TabIndex = 43;
            this.dg_venda.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_venda_CellFormatting);
            // 
            // tb_itemvBindingSource
            // 
            this.tb_itemvBindingSource.DataSource = typeof(FerroVelhoDAO.tb_itemv);
            // 
            // lb_saldo
            // 
            this.lb_saldo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_saldo.AutoSize = true;
            this.lb_saldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_saldo.ForeColor = System.Drawing.Color.Green;
            this.lb_saldo.Location = new System.Drawing.Point(322, 321);
            this.lb_saldo.Name = "lb_saldo";
            this.lb_saldo.Size = new System.Drawing.Size(43, 15);
            this.lb_saldo.TabIndex = 44;
            this.lb_saldo.Text = "0,000";
            // 
            // tb_produtosBindingSource
            // 
            this.tb_produtosBindingSource.DataSource = typeof(FerroVelhoDAO.tb_produtos);
            // 
            // tb_impressoraBindingSource
            // 
            this.tb_impressoraBindingSource.DataSource = typeof(FerroVelhoDAO.tb_impressora);
            // 
            // bd_ferroVelhoDataSet
            // 
            this.bd_ferroVelhoDataSet.DataSetName = "bd_ferroVelhoDataSet";
            this.bd_ferroVelhoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vendaBindingSource
            // 
            this.vendaBindingSource.DataMember = "Venda";
            this.vendaBindingSource.DataSource = this.bd_ferroVelhoDataSet;
            // 
            // vendaTableAdapter
            // 
            this.vendaTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.tb_compraTableAdapter = null;
            this.tableAdapterManager.tb_impressoraTableAdapter = null;
            this.tableAdapterManager.tb_itemcTableAdapter = null;
            this.tableAdapterManager.tb_itemvTableAdapter = null;
            this.tableAdapterManager.tb_produtosTableAdapter = null;
            this.tableAdapterManager.tb_vendaTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = FerroVelho.bd_ferroVelhoDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(781, 322);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(109, 17);
            this.checkBox1.TabIndex = 45;
            this.checkBox1.Text = "Imprimir duas vias";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tbprodutosDataGridViewTextBoxColumn
            // 
            this.tbprodutosDataGridViewTextBoxColumn.DataPropertyName = "tb_produtos";
            this.tbprodutosDataGridViewTextBoxColumn.HeaderText = "Descrição";
            this.tbprodutosDataGridViewTextBoxColumn.Name = "tbprodutosDataGridViewTextBoxColumn";
            this.tbprodutosDataGridViewTextBoxColumn.ReadOnly = true;
            this.tbprodutosDataGridViewTextBoxColumn.Width = 470;
            // 
            // valritemDataGridViewTextBoxColumn
            // 
            this.valritemDataGridViewTextBoxColumn.DataPropertyName = "valr_item";
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.valritemDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.valritemDataGridViewTextBoxColumn.HeaderText = "Valor";
            this.valritemDataGridViewTextBoxColumn.Name = "valritemDataGridViewTextBoxColumn";
            this.valritemDataGridViewTextBoxColumn.ReadOnly = true;
            this.valritemDataGridViewTextBoxColumn.Width = 200;
            // 
            // quantitemDataGridViewTextBoxColumn
            // 
            this.quantitemDataGridViewTextBoxColumn.DataPropertyName = "quant_item";
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = null;
            this.quantitemDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.quantitemDataGridViewTextBoxColumn.HeaderText = "Quant. / Peso";
            this.quantitemDataGridViewTextBoxColumn.Name = "quantitemDataGridViewTextBoxColumn";
            this.quantitemDataGridViewTextBoxColumn.ReadOnly = true;
            this.quantitemDataGridViewTextBoxColumn.Width = 200;
            // 
            // subTotitemDataGridViewTextBoxColumn
            // 
            this.subTotitemDataGridViewTextBoxColumn.DataPropertyName = "subTot_item";
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.subTotitemDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.subTotitemDataGridViewTextBoxColumn.HeaderText = "Subtotal";
            this.subTotitemDataGridViewTextBoxColumn.Name = "subTotitemDataGridViewTextBoxColumn";
            this.subTotitemDataGridViewTextBoxColumn.ReadOnly = true;
            this.subTotitemDataGridViewTextBoxColumn.Width = 200;
            // 
            // iditemDataGridViewTextBoxColumn
            // 
            this.iditemDataGridViewTextBoxColumn.DataPropertyName = "id_item";
            this.iditemDataGridViewTextBoxColumn.HeaderText = "id_item";
            this.iditemDataGridViewTextBoxColumn.Name = "iditemDataGridViewTextBoxColumn";
            this.iditemDataGridViewTextBoxColumn.ReadOnly = true;
            this.iditemDataGridViewTextBoxColumn.Visible = false;
            // 
            // idprodDataGridViewTextBoxColumn
            // 
            this.idprodDataGridViewTextBoxColumn.DataPropertyName = "id_prod";
            this.idprodDataGridViewTextBoxColumn.HeaderText = "id_prod";
            this.idprodDataGridViewTextBoxColumn.Name = "idprodDataGridViewTextBoxColumn";
            this.idprodDataGridViewTextBoxColumn.ReadOnly = true;
            this.idprodDataGridViewTextBoxColumn.Visible = false;
            // 
            // idvendaDataGridViewTextBoxColumn
            // 
            this.idvendaDataGridViewTextBoxColumn.DataPropertyName = "id_venda";
            this.idvendaDataGridViewTextBoxColumn.HeaderText = "id_venda";
            this.idvendaDataGridViewTextBoxColumn.Name = "idvendaDataGridViewTextBoxColumn";
            this.idvendaDataGridViewTextBoxColumn.ReadOnly = true;
            this.idvendaDataGridViewTextBoxColumn.Visible = false;
            // 
            // tbvendaDataGridViewTextBoxColumn
            // 
            this.tbvendaDataGridViewTextBoxColumn.DataPropertyName = "tb_venda";
            this.tbvendaDataGridViewTextBoxColumn.HeaderText = "tb_venda";
            this.tbvendaDataGridViewTextBoxColumn.Name = "tbvendaDataGridViewTextBoxColumn";
            this.tbvendaDataGridViewTextBoxColumn.ReadOnly = true;
            this.tbvendaDataGridViewTextBoxColumn.Visible = false;
            // 
            // fm_vender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 408);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.lb_saldo);
            this.Controls.Add(this.dg_venda);
            this.Controls.Add(this.label6);
            this.Controls.Add(id_vendaLabel);
            this.Controls.Add(this.txt_nNota);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_finalCompra);
            this.Controls.Add(this.txt_quant);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_subTot);
            this.Controls.Add(this.txt_valProd);
            this.Controls.Add(this.cb_desProd);
            this.Controls.Add(this.bt_excluir);
            this.Controls.Add(this.bt_novoItem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_codProd);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_vender";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vender";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fm_vender_FormClosing);
            this.Load += new System.EventHandler(this.fm_vender_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tb_vendaBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_venda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_itemvBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_produtosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_impressoraBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bd_ferroVelhoDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vendaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_codProd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource tb_produtosBindingSource;
        private System.Windows.Forms.TextBox txt_subTot;
        private System.Windows.Forms.TextBox txt_valProd;
        private System.Windows.Forms.ComboBox cb_desProd;
        private System.Windows.Forms.Button bt_excluir;
        private System.Windows.Forms.Button bt_novoItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_quant;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_nNota;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Button bt_finalCompra;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.BindingSource tb_itemvBindingSource;
        private System.Windows.Forms.BindingSource tb_vendaBindingSource;
        private System.Windows.Forms.DataGridView dg_venda;
        private System.Windows.Forms.Label lb_saldo;
        private System.Windows.Forms.BindingSource tb_impressoraBindingSource;
        private bd_ferroVelhoDataSet bd_ferroVelhoDataSet;
        private System.Windows.Forms.BindingSource vendaBindingSource;
        private bd_ferroVelhoDataSetTableAdapters.VendaTableAdapter vendaTableAdapter;
        private bd_ferroVelhoDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbprodutosDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valritemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantitemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subTotitemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iditemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idprodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idvendaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbvendaDataGridViewTextBoxColumn;
    }
}