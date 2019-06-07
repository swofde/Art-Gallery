using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace bizzart_proj.Employee
{
    public partial class EditEmployee : Form
    {
        string login;
        Byte[] file;
        string password;
        private User emp;
        Image selectedIm;
        public EditEmployee(User employee)
        {
            InitializeComponent();
            emp = employee;
        }
        public EditEmployee(string login, string password, User employee)
        {
            this.login = login;
            this.password = password;
            this.emp = employee;
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
                comboBoxRoles.SelectedIndex = comboBoxRoles.Items.IndexOf(db.Roles.Where(
                    role => role.RoleId == emp.RoleId
                        ).FirstOrDefault());
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
                    if (!textBoxFullName.Text.Equals(emp.Name))
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
                    if (openFileDialog1.FileName != null)
                    {
                        db.Users.Where(usr => usr.Id == emp.Id).First().ImagePath = "E:\\ExamplePicture\\" + openFileDialog1.FileName.Split('\\').Last();
                        File.WriteAllBytes("E:\\ExamplePicture\\" + openFileDialog1.FileName.Split('\\').Last(), file);
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

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            label7.Text = op.FileName.Split('\\').Last();
            selectedIm = Image.FromFile(op.FileName);
            file = File.ReadAllBytes(op.FileName);
        }
    }
}
