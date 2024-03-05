using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BlackenBank.MainMenu;

namespace BlackenBank
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        static string FilePath = "Users.txt";


        public stUser User;
        public struct stUser
        {
            public enum enMode { emptyMode = 0, UpdateMode, AddNewUserMode };
            public enMode _Mode;
            public string _Username;
            public string _Password;
             stUser(enMode Mode,string UserName,string Pw)
            {
                _Mode = Mode;
                _Username = UserName;
                _Password = Pw;
            }
            static private List <stUser> _LoadUsersDataFromFile()
            {
                List <stUser> Users = new List <stUser> ();
                string[] UsersList = File.ReadAllLines(FilePath);
                foreach (string Line in UsersList)
                {
                    stUser user = _ConvertLineToUser(Line, '#');
                    Users.Add(user);
                }
                return Users;
            }

            static public List <stUser> GetUsersList()
            {
                return _LoadUsersDataFromFile();
            }
            static stUser _ConvertLineToUser(string Line,char Sep)
            {
                string[] UserData = Line.Split(Sep);
                stUser _User = new stUser(enMode.UpdateMode, UserData[0], UserData[1]);
                return _User;
            }
            static string _ConvertUserToLine(stUser User,char Sep)
            {
                string Line= User._Username+Sep+User._Password;
                return Line;
            }
           static  stUser GetEmptyUserObj()
            {
                stUser User = new stUser(enMode.emptyMode, "", "");
                return User;
            }
            static public stUser Find(string Username)
            {
                List<stUser> Users = _LoadUsersDataFromFile();
                foreach(stUser User in Users)
                {
                    if (Username == User._Username)
                        return User;
                }
                return GetEmptyUserObj();
                
            }

            public bool IsUserExist()
            {
                stUser User = Find(_Username);
                return (!(User._Mode == enMode.emptyMode));
            }
            static public void SaveUsersDataToFile(List <stUser>  Users)
            {
                string Line;
                using (StreamWriter writer = new StreamWriter(FilePath, false))
                {
                    foreach (stUser user in Users)
                    {
                        Line = _ConvertUserToLine(user, '#');
                        writer.WriteLine(Line);
                    }
                }

            }

            public void Delete()
            {
                List<stUser> Users = _LoadUsersDataFromFile();
          
                foreach (stUser user in Users)
                {
                    if (user._Username == _Username)
                    {
                        Users.Remove(user);
                    }
                }
                SaveUsersDataToFile(Users);
            }
        }


        private void Login()
        {
            User = stUser.Find(txtUsername.Text);
            if (!User.IsUserExist() || User._Password!=txtPassword.Text)
            {
                MessageBox.Show("Invalid Username or password!","Wrong",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                 
                txtPassword.Clear();
                return;
            }
            
            MainMenu mainScreen = new MainMenu(User);
            mainScreen.Show();
            this.Hide();
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
           txtUsername.Clear();
            txtPassword.Clear();

        }

        

       
        

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Fill this field!");
                return;
            }
            else
                errorProvider1.SetError(txtUsername, "");

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Fill this field!");
                return;
            }
            else
                errorProvider1.SetError(txtPassword, "");
            Login();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
