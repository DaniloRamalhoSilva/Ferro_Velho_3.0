using FerroVelhoDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerroVelho
{
    public partial class fm_recurcos : Form
    {
        public fm_recurcos()
        {
            InitializeComponent();
        }

        int guia = 0;

        public tb_caixa saldoCorrente
        {
            get
            {
                return (tb_caixa)this.tb_caixaBindingSource.Current;
            }
        }

        private void fm_recurcos_Load(object sender, EventArgs e)
        {
            bt_continuar.Visible = false;
            atualizar();
        }

        public void atualizar()
        {
            decimal saldo = calcular();
            lb_saldo.Text = saldo.ToString("N2");
        }

        public decimal calcular()
        {
            decimal totalSaida, totalEntrada, desconto, saldo, credito;            

            try { totalSaida = Convert.ToDecimal(DataContextFactory.DataContext.tb_itemc.Sum(x => x.subTot_item)); }
            catch { totalSaida = 0; };

            try { totalEntrada = Convert.ToDecimal(DataContextFactory.DataContext.tb_caixa.Sum(x => x.valor_caixa)); }
            catch { totalEntrada = 0; };

            try { desconto = Convert.ToDecimal(DataContextFactory.DataContext.tb_compra.Sum(x => x.desconto_compra)); }
            catch { desconto = 0; };

            try { credito = Convert.ToDecimal(DataContextFactory.DataContext.tb_compra.Sum(x => x.subtot_compra - x.desconto_compra - x.valor_nota)); }
            catch { credito = 0; };

            saldo = totalEntrada - totalSaida + desconto + credito;

            return saldo;
        }

        private void txt_valRecur_Leave(object sender, EventArgs e)
        {
            if (txt_valRecur.Text != "")
            {
                txt_valRecur.Text = Convert.ToDecimal(txt_valRecur.Text).ToString("N2");
            }
            else
            {
                txt_valRecur.Text = (0).ToString("N2");
            }

            if (Convert.ToDecimal(txt_valRecur.Text) != 0)
            {
                txt_valRet.Text = (0).ToString("N2");
                bt_continuar.Text = "Adicionar";
                bt_continuar.Visible = true;
                guia = 1;
                txt_desc.Text = "Adição de recurso";
                txt_desc.Focus();
            }
            else
            {
                txt_desc.Text = "";
            }           

        }

        private void txt_valRet_Leave(object sender, EventArgs e)
        {
            if (txt_valRet.Text != "")
            {
                txt_valRet.Text = Convert.ToDecimal(txt_valRet.Text).ToString("N2");
            }
            else
            {
                txt_valRet.Text = (0).ToString("N2");
            }

            if (Convert.ToDecimal(txt_valRet.Text) != 0)
            {
                txt_valRecur.Text = (0).ToString("N2");
                bt_continuar.Text = "Retirar";
                bt_continuar.Visible = true;
                guia = 2;
                txt_desc.Text = "Sangria";
                txt_desc.Focus();
            }
            else
            {
                txt_desc.Text = "";
            }

        }

        private void bt_continuar_Click(object sender, EventArgs e)
        {
            chamar();
            atualizar();
            guia = 0;
            txt_valRecur.Text = (0).ToString("N2");
            txt_valRet.Text = (0).ToString("N2");
            txt_desc.Text = "";
            bt_continuar.Visible = false;
        }

        private void chamar()
        {
            if (guia == 1)
            {
                decimal valor = Convert.ToDecimal(txt_valRecur.Text);

                if (valor != 0)
                {
                    string texto = "Valor adicionado com sucesso!";
                    apontar(valor, texto);
                }
                else
                {
                    MessageBox.Show("Compo valor é obrigatorio");
                }
            }

            if (guia == 2 )
            {
                decimal valor = Convert.ToDecimal(txt_valRet.Text);

                if (valor != 0)
                {                    
                    valor = valor * (-1);
                    string texto = "Valor retirado com sucesso!";
                    apontar(valor, texto);                    
                }
                else
                {
                    MessageBox.Show("Compo valor é obrigatorio");
                }
            }
        }

        private void apontar(decimal valor, string texto)
        {
            this.tb_caixaBindingSource.DataSource = DataContextFactory.DataContext.tb_caixa;

            this.tb_caixaBindingSource.AddNew();
            this.saldoCorrente.data_caixa = DateTime.Now;
            this.saldoCorrente.valor_caixa = valor;
            this.saldoCorrente.usuario = DataContextFactory.usu.id_usuario;
            this.saldoCorrente.desc_caixa = txt_desc.Text;
            this.tb_caixaBindingSource.EndEdit();
            DataContextFactory.DataContext.SubmitChanges();

            MessageBox.Show(texto);
        }




        private void txt_valRecur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back) || char.IsPunctuation(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == 13)
            {
                txt_desc.Focus();
            }
            e.Handled = true;
        }


        private void txt_valRet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar.Equals((char)Keys.Back) || char.IsPunctuation(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == 13)
            {
                txt_desc.Focus();
            }
            e.Handled = true;
        }

        private void txt_desc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                bt_continuar.Focus();
            }
        }

        
    }
}
