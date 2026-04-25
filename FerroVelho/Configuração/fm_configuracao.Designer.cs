namespace FerroVelho
{
    partial class fm_configuracao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_configuracao));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_datauImpressao = new System.Windows.Forms.TextBox();
            this.bt_alterar = new System.Windows.Forms.Button();
            this.txt_datauser = new System.Windows.Forms.TextBox();
            this.lb_conexao = new System.Windows.Forms.Label();
            this.bt_salvar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "String de conexão impressão:";
            // 
            // txt_datauImpressao
            // 
            this.txt_datauImpressao.Location = new System.Drawing.Point(11, 64);
            this.txt_datauImpressao.Name = "txt_datauImpressao";
            this.txt_datauImpressao.Size = new System.Drawing.Size(501, 20);
            this.txt_datauImpressao.TabIndex = 16;
            // 
            // bt_alterar
            // 
            this.bt_alterar.Location = new System.Drawing.Point(436, 90);
            this.bt_alterar.Name = "bt_alterar";
            this.bt_alterar.Size = new System.Drawing.Size(75, 23);
            this.bt_alterar.TabIndex = 15;
            this.bt_alterar.Text = "Alterar";
            this.bt_alterar.UseVisualStyleBackColor = true;
            this.bt_alterar.Click += new System.EventHandler(this.bt_alterar_Click);
            // 
            // txt_datauser
            // 
            this.txt_datauser.Location = new System.Drawing.Point(11, 25);
            this.txt_datauser.Name = "txt_datauser";
            this.txt_datauser.Size = new System.Drawing.Size(501, 20);
            this.txt_datauser.TabIndex = 14;
            // 
            // lb_conexao
            // 
            this.lb_conexao.AutoSize = true;
            this.lb_conexao.Location = new System.Drawing.Point(12, 9);
            this.lb_conexao.Name = "lb_conexao";
            this.lb_conexao.Size = new System.Drawing.Size(119, 13);
            this.lb_conexao.TabIndex = 13;
            this.lb_conexao.Text = "String de conexão user:";
            // 
            // bt_salvar
            // 
            this.bt_salvar.Location = new System.Drawing.Point(436, 90);
            this.bt_salvar.Name = "bt_salvar";
            this.bt_salvar.Size = new System.Drawing.Size(75, 23);
            this.bt_salvar.TabIndex = 12;
            this.bt_salvar.Text = "Salvar";
            this.bt_salvar.UseVisualStyleBackColor = true;
            this.bt_salvar.Click += new System.EventHandler(this.bt_salvar_Click);
            // 
            // fm_configuracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 123);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_datauImpressao);
            this.Controls.Add(this.bt_alterar);
            this.Controls.Add(this.txt_datauser);
            this.Controls.Add(this.lb_conexao);
            this.Controls.Add(this.bt_salvar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_configuracao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração";
            this.Load += new System.EventHandler(this.fm_configuracao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_datauImpressao;
        private System.Windows.Forms.Button bt_alterar;
        private System.Windows.Forms.TextBox txt_datauser;
        private System.Windows.Forms.Label lb_conexao;
        private System.Windows.Forms.Button bt_salvar;
    }
}