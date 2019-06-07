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
    public partial class ViewEmployee : Form
    {
        string login;
        string password;
        private User emp;
        public ViewEmployee(User employee)
        {
            InitializeComponent();
            emp = employee;
        }
        public ViewEmployee(string login, string password, User employee)
        {
            this.login = login;
            this.password = password;
            this.emp = employee;
            InitializeComponent();
        }

        private void ViewEmployee_Load(object sender, EventArgs e)
        {
            using (var db = new ArtGalletyContext())
            {
                textBoxName.Text = emp.Name;
                textBox4.Text = emp.BirthDate.ToShortDateString();
                textBox3.Text = emp.EmploymentDate.ToShortDateString();
                textBox1.Text = db.Roles.Where(c => c.RoleId == emp.RoleId).FirstOrDefault().RoleName;
                textBox2.Text = db.Departments.Where(c => c.Id == emp.DepartmentId).FirstOrDefault().Name;
            }
        }
    }
}
