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
    public partial class AddEmployee : Form
    {
        string login;
        string password;
        byte[] file;
        Image selectedIm;
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
            foreach (ComboBox c in this.Controls.OfType<ComboBox>())
            {
                if (c.SelectedIndex == null)
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
            }
            foreach (TextBox c in this.Controls.OfType<TextBox>())
            {
                if (c.Text.Length == 0)
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
            }
            if (openFileDialog1.FileName.Length == 0)
            {
                MessageBox.Show("Не выбрана картинка!");
                return;
            }
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
                    Department = agc.Departments.Where(department => department.Id == (dep).Id).FirstOrDefault(),
                    ImagePath = "E:\\ExamplePicture\\" + openFileDialog1.FileName.Split('\\').Last()
                };
                File.WriteAllBytes("E:\\ExamplePicture\\" + openFileDialog1.FileName.Split('\\').Last(), file);
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
