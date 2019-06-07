using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bizzart_proj.Pictures
{
    public partial class EditPicture : Form
    {
        string login;
        string password;
        private Picture pic;
        public EditPicture(Picture selectedPic)
        {
            InitializeComponent();
            pic = selectedPic;
        }

        public EditPicture(Picture selectedPic, string login, string password)
        {
            InitializeComponent();
            pic = selectedPic;
            this.login = login;
            this.password = password;
        }

        private void EditPicture_Load(object sender, EventArgs e)
        {
            using (var db = new ArtGalletyContext())
            {
                comboBoxArtist.Items.Clear();
                comboBoxDepartment.Items.Clear();
                foreach (var s in db.Artists.ToList())
                {
                    comboBoxArtist.Items.Add(s);
                    comboBoxArtist.DisplayMember = "Name";
                }
                foreach (var s in db.Departments.ToList())
                {
                    comboBoxDepartment.Items.Add(s);
                    comboBoxDepartment.DisplayMember = "Name";
                }
                comboBoxArtist.SelectedIndex = comboBoxArtist.Items.IndexOf(db.Artists.Where(
                    art1 => art1.Id == pic.ArtistId
                    ).FirstOrDefault());
                comboBoxDepartment.SelectedIndex = comboBoxDepartment.Items.IndexOf(db.Departments.Where(
                    dep => dep.Id == pic.DepartmentId
                    ).FirstOrDefault());
            }
            textBoxName.Text = pic.Name;
            textBoxHistory.Text = pic.PictureHistory;
            textBoxPrice.Text = pic.Price.ToString();
            textBoxGenre.Text = pic.Genre;
            textBoxSizeX.Text = pic.Size.X.ToString();
            textBoxSizeY.Text = pic.Size.Y.ToString();
            dateTimePickerWrite.Value = pic.WriteDate;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!LogicsSmthg.CheckForRole(login, password, new List<int> { 2, 3 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            using (ArtGalletyContext db = new ArtGalletyContext())
            {
                if (!textBoxName.Text.Equals(pic.Name))
                {
                    db.Pictures.Where(pic1 => pic1.Id == pic.Id).FirstOrDefault().Name = textBoxName.Text;
                }
                if (!textBoxGenre.Text.Equals(pic.Genre))
                {
                    db.Pictures.Where(pic1 => pic1.Id == pic.Id).FirstOrDefault().Genre = textBoxGenre.Text;
                }
                if (!textBoxHistory.Text.Equals(pic.PictureHistory))
                {
                    db.Pictures.Where(pic1 => pic1.Id == pic.Id).FirstOrDefault().PictureHistory = textBoxHistory.Text;
                }
                if (!textBoxPrice.Text.Equals(pic.Price))
                {
                    db.Pictures.Where(pic1 => pic1.Id == pic.Id).FirstOrDefault().Price = Convert.ToInt32(textBoxPrice.Text);
                }
                if (!textBoxSizeX.Text.Equals(pic.Size.X.ToString()) || !textBoxSizeY.Text.Equals(pic.Size.Y.ToString()))
                {
                    db.Pictures.Where(pic1 => pic1.Id == pic.Id).FirstOrDefault().Size = new Point(Convert.ToInt32(textBoxSizeX.Text), Convert.ToInt32(textBoxSizeY.Text));
                }
                if (!dateTimePickerWrite.Value.Equals(dateTimePickerWrite))
                {
                    db.Pictures.Where(pic1 => pic1.Id == pic.Id).FirstOrDefault().WriteDate = dateTimePickerWrite.Value;
                }
                if ((comboBoxArtist.SelectedItem as Artist).Id != pic.ArtistId)
                {
                    db.Pictures.Where(pic1 => pic1.Id == pic.Id).FirstOrDefault().ArtistId = (comboBoxArtist.SelectedItem as Artist).Id;
                }
                if ((comboBoxDepartment.SelectedItem as Department).Id != pic.DepartmentId)
                {
                    db.Pictures.Where(pic1 => pic1.Id == pic.Id).FirstOrDefault().DepartmentId = (comboBoxDepartment.SelectedItem as Department).Id;
                }
                db.SaveChanges();
                MessageBox.Show("Успешно!");
                Close();
            }
        }
    }
}
