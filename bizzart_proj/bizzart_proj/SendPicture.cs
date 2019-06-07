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
    public partial class SendPicture : Form
    {
        string password;
        string login;
        Picture selectedPic;
        public SendPicture()
        {
            InitializeComponent();
        }
        public SendPicture(string login, string password, Picture selectedPic)
        {
            InitializeComponent();
            this.password = password;
            this.login = login;
            this.selectedPic = selectedPic;
        }

        private void SendPicture_Load(object sender, EventArgs e)
        {
            var db = new ArtGalletyContext();
            foreach (var dep in db.Departments)
            {
                comboBox1.Items.Add(dep);
            }
            comboBox1.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var usr = LogicsSmthg.LoginAuth(login, password);
            var db = new ArtGalletyContext();
            if (usr.RoleId == 2 || usr.RoleId == 3)
            {
                db.Pictures.Where(p => p.Id == selectedPic.Id).FirstOrDefault().DepartmentId = (comboBox1.SelectedItem as Department).Id;
                db.SaveChanges();
                MessageBox.Show("Успешно!");
                Close();
            }
        }
    }
}
