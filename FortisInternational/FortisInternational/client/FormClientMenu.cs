using FortisInternational_bus;
using FortisInternational_data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortisInternational.client
{
    public partial class FormClientMenu : Form
    {
        public FormClientMenu()
        {
            InitializeComponent();
            lblTitle.Text = "Welcome, " + LoginInformation.customerSignedIn.FirstName + " " + LoginInformation.customerSignedIn.LastName;
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            FormDeposit formD = new FormDeposit();
            formD.Show();
            this.Hide();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            FormWithdraw formWD = new FormWithdraw();
            formWD.Show();
            this.Hide();
        }

        private void btnCreditCard_Click(object sender, EventArgs e)
        {
            if (LoginInformation.customerSignedIn.AccountsList.Count == 3)
            {
                FormCreditCard ccForm = new FormCreditCard();
                ccForm.Show();
                this.Hide();
            }
            else if (LoginInformation.customerSignedIn.AccountsList.Count == 2)
            {
                MessageBox.Show("You do not have a credit account.\nPlease contact the bank to create a credit account.", "No Credit Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAccSummary_Click(object sender, EventArgs e)
        {
            DialogResult confirmation = MessageBox.Show("Do you want to print an Account Summary Report?", "Report Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            switch (confirmation)
            {
                case DialogResult.Yes:
                    PrintSummary();

                    break;
            }

        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            DialogResult signOutConfirmation = MessageBox.Show("Are you sure you want to sign out?", "Sign Out Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (signOutConfirmation == DialogResult.Yes)
            {
                MainMenu formMainMenu = new MainMenu();
                formMainMenu.Show();
                this.Hide();
            }
        }

        void PrintSummary()
        {
            string fileName = "AccountSummary.txt";
            Checking checkingAccount = (Checking)LoginInformation.customerSignedIn.AccountsList[0];
            Saving savingAccount = (Saving)LoginInformation.customerSignedIn.AccountsList[1];

            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.WriteLine("\t\t\tACCOUNT SUMMARY");
                streamWriter.WriteLine("\t\t\t---------------");
                streamWriter.WriteLine();

                streamWriter.WriteLine(LoginInformation.customerSignedIn.FirstName + " " + LoginInformation.customerSignedIn.LastName);
                streamWriter.WriteLine("Customer ID: " + LoginInformation.customerSignedIn.CustomerID);
                streamWriter.WriteLine("Creation Date: " + checkingAccount.AccountOpenDate.ToString());
                streamWriter.WriteLine();

                streamWriter.WriteLine("CHECKING ACCOUNT");
                streamWriter.WriteLine("Account Balance: \t" + checkingAccount.AccountBalance);
                streamWriter.WriteLine("Transactions Today: " + checkingAccount.DetermineDailyTransactions());
                streamWriter.WriteLine();

                streamWriter.WriteLine("SAVINGS ACCOUNT");
                streamWriter.WriteLine("Account Balance: \t" + savingAccount.AccountBalance);
                streamWriter.WriteLine("Total Savings: \t\t" + savingAccount.TotalSavings);
                streamWriter.WriteLine();

                if (LoginInformation.customerSignedIn.AccountsList.Count == 3)
                {
                    Credit creditAccount = (Credit)LoginInformation.customerSignedIn.AccountsList[2];
                    streamWriter.WriteLine("CREDIT ACCOUNT");
                    streamWriter.WriteLine("Account Balance: \t" + creditAccount.AccountBalance);
                    streamWriter.WriteLine("Limit Amount: \t\t" + creditAccount.LimitAmount);
                    streamWriter.WriteLine("Last Payment: \t\t" + creditAccount.LastPayment.ToShortDateString());
                    streamWriter.WriteLine();
                }

                if ((checkingAccount.TransList.Count != 0) || (savingAccount.TransList.Count != 0))
                {
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("LIST OF TRANSACTIONS");
                    streamWriter.WriteLine("--------------------");
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("ACCOUNT\t\tTYPE\t\tAMOUNT\tDATE");

                    foreach (Transaction transaction in checkingAccount.TransList)
                    {
                        if (transaction.TransType == TransTypes.DEPOSIT)
                        {
                            streamWriter.WriteLine("CHECKING" + "\t" + transaction.TransType + "\t\t" + transaction.Amount + "\t\t" + transaction.TransDate.ToString());
                        }
                        else if (transaction.TransType == TransTypes.WITHDRAW)
                        {
                            streamWriter.WriteLine("CHECKING" + "\t" + transaction.TransType + "\t" + transaction.Amount + "\t\t" + transaction.TransDate.ToString());
                        }

                    }
                    foreach (Transaction transaction in savingAccount.TransList)
                    {
                        if (transaction.TransType == TransTypes.DEPOSIT)
                        {
                            streamWriter.WriteLine("SAVINGS" + "\t\t" + transaction.TransType + "\t\t" + transaction.Amount + "\t\t" + transaction.TransDate.ToString());
                        }
                        else if (transaction.TransType == TransTypes.WITHDRAW)
                        {
                            streamWriter.WriteLine("SAVINGS" + "\t\t" + transaction.TransType + "\t" + transaction.Amount + "\t\t" + transaction.TransDate.ToString());
                        }
                    }
                }

            }

            Process.Start(fileName);

        }

    }
}
