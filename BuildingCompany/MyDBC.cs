using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildingCompany
{
    class MyDBC
    {
        private MySqlConnection mySqlConnection;
        private Utils u = new Utils();

        public MyDBC()
        {
            try
            {
                mySqlConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=company");
                mySqlConnection.Open();
            }
            catch (MySqlException e)
            {
                u.showError(e.Message);
                Environment.Exit(1);
            }
        }



        ~MyDBC()
        {
            mySqlConnection.Close();
        }
    }
}
