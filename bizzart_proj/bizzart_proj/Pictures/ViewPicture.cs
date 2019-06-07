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
    public partial class ViewPicture : Form
    {
        string login;
        string password;
        private Picture pic;
        public ViewPicture(Picture selectedPic)
        {
            InitializeComponent();
            pic = selectedPic;
        }

        public ViewPicture(Picture selectedPic, string login, string password)
        {
            InitializeComponent();
            pic = selectedPic;
            this.login = login;
            this.password = password;
        }

        private void ViewPicture_Load(object sender, EventArgs e)
        {
            using (var arc = new ArtGalletyContext())
            {
                textBoxName.Text = pic.Name;
                textBoxHistory.Text = pic.PictureHistory;
                textBoxPrice.Text = pic.Price.ToString();
                textBoxGenre.Text = pic.Genre;
                textBoxSizeX.Text = pic.Size.X.ToString();
                textBoxSizeY.Text = pic.Size.Y.ToString();
                textBox1.Text = pic.WriteDate.ToShortDateString();
                textBox2.Text = arc.Artists.Where(c => c.Id == pic.ArtistId).FirstOrDefault().Name;
                textBox3.Text = arc.Departments.Where(c => c.Id == pic.DepartmentId).FirstOrDefault().Name;
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxHistory_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
