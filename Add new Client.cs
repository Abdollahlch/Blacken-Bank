using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static BlackenBank.MainMenu;

namespace BlackenBank
{
    public partial class Add_new_Client : Form
    {
        public Add_new_Client()
        {
            InitializeComponent();
        }

        private void Add_new_Client_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public MainMenu.stClient FillClientData()
        {
            MainMenu.stClient client = new MainMenu.stClient();
            client._Mode = stClient.enMode.AddNewUserMode;
            client._ClientName= txtClientName.Text;
            client._Balance = Convert.ToDouble(txtBalance.Text);
            client._Phone= txtPhoneNumber.Text;
            client._PinCode= txtPinCode.Text;
            client._AccNumber = txtAccNumber.Text;
          
            return client;
        }
        static public bool IsEmptyTxtBox(Control textBox,string Message,ErrorProvider eptxtEmpty)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                eptxtEmpty.SetError(textBox, Message);
                textBox = (TextBox)textBox;
                
                return true;
            }
            else
                eptxtEmpty.SetError(textBox, "");
            return false;
        }
        void SaveClientToFile(stClient client)
        {
            List<stClient> clients = stClient.GetUsersList();
            clients.Add(client);
            stClient.SaveClientsDataToFile(clients);
        }
        private void button2_Click(object sender, EventArgs e)
        {
          
                string Message = "This field shouldn't be empty!";
            if (IsEmptyTxtBox(txtAccNumber, Message,eptxtEmpty) ||
               IsEmptyTxtBox(txtBalance, Message, eptxtEmpty) ||
               IsEmptyTxtBox(txtClientName, Message, eptxtEmpty) ||
               IsEmptyTxtBox(txtPhoneNumber, Message, eptxtEmpty) ||
               IsEmptyTxtBox(txtPinCode, Message, eptxtEmpty))
                return;
           if ( MessageBox.Show("Confirm Save?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)==DialogResult.OK)
            {
                stClient Client = FillClientData();
                SaveClientToFile(Client);
                foreach (Control control in groupBox1.Controls)
                {
                    if (control is TextBox)
                    {
                        // Your code for each TextBox goes here
                        TextBox textBox = (TextBox)control;
                        textBox.Text = "";
                        // Do something with the textBox
                    }
                }
                
            }
            
            

        }

        private void txtBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (!(e.KeyChar=='.') || txtBalance.Text.Contains(".")))
            {
                e.Handled = true;
            }
            else e.Handled= false;
        }

        private void txtAccNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccNumber.Text) && !char.IsLetter(e.KeyChar))
                e.Handled = true;
            else 
            if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled= true;
            else
                e.Handled= false;
        }

        private void txtClientName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled= true;
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

        private void txtPinCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
