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
  
    public partial class UsersListScreen : Form
    {
        public UsersListScreen()
        {
            InitializeComponent();
        }
        
        List <LoginScreen.stUser> Users = new List <LoginScreen.stUser>();


        void AddUsersToListView()
        {
            for (int i = 0; i < Users.Count; i++)
            {
                ListViewItem item = new ListViewItem(Users[i]._Username.Trim());
                item.SubItems.Add(Users[i]._Password.Trim());
                lvUsers.Items.Add(item);
                item.ImageIndex = 0;
                
            }

        }
        private void UsersListScreen_Load(object sender, EventArgs e)
        {
            Users = LoginScreen.stUser.GetUsersList();
            AddUsersToListView();
            
        }

        private void rbDetails_CheckedChanged(object sender, EventArgs e)
        {
            lvUsers.View = View.Details;
        }

        private void rbList_CheckedChanged(object sender, EventArgs e)
        {
            lvUsers.View = View.List;
        }

        private void rbLargeIcon_CheckedChanged(object sender, EventArgs e)
        {
            lvUsers.View = View.LargeIcon;
        }

        private void rbSmallIcons_CheckedChanged(object sender, EventArgs e)
        {
            lvUsers.View = View.SmallIcon;
        }

        private void rbTile_CheckedChanged(object sender, EventArgs e)
        {
            lvUsers.View = View.Tile;
        }

        private void lvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
