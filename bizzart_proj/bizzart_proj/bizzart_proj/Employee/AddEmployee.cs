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
    public partial class AddEmployee : Form
    {
        string login;
        string password;
        public AddEmployee()
        {
            InitializeComponent();
        }
        public AddEmployee(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            using (var db = new ArtGalletyContext())
            {
                comboBoxDepartment.Items.Clear();
                foreach (var s in db.Departments.ToList())
                {
                    comboBoxDepartment.Items.Add(s);
                    comboBoxDepartment.DisplayMember = "Name";
                }
                comboBoxRoles.Items.Clear();
                foreach (var s in db.Roles.ToList())
                {
                    comboBoxRoles.Items.Add(s);
                    comboBoxRoles.DisplayMember = "RoleName";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ArtGalletyContext agc = new ArtGalletyContext();
                Department dep = comboBoxDepartment.SelectedItem as Department;
                User pic = new User
                {
                    Name = textBoxFullName.Text,
                    Role = comboBoxRoles.SelectedItem as Role,
                    BirthDate = dateTimePickerBirthDay.Value,
                    EmploymentDate = dateTimePickerEmploymentDate.Value,
                    Department = agc.Departments.Where(department => department.Id == (dep).Id).FirstOrDefault()
                };
                agc.Users.Add(pic);
                agc.SaveChanges();
                MessageBox.Show("Успешно!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
