namespace FerroVelho.Operaçoes
{
    partial class fm_recebmentoOp
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
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label cod_mpLabel;
            System.Windows.Forms.Label desc_mpLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_recebmentoOp));
            this.bt_salvar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_data = new System.Windows.Forms.DateTimePicker();
            this.txt_obs = new System.Windows.Forms.TextBox();
            this.txt_valor = new System.Windows.Forms.TextBox();
            this.bt_select = new System.Windows.Forms.Button();
            this.txt_tel = new System.Windows.Forms.MaskedTextBox();
            this.txt_cnpj = new System.Windows.Forms.MaskedTextBox();
            this.txt_nome = new System.Windows.Forms.TextBox();
            this.txt_id = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            cod_mpLabel = new System.Windows.Forms.Label();
            desc_mpLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(211, 24);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(68, 13);
            label4.TabIndex = 61;
            label4.Text = "Observação:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 25);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(33, 13);
            label2.TabIndex = 57;
            label2.Text = "Data:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(103, 24);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(34, 13);
            label3.TabIndex = 59;
            label3.Text = "Valor:";
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(579, 20);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 13);
            label1.TabIndex = 70;
            label1.Text = "Telefone:";
            // 
            // cod_mpLabel
            // 
            cod_mpLabel.AutoSize = true;
            cod_mpLabel.Location = new System.Drawing.Point(149, 20);
            cod_mpLabel.Name = "cod_mpLabel";
            cod_mpLabel.Size = new System.Drawing.Size(30, 13);
            cod_mpLabel.TabIndex = 68;
            cod_mpLabel.Text = "CPF:";
            // 
            // desc_mpLabel
            // 
            desc_mpLabel.AutoSize = true;
            desc_mpLabel.Location = new System.Drawing.Point(273, 20);
            desc_mpLabel.Name = "desc_mpLabel";
            desc_mpLabel.Size = new System.Drawing.Size(38, 13);
            desc_mpLabel.TabIndex = 69;
            desc_mpLabel.Text = "Nome:";
            // 
            // bt_salvar
            // 
            this.bt_salvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_salvar.Location = new System.Drawing.Point(553, 145);
            this.bt_salvar.Name = "bt_salvar";
            this.bt_salvar.Size = new System.Drawing.Size(130, 36);
            this.bt_salvar.TabIndex = 73;
            this.bt_salvar.Text = "Salvar";
            this.bt_salvar.UseVisualStyleBackColor = true;
            this.bt_salvar.Click += new System.EventHandler(this.bt_salvar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txt_data);
            this.groupBox1.Controls.Add(this.txt_obs);
            this.groupBox1.Controls.Add(label4);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(label3);
            this.groupBox1.Controls.Add(this.txt_valor);
            this.groupBox1.Location = new System.Drawing.Point(13, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 77);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Movimentação";
            // 
            // txt_data
            // 
            this.txt_data.Enabled = false;
            this.txt_data.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txt_data.Location = new System.Drawing.Point(9, 40);
            this.txt_data.Name = "txt_data";
            this.txt_data.Size = new System.Drawing.Size(92, 20);
            this.txt_data.TabIndex = 62;
            this.txt_data.Value = new System.DateTime(2021, 8, 31, 8, 42, 42, 0);
            // 
            // txt_obs
            // 
            this.txt_obs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_obs.Location = new System.Drawing.Point(215, 40);
            this.txt_obs.Name = "txt_obs";
            this.txt_obs.Size = new System.Drawing.Size(451, 20);
            this.txt_obs.TabIndex = 60;
            // 
            // txt_valor
            // 
            this.txt_valor.Location = new System.Drawing.Point(107, 40);
            this.txt_valor.Name = "txt_valor";
            this.txt_valor.Size = new System.Drawing.Size(102, 20);
            this.txt_valor.TabIndex = 58;
            // 
            // bt_select
            // 
            this.bt_select.Location = new System.Drawing.Point(13, 20);
            this.bt_select.Name = "bt_select";
            this.bt_select.Size = new System.Drawing.Size(130, 36);
            this.bt_select.TabIndex = 71;
            this.bt_select.Text = "Selecionar cliente";
            this.bt_select.UseVisualStyleBackColor = true;
            this.bt_select.Click += new System.EventHandler(this.bt_select_Click);
            // 
            // txt_tel
            // 
            this.txt_tel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_tel.Enabled = false;
            this.txt_tel.Location = new System.Drawing.Point(582, 36);
            this.txt_tel.Mask = "(00)00000-9999";
            this.txt_tel.Name = "txt_tel";
            this.txt_tel.Size = new System.Drawing.Size(103, 20);
            this.txt_tel.TabIndex = 67;
            // 
            // txt_cnpj
            // 
            this.txt_cnpj.Enabled = false;
            this.txt_cnpj.Location = new System.Drawing.Point(152, 36);
            this.txt_cnpj.Mask = "###.###.###-##";
            this.txt_cnpj.Name = "txt_cnpj";
            this.txt_cnpj.Size = new System.Drawing.Size(118, 20);
            this.txt_cnpj.TabIndex = 65;
            // 
            // txt_nome
            // 
            this.txt_nome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_nome.Enabled = false;
            this.txt_nome.Location = new System.Drawing.Point(276, 36);
            this.txt_nome.Name = "txt_nome";
            this.txt_nome.Size = new System.Drawing.Size(300, 20);
            this.txt_nome.TabIndex = 66;
            // 
            // txt_id
            // 
            this.txt_id.Location = new System.Drawing.Point(13, 29);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(101, 20);
            this.txt_id.TabIndex = 74;
            // 
            // fm_recebmentoOp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 188);
            this.Controls.Add(this.bt_salvar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_select);
            this.Controls.Add(this.txt_tel);
            this.Controls.Add(label1);
            this.Controls.Add(this.txt_cnpj);
            this.Controls.Add(cod_mpLabel);
            this.Controls.Add(desc_mpLabel);
            this.Controls.Add(this.txt_nome);
            this.Controls.Add(this.txt_id);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_recebmentoOp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recebimento de Cliente";
            this.Load += new System.EventHandler(this.fm_recebmentoOp_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_salvar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker txt_data;
        private System.Windows.Forms.TextBox txt_obs;
        private System.Windows.Forms.TextBox txt_valor;
        private System.Windows.Forms.Button bt_select;
        private System.Windows.Forms.MaskedTextBox txt_tel;
        private System.Windows.Forms.MaskedTextBox txt_cnpj;
        private System.Windows.Forms.TextBox txt_nome;
        private System.Windows.Forms.TextBox txt_id;
    }
}