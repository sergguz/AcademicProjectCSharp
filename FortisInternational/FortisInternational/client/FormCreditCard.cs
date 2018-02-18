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
    public partial class FormCreditCard : Form
    {
        Credit creditAccount = (Credit)LoginInformation.customerSignedIn.AccountsList[2];

        enum ComboBoxEnum { CHECKINGS, SAVINGS }

        public FormCreditCard()
        {
            InitializeComponent();
            cbAccount.DataSource = Enum.GetNames(typeof(ComboBoxEnum));
            RefreshInformation();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if ((tbAmount.TextLength == 0) || (cbAccount.SelectedIndex == -1))
            {
                MessageBox.Show("Please fill in both the required fields above to make a payment.", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MakePayment();
                CustomerDB.SaveDatabase();
                RefreshInformation();
            }

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormClientMenu formCM = new FormClientMenu();
            formCM.Show();
            this.Hide();
        }

        void MakePayment()
        {
            double amountInput = Convert.ToDouble(tbAmount.Text);

            switch (cbAccount.SelectedIndex)
            {
                case 0:
                    Checking checkingAccount = (Checking)LoginInformation.customerSignedIn.AccountsList[0];

                    if (amountInput > checkingAccount.AccountBalance)
                    {
                        MessageBox.Show("You do not have sufficient funds in your Checking account to make payment", "Insufficient Funds", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        checkingAccount.AccountBalance -= amountInput;
                        creditAccount.UpdateBalance(TransTypes.DEPOSIT, amountInput);
                    }

                    break;
                case 1:
                    Saving savingAccount = (Saving)LoginInformation.customerSignedIn.AccountsList[1];

                    if (amountInput > savingAccount.AccountBalance)
                    {
                        MessageBox.Show("You do not have sufficient funds in your Saving account to make payment", "Insufficient Funds", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        savingAccount.AccountBalance -= amountInput;
                        creditAccount.UpdateBalance(TransTypes.DEPOSIT, amountInput);

                        MessageBox.Show("Payment made and account updated!", "Payment Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    break;
            }
        }

        void RefreshInformation()
        {
            tbCreditBalance.Text = creditAccount.AccountBalance.ToString();
            tbLastPayment.Text = creditAccount.LastPayment.ToShortDateString();
            tbAmount.Clear();
            cbAccount.SelectedIndex = 0;
        }

    }
}
