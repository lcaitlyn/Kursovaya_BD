using MySql.Data.MySqlClient;
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

namespace BuildingCompany
{
    public partial class Form1 : Form
    {
        // названия таблиц
        private const String CONTRACT = "contract";
        private const String CUSTOMER = "customer";
        private const String OBJECT = "object";
        private const String EMPLOYEE = "employee";
        private const String BRIGADE = "brigade";
        private const String POSITION = "position";

        // Соединение с MySql db
        MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=company");
        

        // Инициализация
        public Form1()
        {
            InitializeComponent();
            mySqlConnection.Open();

            showTables();
        }

        // Деструктор
        ~Form1()
        {
            mySqlConnection.Close();
        }

        // Отобразить все таблицы
        public void showTables()
        {
            showTable(dataGridContract, CONTRACT);
            showTable(dataGridCustomer, CUSTOMER);
            showComboBox(CUSTOMER, customerComboBox, "name", "id");
            showComboBox(CUSTOMER, customersIdComboBox, "id", "id");
            showTable(dataGridObject, OBJECT);
            showTable(dataGridEmployee, EMPLOYEE);
            showTable(dataGridBrigade, BRIGADE);
            showTable(dataGridPosition, POSITION);
            showComboBox(POSITION, positionComboBox, "name", "id");
        }

        // Отобразить таблицу
        public void showTable(DataGridView dataGridView, String name)
        {
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter("select * from company." + name, mySqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView.DataSource = dataSet.Tables[0];
        }

        // Отображение ComboBox
        private void showComboBox(String name, ComboBox comboBox, String displayMember, String valueMember)
        {
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter("select * from company." + name, mySqlConnection);

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
        }

        // Добавление в таблицу по названию таблицы, колонкам и значениям
        private void addToTable(string table, string column, string value)
        {
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand(
                    "insert into company." + table + " (" + column + ") values (" + value + ")", mySqlConnection);

                mySqlCommand.ExecuteNonQuery();
            }
            finally
            {
                showTables();
            }
        }

        // Функция заворачивает String в 'String' (wrapInSingleQuote)
        private String wISQ(String str)
        {
            return "'" + str + "'";
        }

        // Функция создает Строку where
        private String createWhere(ComboBox comboBox)
        {
            String where;
            if (comboBox.SelectedValue != null)
                where = "id=" + comboBox.SelectedValue.ToString();
            else
                where = "name='" + comboBox.Text.Trim(' ') + "'";

            return where;
        }

        // Проверка на пустоту TextBox.Text
        private bool checkTextBoxForNull(TextBox textBox)
        {
            if (String.IsNullOrEmpty(textBox.Text.Trim(' ')))
            {
                showError("Заполните " + textBox.Name);
                return false;
            }
            return true;
        }

        // Проверка на пустоту ComboBox.Text
        private bool checkTextBoxForNull(ComboBox comboBox)
        {
            if (String.IsNullOrEmpty(comboBox.Text.Trim(' ')))
            {
                showError("Заполните " + comboBox.Name);
                return false;
            }
            return true;
        }

        // Вывести ошибку
        private void showError(String message)
        {
            MessageBox.Show(message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Удаление из таблицы по названию таблицы и аргументу
        private void deleteFromTable(String table, String value)
        {
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand(
                    "delete from company." + table + " where " + value, mySqlConnection);

                mySqlCommand.ExecuteNonQuery();
            }
            finally
            {
                showTables();
            }
        }

        // Обновление значений таблицы
        private void updateTable(string table, string column, string value)
        {
            try
            {
                MessageBox.Show("update " + table + " set " + column + " where " + value);
                MySqlCommand mySqlCommand = new MySqlCommand(
                    "update company." + table + " set " + column + " where " + value, mySqlConnection);

                mySqlCommand.ExecuteNonQuery();
            }
            finally
            {
                showTables();
            }
        }


        // Добавление должности
        private void addPosition_Click(object sender, EventArgs e)
        {
            if (!checkTextBoxForNull(positionName) || !checkTextBoxForNull(positionSalary)) return;

            try
            {
                String name = positionName.Text.Trim(' ');
                uint salary = uint.Parse(positionSalary.Text.Trim(' '));

                addToTable(POSITION, "name, salary", wISQ(name) + ", " + salary);

                showTables();
            }
            catch (FormatException)
            {
                showError("Попробуйте другое число!");
            }
            catch (MySqlException ex)
            {
                showError(ex.Message);
            }
        }

        // Кнопка удаления Должности
        private void button2_Click(object sender, EventArgs e)
        {
            if (!checkTextBoxForNull(positionComboBox)) return;

            deleteFromTable(POSITION, createWhere(positionComboBox));
        }

        // Кнопка изменения должности
        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkTextBoxForNull(positionComboBox)) return;
            if (!checkTextBoxForNull(positionName) || !checkTextBoxForNull(positionSalary)) return;

            try
            {
                String name = positionName.Text.Trim(' ');
                uint salary = uint.Parse(positionSalary.Text.Trim(' '));

                updateTable(POSITION, String.Format("name='{0}', salary={1}", name, salary), createWhere(positionComboBox));

                showTables();
            }
            catch (FormatException)
            {
                showError("Попробуйте другое число!");
            }
            catch (MySqlException ex)
            {
                showError(ex.Message);
            }
        }

        // Добавление Заказчика
        private void addCustomer_Click(object sender, EventArgs e)
        {
            if (!checkTextBoxForNull(customersNameTextBox) || !checkTextBoxForNull(customersPhoneTextBox) || !checkTextBoxForNull(customersEmailTextBox)) return;

            try
            {
                String name = customersNameTextBox.Text.Trim(' ');
                String phone = customersPhoneTextBox.Text.Trim(' ');
                String email = customersEmailTextBox.Text.Trim(' ');

                addToTable(CUSTOMER, "name, phone, email", wISQ(name) + ", " + wISQ(phone) + ", " + wISQ(email));

                showTables();
            }
            catch (MySqlException ex)
            {
                showError(ex.Message);
            }
        }

        // Удаления Заказчика
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (!checkTextBoxForNull(customerComboBox)) return;

            deleteFromTable("position", createWhere(customerComboBox));
        }

        // Изменения Заказчика
        private void button1_Click_1(object sender, EventArgs e)
        {/*
            if (!checkTextBoxForNull(positionComboBox)) return;
            if (!checkTextBoxForNull(positionName) || !checkTextBoxForNull(positionSalary)) return;

            try
            {
                String name = positionName.Text.Trim(' ');
                uint salary = uint.Parse(positionSalary.Text.Trim(' '));

                updateTable("position", String.Format("name='{0}', salary={1}", name, salary), where);

                showTables();
            }
            catch (FormatException)
            {
                showError("Попробуйте другое число!");
            }
            catch (MySqlException ex)
            {
                showError(ex.Message);
            }*/
        }
    }
}
