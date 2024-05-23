namespace FerroVelho
{
    partial class fm_cabecario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_cabecario));
            this.bt_alterar = new System.Windows.Forms.Button();
            this.bt_salvar = new System.Windows.Forms.Button();
            this.txt_endereco = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_telefone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_empressa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bt_alterar
            // 
            this.bt_alterar.Location = new System.Drawing.Point(185, 174);
            this.bt_alterar.Name = "bt_alterar";
            this.bt_alterar.Size = new System.Drawing.Size(75, 23);
            this.bt_alterar.TabIndex = 15;
            this.bt_alterar.Text = "Alterar";
            this.bt_alterar.UseVisualStyleBackColor = true;
            this.bt_alterar.Click += new System.EventHandler(this.bt_alterar_Click);
            // 
            // bt_salvar
            // 
            this.bt_salvar.Location = new System.Drawing.Point(185, 174);
            this.bt_salvar.Name = "bt_salvar";
            this.bt_salvar.Size = new System.Drawing.Size(75, 23);
            this.bt_salvar.TabIndex = 14;
            this.bt_salvar.Text = "Salva";
            this.bt_salvar.UseVisualStyleBackColor = true;
            this.bt_salvar.Click += new System.EventHandler(this.bt_salvar_Click);
            // 
            // txt_endereco
            // 
            this.txt_endereco.Enabled = false;
            this.txt_endereco.Location = new System.Drawing.Point(15, 130);
            this.txt_endereco.Name = "txt_endereco";
            this.txt_endereco.Size = new System.Drawing.Size(245, 20);
            this.txt_endereco.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Endereço:";
            // 
            // txt_telefone
            // 
            this.txt_telefone.Enabled = false;
            this.txt_telefone.Location = new System.Drawing.Point(15, 77);
            this.txt_telefone.Name = "txt_telefone";
            this.txt_telefone.Size = new System.Drawing.Size(245, 20);
            this.txt_telefone.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Telefone:";
            // 
            // txt_empressa
            // 
            this.txt_empressa.Enabled = false;
            this.txt_empressa.Location = new System.Drawing.Point(15, 25);
            this.txt_empressa.Name = "txt_empressa";
            this.txt_empressa.Size = new System.Drawing.Size(245, 20);
            this.txt_empressa.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Empressa:";
            // 
            // fm_cabecario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 203);
            this.Controls.Add(this.bt_alterar);
            this.Controls.Add(this.bt_salvar);
            this.Controls.Add(this.txt_endereco);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_telefone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_empressa);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_cabecario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cabeçalho";
            this.Load += new System.EventHandler(this.fm_cabecario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_alterar;
        private System.Windows.Forms.Button bt_salvar;
        private System.Windows.Forms.TextBox txt_endereco;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_telefone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_empressa;
        private System.Windows.Forms.Label label1;
    }
}