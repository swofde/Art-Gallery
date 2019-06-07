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
    public partial class MainForm : Form
    {
        private string login;
        private string password;
        public MainForm(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            View_Pictures vp = new View_Pictures(login, password);
            vp.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            View_Employee ve = new View_Employee(login, password);
            ve.Visible = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Привет, "+login+"!";
        }
    }
}
