using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bizzart_proj
{
    public partial class AuthorisationForm : Form
    {
        public AuthorisationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkPassword(textBoxLogin.Text, textBoxPassword.Text))
            {
                var mainForm = new MainForm(textBoxLogin.Text, textBoxPassword.Text);
                mainForm.Visible = true;
            }
            else MessageBox.Show("Пользователь с такими данными не найден. Попробуйте ввести данные снова.");
        }

        private bool checkPassword(string login, string password)
        {
            using(var db = new ArtGalletyContext())
            {
                var usr = db.Users.Where
                    (
                        u => u.Login.Equals(login) && u.Password.Equals(password)
                    ).FirstOrDefault();
                if (usr != null)
                {
                    return true;
                }
            }
            return false;
        }



        private void AuthorisationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
