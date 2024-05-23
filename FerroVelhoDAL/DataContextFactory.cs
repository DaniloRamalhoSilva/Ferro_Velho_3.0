using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerroVelhoDAL
{
    public class DataContextFactory
    {
        private static DataClasses1DataContext dataContext;
        DataClasses1DataContext DataContext
        {
            get
            {
                if (dataContext == null)
                    dataContext = new DataClasses1DataContext();
                return dataContext;
            }
        }
        
    }
}
