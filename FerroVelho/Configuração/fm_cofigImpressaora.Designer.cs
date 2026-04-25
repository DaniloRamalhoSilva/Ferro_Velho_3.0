namespace FerroVelho
{
    partial class fm_cofigImpressaora
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_cofigImpressaora));
            this.cb_ImpCupomFiscal = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_ImpRelatorio = new System.Windows.Forms.ComboBox();
            this.btn_impCupomFiscal = new System.Windows.Forms.Button();
            this.btn_impRelatorio = new System.Windows.Forms.Button();
            this.lb_impRelatorio = new System.Windows.Forms.Label();
            this.lb_impCupomFiscal = new System.Windows.Forms.Label();
            this.tb_impressoraBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tb_impressoraBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_ImpCupomFiscal
            // 
            this.cb_ImpCupomFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_ImpCupomFiscal.FormattingEnabled = true;
            this.cb_ImpCupomFiscal.Location = new System.Drawing.Point(12, 45);
            this.cb_ImpCupomFiscal.Name = "cb_ImpCupomFiscal";
            this.cb_ImpCupomFiscal.Size = new System.Drawing.Size(484, 28);
            this.cb_ImpCupomFiscal.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Impressão do cupom fiscal:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Impressão de relatorios:";
            // 
            // cb_ImpRelatorio
            // 
            this.cb_ImpRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_ImpRelatorio.FormattingEnabled = true;
            this.cb_ImpRelatorio.Location = new System.Drawing.Point(12, 137);
            this.cb_ImpRelatorio.Name = "cb_ImpRelatorio";
            this.cb_ImpRelatorio.Size = new System.Drawing.Size(484, 28);
            this.cb_ImpRelatorio.TabIndex = 2;
            // 
            // btn_impCupomFiscal
            // 
            this.btn_impCupomFiscal.Location = new System.Drawing.Point(502, 45);
            this.btn_impCupomFiscal.Name = "btn_impCupomFiscal";
            this.btn_impCupomFiscal.Size = new System.Drawing.Size(119, 28);
            this.btn_impCupomFiscal.TabIndex = 4;
            this.btn_impCupomFiscal.Text = "Alterar";
            this.btn_impCupomFiscal.UseVisualStyleBackColor = true;
            this.btn_impCupomFiscal.Click += new System.EventHandler(this.btn_impCupomFiscal_Click);
            // 
            // btn_impRelatorio
            // 
            this.btn_impRelatorio.Location = new System.Drawing.Point(502, 137);
            this.btn_impRelatorio.Name = "btn_impRelatorio";
            this.btn_impRelatorio.Size = new System.Drawing.Size(119, 28);
            this.btn_impRelatorio.TabIndex = 5;
            this.btn_impRelatorio.Text = "Alterar";
            this.btn_impRelatorio.UseVisualStyleBackColor = true;
            this.btn_impRelatorio.Click += new System.EventHandler(this.btn_impRelatorio_Click);
            // 
            // lb_impRelatorio
            // 
            this.lb_impRelatorio.AutoSize = true;
            this.lb_impRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_impRelatorio.Location = new System.Drawing.Point(197, 118);
            this.lb_impRelatorio.Name = "lb_impRelatorio";
            this.lb_impRelatorio.Size = new System.Drawing.Size(66, 16);
            this.lb_impRelatorio.TabIndex = 7;
            this.lb_impRelatorio.Text = "Nenhuma";
            // 
            // lb_impCupomFiscal
            // 
            this.lb_impCupomFiscal.AutoSize = true;
            this.lb_impCupomFiscal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_impCupomFiscal.Location = new System.Drawing.Point(220, 26);
            this.lb_impCupomFiscal.Name = "lb_impCupomFiscal";
            this.lb_impCupomFiscal.Size = new System.Drawing.Size(66, 16);
            this.lb_impCupomFiscal.TabIndex = 6;
            this.lb_impCupomFiscal.Text = "Nenhuma";
            // 
            // tb_impressoraBindingSource
            // 
            this.tb_impressoraBindingSource.DataSource = typeof(FerroVelhoDAO.tb_impressora);
            // 
            // fm_cofigImpressaora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 195);
            this.Controls.Add(this.lb_impRelatorio);
            this.Controls.Add(this.lb_impCupomFiscal);
            this.Controls.Add(this.btn_impRelatorio);
            this.Controls.Add(this.btn_impCupomFiscal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_ImpRelatorio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_ImpCupomFiscal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_cofigImpressaora";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração de Impressoras";
            this.Load += new System.EventHandler(this.fm_cofigImpressaora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tb_impressoraBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_ImpCupomFiscal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_ImpRelatorio;
        private System.Windows.Forms.Button btn_impCupomFiscal;
        private System.Windows.Forms.Button btn_impRelatorio;
        private System.Windows.Forms.Label lb_impRelatorio;
        private System.Windows.Forms.Label lb_impCupomFiscal;
        private System.Windows.Forms.BindingSource tb_impressoraBindingSource;
    }
}