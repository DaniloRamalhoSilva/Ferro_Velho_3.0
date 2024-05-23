namespace FerroVelho
{
    partial class fm_recurcos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_recurcos));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_valRecur = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_saldo = new System.Windows.Forms.Label();
            this.txt_valRet = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_desc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_continuar = new System.Windows.Forms.Button();
            this.tb_caixaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_caixaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Valor a ser adicionado:";
            // 
            // txt_valRecur
            // 
            this.txt_valRecur.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_valRecur.Location = new System.Drawing.Point(7, 110);
            this.txt_valRecur.Name = "txt_valRecur";
            this.txt_valRecur.Size = new System.Drawing.Size(253, 20);
            this.txt_valRecur.TabIndex = 1;
            this.txt_valRecur.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_valRecur_KeyPress);
            this.txt_valRecur.Leave += new System.EventHandler(this.txt_valRecur_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lb_saldo);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 68);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Saldo Disponivel:";
            // 
            // lb_saldo
            // 
            this.lb_saldo.AutoSize = true;
            this.lb_saldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_saldo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lb_saldo.Location = new System.Drawing.Point(6, 16);
            this.lb_saldo.Name = "lb_saldo";
            this.lb_saldo.Size = new System.Drawing.Size(117, 42);
            this.lb_saldo.TabIndex = 15;
            this.lb_saldo.Text = "00,00";
            this.lb_saldo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_valRet
            // 
            this.txt_valRet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_valRet.Location = new System.Drawing.Point(7, 149);
            this.txt_valRet.Name = "txt_valRet";
            this.txt_valRet.Size = new System.Drawing.Size(253, 20);
            this.txt_valRet.TabIndex = 2;
            this.txt_valRet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_valRet_KeyPress);
            this.txt_valRet.Leave += new System.EventHandler(this.txt_valRet_Leave);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Valor a ser retirado:";
            // 
            // txt_desc
            // 
            this.txt_desc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_desc.Location = new System.Drawing.Point(7, 200);
            this.txt_desc.Multiline = true;
            this.txt_desc.Name = "txt_desc";
            this.txt_desc.Size = new System.Drawing.Size(253, 108);
            this.txt_desc.TabIndex = 3;
            this.txt_desc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_desc_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Descrição da movimentação:";
            // 
            // bt_continuar
            // 
            this.bt_continuar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_continuar.Location = new System.Drawing.Point(150, 314);
            this.bt_continuar.Name = "bt_continuar";
            this.bt_continuar.Size = new System.Drawing.Size(110, 23);
            this.bt_continuar.TabIndex = 4;
            this.bt_continuar.Text = "Retirar";
            this.bt_continuar.UseVisualStyleBackColor = true;
            this.bt_continuar.Click += new System.EventHandler(this.bt_continuar_Click);
            // 
            // tb_caixaBindingSource
            // 
            this.tb_caixaBindingSource.DataSource = typeof(FerroVelhoDAO.tb_caixa);
            // 
            // fm_recurcos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 345);
            this.Controls.Add(this.bt_continuar);
            this.Controls.Add(this.txt_desc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_valRet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_valRecur);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_recurcos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adicionar Recursos";
            this.Load += new System.EventHandler(this.fm_recurcos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_caixaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_valRecur;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lb_saldo;
        private System.Windows.Forms.BindingSource tb_caixaBindingSource;
        private System.Windows.Forms.TextBox txt_valRet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_desc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_continuar;
    }
}