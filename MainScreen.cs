using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackenBank
{
    public partial class MainScreen : Form
    {
        
        public MainScreen(LoginScreen.stUser _User)
        {
            InitializeComponent();
            User = _User;
            IsMdiContainer = true;
        }
        LoginScreen.stUser User;
        

        private void MainScreen_Load(object sender, EventArgs e)
        {
            // this.BackColor = Color.Black;
            
            lblCurrentUser.Text = "Username : " + User._Username.ToString();
            lblTime.Text= "Time : "+DateTime.Now.ToString();
        }

        

        

        private void MainScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms[0].Show();
        }


        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnManageClients_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu(User);
            form.ShowDialog();
            

        }
    }
}
