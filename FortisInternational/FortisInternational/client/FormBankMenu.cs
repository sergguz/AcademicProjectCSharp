using FortisInternational_bus;
using FortisInternational_data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortisInternational.client
{
    public enum enumProvinces { BC, AB, SK, MB, ON, QC, NL, NB, NS, PE, YT, NT, NU, USA }
    public enum YesOrNo {Yes, No}

    public partial class FormBankMenu : Form
    {
        public FormBankMenu()
        {
            InitializeComponent();
            cbProvince.DataSource = Enum.GetNames(typeof(enumProvinces));
            cbCreditCard.DataSource = Enum.GetNames(typeof(YesOrNo));
            cbProvince.SelectedIndex = -1;
            cbCreditCard.SelectedIndex = 1;
            lblCreditLimit.Visible = false;
            tbCreditLimit.Visible = false;
        }

        private void ExitAppButton_Click(object sender, EventArgs e)
        {
            DialogResult confirmationOption = MessageBox.Show("Are you sure you want to log out? ", "Log Out Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmationOption == DialogResult.Yes)
            {
                MainMenu formMainMenu = new MainMenu();
                formMainMenu.Show();
                this.Hide();
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if ((tbCustomerID.Text == null) || (tbPIN.Text == null) || (tbFirstName.Text == null) || (tbLastName.Text == null) || (tbCountryCode.Text == null) || (tbAreaCode.Text == null) || (tbFirstThree.Text == null) || (tbLastFour.Text == null) || (tbEmail.Text == null) || (tbAptNum.Text == null) || (tbBuildingNum.Text == null) || (tbStreet.Text == null) || (tbCity.Text == null) || (cbProvince.SelectedIndex == -1) || (tbPostalCode.Text == null) || (tbCountry.Text == null) || (cbCreditCard.SelectedIndex == -1) || ((cbCreditCard.SelectedIndex == 0) && (tbCreditLimit.Text == null)) || (tbStartingChecking.Text == null) || (tbStartingSaving.Text == null) || (tbDateDD.Text == null) || (tbDateMM.Text == null) || (tbDateYYYY.Text == null))
            {
                MessageBox.Show("Please fill out all fields to add customer into the database", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                AddCustomer();
                CustomerDB.SaveDatabase();
                LoginInformation.SaveLoginInformation();
                ClearFields();
            }

        }

        void AddCustomer()
        {
            int customerID, pinCode;
            string firstName, lastName;
            Phone phone;
            Email email;
            Address address;
            Customer customer;

            customerID = Convert.ToInt32(tbCustomerID.Text);
            pinCode = Convert.ToInt32(tbPIN.Text);
            firstName = tbFirstName.Text;
            lastName = tbLastName.Text;
            phone = new Phone(Convert.ToInt32(tbCountryCode.Text), Convert.ToInt32(tbAreaCode.Text), Convert.ToInt32(tbFirstThree.Text), Convert.ToInt32(tbLastFour.Text));
            email = new Email(tbEmail.Text);
            address = new Address(Convert.ToInt32(tbAptNum.Text), Convert.ToInt32(tbBuildingNum.Text), tbStreet.Text, tbCity.Text, cbProvince.Text, tbPostalCode.Text, tbCountry.Text);

            customer = new Customer(customerID, pinCode, firstName, lastName, email, phone, address);

            customer.AccountsList.Add(new Checking(customerID, new Date(Convert.ToInt32(tbDateDD.Text), Convert.ToInt32(tbDateMM.Text), Convert.ToInt32(tbDateYYYY.Text)), Convert.ToDouble(tbStartingChecking.Text)));
            customer.AccountsList.Add(new Saving(customerID, new Date(Convert.ToInt32(tbDateDD.Text), Convert.ToInt32(tbDateMM.Text), Convert.ToInt32(tbDateYYYY.Text)), Convert.ToDouble(tbStartingSaving.Text)));
            if (cbCreditCard.SelectedIndex == 0)
            {
                customer.AccountsList.Add(new Credit(customerID, new Date(Convert.ToInt32(tbDateDD.Text), Convert.ToInt32(tbDateMM.Text), Convert.ToInt32(tbDateYYYY.Text)), Convert.ToDouble(tbCreditLimit.Text), 0.0d));
            }

            LoginInformation.loginInfoDict.Add(customerID.ToString(), pinCode.ToString());
            CustomerDB.customerDatabase.Add(customer.CustomerID.ToString(), customer);

        }

        void ClearFields()
        {
            tbCustomerID.Clear();
            tbPIN.Clear();
            tbFirstName.Clear();
            tbLastName.Clear();
            tbCountryCode.Clear();
            tbAreaCode.Clear();
            tbFirstThree.Clear();
            tbLastFour.Clear();
            tbEmail.Clear();
            tbAptNum.Clear();
            tbBuildingNum.Clear();
            tbStreet.Clear();
            tbCity.Clear();
            cbProvince.SelectedIndex = -1;
            tbPostalCode.Clear();
            tbCountry.Clear();
            cbCreditCard.SelectedIndex = 1;
            tbStartingChecking.Clear();
            tbStartingSaving.Clear();
            tbCreditLimit.Clear();
            tbDateDD.Clear();
            tbDateMM.Clear();
            tbDateYYYY.Clear();
            tbCustomerID.Focus();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            DateTime dateToday = DateTime.Today;

            tbDateDD.Text = dateToday.Day.ToString();
            tbDateMM.Text = dateToday.Month.ToString();
            tbDateYYYY.Text = dateToday.Year.ToString();
        }

        private void cbCreditCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCreditCard.SelectedIndex == 0)
            {
                lblCreditLimit.Visible = true;
                tbCreditLimit.Visible = true;
            }
            else if (cbCreditCard.SelectedIndex == 1)
            {
                lblCreditLimit.Visible = false;
                tbCreditLimit.Visible = false;
            }
        }

        private void btnManageCustomers_Click(object sender, EventArgs e)
        {
            FormManageCustomers manageCustomersForm = new FormManageCustomers();
            manageCustomersForm.Show();
        }

    }
}
