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
            if (!AuthorizationLogics.CheckForRole(login, password, new List<int> { 6 }))
            {
                MessageBox.Show("У вас недостаточно прав для использования этой функции.");
                return;
            }
            var addPic = new bizzart_proj.Pictures.AddPicture();
            addPic.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!AuthorizationLogics.CheckForRole(login, password, new List<int> { 6 }))
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
            if (!AuthorizationLogics.CheckForRole(login, password, new List<int> { 1, 6 }))
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
            // параметра передать Missing.Value
            System.Reflection.Missing missingValue = System.Reflection.Missing.Value;

            //создаем и инициализируем объекты Excel
            Microsoft.Office.Interop.Excel.Application App;
            Microsoft.Office.Interop.Excel.Workbook xlsWB;
            Microsoft.Office.Interop.Excel.Worksheet xlsSheet;

            App = new Microsoft.Office.Interop.Excel.Application();
            //добавляем в файл Excel книгу. Параметр в данной функции - используемый для создания книги шаблон.
            //если нас устраивает вид по умолчанию, то можно спокойно передавать пустой параметр.
            xlsWB = App.Workbooks.Add(missingValue);
            //и использует из нее
            xlsSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsWB.Worksheets.get_Item(1);

            List<string[]> rows = new List<string[]>();
            rows.Add(new string[] { "1", "2", "3", "4" });
            rows.Add(new string[] { "5", "6", "7", "8" });
            rows.Add(new string[] { "9", "10", "11", "12" });
            for (int i = 0; i < rows.Count; i++)
            {
                for (int j = 0; j < rows[i].Length; j++)
                {
                    xlsSheet.Cells[i + 1, j + 1] = rows[i][j];
                }
            }

            xlsWB.Close(true, missingValue, missingValue);
            //закрываем приложение
            App.Quit();  
            //уменьшаем счетчики ссылок на COM объекты, что, по идее должно их освободить.
            //почему это не произойдет - читайте ниже ;)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWB);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(App); 
        }
    }
}
