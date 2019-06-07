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
    public partial class AddPicture : Form
    {
        string login;
        string password;
        public AddPicture()
        {
            InitializeComponent();
            textBoxSizeX.KeyPress += _txtPath_KeyDown;
            textBoxPrice.KeyPress += _txtPath_KeyDown;
            textBoxSizeY.KeyPress += _txtPath_KeyDown;
        }
        public AddPicture(string login, string password )
        {
            InitializeComponent();
            textBoxSizeX.KeyPress += _txtPath_KeyDown;
            textBoxPrice.KeyPress += _txtPath_KeyDown;
            textBoxSizeY.KeyPress += _txtPath_KeyDown;
            this.login = login;
            this.password = password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!LogicsSmthg.CheckForRole(login, password, new List<int> { 2 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            ArtGalletyContext agc = new ArtGalletyContext();
            Artist art = comboBoxArtist.SelectedItem as Artist;
            Department dep = comboBoxDepartment.SelectedItem as Department;
            Picture pic = new Picture
            {
                Name = textBoxName.Text,
                Genre = textBoxGenre.Text,
                WriteDate = dateTimePickerWrite.Value,
                Price = int.Parse(textBoxPrice.Text),
                Size = new Point(int.Parse(textBoxSizeX.Text),int.Parse(textBoxSizeY.Text)),
                PictureHistory = textBoxHistory.Text,
                Artist = agc.Artists.Where(artist => artist.Id == (art).Id).FirstOrDefault(),
                Department = agc.Departments.Where(department => department.Id == (dep).Id).FirstOrDefault()
                
            };
            agc.Pictures.Add(pic);
            agc.SaveChanges();
            MessageBox.Show("Успешно!");
            Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void _txtPath_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back
                & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void AddPicture_Load(object sender, EventArgs e)
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
            }
        }
    }
}
