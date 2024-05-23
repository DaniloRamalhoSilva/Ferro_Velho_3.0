namespace FerroVelho
{
    partial class fm_notasCompra
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
            System.Windows.Forms.Label nome_usuarioLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_notasCompra));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.Label label4;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dt_fim = new System.Windows.Forms.DateTimePicker();
            this.bt_pesquisa = new System.Windows.Forms.Button();
            this.dt_inicio = new System.Windows.Forms.DateTimePicker();
            this.bt_imprimir = new System.Windows.Forms.Button();
            this.btn_excluir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_itemcDataGridView = new System.Windows.Forms.DataGridView();
            this.idprodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbprodutosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valoritemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantitemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subTotitemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iditemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idcompraDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcompraDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_itemcBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tb_vendaDataGridView = new System.Windows.Forms.DataGridView();
            this.tb_compraBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_notaFiscal = new System.Windows.Forms.TextBox();
            this.lb_usuario = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_cliente = new System.Windows.Forms.Label();
            this.idcompraDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datacompraDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtot_compra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desconto_compra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valornotaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuarioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            nome_usuarioLabel = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_itemcDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_itemcBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_vendaDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_compraBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // nome_usuarioLabel
            // 
            nome_usuarioLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            nome_usuarioLabel.AutoSize = true;
            nome_usuarioLabel.Location = new System.Drawing.Point(9, 544);
            nome_usuarioLabel.Name = "nome_usuarioLabel";
            nome_usuarioLabel.Size = new System.Drawing.Size(54, 13);
            nome_usuarioLabel.TabIndex = 28;
            nome_usuarioLabel.Text = "Operador:";
            // 
            // dt_fim
            // 
            this.dt_fim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dt_fim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_fim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_fim.Location = new System.Drawing.Point(669, 76);
            this.dt_fim.Name = "dt_fim";
            this.dt_fim.Size = new System.Drawing.Size(92, 26);
            this.dt_fim.TabIndex = 25;
            this.dt_fim.Value = new System.DateTime(2021, 6, 15, 0, 0, 0, 0);
            // 
            // bt_pesquisa
            // 
            this.bt_pesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_pesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_pesquisa.Image = ((System.Drawing.Image)(resources.GetObject("bt_pesquisa.Image")));
            this.bt_pesquisa.Location = new System.Drawing.Point(764, 71);
            this.bt_pesquisa.Margin = new System.Windows.Forms.Padding(0);
            this.bt_pesquisa.Name = "bt_pesquisa";
            this.bt_pesquisa.Size = new System.Drawing.Size(38, 31);
            this.bt_pesquisa.TabIndex = 24;
            this.bt_pesquisa.UseVisualStyleBackColor = true;
            this.bt_pesquisa.Click += new System.EventHandler(this.bt_pesquisa_Click);
            // 
            // dt_inicio
            // 
            this.dt_inicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dt_inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_inicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_inicio.Location = new System.Drawing.Point(567, 76);
            this.dt_inicio.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dt_inicio.Name = "dt_inicio";
            this.dt_inicio.Size = new System.Drawing.Size(98, 26);
            this.dt_inicio.TabIndex = 23;
            this.dt_inicio.Value = new System.DateTime(2021, 6, 15, 0, 0, 0, 0);
            // 
            // bt_imprimir
            // 
            this.bt_imprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_imprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_imprimir.Location = new System.Drawing.Point(561, 201);
            this.bt_imprimir.Name = "bt_imprimir";
            this.bt_imprimir.Size = new System.Drawing.Size(244, 31);
            this.bt_imprimir.TabIndex = 22;
            this.bt_imprimir.Text = "Imprimir";
            this.bt_imprimir.UseVisualStyleBackColor = true;
            this.bt_imprimir.Click += new System.EventHandler(this.bt_imprimir_Click);
            // 
            // btn_excluir
            // 
            this.btn_excluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_excluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_excluir.Location = new System.Drawing.Point(561, 164);
            this.btn_excluir.Name = "btn_excluir";
            this.btn_excluir.Size = new System.Drawing.Size(244, 31);
            this.btn_excluir.TabIndex = 21;
            this.btn_excluir.Text = "Excluir";
            this.btn_excluir.UseVisualStyleBackColor = true;
            this.btn_excluir.Click += new System.EventHandler(this.btn_excluir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tb_itemcDataGridView);
            this.groupBox1.Location = new System.Drawing.Point(12, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(790, 302);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalhes da venda";
            // 
            // tb_itemcDataGridView
            // 
            this.tb_itemcDataGridView.AllowUserToAddRows = false;
            this.tb_itemcDataGridView.AllowUserToDeleteRows = false;
            this.tb_itemcDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_itemcDataGridView.AutoGenerateColumns = false;
            this.tb_itemcDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tb_itemcDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idprodDataGridViewTextBoxColumn,
            this.tbprodutosDataGridViewTextBoxColumn,
            this.valoritemDataGridViewTextBoxColumn,
            this.quantitemDataGridViewTextBoxColumn,
            this.subTotitemDataGridViewTextBoxColumn,
            this.iditemDataGridViewTextBoxColumn,
            this.idcompraDataGridViewTextBoxColumn1,
            this.tbcompraDataGridViewTextBoxColumn});
            this.tb_itemcDataGridView.DataSource = this.tb_itemcBindingSource;
            this.tb_itemcDataGridView.Enabled = false;
            this.tb_itemcDataGridView.Location = new System.Drawing.Point(6, 19);
            this.tb_itemcDataGridView.Name = "tb_itemcDataGridView";
            this.tb_itemcDataGridView.ReadOnly = true;
            this.tb_itemcDataGridView.Size = new System.Drawing.Size(784, 275);
            this.tb_itemcDataGridView.TabIndex = 0;
            this.tb_itemcDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.tb_itemcDataGridView_CellFormatting_1);
            // 
            // idprodDataGridViewTextBoxColumn
            // 
            this.idprodDataGridViewTextBoxColumn.DataPropertyName = "id_prod";
            this.idprodDataGridViewTextBoxColumn.HeaderText = "Codigo";
            this.idprodDataGridViewTextBoxColumn.Name = "idprodDataGridViewTextBoxColumn";
            this.idprodDataGridViewTextBoxColumn.ReadOnly = true;
            this.idprodDataGridViewTextBoxColumn.Width = 50;
            // 
            // tbprodutosDataGridViewTextBoxColumn
            // 
            this.tbprodutosDataGridViewTextBoxColumn.DataPropertyName = "tb_produtos";
            dataGridViewCellStyle1.Format = "((tb_produtos)e.Value).desc_prod";
            this.tbprodutosDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.tbprodutosDataGridViewTextBoxColumn.HeaderText = "Descrição";
            this.tbprodutosDataGridViewTextBoxColumn.Name = "tbprodutosDataGridViewTextBoxColumn";
            this.tbprodutosDataGridViewTextBoxColumn.ReadOnly = true;
            this.tbprodutosDataGridViewTextBoxColumn.Width = 200;
            // 
            // valoritemDataGridViewTextBoxColumn
            // 
            this.valoritemDataGridViewTextBoxColumn.DataPropertyName = "valor_item";
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.valoritemDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.valoritemDataGridViewTextBoxColumn.HeaderText = "Valor uni.";
            this.valoritemDataGridViewTextBoxColumn.Name = "valoritemDataGridViewTextBoxColumn";
            this.valoritemDataGridViewTextBoxColumn.ReadOnly = true;
            this.valoritemDataGridViewTextBoxColumn.Width = 80;
            // 
            // quantitemDataGridViewTextBoxColumn
            // 
            this.quantitemDataGridViewTextBoxColumn.DataPropertyName = "quant_item";
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = null;
            this.quantitemDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.quantitemDataGridViewTextBoxColumn.HeaderText = "Quant./Peso";
            this.quantitemDataGridViewTextBoxColumn.Name = "quantitemDataGridViewTextBoxColumn";
            this.quantitemDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // subTotitemDataGridViewTextBoxColumn
            // 
            this.subTotitemDataGridViewTextBoxColumn.DataPropertyName = "subTot_item";
            dataGridViewCellStyle4.Format = "C2";
            this.subTotitemDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.subTotitemDataGridViewTextBoxColumn.HeaderText = "Subtotal";
            this.subTotitemDataGridViewTextBoxColumn.Name = "subTotitemDataGridViewTextBoxColumn";
            this.subTotitemDataGridViewTextBoxColumn.ReadOnly = true;
            this.subTotitemDataGridViewTextBoxColumn.Width = 150;
            // 
            // iditemDataGridViewTextBoxColumn
            // 
            this.iditemDataGridViewTextBoxColumn.DataPropertyName = "id_item";
            this.iditemDataGridViewTextBoxColumn.HeaderText = "id_item";
            this.iditemDataGridViewTextBoxColumn.Name = "iditemDataGridViewTextBoxColumn";
            this.iditemDataGridViewTextBoxColumn.ReadOnly = true;
            this.iditemDataGridViewTextBoxColumn.Visible = false;
            // 
            // idcompraDataGridViewTextBoxColumn1
            // 
            this.idcompraDataGridViewTextBoxColumn1.DataPropertyName = "id_compra";
            this.idcompraDataGridViewTextBoxColumn1.HeaderText = "id_compra";
            this.idcompraDataGridViewTextBoxColumn1.Name = "idcompraDataGridViewTextBoxColumn1";
            this.idcompraDataGridViewTextBoxColumn1.ReadOnly = true;
            this.idcompraDataGridViewTextBoxColumn1.Visible = false;
            // 
            // tbcompraDataGridViewTextBoxColumn
            // 
            this.tbcompraDataGridViewTextBoxColumn.DataPropertyName = "tb_compra";
            this.tbcompraDataGridViewTextBoxColumn.HeaderText = "tb_compra";
            this.tbcompraDataGridViewTextBoxColumn.Name = "tbcompraDataGridViewTextBoxColumn";
            this.tbcompraDataGridViewTextBoxColumn.ReadOnly = true;
            this.tbcompraDataGridViewTextBoxColumn.Visible = false;
            // 
            // tb_itemcBindingSource
            // 
            this.tb_itemcBindingSource.DataSource = typeof(FerroVelhoDAO.tb_itemc);
            // 
            // tb_vendaDataGridView
            // 
            this.tb_vendaDataGridView.AllowUserToAddRows = false;
            this.tb_vendaDataGridView.AllowUserToDeleteRows = false;
            this.tb_vendaDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_vendaDataGridView.AutoGenerateColumns = false;
            this.tb_vendaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tb_vendaDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idcompraDataGridViewTextBoxColumn,
            this.datacompraDataGridViewTextBoxColumn,
            this.subtot_compra,
            this.desconto_compra,
            this.valornotaDataGridViewTextBoxColumn,
            this.usuarioDataGridViewTextBoxColumn,
            this.id_cliente});
            this.tb_vendaDataGridView.DataSource = this.tb_compraBindingSource;
            this.tb_vendaDataGridView.Location = new System.Drawing.Point(12, 12);
            this.tb_vendaDataGridView.Name = "tb_vendaDataGridView";
            this.tb_vendaDataGridView.ReadOnly = true;
            this.tb_vendaDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tb_vendaDataGridView.Size = new System.Drawing.Size(545, 220);
            this.tb_vendaDataGridView.TabIndex = 19;
            this.tb_vendaDataGridView.CurrentCellChanged += new System.EventHandler(this.tb_vendaDataGridView_CurrentCellChanged);
            // 
            // tb_compraBindingSource
            // 
            this.tb_compraBindingSource.DataSource = typeof(FerroVelhoDAO.tb_compra);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(764, 26);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 31);
            this.button1.TabIndex = 28;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(562, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Nº Nota Fiscal:";
            // 
            // txt_notaFiscal
            // 
            this.txt_notaFiscal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_notaFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_notaFiscal.Location = new System.Drawing.Point(565, 28);
            this.txt_notaFiscal.Name = "txt_notaFiscal";
            this.txt_notaFiscal.Size = new System.Drawing.Size(196, 29);
            this.txt_notaFiscal.TabIndex = 26;
            // 
            // lb_usuario
            // 
            this.lb_usuario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_usuario.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tb_compraBindingSource, "tb_usuario.nome_usuario", true));
            this.lb_usuario.Location = new System.Drawing.Point(59, 544);
            this.lb_usuario.Name = "lb_usuario";
            this.lb_usuario.Size = new System.Drawing.Size(250, 15);
            this.lb_usuario.TabIndex = 29;
            this.lb_usuario.Text = "label1";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(564, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Data Inicio:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(668, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Data Fim:";
            // 
            // label4
            // 
            label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(400, 546);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(42, 13);
            label4.TabIndex = 32;
            label4.Text = "Cliente:";
            // 
            // lb_cliente
            // 
            this.lb_cliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_cliente.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tb_compraBindingSource, "tb_usuario.nome_usuario", true));
            this.lb_cliente.Location = new System.Drawing.Point(438, 546);
            this.lb_cliente.Name = "lb_cliente";
            this.lb_cliente.Size = new System.Drawing.Size(250, 15);
            this.lb_cliente.TabIndex = 33;
            this.lb_cliente.Text = "label1";
            // 
            // idcompraDataGridViewTextBoxColumn
            // 
            this.idcompraDataGridViewTextBoxColumn.DataPropertyName = "id_compra";
            this.idcompraDataGridViewTextBoxColumn.HeaderText = "Nº Nota";
            this.idcompraDataGridViewTextBoxColumn.Name = "idcompraDataGridViewTextBoxColumn";
            this.idcompraDataGridViewTextBoxColumn.ReadOnly = true;
            this.idcompraDataGridViewTextBoxColumn.Width = 80;
            // 
            // datacompraDataGridViewTextBoxColumn
            // 
            this.datacompraDataGridViewTextBoxColumn.DataPropertyName = "data_compra";
            dataGridViewCellStyle5.Format = "g";
            dataGridViewCellStyle5.NullValue = null;
            this.datacompraDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.datacompraDataGridViewTextBoxColumn.HeaderText = "Data da compra";
            this.datacompraDataGridViewTextBoxColumn.Name = "datacompraDataGridViewTextBoxColumn";
            this.datacompraDataGridViewTextBoxColumn.ReadOnly = true;
            this.datacompraDataGridViewTextBoxColumn.Width = 160;
            // 
            // subtot_compra
            // 
            this.subtot_compra.DataPropertyName = "subtot_compra";
            this.subtot_compra.HeaderText = "Subtotal";
            this.subtot_compra.Name = "subtot_compra";
            this.subtot_compra.ReadOnly = true;
            this.subtot_compra.Width = 80;
            // 
            // desconto_compra
            // 
            this.desconto_compra.DataPropertyName = "desconto_compra";
            this.desconto_compra.HeaderText = "Desconto";
            this.desconto_compra.Name = "desconto_compra";
            this.desconto_compra.ReadOnly = true;
            this.desconto_compra.Width = 80;
            // 
            // valornotaDataGridViewTextBoxColumn
            // 
            this.valornotaDataGridViewTextBoxColumn.DataPropertyName = "valor_nota";
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.valornotaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.valornotaDataGridViewTextBoxColumn.HeaderText = "Valor";
            this.valornotaDataGridViewTextBoxColumn.Name = "valornotaDataGridViewTextBoxColumn";
            this.valornotaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usuarioDataGridViewTextBoxColumn
            // 
            this.usuarioDataGridViewTextBoxColumn.DataPropertyName = "usuario";
            this.usuarioDataGridViewTextBoxColumn.HeaderText = "usuario";
            this.usuarioDataGridViewTextBoxColumn.Name = "usuarioDataGridViewTextBoxColumn";
            this.usuarioDataGridViewTextBoxColumn.ReadOnly = true;
            this.usuarioDataGridViewTextBoxColumn.Visible = false;
            // 
            // id_cliente
            // 
            this.id_cliente.DataPropertyName = "id_cliente";
            dataGridViewCellStyle7.NullValue = "Cliente não informado";
            this.id_cliente.DefaultCellStyle = dataGridViewCellStyle7;
            this.id_cliente.HeaderText = "Cliente";
            this.id_cliente.Name = "id_cliente";
            this.id_cliente.ReadOnly = true;
            this.id_cliente.Visible = false;
            // 
            // fm_notasCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 567);
            this.Controls.Add(label4);
            this.Controls.Add(this.lb_cliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(nome_usuarioLabel);
            this.Controls.Add(this.lb_usuario);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_notaFiscal);
            this.Controls.Add(this.dt_fim);
            this.Controls.Add(this.bt_pesquisa);
            this.Controls.Add(this.dt_inicio);
            this.Controls.Add(this.bt_imprimir);
            this.Controls.Add(this.btn_excluir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_vendaDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_notasCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notas Fiscais de Compra";
            this.Load += new System.EventHandler(this.fm_notasCompra_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tb_itemcDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_itemcBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_vendaDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_compraBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dt_fim;
        private System.Windows.Forms.Button bt_pesquisa;
        private System.Windows.Forms.DateTimePicker dt_inicio;
        private System.Windows.Forms.Button bt_imprimir;
        private System.Windows.Forms.Button btn_excluir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView tb_itemcDataGridView;
        private System.Windows.Forms.DataGridView tb_vendaDataGridView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_notaFiscal;
        private System.Windows.Forms.BindingSource tb_itemcBindingSource;
        private System.Windows.Forms.BindingSource tb_compraBindingSource;
        private System.Windows.Forms.Label lb_usuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idprodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbprodutosDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valoritemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantitemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subTotitemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iditemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idcompraDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcompraDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label lb_cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn idcompraDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datacompraDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtot_compra;
        private System.Windows.Forms.DataGridViewTextBoxColumn desconto_compra;
        private System.Windows.Forms.DataGridViewTextBoxColumn valornotaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuarioDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_cliente;
    }
}