using System;
using System.Data;
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

        // Отобразить все таблицы (DataGrid)
        public void showTables()
        {   // главная
            showMainTable();
            showComboBox(OBJECT, mainObjectNameComboBox);
            showComboBox(CUSTOMER, mainCustomerNameComboBox);
            // Договора
            showTable(CONTRACT, dataGridContract);
            showComboBox(OBJECT, contractObjectComboBox);
            showComboBox(CUSTOMER, contractCustomerComboBox);
            showComboBox(EMPLOYEE, contractSellerComboBox, "name", "id",
                $"WHERE positionId={dBC.selectFromTable("id", POSITION, "name='Менеджер по продажам'")}");
            showComboBox(CONTRACT, contractNameComboBox);
            // Заказчики
            showTable(CUSTOMER, dataGridCustomer);
            showComboBox(CUSTOMER, customerComboBox);
            // Объекты
            showTable(OBJECT, dataGridObject);
            showComboBox(BRIGADE, objectBrigadeIdComboBox);
            showComboBox(OBJECT, objectNameComboBox);
            // Работники
            showTable(EMPLOYEE, dataGridEmployee);
            showComboBox(EMPLOYEE, employeeNameComboBox);
            showComboBox(BRIGADE, employeeBridageIdComboBox);
            showComboBox(POSITION, employeePositionIdComboBox);
            // Бригады
            showTable(BRIGADE, dataGridBrigade);
            showComboBox(BRIGADE, bridageComboBox);
            showComboBox(EMPLOYEE, foremanNameComboBox, "name", "id",
                $"WHERE positionId={dBC.selectFromTable("id", POSITION, "name='Бригадир'")}");
            // Должности
            showTable(POSITION, dataGridPosition);
            showComboBox(POSITION, positionComboBox);
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

        // Изменение Объекта
        private void updateObjectButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(objectNameTextBox) || u.checkTextForNull(objectAddressTextBox)) return;
            if (u.checkTextForNull(objectNameComboBox)) return;

            String set = $"name={u.wISQ(objectNameTextBox.Text.Trim(' '))}, address={u.wISQ(objectAddressTextBox.Text.Trim(' '))}" +
                $", startDate={u.wISQ(objectStartDateTimePicker.Value.ToString("yyyy-MM-dd"))}, endDate={u.wISQ(objectEndDateTimePicker.Value.ToString("yyyy-MM-dd"))}";

            set += u.getStringForSet(objectBrigadeIdComboBox, BRIGADE, "brigadeId=", "Такой Бригады не существует");

            dBC.updateTable(OBJECT, set, u.createWhere(objectNameComboBox));
        }

        // Кнопка удаления Объекта
        private void deleteObjectButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(objectNameComboBox)) return;

            dBC.deleteFromTable(OBJECT, u.createWhere(objectNameComboBox));
        }

        // Кнопка добавления Контракта
        private void addContractButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(contractObjectComboBox) || u.checkTextForNull(contractCustomerComboBox)
                || u.checkTextForNull(contractSellerComboBox) || u.checkTextForNull(contractAmountTextBox)) return;
            if (!u.checkForParseText(contractAmountTextBox.Text)) return;


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

        // Кнопка изменения Контракта
        private void changeContractButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(contractObjectComboBox) || u.checkTextForNull(contractCustomerComboBox)
                || u.checkTextForNull(contractSellerComboBox) || u.checkTextForNull(contractAmountTextBox)
                || !u.checkForParseText(contractAmountTextBox.Text)) return;


            String set = $"date={u.wISQ(contractDateTimePicker.Value.ToString("yyyy-MM-dd"))}, "
                + $"price={uint.Parse(contractAmountTextBox.Text)}";

            set += u.getStringForSet(contractObjectComboBox, OBJECT, "objectId=", "Такого Объекта не существует");
            set += u.getStringForSet(contractCustomerComboBox, CUSTOMER, "customerId=", "Такого Покупателя не существует");
            set += u.getStringForSet(contractSellerComboBox, EMPLOYEE, "salesmanId=", "Такого Продавца не существует");

            dBC.updateTable(CONTRACT, set, u.createWhere(contractNameComboBox));
        }

        // Кнопка удаления Контракта
        private void deleteContractButton_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(contractNameComboBox)) return;

            dBC.deleteFromTable(CONTRACT, u.createWhere(contractNameComboBox));
        }

        // Кнопка отображает Название и Адрес Объекта
        private void mainObjectNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mainObjectNameComboBox.SelectedValue.ToString().Equals("System.Data.DataRowView"))
            {
                mainObjectNameTextBox.Text = dBC.selectFromTable("name", OBJECT, $"id = {mainObjectNameComboBox.SelectedValue}");
                mainObjectAddressTextBox.Text = dBC.selectFromTable("address", OBJECT, $"id = {mainObjectNameComboBox.SelectedValue}");
            }
        }

        // Кнопка изменения объекта
        private void button1_Click_2(object sender, EventArgs e)
        {
            if (u.checkTextForNull(mainObjectNameComboBox)) return;

            mainObjectNameTextBox.Text = dBC.selectFromTable("name", OBJECT, $"name = {u.wISQ(mainObjectNameComboBox.Text)}");
            mainObjectAddressTextBox.Text = dBC.selectFromTable("address", OBJECT, $"name = {u.wISQ(mainObjectNameComboBox.Text)}");
        }

        // Кнопка отображает ФИО, Номер и Почтовый адрес покупателя
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mainCustomerNameComboBox.SelectedValue.ToString().Equals("System.Data.DataRowView"))
            {
                mainCustomerNameTextBox.Text = dBC.selectFromTable("name", CUSTOMER, $"id = {mainCustomerNameComboBox.SelectedValue}");
                mainCustomerPhoneTextBox.Text = dBC.selectFromTable("phone", CUSTOMER, $"id = {mainCustomerNameComboBox.SelectedValue}");
                mainCutomerEmailTextBox.Text = dBC.selectFromTable("email", CUSTOMER, $"id = {mainCustomerNameComboBox.SelectedValue}");
            }
        }

        // Кнопка изменения Покупателя
        private void button2_Click_2(object sender, EventArgs e)
        {
            if (u.checkTextForNull(mainCustomerNameComboBox)) return;

            mainCustomerNameTextBox.Text = dBC.selectFromTable("name", CUSTOMER, $"name = {u.wISQ(mainCustomerNameComboBox.Text)}");
            mainCustomerPhoneTextBox.Text = dBC.selectFromTable("phone", CUSTOMER, $"name = {u.wISQ(mainCustomerNameComboBox.Text)}");
            mainCutomerEmailTextBox.Text = dBC.selectFromTable("email", CUSTOMER, $"name = {u.wISQ(mainCustomerNameComboBox.Text)}");
        }

        // Кнопка вызывается форму добовления покупателя
        private void button3_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer(this);

            customer.Show();
        }

        // Кнопка добаляет нового Покупателя
        private void button4_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(mainCustomerNameComboBox) || u.checkTextForNull(mainObjectNameComboBox)
                || u.checkTextForNull(mainPriceTextBox)) return;
            if (!u.checkForParseText(mainPriceTextBox.Text)) return;

            String column = "date, price";
            String value = $"{u.wISQ(mainDateTimePicker.Value.ToString("yyyy-MM-dd"))}, {u.wISQ(mainPriceTextBox.Text.Trim(' '))}";

            var tmp = u.getStringForSet(mainObjectNameComboBox, OBJECT, null, "Такого Объекта не существует");
            column += (tmp != null) ? ", objectId" : null;
            value += tmp;

            tmp = u.getStringForSet(mainCustomerNameComboBox, CUSTOMER, null, "Такого Покупателя не существует");
            column += (tmp != null) ? ", customerId" : null;
            value += tmp;

            dBC.addToTable(CONTRACT, column, value);
        }
    }
}
