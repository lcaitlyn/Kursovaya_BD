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
        private MyDBC dBC;

        public Utils() { }

        public Utils(MyDBC dBC)
        {
            this.dBC = dBC;
        }

        // Функция заворачивает String в 'String' (wrapInSingleQuote)
        public String wISQ(String str)
        {
            return $"'{str}'";
        }

        // Функция создает Строку where
        public String createWhere(ComboBox comboBox)
        {
            if (comboBox.SelectedValue != null)
                return $"id={comboBox.SelectedValue}";
            else
                return $"name='{comboBox.Text.Trim(' ')}'";
        }

        // Проверка на пустоту TextBox.Text
        public bool checkTextForNull(TextBox textBox)
        {
            if (String.IsNullOrEmpty(textBox.Text.Trim(' '))) showError("Заполните " + textBox.Name);
            
            return String.IsNullOrEmpty(textBox.Text.Trim(' '));
        }

        // Проверка на пустоту ComboBox.Text
        public bool checkTextForNull(ComboBox comboBox)
        {
            if (String.IsNullOrEmpty(comboBox.Text.Trim(' '))) showError("Заполните " + comboBox.Name);
            
            return String.IsNullOrEmpty(comboBox.Text.Trim(' '));
        }

        public bool checkTextForNull(string what, string errorMessage)
        {
            if (what == null)
                showError(errorMessage);

            return what == null;
        }

        // Проверяет строку на валидность
        public bool checkForParseText(string text)
        {
            try
            {
                uint.Parse(text.Trim(' '));
                return true;
            }
            catch (Exception)
            {
                showError("Попробуйте другое число!");
                return false;
            }
        }

        // Вывести ошибку
        public void showError(String message)
        {
            MessageBox.Show(message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public string getStringForSet(ComboBox comboBox, string table, string set, string errorMessage)
        {
            string id = null;
            if (comboBox != null && !String.IsNullOrEmpty(comboBox.Text))
            {
                id = dBC.selectFromTable("id", table, $"name={wISQ(comboBox.Text.Trim(' '))}");

                checkTextForNull(id, errorMessage);
            }
            return (id != null) ? $", {set}{id}" : null;
        }
    }
}
