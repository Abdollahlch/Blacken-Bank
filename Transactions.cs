using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackenBank
{
    public partial class Transactions : Form
    {
        public Transactions(MainMenu.stClient CurrentClient)
        {
            InitializeComponent();
            Client = CurrentClient;
        }

        MainMenu.stClient Client;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Transactions_Load(object sender, EventArgs e)
        {
            //for deposit tab
            lblAccNumber.Text ="Acc.Number : " +Client._AccNumber;
            lblClientName.Text ="Client Name : "+ Client._ClientName;
            lblPhone.Text ="Phone : "+ Client._Phone;
            lblPinCode.Text ="Pin Code : "+ Client._PinCode;
            lblBalance.Text = Client._Balance.ToString() + "$";
            
            //for withdraw tab
            lblAccNumberWithdraw.Text = lblAccNumber.Text;
            lblClientNameWithdraw.Text = lblClientName.Text;
            lblPhoneWithdraw.Text = lblPhone.Text;
            lblPinCodeWithdraw.Text = lblPinCode.Text;
            lblBalanceWithdraw.Text = lblBalance.Text;

            //nudWithdraw.Maximum = Convert.ToDecimal(Client._Balance);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (nudDeposit.Value < 1)
            {
                errorProvider1.SetError(nudDeposit, "Minimum amount of deposit is 1$");
                return;
            }
            else
            {
                errorProvider1.SetError(nudDeposit, "");
                
            }
            if (MessageBox.Show("Are you sure you want to perform this transaction?","Comfirm",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Client.Deposit(Convert.ToDouble(nudDeposit.Value));
                MainMenu.stClient.UpdateClient(Client);
                MessageBox.Show("Amout Deposit Successfully!","Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                lblBalance.Text = Client._Balance.ToString() + "$";
                lblBalanceWithdraw.Text = Client._Balance.ToString() + "$";
            }

        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
           
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (nudWithdraw.Value < 1)
            {
                errorProvider1.SetError(nudWithdraw, "Minimum amount of Withdraw is 1$");
                
                return;
            }
            if (Convert.ToDouble(nudWithdraw.Value) > Client._Balance)
            {
                errorProvider1.SetError(nudWithdraw, "Maximum amount of Withdraw is " + Client._Balance + "$");
                
                return;
            }
            errorProvider1.SetError(nudWithdraw, "");
            if (MessageBox.Show("Are you sure you want to perform this transaction?", "Comfirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Client.Withdraw(Convert.ToDouble(nudWithdraw.Value));
                MainMenu.stClient.UpdateClient(Client);
                MessageBox.Show("Amount Successfully withdrawed!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblBalanceWithdraw.Text = Client._Balance.ToString() + "$";
                lblBalance.Text = Client._Balance.ToString() + "$";
            }
            
        }

        private void nudDeposit_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
