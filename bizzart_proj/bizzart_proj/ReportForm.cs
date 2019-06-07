using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace bizzart_proj
{
    public partial class ReportForm : Form
    {
        ArtGalletyContext db = new ArtGalletyContext();
        public ReportForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    List<InfoArtist> infoa = new List<InfoArtist>();
                    foreach (var s in db.Artists.ToList())
                    {
                        string picnames = "";
                        foreach(string str in db.Pictures.Where(c => c.ArtistId == s.Id ).Select(k => k.Name).ToList())
                        {
                            picnames += str + ";";
                        }
                        infoa.Add(new InfoArtist { 
                            Birth = s.BirthDate,
                            id = s.Id,
                            pictures = picnames,
                            name = s.Name,
                            deathDate = s.DeathDate,
                            birthPlace = s.BirthPlace,
                            bio = s.Biography,
                        });
                    }
                    dataGridView1.DataSource = infoa;
                    break;
                case 1:
                    List<InfoPicture> infop = new List<InfoPicture>();
                    foreach (var s in db.Pictures.ToList())
                    {
                        infop.Add(new InfoPicture { 
                            Id = s.Id,
                            Name = s.Name,
                            PictureHistory = s.PictureHistory,
                            Price = s.Price,
                            Department = db.Departments.Where(dep => dep.Id == s.DepartmentId).FirstOrDefault().Name,
                            Genre = s.Genre,
                            Size = s.Size.X.ToString() + " x " + s.Size.Y.ToString(),
                            WriteDate = s.WriteDate,
                            Artist = db.Artists.Where(a => a.Id == s.ArtistId).FirstOrDefault().Name
                        });
                    }
                    infop.Sort(new NameSort());
                    dataGridView1.DataSource = infop;
                    break;
                case 2:
                    List<InfoUser> infou = new List<InfoUser>();
                    foreach (var s in db.Users.ToList())
                    {
                        infou.Add(new InfoUser
                        { 
                            Id = s.Id,
                            BirthDate = s.BirthDate,
                            Department = db.Departments.Where(dep => dep.Id == s.DepartmentId).FirstOrDefault().Name,
                            EmploymentDate = s.EmploymentDate,
                            Name = s.Name,
                            Role = db.Roles.Where(r => r.RoleId == s.RoleId).FirstOrDefault().RoleName,
                        });
                    }
                    dataGridView1.DataSource = infou;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для выгрузки в Excel!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Выгрузить найденные строки в Excel?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            Excel.Application xlApp;
            Excel.Workbook xlWB;
            Excel.Worksheet xlSht;
            Excel.Range Rng;

            xlApp = new Excel.Application();
            xlWB = xlApp.Workbooks.Add();
            xlSht = xlWB.Worksheets[1]; //первый по порядку лист в книге Excel

            int RowsCount = this.dataGridView1.RowCount;
            int ColumnsCount = this.dataGridView1.ColumnCount;
            object[,] arrData = new object[RowsCount, ColumnsCount];

            for (int j = 0; j < RowsCount; j++)
                for (int i = 0; i < ColumnsCount; i++)
                    if (j != this.dataGridView1.NewRowIndex)
                    {
                        if(this.dataGridView1.Rows[j].Cells[i].Value != null)
                            arrData[j, i] = this.dataGridView1.Rows[j].Cells[i].Value.ToString();
                        else if (this.dataGridView1.Rows[j].Cells[i].Value == null)
                            arrData[j, i] = "Не назначено";
                    }

            //выгрузка данных на лист Excel
            xlSht.Range["A2"].Resize[arrData.GetUpperBound(0) + 1, arrData.GetUpperBound(1) + 1].Value = arrData;
            //переносим названия столбцов в Excel файл
            for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
                xlSht.Cells[1, j + 1] = this.dataGridView1.Columns[j].HeaderCell.Value.ToString();

            //украшательство таблицы
            xlSht.Cells[1, 1].CurrentRegion.Borders.LineStyle = Excel.XlLineStyle.xlContinuous; //границы
            xlSht.Rows[1].Font.Bold = true;
            xlSht.Range["A:H"].EntireColumn.AutoFit();

            //отображаем Excel
            xlApp.Visible = true;
        }
    }
    public class InfoArtist
    {
        public int id { get; set; }
        public string name { get; set; }
        public string pictures { get; set; }
        public DateTime Birth { get; set; }
        public DateTime deathDate { get; set; }
        public string bio { get; set; }
        public string birthPlace { get; set; }
    }

    public class InfoPicture
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Genre { get; set; }
        public String Artist { get; set; }
        public DateTime WriteDate { get; set; }
        public int Price { get; set; }
        public String Size { get; set; }
        public String Department { get; set; }
        public String PictureHistory { get; set; }
    }
    public class InfoUser
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public String Role { get; set; }
        public String Department { get; set; }
    }
    public class NameSort : IComparer<InfoPicture>
    {
        public int Compare(InfoPicture a, InfoPicture b)
        {
            return a.Name.CompareTo(b.Name);
        }
    }

}
