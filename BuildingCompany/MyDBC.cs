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
        private Form1 form1;
        private MySqlConnection mySqlConnection;
        private Utils u = new Utils();

        private void constructor()
        {
            try
            {
                mySqlConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=company");
                mySqlConnection.Open();
            }
            catch (MySqlException e)
            {
                u.showError("Соединение с базой данной не уставлено:\n\n" + e.Message);
                Environment.Exit(1);
            }
        }
        public MyDBC()
        {
            constructor();
        }

        public MyDBC(Form1 form1)
        {
            constructor();
            this.form1 = form1;
        }



        public string executeQuery(string query, bool updateTable = true)
        {
            object result = null;
            try
            {
                //MessageBox.Show(query);
                MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);

                result = mySqlCommand.ExecuteScalar();
                if (updateTable)
                    form1.showTables();
            }
            catch (MySqlException e)
            {
                u.showError(e.Message);
            }
            return (result != null) ? result.ToString() : null;
        }

        public MySqlDataAdapter executeAdapterQuery(string query)
        {
            return new MySqlDataAdapter(query, mySqlConnection);
        }

        // Добавление в таблицу по названию таблицы, колонкам и значениям
        public void addToTable(string table, string column, string value)
        {
            executeQuery($"INSERT INTO {table} ({column}) VALUES ({value})");
        }

        // Удаление из таблицы по названию таблицы и значению
        public void deleteFromTable(String table, String where)
        {
            executeQuery($"DELETE FROM {table} WHERE {where}");
        }

        // Обновление значений таблицы
        public void updateTable(string table, string set, string where)
        {
            executeQuery($"UPDATE {table} SET {set} WHERE {where}");
        }

        public string selectFromTable(string column, string table, string where)
        {
            return executeQuery($"SELECT {column} FROM {table} WHERE {where}", false);
        }


        ~MyDBC()
        {
            mySqlConnection.Close();
        }
    }
}
