using FerroVelhoDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerroVelho
{    
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        
        [STAThread]        
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            fm_login fm = new fm_login();
            fm.ShowDialog();            

            if (fm.logar == true)
            {
               Application.Run(new fm_menulPrincipal());
            }
        }
    }
}
