using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using ExcelApp = Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;
using ExcelDataReader;


namespace FormXmlKullanimi
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)      //Data Grid Viewden veriler Data Table a çekiliyor
            {
                if (column.Visible)
                {

                    dt.Columns.Add(column.HeaderText);
                }
            }
            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }
            return dt;
        }

        private void ExcelCek()
        {
            try
            {
                FileStream stream = File.Open(@"D:\\urun-takip.xlsx", FileMode.Open, FileAccess.Read);

                IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);     //DataSet ile sabit olarak verilen Excel dosyasındaki veriler çekiliyor.

                DataSet result = excelReader.AsDataSet();
                dataGridView1.DataSource = result.Tables[0];   //DataGridView e yükleniyor

                excelReader.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error in Import");
                return;
            }

            try
            {
                DataTable dt = GetDataTableFromDGV(dataGridView1);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.WriteXml(File.OpenWrite(@"D:\XMLDosyasi.xml"));           //GetDataTableFromDGV fonksiyonu çağrılarak Data table ile xml dönüşümü yapılıyor.
                //MessageBox.Show("Kaydedildi");                             //Verilen isimle belirtilen dosya yoluna dönüştürülmüş xml dosyası kaydediliyor.
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ExcelCek();
            label1.Text = "0";
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        { 
            ExcelCek();
            timer1.Tick += new EventHandler(timer1_Tick);
            Form1 formaktif = new Form1();
            formaktif.Show();
        }

        private void buttonyeniForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDosyaYukle formYeni = new FormDosyaYukle();
            formYeni.Show();
        }
        
        private void buttonTimerBaslat_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;

            long ms = Convert.ToInt64(timer1.Interval);                 //timer1 in artış değeri ms den dk ya çevrildi.
            TimeSpan t = TimeSpan.FromMilliseconds(ms);
            string minute = Convert.ToString(t.Minutes);
            label1.Text = string.Format(minute);                        //Dakika cinsinden label da gözükmesi sağlandı.
            int deger = Convert.ToInt32(label1.Text);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Increment = 60000; //Artış miktarı

            numericUpDown1.Minimum = 60000;  //10000 : 10 sn

            timer1.Interval = (int)numericUpDown1.Value;            //Timer ın interval değeri olarak numeric up downda girilen değer alınıyor.
           
        }

        private void buttonTimer_Click(object sender, EventArgs e)
        {
            timer1.Stop();
           
        }       
    }
}
