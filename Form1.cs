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
            foreach (DataGridViewColumn column in dgv.Columns)
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

                IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);

                DataSet result = excelReader.AsDataSet();
                dataGridView1.DataSource = result.Tables[0];

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
                ds.WriteXml(File.OpenWrite(@"D:\XMLDosyasi.xml"));
                //MessageBox.Show("Kaydedildi");
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

            long ms = Convert.ToInt64(timer1.Interval);
            TimeSpan t = TimeSpan.FromMilliseconds(ms);
            string minute = Convert.ToString(t.Minutes);
            label1.Text = string.Format(minute);
            int deger = Convert.ToInt32(label1.Text);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Increment = 60000; //Artış miktarı

            numericUpDown1.Minimum = 60000;  //10000 : 10 sn

            timer1.Interval = (int)numericUpDown1.Value;
           
        }

        private void buttonTimer_Click(object sender, EventArgs e)
        {
            timer1.Stop();  
        }
     
        private void buttonSifirla_Click(object sender, EventArgs e)
        {
            label1.Text = "0";
        }
    }
}
