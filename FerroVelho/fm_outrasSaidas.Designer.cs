namespace FerroVelho
{
    partial class fm_outrasSaidas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_outrasSaidas));
            this.bt_finalCompra = new System.Windows.Forms.Button();
            this.txt_quant = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_desProd = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_codProd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.id_vendaLabel1 = new System.Windows.Forms.Label();
            this.tb_saidaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lb_saldo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_produtosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tb_itemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tb_saidaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_produtosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_itemsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_finalCompra
            // 
            this.bt_finalCompra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_finalCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_finalCompra.Location = new System.Drawing.Point(640, 23);
            this.bt_finalCompra.Name = "bt_finalCompra";
            this.bt_finalCompra.Size = new System.Drawing.Size(109, 48);
            this.bt_finalCompra.TabIndex = 4;
            this.bt_finalCompra.Text = "Finalizar";
            this.bt_finalCompra.UseVisualStyleBackColor = true;
            this.bt_finalCompra.Click += new System.EventHandler(this.bt_finalCompra_Click_1);
            // 
            // txt_quant
            // 
            this.txt_quant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_quant.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_quant.Location = new System.Drawing.Point(486, 25);
            this.txt_quant.Name = "txt_quant";
            this.txt_quant.Size = new System.Drawing.Size(148, 44);
            this.txt_quant.TabIndex = 3;
            this.txt_quant.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_quant_KeyPress);
            this.txt_quant.Leave += new System.EventHandler(this.txt_quant_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(483, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Quantidade/Peso:";
            // 
            // cb_desProd
            // 
            this.cb_desProd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_desProd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_desProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_desProd.FormattingEnabled = true;
            this.cb_desProd.Location = new System.Drawing.Point(100, 24);
            this.cb_desProd.Name = "cb_desProd";
            this.cb_desProd.Size = new System.Drawing.Size(380, 45);
            this.cb_desProd.TabIndex = 2;
            this.cb_desProd.Leave += new System.EventHandler(this.cb_desProd_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Descrição Produto:";
            // 
            // txt_codProd
            // 
            this.txt_codProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_codProd.Location = new System.Drawing.Point(14, 24);
            this.txt_codProd.Name = "txt_codProd";
            this.txt_codProd.Size = new System.Drawing.Size(80, 44);
            this.txt_codProd.TabIndex = 1;
            this.txt_codProd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_codProd_KeyPress);
            this.txt_codProd.Leave += new System.EventHandler(this.txt_codProd_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Codigo Produto:";
            // 
            // id_vendaLabel1
            // 
            this.id_vendaLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tb_saidaBindingSource, "id_venda", true));
            this.id_vendaLabel1.Location = new System.Drawing.Point(646, 81);
            this.id_vendaLabel1.Name = "id_vendaLabel1";
            this.id_vendaLabel1.Size = new System.Drawing.Size(60, 15);
            this.id_vendaLabel1.TabIndex = 46;
            this.id_vendaLabel1.Text = "label4";
            this.id_vendaLabel1.Visible = false;
            // 
            // tb_saidaBindingSource
            // 
            this.tb_saidaBindingSource.DataSource = typeof(FerroVelhoDAO.tb_venda);
            // 
            // lb_saldo
            // 
            this.lb_saldo.AutoSize = true;
            this.lb_saldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_saldo.ForeColor = System.Drawing.Color.Green;
            this.lb_saldo.Location = new System.Drawing.Point(321, 79);
            this.lb_saldo.Name = "lb_saldo";
            this.lb_saldo.Size = new System.Drawing.Size(43, 15);
            this.lb_saldo.TabIndex = 48;
            this.lb_saldo.Text = "0,000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(304, 13);
            this.label6.TabIndex = 47;
            this.label6.Text = "Saldo disponivel em estoque referente ao produto selecionado:";
            // 
            // tb_produtosBindingSource
            // 
            this.tb_produtosBindingSource.DataSource = typeof(FerroVelhoDAO.tb_produtos);
            // 
            // tb_itemsBindingSource
            // 
            this.tb_itemsBindingSource.DataSource = typeof(FerroVelhoDAO.tb_itemv);
            // 
            // fm_outrasSaidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 103);
            this.Controls.Add(this.lb_saldo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.id_vendaLabel1);
            this.Controls.Add(this.bt_finalCompra);
            this.Controls.Add(this.txt_quant);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_desProd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_codProd);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_outrasSaidas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Outras Saidas";
            this.Load += new System.EventHandler(this.fm_outrasSaidas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tb_saidaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_produtosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_itemsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_finalCompra;
        private System.Windows.Forms.TextBox txt_quant;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_desProd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_codProd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource tb_produtosBindingSource;
        private System.Windows.Forms.BindingSource tb_saidaBindingSource;
        private System.Windows.Forms.Label id_vendaLabel1;
        private System.Windows.Forms.BindingSource tb_itemsBindingSource;
        private System.Windows.Forms.Label lb_saldo;
        private System.Windows.Forms.Label label6;
    }
}