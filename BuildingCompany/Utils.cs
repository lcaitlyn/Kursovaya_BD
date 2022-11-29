using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildingCompany
{
    class Utils
    {
        // Функция заворачивает String в 'String' (wrapInSingleQuote)
        public String wISQ(String str)
        {
            return "'" + str + "'";
        }

        // Функция создает Строку where
        public String createWhere(ComboBox comboBox)
        {
            String where;
            if (comboBox.SelectedValue != null)
                where = "id=" + comboBox.SelectedValue.ToString();
            else
                where = "name='" + comboBox.Text.Trim(' ') + "'";

            return where;
        }

        // Проверка на пустоту TextBox.Text
        public bool checkTextBoxForNull(TextBox textBox)
        {
            if (String.IsNullOrEmpty(textBox.Text.Trim(' ')))
            {
                showError("Заполните " + textBox.Name);
                return false;
            }
            return true;
        }

        // Проверка на пустоту ComboBox.Text
        public bool checkTextBoxForNull(ComboBox comboBox)
        {
            if (String.IsNullOrEmpty(comboBox.Text.Trim(' ')))
            {
                showError("Заполните " + comboBox.Name);
                return false;
            }
            return true;
        }

        // Вывести ошибку
        public void showError(String message)
        {
            MessageBox.Show(message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
