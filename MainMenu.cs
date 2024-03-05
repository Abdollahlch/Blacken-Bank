using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BlackenBank
{
    public partial class MainMenu : Form
    {
        public MainMenu(LoginScreen.stUser User)
        {
            InitializeComponent();
            toolTip.SetToolTip(pbAddNew, "Add new client");
            toolTip.SetToolTip(pbRemove, "Remove client");
            toolTip.SetToolTip(pbUpdate, "Update Client");
            toolTip.SetToolTip(pbTransactions, "Transactions");
            CurrentUser = User;
        }

        static string FilePath = "Clients.txt";

        LoginScreen.stUser CurrentUser;
        public stClient Client;
        public struct stClient
        {
            public enum enMode { emptyMode = 0, UpdateMode, AddNewUserMode };
            public enMode _Mode { get;set;}
            public string _AccNumber { get; set; }
            public string _PinCode { get; set; }
            public string _ClientName { get; set; }
            public string _Phone { get; set; }
            public double _Balance { get; set; }

        
            stClient(enMode Mode, string AccNumber, string PinCode,string ClientName,string Phone,double Balance)
            {
                _Mode = Mode;
                _AccNumber = AccNumber;
                _PinCode = PinCode;
                _ClientName = ClientName;
                _Phone = Phone;
                _Balance = Balance;
                
            }
            static private List<stClient> _LoadUsersDataFromFile()
            {
                List<stClient> Clients = new List<stClient>();
                string[] ClientsList = File.ReadAllLines(FilePath);
                foreach (string Line in ClientsList)
                {
                    stClient Client = _ConvertLineToClient(Line, '#');
                    Clients.Add(Client);
                }
                return Clients;
            }

            static public List<stClient> GetUsersList()
            {
                return _LoadUsersDataFromFile();
            }
            static stClient _ConvertLineToClient(string Line, char Sep)
            {
                string[] ClientData = Line.Split(Sep);
                stClient _Client = new stClient(enMode.UpdateMode, ClientData[0], ClientData[1], ClientData[2], ClientData[3], Convert.ToDouble(ClientData[4]));
                return _Client;
            }
            static string _ConvertClientToLine(stClient Client, char Sep)
            {
                string Line = Client._AccNumber + Sep + Client._PinCode+ Sep+ Client._ClientName+Sep+ Client._Phone+Sep+ Client._Balance.ToString();
                return Line;
            }
            static stClient GetEmptyClientObj()
            {
                stClient Client = new stClient(enMode.emptyMode, "", "","","",0);
                return Client;
            }
            static public stClient Find(string AccNumber)
            {
                List<stClient> Clients = _LoadUsersDataFromFile();
                foreach (stClient Client in Clients)
                {
                    if (AccNumber == Client._AccNumber)
                        return Client;
                }
                return GetEmptyClientObj();

            }

            public bool IsClientExist()
            {
                stClient Client = Find(_AccNumber);
                return (!(Client._Mode == enMode.emptyMode));
            }
            static public void SaveClientsDataToFile(List <stClient> Clients)
            {
                

                string Line;
                using (StreamWriter writer = new StreamWriter(FilePath, false))
                {
                    foreach (stClient Client in Clients)
                    {
                        Line = _ConvertClientToLine(Client, '#');
                        writer.WriteLine(Line);
                    }
                }

            }


            static public void Delete(string AccNumForDelete)
            {
                List<stClient> ClientsList =GetUsersList();
                stClient Client = Find(AccNumForDelete);
                if (Client.IsClientExist())
                {
                    ClientsList.Remove(Client);
                    SaveClientsDataToFile(ClientsList);
                    
                }
                
            }
       
        
            static public void UpdateClient(stClient Client)
            {
                List <stClient> Clients = GetUsersList();
                
                    for (int i=0;i<Clients.Count;i++)
                {
                    if (Clients[i]._AccNumber==Client._AccNumber)
                    {
                        Clients[i] = Client;
                        break;
                    }
                }
                SaveClientsDataToFile(Clients);
                
            }
        
            public void Deposit(double Amount)
            {
                if (Amount < 0)
                    Amount = 0;
                this._Balance += Amount;

            }
            public bool Withdraw(double Amount)
            {
                if (Amount > this._Balance || Amount<1)
                    return false;
                this._Balance -= Amount;
                
                return true;
            }
        }

       public void SaveClientToFile(stClient client)
        {
            List<stClient> clients=stClient.GetUsersList();
            clients.Add(client);
            stClient.SaveClientsDataToFile(clients);
        }

        enum enSearchMode { AccNumber, PinCode,Phone };
        enSearchMode SearchMode= enSearchMode.AccNumber;

        void Search()
        {
            if (string.IsNullOrEmpty(tbSearch.Text))
            {
                ep1.SetError(tbSearch, "Fill this field!");
                return;
            }
            else
                ep1.SetError(tbSearch, "");
            switch (SearchMode)
            {
                case enSearchMode.AccNumber:
                    
                    foreach (ListViewItem item in lvClients.Items)
                    {
                        if (item.SubItems[0].Text.ToLower() == tbSearch.Text.ToLower())
                        {
                            item.Selected = true;
                            return;
                        }
                    }
                    MessageBox.Show("No items match your search.","Result",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    tbSearch.Clear();
                    tbSearch.Focus();

                    break;
            }
           
        }

        void SelectSearchMode(int index)
        {
           
            switch(index)
            {
                case 0:
                    SearchMode = enSearchMode.AccNumber;
                    break;
                case 1:
                    SearchMode = enSearchMode.PinCode;
                    break;
                case 2:
                    SearchMode = enSearchMode.Phone;
                    break;
            }
        }

         void ShowClientsList()
        {
            List<stClient> Clients = stClient.GetUsersList();
            if (Clients.Count == 0)
            {
                lblNoClients.Visible = true;
                tbSearch.Enabled = false;
                cbSearchBy.Enabled = false;
                pbSearch.Enabled = false;
                //lvClients.Clear();
                return;
            }
            
            lvClients.Items.Clear();
            foreach(stClient Client in Clients)
            {
                ListViewItem item = new ListViewItem(Client._AccNumber, 0);
                item.SubItems.Add(Client._PinCode);
                item.SubItems.Add(Client._ClientName);
                item.SubItems.Add(Client._Phone);
                item.SubItems.Add(Client._Balance.ToString());
                
                lvClients.Items.Add(item);

            }
            lblNoClients.Visible=false;
            tbSearch.Enabled = true;
            cbSearchBy.Enabled = true;
            pbSearch.Enabled = true;
        }
       
        void NotClientExist()
        {
            if (lvClients.Items.Count == 0)
            {
              
                lblNoClients.Visible = true;
                tbSearch.Enabled = false;
                cbSearchBy.Enabled = false;
                pbSearch.Enabled = false;

            }
            else
            {
               
                lblNoClients.Visible = false;
                tbSearch.Enabled = true;
                cbSearchBy.Enabled = true;
                pbSearch.Enabled = true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cbSearchBy.SelectedIndex = 0;
            cbBackMode.SelectedIndex = 0;
            lblCurrentUser.Text = "Current User : " + CurrentUser._Username;
            lblTime.Text = "Time : " + DateTime.Now.ToString();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
           

            Application.OpenForms[0].Show();
            
            this.Close();

        }

        

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tpMainMenu;
        }

        private void btnShowClientsList_Click(object sender, EventArgs e)
        {
            ShowClientsList();
            tabControl1.SelectedTab = tpManageClient;
        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rbDetails_CheckedChanged_1(object sender, EventArgs e)
        {
            lvClients.View = View.Details;
        }

        private void rbLargeIcon_CheckedChanged_1(object sender, EventArgs e)
        {
            lvClients.View = View.LargeIcon;
        }

        private void rbList_CheckedChanged_1(object sender, EventArgs e)
        {
            lvClients.View = View.List;
        }

        private void rbSmallIcons_CheckedChanged_1(object sender, EventArgs e)
        {
            lvClients.View = View.SmallIcon;
        }

        private void rbTile_CheckedChanged_1(object sender, EventArgs e)
        {
            lvClients.View = View.Tile;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void cbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectSearchMode(cbSearchBy.SelectedIndex);
        }

        private void lvClients_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right)
            {
                if (lvClients.SelectedItems.Count>0 && lvClients.Items.Count>0)
                {
                    lvClients.ContextMenuStrip = cmClient;
                }
                else
                    
                    lvClients.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void tsmUpdateClient_Click(object sender, EventArgs e)
        {
            pbUpdate_Click(sender, e);

        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            
            if (lvClients.SelectedItems.Count < 0)
            {
                return;
            }
            
            List<string> accNumbersToDelete = new List<string>();

            foreach (ListViewItem selectedItem in lvClients.SelectedItems)
            {
                string accNumberToDelete = selectedItem.SubItems[0].Text;
                accNumbersToDelete.Add(accNumberToDelete);
            }
            
            foreach (ListViewItem item in lvClients.SelectedItems)
            {
                if (MessageBox.Show("Are you sure you want to delete that client?",
                    "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    return;

                stClient.Delete(item.Text);
                //lvClients.Refresh();
                lvClients.Items.Remove(item);
                NotClientExist();


            }
            
            
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.DrawString(" ", Font, Brushes.Transparent, e.Bounds);
        }

        private void tpMainMenu_Click(object sender, EventArgs e)
        {

        }

        private void lvClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvClients.SelectedItems.Count==0)
                pbRemove.Enabled = false;
            else
                pbRemove.Enabled = true;
            //if only one item is selected update and transactions will be available
            if (lvClients.SelectedItems.Count==0 || lvClients.SelectedItems.Count>1)
            {
                pbUpdate.Enabled = false;
                pbTransactions.Enabled = false;
            }
            
            else
            {
                pbUpdate.Enabled = true;
                pbTransactions.Enabled = true;
            }

            if (lvClients.SelectedItems.Count > 0)
            {
                lvClients.ContextMenuStrip = cmClient;
            }
            else
                lvClients.ContextMenuStrip = contextMenuStrip1;
        }

        private void lvClients_ControlRemoved(object sender, ControlEventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbBackMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBackMode.SelectedIndex == 1)
            {
                tpManageClient.BackColor = Color.DarkGray;
                lvClients.BackColor=Color.DarkGray;
            }
            else
            {
                tpManageClient.BackColor = default;
                lvClients.BackColor = default;
            }
        }

        private void pbRemove_Click(object sender, EventArgs e)
        {
            tsmDelete_Click(sender, e);
        }

        private void pbAddNew_Click(object sender, EventArgs e)
        {
            Add_new_Client newClient = new Add_new_Client();
            newClient.ShowDialog();
            ShowClientsList();
        }

        private void addNewClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pbAddNew_Click(sender, e);
        }

        private void tpManageClient_Click(object sender, EventArgs e)
        {

        }

        private void MainMenu_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms[0].Show();
        }

        void UpdateClient()
        {
            Update_Client update_Client = new Update_Client(stClient.Find(lvClients.SelectedItems[0].Text));
            update_Client.ShowDialog();
            ShowClientsList();
            pbUpdate.Enabled = false;
        }
        private void pbUpdate_Click(object sender, EventArgs e)
        {
            UpdateClient();


        }

        void Transactions(int TabIndex)
        {
            Transactions transactions = new Transactions(stClient.Find(lvClients.SelectedItems[0].Text));
            transactions.tabControl1.SelectTab(TabIndex);
            transactions.ShowDialog();
            ShowClientsList();
            pbTransactions.Enabled = false;
            pbRemove.Enabled = false;
            pbUpdate.Enabled = false;
        }

        private void depositToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pbTransactions_Click(sender, e);
        }

        private void pbTransactions_Click(object sender, EventArgs e)
        {
            Transactions(0);
            

        }

        private void withdrawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transactions(1);
        }

        void LoadUsersToListView()
        {
            List<LoginScreen.stUser> Users= LoginScreen.stUser.GetUsersList();
            if (Users.Count==0)
            {
                lblNoUsers.Visible = true;
            }
            foreach (LoginScreen.stUser user in Users)
            {
                ListViewItem item = new ListViewItem(user._Username);
                item.SubItems.Add(user._Password);
                lvUsers.Items.Add(item);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tpManageUsers;
            LoadUsersToListView();



        }


        private void listView1_SelectedIndexChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
           
        }

        private void lvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(lvUsers.SelectedItems.Count == 0))
            {
                pbRemoveUser.Enabled = true;
            }
            else
                pbRemoveUser.Enabled = false;
        }

        private void pbRemoveUser_Click(object sender, EventArgs e)
        {
            List<LoginScreen.stUser> Users = LoginScreen.stUser.GetUsersList();
            foreach(ListViewItem item in lvUsers.SelectedItems)
            {
                LoginScreen.stUser user = LoginScreen.stUser.Find(item.Text);
                Users.Remove(user);
                lvUsers.Items.Remove(item);

            }
            LoginScreen.stUser.SaveUsersDataToFile(Users);
            if (lvUsers.Items.Count == 0)
            {
                lblNoUsers.Visible = true;
            }
            else
                lblNoUsers.Visible = false;
        }

        private void lvUsers_ControlRemoved(object sender, ControlEventArgs e)
        {
            
        }
    }
}
