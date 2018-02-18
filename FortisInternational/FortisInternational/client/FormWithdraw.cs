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
    public partial class FormWithdraw : Form
    {
        enum ComboBoxEnum { CHECKINGS, SAVINGS }

        public FormWithdraw()
        {
            InitializeComponent();
            cbAccounts.DataSource = Enum.GetNames(typeof(ComboBoxEnum));
            cbAccounts.SelectedIndex = -1;
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if ((tbAmount.TextLength == 0) || (cbAccounts.SelectedIndex == -1))
            {
                MessageBox.Show("There are missing fields. Please ensure both fields are filled in with appropriate data.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                WithdrawAndSave();
                cbAccounts.SelectedIndex = -1;
                tbAmount.Clear();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormClientMenu formCM = new FormClientMenu();
            formCM.Show();
            this.Hide();
        }

        void WithdrawAndSave()
        {
            int transactionNumber;
            DateTime DateToday = DateTime.Today;
            double amountInput = Convert.ToDouble(tbAmount.Text);
            string selectedCBItem = cbAccounts.SelectedItem.ToString();

            DialogResult withdrawConfirmation = MessageBox.Show("Are you sure you want to withdraw $" + amountInput + " from " + selectedCBItem + "?", "Withdraw Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (withdrawConfirmation == DialogResult.Yes)
            {
                switch (selectedCBItem)
                {
                    case "CHECKINGS":
                        Checking checkingAccount = (Checking)LoginInformation.customerSignedIn.AccountsList[0];
                        transactionNumber = 0;

                        if (checkingAccount.TransList.Count != 0)
                        {
                            transactionNumber = checkingAccount.TransList[checkingAccount.TransList.Count - 1].TransNumber + 1;
                        }

                        checkingAccount.UpdateBalance(TransTypes.WITHDRAW, amountInput);
                        checkingAccount.TransList.Add(new Transaction(transactionNumber, new Date(DateToday.Day, DateToday.Month, DateToday.Year), amountInput, TransTypes.WITHDRAW));

                        break;
                    case "SAVINGS":
                        Saving savingsAccount = (Saving)LoginInformation.customerSignedIn.AccountsList[1];
                        transactionNumber = 0;

                        if (savingsAccount.TransList.Count != 0)
                        {
                            transactionNumber = savingsAccount.TransList[savingsAccount.TransList.Count - 1].TransNumber + 1;
                        }
                        
                        savingsAccount.UpdateBalance(TransTypes.WITHDRAW, amountInput);
                        savingsAccount.TransList.Add(new Transaction(transactionNumber, new Date(DateToday.Day, DateToday.Month, DateToday.Year), amountInput, TransTypes.WITHDRAW));

                        break;
                }
                CustomerDB.SaveDatabase();
            }
        }

        private void cbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAccounts.SelectedItem != null)
            {
                switch (cbAccounts.SelectedItem.ToString())
                {
                    case "CHECKINGS":
                        Checking checkingAccount = (Checking)LoginInformation.customerSignedIn.AccountsList[0];

                        checkingAccount.UpdateBalance(TransTypes.DEPOSIT, 0.0d);
                        tbDisplay.Text = checkingAccount.AccountType.ToString() + "\r\n\r\nAccount Balance: " + checkingAccount.AccountBalance + "\r\nNumber of Transactions Today: " + checkingAccount.DetermineDailyTransactions().ToString() + "\r\nOpen Date: " + checkingAccount.AccountOpenDate;

                        break;
                    case "SAVINGS":
                        Saving savingAccount = (Saving)LoginInformation.customerSignedIn.AccountsList[1];

                        savingAccount.UpdateBalance(TransTypes.DEPOSIT, 0.0d);
                        tbDisplay.Text = savingAccount.AccountType.ToString() + "\r\n\r\nAccount Balance: " + savingAccount.AccountBalance + "\r\nOpen Date: " + savingAccount.AccountOpenDate;

                        break;
                    default:
                        tbDisplay.Clear();

                        break;
                }

            }
            else
            {
                tbDisplay.Clear();
            }
        }
    }
}
