using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsemkaEsport
{
    class Properti
    {
        public static SqlConnection conn()
        {
            return new SqlConnection("Data Source=DESKTOP-DHE9K9C;Initial Catalog=EsemkaEsport;Integrated Security=True;TrustServerCertificate=True");
        }


        public static bool validasi(Control.ControlCollection container, TextBox empty = null)
        {
            foreach (Control c in container)
            {
                if (c is TextBoxBase textBox && string.IsNullOrEmpty(textBox.Text) && textBox != empty)
                {
                    return true;
                }            
            }
            return false;
        }

        public static int userid = 1;


    }
}
