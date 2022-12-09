using System;
using System.Windows.Forms;

namespace BuildingCompany
{
    public partial class Customer : Form
    {
        private Utils u;
        private MyDBC dBC;
        private const String CUSTOMER = "company.customer";
        private Form1 form1;

        // Конструктор
        public Customer(Form1 form1)
        {
            InitializeComponent();

            dBC = new MyDBC(form1);
            u = new Utils(dBC);
        }

        // Кнопка добавления пользователя
        private void addCustomer_Click(object sender, EventArgs e)
        {
            if (u.checkTextForNull(customersNameTextBox) || u.checkTextForNull(customersPhoneTextBox) || u.checkTextForNull(customersEmailTextBox)) return;

            String name = customersNameTextBox.Text.Trim(' ');
            String phone = customersPhoneTextBox.Text.Trim(' ');
            String email = customersEmailTextBox.Text.Trim(' ');

            dBC.addToTable(CUSTOMER, "name, phone, email", $"{u.wISQ(name)}, {u.wISQ(phone)}, {u.wISQ(name)}");
        }
    }
}
