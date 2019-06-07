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
    public partial class View_Employee : Form
    {
        private string login;
        private string password;
        public View_Employee(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
            listBox1.SelectedValueChanged += listBox1_SelectedValueChanged;
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            User usr = listBox1.SelectedItem as User;
            try
            {
                if (usr != null)
                {
                    using (ArtGalletyContext db = new ArtGalletyContext())
                    {
                        label3.Text = usr.Name;
                        Role selectedRole = db.Roles.Where(r => r.RoleId == usr.RoleId).First();
                        if (selectedRole != null)
                            label4.Text = selectedRole.RoleName;
                        else label4.Text = "Должность не назначена";
                        Department selectedDep = db.Departments.Where(dep1 => dep1.Id == usr.DepartmentId).First();
                        if (selectedDep == null)
                        {
                            label5.Text = "Отдел не назначен";
                        }
                        else
                        {
                            label5.Text = selectedDep.Name;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!AuthorizationLogics.CheckForRole(login, password, new List<int> { 7 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            if (listBox1.SelectedItem != null)
            {
                DialogResult res = MessageBox.Show("Удалить выбранную картину(ы)?", "Удалить картину", MessageBoxButtons.OKCancel);
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    using (var db = new ArtGalletyContext())
                    {
                        foreach (var a in listBox1.SelectedItems)
                        {
                            User pic = a as User;
                            db.Users.Remove(db.Users.Where(
                                s => s.Id == pic.Id
                                ).FirstOrDefault());
                        }
                        db.SaveChanges();
                        MessageBox.Show("Успешно!");

                        Update_window();
                    }
                 }
              }
          }

        private void Update_window()
        {
            listBox1.Items.Clear();
            ArtGalletyContext db = new ArtGalletyContext();
            foreach (var s in db.Users.ToList())
            {
                listBox1.Items.Add(s);
                listBox1.DisplayMember = "Name";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Update_window();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!AuthorizationLogics.CheckForRole(login, password, new List<int> { 7 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            var addEmp = new bizzart_proj.Employee.AddEmployee();
            addEmp.Visible = true;
        }

        private void View_Employee_Load(object sender, EventArgs e)
        {
            Update_window();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!AuthorizationLogics.CheckForRole(login, password, new List<int> { 7 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            if (listBox1.SelectedItem != null)
            {
                var editEmployee = new bizzart_proj.Employee.EditEmployee(listBox1.SelectedItem as User);
                editEmployee.Visible = true;
            }
            else MessageBox.Show("Выберите сотрудника.");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
