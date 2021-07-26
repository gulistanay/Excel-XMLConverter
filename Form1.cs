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
                FileStream stream = File.Open(@"D:\\avturk\\urun-takip.xlsx", FileMode.Open, FileAccess.Read);

                IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);

                DataSet result = excelReader.AsDataSet();
                dataGridView1.DataSource = result.Tables[0];

                excelReader.Close();

                if (dataGridView1.Columns[0].HeaderText != null)
                {
                    dataGridView1.Columns[0].HeaderText = "ÜrünKodu";
                    dataGridView1.Columns[1].HeaderText = "ÜrünAdı";
                    dataGridView1.Columns[2].HeaderText = "SatışDurum";
                    dataGridView1.Columns[3].HeaderText = "Kategori";
                    dataGridView1.Columns[4].HeaderText = "AltKategori";
                    dataGridView1.Columns[5].HeaderText = "Stok";
                    dataGridView1.Columns[6].HeaderText = "Maliyet";
                    dataGridView1.Columns[7].HeaderText = "Satış";
                    dataGridView1.Columns[8].HeaderText = "EnDipFiyat";
                    dataGridView1.Columns[9].HeaderText = "Desi";
                    dataGridView1.Columns[10].HeaderText = "Notlar";
                    dataGridView1.Columns[11].HeaderText = "N11TRY";
                    dataGridView1.Columns[12].HeaderText = "N11_";
                    dataGridView1.Columns[13].HeaderText = "N11Kom";
                    dataGridView1.Columns[14].HeaderText = "N11Kalan";
                    dataGridView1.Columns[15].HeaderText = "N11Link";
                    dataGridView1.Columns[16].HeaderText = "N11Sıra";
                    dataGridView1.Columns[17].HeaderText = "HBTRY";
                    dataGridView1.Columns[18].HeaderText = "HB_";
                    dataGridView1.Columns[19].HeaderText = "HBKom";
                    dataGridView1.Columns[20].HeaderText = "HBKalan";
                    dataGridView1.Columns[21].HeaderText = "HBLink";
                    dataGridView1.Columns[22].HeaderText = "HBSıra";
                    dataGridView1.Columns[23].HeaderText = "TRDTRY";
                    dataGridView1.Columns[24].HeaderText = "TRD_";
                    dataGridView1.Columns[25].HeaderText = "TRDKom";
                    dataGridView1.Columns[26].HeaderText = "TRDKalan";
                    dataGridView1.Columns[27].HeaderText = "TRDLink";
                    dataGridView1.Columns[28].HeaderText = "TRDSıra";
                    dataGridView1.Columns[29].HeaderText = "GGTRY";
                    dataGridView1.Columns[30].HeaderText = "GG_";
                    dataGridView1.Columns[31].HeaderText = "GGKom";
                    dataGridView1.Columns[32].HeaderText = "GGKalan";
                    dataGridView1.Columns[33].HeaderText = "GGLink";
                    dataGridView1.Columns[34].HeaderText = "GGSıra";
                    dataGridView1.Columns[35].HeaderText = "ÇSPTRY";
                    dataGridView1.Columns[36].HeaderText = "ÇSP_";
                    dataGridView1.Columns[37].HeaderText = "ÇSPKom";
                    dataGridView1.Columns[38].HeaderText = "ÇSPKalan";
                    dataGridView1.Columns[39].HeaderText = "ÇSPLink";
                    dataGridView1.Columns[40].HeaderText = "ÇSPSıra";
                    dataGridView1.Columns[41].HeaderText = "AMZTRY";
                    dataGridView1.Columns[42].HeaderText = "AMZ_";
                    dataGridView1.Columns[43].HeaderText = "AMZKom";
                    dataGridView1.Columns[44].HeaderText = "AMZKalan";
                    dataGridView1.Columns[45].HeaderText = "AMZLink";
                    dataGridView1.Columns[46].HeaderText = "AMZSıra";
                    dataGridView1.Columns[47].HeaderText = "WEBTRY";
                    dataGridView1.Columns[48].HeaderText = "WEB_";
                    dataGridView1.Columns[49].HeaderText = "WEBKom";
                    dataGridView1.Columns[50].HeaderText = "WEBKalan";
                    dataGridView1.Columns[51].HeaderText = "WEBLink";

                    dataGridView1.Rows.RemoveAt(0);
                }

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
                ds.WriteXml(File.OpenWrite(@"D:\avturk\XMLDosyasi.xml"));
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
