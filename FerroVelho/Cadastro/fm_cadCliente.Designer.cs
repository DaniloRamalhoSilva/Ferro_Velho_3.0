namespace FerroVelho
{
    partial class fm_cadCliente
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
            System.Windows.Forms.Label cod_mpLabel;
            System.Windows.Forms.Label desc_mpLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_cadCliente));
            this.txt_cnpj = new System.Windows.Forms.MaskedTextBox();
            this.btn_novo = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_excluir = new System.Windows.Forms.Button();
            this.btn_alterar = new System.Windows.Forms.Button();
            this.btn_salvar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_descricao = new System.Windows.Forms.TextBox();
            this.txt_tel = new System.Windows.Forms.MaskedTextBox();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.bt_select = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxCompleto = new System.Windows.Forms.CheckBox();
            this.checkBox_AdPg = new System.Windows.Forms.CheckBox();
            this.dg_adPg = new System.Windows.Forms.DataGridView();
            this.tb_clienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idclienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cpfclienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomeclienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telclienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            cod_mpLabel = new System.Windows.Forms.Label();
            desc_mpLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_adPg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_clienteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cod_mpLabel
            // 
            cod_mpLabel.AutoSize = true;
            cod_mpLabel.Location = new System.Drawing.Point(12, 9);
            cod_mpLabel.Name = "cod_mpLabel";
            cod_mpLabel.Size = new System.Drawing.Size(30, 13);
            cod_mpLabel.TabIndex = 38;
            cod_mpLabel.Text = "CPF:";
            // 
            // desc_mpLabel
            // 
            desc_mpLabel.AutoSize = true;
            desc_mpLabel.Location = new System.Drawing.Point(136, 9);
            desc_mpLabel.Name = "desc_mpLabel";
            desc_mpLabel.Size = new System.Drawing.Size(38, 13);
            desc_mpLabel.TabIndex = 39;
            desc_mpLabel.Text = "Nome:";
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(419, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 13);
            label1.TabIndex = 48;
            label1.Text = "Telefone:";
            // 
            // txt_cnpj
            // 
            this.txt_cnpj.Location = new System.Drawing.Point(15, 25);
            this.txt_cnpj.Mask = "###.###.###-##";
            this.txt_cnpj.Name = "txt_cnpj";
            this.txt_cnpj.Size = new System.Drawing.Size(118, 20);
            this.txt_cnpj.TabIndex = 1;
            this.txt_cnpj.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_cnpj_KeyUp);
            // 
            // btn_novo
            // 
            this.btn_novo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_novo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_novo.Location = new System.Drawing.Point(625, 51);
            this.btn_novo.Name = "btn_novo";
            this.btn_novo.Size = new System.Drawing.Size(123, 31);
            this.btn_novo.TabIndex = 46;
            this.btn_novo.Text = "Novo";
            this.btn_novo.UseVisualStyleBackColor = true;
            this.btn_novo.Click += new System.EventHandler(this.btn_novo_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.Location = new System.Drawing.Point(625, 199);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(123, 31);
            this.btn_cancelar.TabIndex = 45;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = true;
            this.btn_cancelar.Visible = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_excluir
            // 
            this.btn_excluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_excluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_excluir.Location = new System.Drawing.Point(625, 125);
            this.btn_excluir.Name = "btn_excluir";
            this.btn_excluir.Size = new System.Drawing.Size(123, 31);
            this.btn_excluir.TabIndex = 44;
            this.btn_excluir.Text = "Excluir";
            this.btn_excluir.UseVisualStyleBackColor = true;
            this.btn_excluir.Click += new System.EventHandler(this.btn_excluir_Click);
            // 
            // btn_alterar
            // 
            this.btn_alterar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_alterar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_alterar.Location = new System.Drawing.Point(625, 88);
            this.btn_alterar.Name = "btn_alterar";
            this.btn_alterar.Size = new System.Drawing.Size(123, 31);
            this.btn_alterar.TabIndex = 43;
            this.btn_alterar.Text = "Alterar";
            this.btn_alterar.UseVisualStyleBackColor = true;
            this.btn_alterar.Click += new System.EventHandler(this.btn_alterar_Click);
            // 
            // btn_salvar
            // 
            this.btn_salvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_salvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_salvar.Location = new System.Drawing.Point(625, 236);
            this.btn_salvar.Name = "btn_salvar";
            this.btn_salvar.Size = new System.Drawing.Size(123, 31);
            this.btn_salvar.TabIndex = 4;
            this.btn_salvar.Text = "Salvar";
            this.btn_salvar.UseVisualStyleBackColor = true;
            this.btn_salvar.Visible = false;
            this.btn_salvar.Click += new System.EventHandler(this.btn_salvar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idclienteDataGridViewTextBoxColumn,
            this.cpfclienteDataGridViewTextBoxColumn,
            this.nomeclienteDataGridViewTextBoxColumn,
            this.telclienteDataGridViewTextBoxColumn,
            this.Saldo});
            this.dataGridView1.DataSource = this.tb_clienteBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(607, 222);
            this.dataGridView1.TabIndex = 41;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            // 
            // txt_descricao
            // 
            this.txt_descricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_descricao.Location = new System.Drawing.Point(139, 25);
            this.txt_descricao.Name = "txt_descricao";
            this.txt_descricao.Size = new System.Drawing.Size(277, 20);
            this.txt_descricao.TabIndex = 2;
            this.txt_descricao.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_descricao_KeyUp);
            // 
            // txt_tel
            // 
            this.txt_tel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_tel.Location = new System.Drawing.Point(422, 25);
            this.txt_tel.Mask = "(00)00000-9999";
            this.txt_tel.Name = "txt_tel";
            this.txt_tel.Size = new System.Drawing.Size(103, 20);
            this.txt_tel.TabIndex = 3;
            this.txt_tel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_tel_KeyUp);
            // 
            // txt_id
            // 
            this.txt_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_id.Enabled = false;
            this.txt_id.Location = new System.Drawing.Point(139, 25);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(116, 20);
            this.txt_id.TabIndex = 50;
            // 
            // bt_select
            // 
            this.bt_select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_select.Location = new System.Drawing.Point(625, 162);
            this.bt_select.Name = "bt_select";
            this.bt_select.Size = new System.Drawing.Size(123, 31);
            this.bt_select.TabIndex = 51;
            this.bt_select.Text = "Selecionar";
            this.bt_select.UseVisualStyleBackColor = true;
            this.bt_select.Click += new System.EventHandler(this.bt_select_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxCompleto);
            this.groupBox1.Controls.Add(this.checkBox_AdPg);
            this.groupBox1.Controls.Add(this.dg_adPg);
            this.groupBox1.Location = new System.Drawing.Point(12, 279);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(736, 260);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Historico ";
            // 
            // checkBoxCompleto
            // 
            this.checkBoxCompleto.AutoSize = true;
            this.checkBoxCompleto.Location = new System.Drawing.Point(162, 19);
            this.checkBoxCompleto.Name = "checkBoxCompleto";
            this.checkBoxCompleto.Size = new System.Drawing.Size(70, 17);
            this.checkBoxCompleto.TabIndex = 44;
            this.checkBoxCompleto.Text = "Completo";
            this.checkBoxCompleto.UseVisualStyleBackColor = true;
            this.checkBoxCompleto.CheckedChanged += new System.EventHandler(this.checkBoxCompleto_CheckedChanged);
            // 
            // checkBox_AdPg
            // 
            this.checkBox_AdPg.AutoSize = true;
            this.checkBox_AdPg.Checked = true;
            this.checkBox_AdPg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_AdPg.Location = new System.Drawing.Point(6, 19);
            this.checkBox_AdPg.Name = "checkBox_AdPg";
            this.checkBox_AdPg.Size = new System.Drawing.Size(150, 17);
            this.checkBox_AdPg.TabIndex = 43;
            this.checkBox_AdPg.Text = "Adiantamento/Pagamento";
            this.checkBox_AdPg.UseVisualStyleBackColor = true;
            this.checkBox_AdPg.CheckedChanged += new System.EventHandler(this.checkBox_AdPg_CheckedChanged);
            // 
            // dg_adPg
            // 
            this.dg_adPg.AllowUserToAddRows = false;
            this.dg_adPg.AllowUserToDeleteRows = false;
            this.dg_adPg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_adPg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dg_adPg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_adPg.DefaultCellStyle = dataGridViewCellStyle4;
            this.dg_adPg.Location = new System.Drawing.Point(3, 42);
            this.dg_adPg.Name = "dg_adPg";
            this.dg_adPg.ReadOnly = true;
            this.dg_adPg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_adPg.Size = new System.Drawing.Size(724, 212);
            this.dg_adPg.TabIndex = 42;
            this.dg_adPg.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dg_adPg_CellFormatting);
            // 
            // tb_clienteBindingSource
            // 
            this.tb_clienteBindingSource.DataSource = typeof(FerroVelhoDAO.tb_cliente);
            // 
            // idclienteDataGridViewTextBoxColumn
            // 
            this.idclienteDataGridViewTextBoxColumn.DataPropertyName = "id_cliente";
            this.idclienteDataGridViewTextBoxColumn.HeaderText = "id_cliente";
            this.idclienteDataGridViewTextBoxColumn.Name = "idclienteDataGridViewTextBoxColumn";
            this.idclienteDataGridViewTextBoxColumn.ReadOnly = true;
            this.idclienteDataGridViewTextBoxColumn.Visible = false;
            // 
            // cpfclienteDataGridViewTextBoxColumn
            // 
            this.cpfclienteDataGridViewTextBoxColumn.DataPropertyName = "cpf_cliente";
            dataGridViewCellStyle1.Format = "###.###.###-##";
            dataGridViewCellStyle1.NullValue = null;
            this.cpfclienteDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.cpfclienteDataGridViewTextBoxColumn.HeaderText = "CPF";
            this.cpfclienteDataGridViewTextBoxColumn.Name = "cpfclienteDataGridViewTextBoxColumn";
            this.cpfclienteDataGridViewTextBoxColumn.ReadOnly = true;
            this.cpfclienteDataGridViewTextBoxColumn.Width = 110;
            // 
            // nomeclienteDataGridViewTextBoxColumn
            // 
            this.nomeclienteDataGridViewTextBoxColumn.DataPropertyName = "nome_cliente";
            this.nomeclienteDataGridViewTextBoxColumn.HeaderText = "Nome";
            this.nomeclienteDataGridViewTextBoxColumn.Name = "nomeclienteDataGridViewTextBoxColumn";
            this.nomeclienteDataGridViewTextBoxColumn.ReadOnly = true;
            this.nomeclienteDataGridViewTextBoxColumn.Width = 250;
            // 
            // telclienteDataGridViewTextBoxColumn
            // 
            this.telclienteDataGridViewTextBoxColumn.DataPropertyName = "tel_cliente";
            this.telclienteDataGridViewTextBoxColumn.HeaderText = "Telefone";
            this.telclienteDataGridViewTextBoxColumn.Name = "telclienteDataGridViewTextBoxColumn";
            this.telclienteDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Saldo
            // 
            this.Saldo.DataPropertyName = "Saldo";
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.Saldo.DefaultCellStyle = dataGridViewCellStyle2;
            this.Saldo.HeaderText = "R$ Saldo";
            this.Saldo.Name = "Saldo";
            this.Saldo.ReadOnly = true;
            // 
            // fm_cadCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 545);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_select);
            this.Controls.Add(this.txt_tel);
            this.Controls.Add(label1);
            this.Controls.Add(this.txt_cnpj);
            this.Controls.Add(this.btn_novo);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_excluir);
            this.Controls.Add(this.btn_alterar);
            this.Controls.Add(this.btn_salvar);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(cod_mpLabel);
            this.Controls.Add(desc_mpLabel);
            this.Controls.Add(this.txt_descricao);
            this.Controls.Add(this.txt_id);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_cadCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Cliente";
            this.Load += new System.EventHandler(this.fm_cadCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_adPg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_clienteBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox txt_cnpj;
        private System.Windows.Forms.Button btn_novo;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Button btn_excluir;
        private System.Windows.Forms.Button btn_alterar;
        private System.Windows.Forms.Button btn_salvar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_descricao;
        private System.Windows.Forms.MaskedTextBox txt_tel;
        private System.Windows.Forms.BindingSource tb_clienteBindingSource;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Button bt_select;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dg_adPg;
        private System.Windows.Forms.CheckBox checkBoxCompleto;
        private System.Windows.Forms.CheckBox checkBox_AdPg;
        private System.Windows.Forms.DataGridViewTextBoxColumn idclienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cpfclienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomeclienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telclienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Saldo;
    }
}