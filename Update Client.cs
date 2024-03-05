using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BlackenBank.MainMenu;

namespace BlackenBank
{
    public partial class Update_Client : Form
    {
        public Update_Client(MainMenu.stClient _Client)
        {
            InitializeComponent();
            Client = _Client;
        }

        MainMenu.stClient Client;
        private void Update_Client_Load(object sender, EventArgs e)
        {
            FillTextBoxesWithCurrentClientData();
            label1.Text = "Account Number : " + Client._AccNumber;
        }
        void FillTextBoxesWithCurrentClientData()
        {
            
            txtClientName.Text = Client._ClientName;
            txtPinCode.Text = Client._PinCode;
            txtPhoneNumber.Text = Client._Phone;
            txtBalance.Text = Client._Balance.ToString();
        }
        public MainMenu.stClient FillClientData()
        {
            MainMenu.stClient client = new MainMenu.stClient();
            client._Mode = stClient.enMode.AddNewUserMode;
            client._ClientName = txtClientName.Text;
            client._Balance = Convert.ToDouble(txtBalance.Text);
            client._Phone = txtPhoneNumber.Text;
            client._PinCode = txtPinCode.Text;
            client._AccNumber = Client._AccNumber;
            
            
            return client;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string Message = "Don't be dumb!";
            if ( Add_new_Client.IsEmptyTxtBox(txtBalance, Message,errorProvider1) ||
               Add_new_Client.IsEmptyTxtBox(txtClientName, Message,errorProvider1) ||
               Add_new_Client.IsEmptyTxtBox(txtPhoneNumber, Message,errorProvider1) ||
               Add_new_Client.IsEmptyTxtBox(txtPinCode, Message,errorProvider1))
                return;
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                stClient Client = FillClientData();
                stClient.UpdateClient(Client);
                MessageBox.Show("Client Updated Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void txtBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (!(e.KeyChar == '.') || txtBalance.Text.Contains(".")))
            {
                e.Handled = true;
            }
            else e.Handled = false;
        }

        private void txtClientName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtPinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
            else
                e.Handled = false;
        }
    }
}
