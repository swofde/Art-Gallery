using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bizzart_proj.Employee
{
    public partial class AuthorisationForm : Form
    {
        public AuthorisationForm()
        {
            InitializeComponent();
        }

        private void buttonConfirmLogin_Click(object sender, EventArgs e)
        {
            User u = LogicsSmthg.LoginAuth(textBoxlogin.Text, textBoxPassword.Text);
            if (u == null) MessageBox.Show("Login or password is incorrect");
            else
            {
                MessageBox.Show("Hello! "+u.Name);
                this.Hide();
                MainForm cusf = new MainForm(textBoxlogin.Text, textBoxPassword.Text);
                cusf.ShowDialog();
                this.Close();
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void AuthorisationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
