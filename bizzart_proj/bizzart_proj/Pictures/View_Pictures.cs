using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using Microsoft.Office.Interop.Excel;

namespace bizzart_proj
{
    public partial class View_Pictures : Form
    {
        private string login;
        private string password;
        public View_Pictures(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
            listBox1.SelectedValueChanged += listBox1_SelectedValueChanged;
        }

        void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Picture pic = listBox1.SelectedItem as Picture;
            try
            {
                if (pic != null)
                {
                    using (ArtGalletyContext db = new ArtGalletyContext())
                    {
                        label3.Text = (listBox1.SelectedItem as Picture).Name;
                        label4.Text = (listBox1.SelectedItem as Picture).Genre;
                        pictureBox1.ImageLocation = (listBox1.SelectedItem as Picture).ImagePath;
                        Picture selectedPic = listBox1.SelectedItem as Picture;
                        Artist selectedArt = db.Artists.Where(
                            art1 => art1.Id == selectedPic.ArtistId
                            ).FirstOrDefault();
                        if (selectedArt == null)
                        {
                            label5.Text = "Художник не назначен";
                        }
                        else
                        {
                            label5.Text = selectedArt.Name;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void View_Pictures_Load(object sender, EventArgs e)
        {
            Update_window();           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!LogicsSmthg.CheckForRole(login, password, new List<int> { 2 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            var addPic = new bizzart_proj.Pictures.AddPicture();
            addPic.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!LogicsSmthg.CheckForRole(login, password, new List<int> { 2 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            if (listBox1.SelectedItem != null)
            {
                DialogResult res = MessageBox.Show( "Удалить выбранную картину(ы)?", "Удалить картину", MessageBoxButtons.OKCancel);
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    using (var db = new ArtGalletyContext())
                    {
                        foreach(var a in listBox1.SelectedItems)
                        {
                            Picture pic = a as Picture;
                            db.Pictures.Remove(db.Pictures.Where(
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

        private void button6_Click(object sender, EventArgs e)
        {
            Update_window();
        }
        private void Update_window()
        {
            listBox1.Items.Clear();
            ArtGalletyContext db = new ArtGalletyContext();
            foreach (var s in db.Pictures.ToList())
            {
                listBox1.Items.Add(s);
                listBox1.DisplayMember = "Name";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!LogicsSmthg.CheckForRole(login, password, new List<int> { 2, 3 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            if (listBox1.SelectedItem != null)
            {
                var editPic = new Pictures.EditPicture(listBox1.SelectedItem as Picture);
                editPic.Visible = true;
            }
            else MessageBox.Show("Выберите картину.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var makeRep = new bizzart_proj.ReportForm();
            makeRep.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!LogicsSmthg.CheckForRole(login, password, new List<int> { 2, 3 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            if (listBox1.SelectedItem != null)
            {
                var sendPic = new SendPicture(login, password,listBox1.SelectedItem as Picture);
                sendPic.Visible = true;
            }
            else MessageBox.Show("Выберите картину.");
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!LogicsSmthg.CheckForRole(login, password, new List<int> { 2 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            var addPic = new bizzart_proj.Pictures.AddPicture();
            addPic.Visible = true;
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!LogicsSmthg.CheckForRole(login, password, new List<int> { 2, 3 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            if (listBox1.SelectedItem != null)
            {
                var editPic = new Pictures.EditPicture(listBox1.SelectedItem as Picture);
                editPic.Visible = true;
            }
            else MessageBox.Show("Выберите картину.");
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!LogicsSmthg.CheckForRole(login, password, new List<int> { 2 }))
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
                            Picture pic = a as Picture;
                            db.Pictures.Remove(db.Pictures.Where(
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

        private void отправитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!LogicsSmthg.CheckForRole(login, password, new List<int> { 2, 3 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            if (listBox1.SelectedItem != null)
            {
                var sendPic = new SendPicture(login, password, listBox1.SelectedItem as Picture);
                sendPic.Visible = true;
            }
            else MessageBox.Show("Выберите картину.");
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var viewPic = new bizzart_proj.Pictures.ViewPicture(listBox1.SelectedItem as Picture, login, password);
            viewPic.Visible = true;
        }

        private void видToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void отчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var makeRep = new bizzart_proj.ReportForm();
            makeRep.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            ArtGalletyContext db = new ArtGalletyContext();
            List<Picture> u1 = db.Pictures.ToList();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    u1.Sort(new PictureSortByName());
                    listBox1.Items.AddRange(u1.ToArray());
                    break;
                case 1:
                    u1.Sort(new PictureSortByWriteDate());
                    listBox1.Items.AddRange(u1.ToArray());
                    break;
            }
        }
        private class PictureSortByName : IComparer<Picture>
        {
            public int Compare(Picture a, Picture b)
            {
                return a.Name.CompareTo(b.Name);
            }
        }

        private class PictureSortByWriteDate : IComparer<Picture>
        {
            public int Compare(Picture a, Picture b)
            {
                return a.WriteDate.CompareTo(b.WriteDate);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
