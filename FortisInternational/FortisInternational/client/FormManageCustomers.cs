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
    public partial class FormManageCustomers : Form
    {
        public FormManageCustomers()
        {
            InitializeComponent();
            RefreshCustomersListView();
        }

        void RefreshCustomersListView()
        {
            listViewCustomers.Items.Clear();

            foreach (Customer customer in CustomerDB.customerDatabase.Values)
            {
                ListViewItem item = new ListViewItem(customer.CustomerID.ToString());
                item.SubItems.Add(customer.FirstName + " " + customer.LastName);
                item.SubItems.Add(customer.EmailAddress.ToString());
                item.SubItems.Add(customer.PhoneNumber.ToString());
                item.SubItems.Add(customer.Address.ToString());

                listViewCustomers.Items.Add(item);
            }

            listViewCustomers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewCustomers.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (listViewCustomers.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Please select a customer from the list above", "No Customer Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (listViewCustomers.SelectedIndices.Count == 1)
            {
                DeleteCustomer();
                RefreshCustomersListView();
                CustomerDB.SaveDatabase();
                LoginInformation.LoadLoginInformation();
            }
        }

        void DeleteCustomer()
        {
            bool customerFound = false;
            Customer matchedCustomer = new Customer();

            foreach (Customer customer in CustomerDB.customerDatabase.Values)
            {
                if (listViewCustomers.SelectedItems[0].Text == customer.CustomerID.ToString())
                {
                    customerFound = true;
                    matchedCustomer = customer;
                    break;
                }
                else
                {
                    customerFound = false;
                }
            }

            if (customerFound == false)
            {
                MessageBox.Show("Error syncing with customer database. Please restart application and try again", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult deleteConfirmation = MessageBox.Show("Are you sure you want to delete " + matchedCustomer.FirstName + " " + matchedCustomer.LastName + "?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (deleteConfirmation == DialogResult.Yes)
                {
                    CustomerDB.customerDatabase.Remove(matchedCustomer.CustomerID.ToString());
                }
            }

        }

        private void listViewCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAccountsListView();
        }

        void RefreshAccountsListView()
        {
            bool matchedBool = false;
            Customer matchedCustomer = new Customer();

            listViewAccounts.Items.Clear();

            if (listViewCustomers.SelectedIndices.Count != 0)
            {
                foreach (Customer customer in CustomerDB.customerDatabase.Values)
                {
                    ListViewItem selectedCustomer = listViewCustomers.SelectedItems[0];
                    ListViewItem.ListViewSubItem selectedCustomerIDSubItem = selectedCustomer.SubItems[0];

                    int selectedID = Convert.ToInt32(selectedCustomerIDSubItem.Text);

                    if (selectedID == customer.CustomerID)
                    {
                        matchedBool = true;
                        matchedCustomer = customer;
                        break;
                    }
                    else
                    {
                        matchedBool = false;
                    }
                }

                if (matchedBool == false)
                {
                    MessageBox.Show("Error syncing with customer database. Please restart application and try again", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    foreach (Account account in matchedCustomer.AccountsList)
                    {
                        ListViewItem item = new ListViewItem(account.AccountNumber.ToString());
                        item.SubItems.Add(account.AccountType.ToString());
                        item.SubItems.Add(account.AccountBalance.ToString());
                        item.SubItems.Add(account.AccountOpenDate.ToString());

                        listViewAccounts.Items.Add(item);
                    }

                    listViewAccounts.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listViewAccounts.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listViewAccounts.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            else
            {
                listViewAccounts.Items.Clear();
            }

        }

    }
}
