/*
 Abdul Sadiq    - 1532244
 Sergio Guzman  - 1530334

 420-P34-AS - Advanced Object-Programming
 FINAL PROJECT - Fortis International

 Submission Date - 12/09/2016 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FortisInternational_bus;
using FortisInternational_data;
using FortisInternational.client;

namespace FortisInternational
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            CustomerDB.LoadDatabase();
            LoginInformation.LoadLoginInformation();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult option = MessageBox.Show("Do you want to exit the application? ", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (option == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginInformation.userSignedIn = LoginAttempt();

            switch (LoginInformation.userSignedIn)
            {
                case "admin":
                    FormBankMenu formBankMenu = new FormBankMenu();
                    formBankMenu.Show();
                    this.Hide();

                    break;
                case "fail":
                    MessageBox.Show("You have entered an incorrect username and/or password.\n\nPlease try again", "Incorrect Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearFields();

                    break;
                default:
                    LoginInformation.customerSignedIn = CustomerDB.customerDatabase[LoginInformation.userSignedIn];
                    
                    FormClientMenu formClientMenu = new FormClientMenu();
                    formClientMenu.Show();
                    this.Hide();

                    break;
            }
        }

        string LoginAttempt()
        {
            if (tbCustomerID.Text == "admin")
            {
                if (tbPassword.Text == "admin")
                {
                    return "admin";
                }
                else
                {
                    return "fail";
                }
            }
            else
            {
                foreach (KeyValuePair<string, string> user in LoginInformation.loginInfoDict)
                {
                    if (tbCustomerID.Text == user.Key)
                    {
                        if (tbPassword.Text == user.Value)
                        {
                            return user.Key;
                        }
                        else
                        {
                            return "fail";
                        }
                    }
                }                                
            }

            return "fail";
        }

        void ClearFields()
        {
            tbCustomerID.Clear();
            tbPassword.Clear();
            tbCustomerID.Focus();
        }
        
    }
}
