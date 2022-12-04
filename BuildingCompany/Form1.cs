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
        private const String CONTRACT = "company.contract";
        private const String CUSTOMER = "company.customer";
        private const String OBJECT = "company.object";
        private const String EMPLOYEE = "company.employee";
        private const String BRIGADE = "company.brigade";
        private const String POSITION = "company.position";

        private Utils u;
        private MyDBC dBC;

        public Form1()
        {
            InitializeComponent();
            dBC = new MyDBC(this);
            u = new Utils(dBC);

            showTables();
        }

        // Отобразить все таблицы
        public void showTables()
        {
            showTable(CONTRACT, dataGridContract);
            showComboBox(OBJECT, contractObjectComboBox);
            showComboBox(CUSTOMER, contractCustomerComboBox);
            showComboBox(EMPLOYEE, contractSellerComboBox, "name", "id", $"WHERE positionId={dBC.selectFromTable("id", POSITION, "name='Менеджер по продажам'")}");
            showComboBox(CONTRACT, contractNameComboBox);
            
            showTable(CUSTOMER, dataGridCustomer);
            showComboBox(CUSTOMER, customerComboBox);
            
            showTable(OBJECT, dataGridObject);
            showComboBox(BRIGADE, objectBrigadeIdComboBox);
            showComboBox(OBJECT, objectNameComboBox);
            
            showTable(EMPLOYEE, dataGridEmployee);
            showComboBox(EMPLOYEE, employeeNameComboBox);
            showComboBox(BRIGADE, employeeBridageIdComboBox);
            showComboBox(POSITION, employeePositionIdComboBox);

            showTable(BRIGADE, dataGridBrigade);
            showComboBox(BRIGADE, bridageComboBox);
            showComboBox(EMPLOYEE, foremanNameComboBox, "name", "id", $"WHERE positionId={dBC.selectFromTable("id", POSITION, "name='Бригадир'")}");
            
            showTable(POSITION, dataGridPosition);
            showComboBox(POSITION, positionComboBox);

            showMainTable();
        }

        // Отобразить таблицу
        public void showTable(String table, DataGridView dataGridView)
        {
            var dataAdapter = dBC.executeAdapterQuery($"SELECT * FROM {table}");

            u.showDataGrid(dataGridView, dataAdapter);
        }

        // Отобразить главную таблицу 

        public void showMainTable()
        {
            var dataAdapter = dBC.executeAdapterQuery("SELECT contract.id AS 'Договор №', object.name AS 'Название'," +
                "object.address AS 'Адрес', customer.name AS 'Покупатель', customer.phone AS 'Телефон покупателя', " +
                "price AS 'Сумма сделки', contract.date AS 'Дата сделки'" +
                "FROM company.contract " +
                "INNER JOIN company.object on objectId = object.id " +
                "INNER JOIN company.customer on customerId = customer.id;");

            u.showDataGrid(mainDataGridView, dataAdapter);
        }

        // Отображение ComboBox
        private void showComboBox(String table, ComboBox comboBox, String displayMember = "name", String valueMember = "id", String where = "")
        {
            var dataAdapter = dBC.executeAdapterQuery($"SELECT * FROM {table} {where}");

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
        }

        // Добавление должности
        private void addPosition_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(positionName) || u.checkTextForNull(positionSalary) || !u.checkForParseText(positionSalary.Text)) return;

            String name = u.wISQ(positionName.Text.Trim(' '));
            uint salary = uint.Parse(positionSalary.Text.Trim(' '));

            dBC.addToTable(POSITION, "name, salary", $"{name}, {salary}");
        }
        
        // Кнопка удаления Должности
        private void button2_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(positionComboBox)) return;

            dBC.deleteFromTable(POSITION, u.createWhere(positionComboBox));
        }

        // Кнопка изменения должности
        private void button1_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(positionComboBox)) return;
            if (u.checkTextForNull(positionName) || u.checkTextForNull(positionSalary)) return;

            String name = positionName.Text.Trim(' ');
            uint salary = uint.Parse(positionSalary.Text.Trim(' '));

            dBC.updateTable(POSITION, $"name={name}, salary={salary}", u.createWhere(positionComboBox));
        }

        // Добавление Заказчика
        private void addCustomer_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(customersNameTextBox) || u.checkTextForNull(customersPhoneTextBox) || u.checkTextForNull(customersEmailTextBox)) return;

            String name = customersNameTextBox.Text.Trim(' ');
            String phone = customersPhoneTextBox.Text.Trim(' ');
            String email = customersEmailTextBox.Text.Trim(' ');

            dBC.addToTable(CUSTOMER, "name, phone, email", $"{u.wISQ(name)}, {u.wISQ(phone)}, {u.wISQ(name)}");
        }

        // Изменить Заказчика
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (u.checkTextForNull(customerComboBox)) return;
            if (u.checkTextForNull(customersNameTextBox) || u.checkTextForNull(customersPhoneTextBox) || u.checkTextForNull(customersEmailTextBox)) return;

            String name = u.wISQ(customersNameTextBox.Text.Trim(' '));
            String phone = u.wISQ(customersPhoneTextBox.Text.Trim(' '));
            String email = u.wISQ(customersEmailTextBox.Text.Trim(' '));

            dBC.updateTable(CUSTOMER, $"name={name}, phone={phone}, email={email}", u.createWhere(customerComboBox));
        }

        // Удаления Заказчика
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (u.checkTextForNull(customerComboBox)) return;

            dBC.deleteFromTable(CUSTOMER, u.createWhere(customerComboBox));
        }

        // Добавление бригады
        private void addBrigadeButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(brigadeNameTextBox)) return;

            String column = "name";
            String value = $"{u.wISQ(brigadeNameTextBox.Text.Trim(' '))}";

            var tmp = u.getStringForSet(foremanNameComboBox, EMPLOYEE, null, "Такого Бригадира не существует!");
            if (tmp != null)
                column += ", foremanId";
            value += tmp;

            dBC.addToTable(BRIGADE, column, value);
        }

        // Изменение Бригады
        private void changeBrigadeButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(bridageComboBox) || u.checkTextForNull(brigadeNameTextBox)) return;

            String set = $"name={u.wISQ(brigadeNameTextBox.Text.Trim(' '))}";

            set += u.getStringForSet(foremanNameComboBox, EMPLOYEE, "foremanId=", "Такого Бригадира не существует");

            dBC.updateTable(BRIGADE, set, u.createWhere(bridageComboBox));
        }

        private void deleteBrigadeButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(bridageComboBox)) return;

            dBC.deleteFromTable(BRIGADE, u.createWhere(bridageComboBox));
        }

        // Добавление Рабочего
        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(employeeNameTextBox) || u.checkTextForNull(employeePhoneTextBox)) return;

            String column = "name, phone";
            String value = $"{u.wISQ(employeeNameTextBox.Text.Trim(' '))}, {u.wISQ(employeePhoneTextBox.Text.Trim(' '))}";

            var tmp = u.getStringForSet(employeeBridageIdComboBox, BRIGADE, null, "Такой Бригады не существует");
            if (tmp != null)
                column += ", brigadeId";
            value += tmp;

            tmp = u.getStringForSet(employeePositionIdComboBox, POSITION, null, "Такой Должности не существует");
            if (tmp != null)
                column += ", positionId";
            value += tmp;

            dBC.addToTable(EMPLOYEE, column, value);
        }

        // Изменение рабочего
        private void changeEmployeeButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(employeeNameTextBox) || u.checkTextForNull(employeePhoneTextBox)) return;
            if (u.checkTextForNull(employeeNameComboBox)) return;

            String set = $"name={u.wISQ(employeeNameTextBox.Text.Trim(' '))}, phone={u.wISQ(employeePhoneTextBox.Text.Trim(' '))}";

            set += u.getStringForSet(employeeBridageIdComboBox, BRIGADE, "brigadeId=", "Такой Бригады не существует");
            set += u.getStringForSet(employeePositionIdComboBox, POSITION, "positionId=", "Такой Должности не существует");

            dBC.updateTable(EMPLOYEE, set, u.createWhere(employeeNameComboBox));
        }

        // Удаление рабочего
        private void deleteEmployeeButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(employeeNameComboBox)) return;

            dBC.deleteFromTable(EMPLOYEE, u.createWhere(employeeNameComboBox));
        }

        // Создание Объекта
        private void addObjectButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(objectNameTextBox) || u.checkTextForNull(objectAddressTextBox)) return;

            String column = "name, address, startDate, endDate";
            String value = $"{u.wISQ(objectNameTextBox.Text.Trim(' '))}, {u.wISQ(objectAddressTextBox.Text.Trim(' '))}" +
                $", {u.wISQ(objectStartDateTimePicker.Value.ToString("yyyy-MM-dd"))}, {u.wISQ(objectEndDateTimePicker.Value.ToString("yyyy-MM-dd"))}";

            var tmp = u.getStringForSet(objectBrigadeIdComboBox, BRIGADE, null, "Такой Бригады не существует");
            if (tmp != null)
                column += ", brigadeId";
            value += tmp;

            dBC.addToTable(OBJECT, column, value);
        }

        // Изменение объекта
        private void updateObjectButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(objectNameTextBox) || u.checkTextForNull(objectAddressTextBox)) return;
            if (u.checkTextForNull(objectNameComboBox)) return;

            String set = $"name={u.wISQ(objectNameTextBox.Text.Trim(' '))}, address={u.wISQ(objectAddressTextBox.Text.Trim(' '))}" +
                $", startDate={u.wISQ(objectStartDateTimePicker.Value.ToString("yyyy-MM-dd"))}, endDate={u.wISQ(objectEndDateTimePicker.Value.ToString("yyyy-MM-dd"))}";

            set += u.getStringForSet(objectBrigadeIdComboBox, BRIGADE, "brigadeId=", "Такой Бригады не существует");

            dBC.updateTable(OBJECT, set, u.createWhere(objectNameComboBox));
        }

        private void deleteObjectButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(objectNameComboBox)) return;

            dBC.deleteFromTable(OBJECT, u.createWhere(objectNameComboBox));
        }

        private void addContractButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(contractObjectComboBox) || u.checkTextForNull(contractCustomerComboBox)
                || u.checkTextForNull(contractSellerComboBox) || u.checkTextForNull(contractAmountTextBox)) return;


            String column = "date, price";
            String value = $"{u.wISQ(contractDateTimePicker.Value.ToString("yyyy-MM-dd"))}, {u.wISQ(contractAmountTextBox.Text.Trim(' '))}";

            var tmp = u.getStringForSet(contractObjectComboBox, OBJECT, null, "Такого Объекта не существует");
            column += (tmp != null) ? ", objectId" : null;
            value += tmp;

            tmp = u.getStringForSet(contractCustomerComboBox, CUSTOMER, null, "Такого Покупателя не существует");
            column += (tmp != null) ? ", customerId" : null;
            value += tmp;

            tmp = u.getStringForSet(contractSellerComboBox, EMPLOYEE, null, "Такого Продавца не существует");
            column += (tmp != null) ? ", salesmanId" : null;
            value += tmp;

            dBC.addToTable(CONTRACT, column, value);
        }

        private void changeContractButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(contractObjectComboBox) || u.checkTextForNull(contractCustomerComboBox)
                || u.checkTextForNull(contractSellerComboBox) || u.checkTextForNull(contractAmountTextBox)
                || !u.checkForParseText(contractAmountTextBox.Text)) return;


            String set = $"date={u.wISQ(contractDateTimePicker.Value.ToString("yyyy-MM-dd"))}";
            set += $", price={uint.Parse(contractAmountTextBox.Text)}";

            set += u.getStringForSet(contractObjectComboBox, OBJECT, "objectId=", "Такого Объекта не существует");
            set += u.getStringForSet(contractCustomerComboBox, CUSTOMER, "customerId=", "Такого Покупателя не существует");
            set += u.getStringForSet(contractSellerComboBox, EMPLOYEE, "salesmanId=", "Такого Продавца не существует");

            dBC.updateTable(CONTRACT, set, u.createWhere(contractNameComboBox));
        }

        private void deleteContractButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(contractNameComboBox)) return;

            dBC.deleteFromTable(CONTRACT, u.createWhere(contractNameComboBox));
        }
    }
}
