
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace userLoginOx
{
    public partial class register : Form
    {


        public register()
        {
            InitializeComponent();
        }

        private void userRegister_Click(object sender, EventArgs e)
        {
            userRegister.ForeColor = Color.Black;
            userRegister.Text = "";
        }

        private void userNameRegister_Click(object sender, EventArgs e)
        {
            userNameRegister.ForeColor = Color.Black;
            userNameRegister.Text = "";
        }

        private void passwordRegister_Click(object sender, EventArgs e)
        {
            passwordRegister.ForeColor = Color.Black;
            passwordRegister.Text = "";
        }

        private void userRegister_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 9)
            {

                userRegister.ForeColor = Color.Black;
            }
        }

        private void userNameRegister_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 9)
            {

                userNameRegister.ForeColor = Color.Black;
            }
        }

        private void passwordRegister_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 9)
            {

                passwordRegister.ForeColor = Color.Black;
            }
        }

        private void signIn_Click(object sender, EventArgs e)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://admin:a123456@ds141902.mlab.com:41902/ox");
                MongoServer server = client.GetServer();
                MongoDatabase database = server.GetDatabase("ox");
                MongoCollection symbolcollection = database.GetCollection<Register>("User"); 
                Register register = new Register();
                BindingList<Register> doclist = new BindingList<Register>();
                var registerDB = database.GetCollection<Register>("User");
                var registerDB2 = registerDB.AsQueryable().Where(pd => pd.User == userRegister.Text);
                foreach (var p in registerDB2)
                {
                    doclist.Add(p);
                    Application.DoEvents();
                }
                dataGridView1.DataSource = doclist;
                if (dataGridView1.Rows.Count == 0)
                {
                    register.User = userRegister.Text;
                    register.Username = userNameRegister.Text;
                    register.Password = passwordRegister.Text;
                    symbolcollection.Insert(register);
                    register.picture = null;
                    register.win = "0";
                    register.draw = "0";
                    register.lose = "0";
                    MessageBox.Show("สมัครเรียบร้อย");
                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex);
            }
        }
        public class Register
        {
            public ObjectId _id { get; set; }
            public string User
            {
                get; set;
            }
            public string Username
            {
                get; set;
            }
            public string Password
            {
                get; set;
            }
            public string picture
            {
                get; set;
            }
            public string win
            {
                get; set;
            }
            public string draw
            {
                get; set;
            }
            public string lose
            {
                get; set;
            }

        }

        private void showPassword_Click(object sender, EventArgs e)
        {
           
            passwordRegister.PasswordChar = '\0';
        }

        private void register_Click(object sender, EventArgs e)
        {
            passwordRegister.PasswordChar = '*';
        }

        private void back_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
