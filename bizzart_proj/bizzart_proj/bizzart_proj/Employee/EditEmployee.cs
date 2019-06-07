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
    public partial class EditEmployee : Form
    {
        private User emp;
        public EditEmployee(User employee)
        {
            InitializeComponent();
            emp = employee;
        }
        public EditEmployee(string login, string password)
        {
            InitializeComponent();
        }

        private void EditEmployee_Load(object sender, EventArgs e)
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
                textBoxFullName.Text = emp.Name;
                dateTimePickerBirthDay.Value = emp.BirthDate;
                dateTimePickerEmploymentDate.Value = emp.EmploymentDate;
                comboBoxDepartment.SelectedIndex = comboBoxDepartment.Items.IndexOf(db.Departments.Where(
                    dep => dep.Id == emp.DepartmentId
                    ).FirstOrDefault());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                using (var db = new ArtGalletyContext())
                {
                    if (textBoxFullName.Text.Equals(emp.Name))
                    {
                        db.Users.Where(usr => usr.Id == emp.Id).First().Name = textBoxFullName.Text;
                    }
                    if ((comboBoxRoles.SelectedItem as Role).RoleId != emp.RoleId)
                    {
                        db.Users.Where(usr => usr.Id == emp.Id).First().Role = (comboBoxRoles.SelectedItem as Role);
                    }
                    if (dateTimePickerBirthDay.Value != emp.BirthDate)
                    {
                        db.Users.Where(usr => usr.Id == emp.Id).First().BirthDate = dateTimePickerBirthDay.Value;
                    }
                    if (dateTimePickerEmploymentDate.Value != emp.EmploymentDate)
                    {
                        db.Users.Where(usr => usr.Id == emp.Id).First().EmploymentDate = dateTimePickerEmploymentDate.Value;
                    }
                    if ((comboBoxDepartment.SelectedItem as Department).Id != emp.DepartmentId)
                    {
                        db.Users.Where(usr => usr.Id == emp.Id).First().Department = (comboBoxDepartment.SelectedItem as Department);
                    }
                    db.SaveChanges();
                }
                MessageBox.Show("Успешно!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
