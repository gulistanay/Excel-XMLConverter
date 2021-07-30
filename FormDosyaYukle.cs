using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace FormXmlKullanimi
{
    public partial class FormDosyaYukle : Form
    {
        public FormDosyaYukle()
        {
            InitializeComponent();
        }

        DataTable dt;
        private void buttonDosyaSec_Click(object sender, EventArgs e)
        {
            try
            {

                string DosyaYolu;
                string DosyaAdi;

                OpenFileDialog file = new OpenFileDialog();

                file.Filter = "Excel Dosyası | *.xls; *.xlsx; *.xlsm";

                if (file.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    DosyaYolu = file.FileName;// seçilen dosyanın tüm yolunu verir
                    DosyaAdi = file.SafeFileName;// seçilen dosyanın adını verir.
                    ExcelApp.Application excelApp = new ExcelApp.Application();
                    if (excelApp == null)
                    {
                        MessageBox.Show("Excel yüklü değil.");
                        return;
                    }
                    //Excel Dosyası Açılıyor.
                    ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(DosyaYolu);
                    //Excel Dosyasının Sayfası Seçilir.
                    ExcelApp._Worksheet excelSheet = excelBook.Sheets[1];
                    //Excel Dosyasının ne kadar satır ve sütun kaplıyorsa tüm alanları alır.
                    ExcelApp.Range excelRange = excelSheet.UsedRange;
                    int satirSayisi = excelRange.Rows.Count; //Sayfanın satır sayısını alır.
                    int sutunSayisi = excelRange.Columns.Count;//Sayfanın sütun sayısını alır.
                    dt = ToDataTable(excelRange, satirSayisi, sutunSayisi);

                    dt.TableName = DosyaAdi.Split('.')[0]; //XML e kayıt

                    dataGridView1.DataSource = dt;
                    dataGridView1.Refresh();

                    Cursor.Current = Cursors.Default;

                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }
                else
                {
                    MessageBox.Show("Dosya Seçilemedi.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Dosya okunamadı.");
            }
        }

        public DataTable ToDataTable(ExcelApp.Range range, int rows, int cols)
        {
            DataTable table = new DataTable();
            for (int i = 1; i <= rows; i++)
            {
                if (i == 1)
                { // ilk satırı Sutun Adları olarak kullanıldığından
                  // bunları Sutün Adları Olarak Kaydediyoruz.
                    for (int j = 1; j <= cols; j++)
                    {
                        //Sütunların içeriği boş mu kontrolü yapılmaktadır.
                        if (range.Cells[i, j] != null && range.Cells[i, j].Value2 != null)
                            table.Columns.Add(range.Cells[i, j].Value2.ToString());
                        else //Boş olduğunda Kaçınsı Sutünsa Adı veriliyor.
                            table.Columns.Add(j.ToString() + ".Sütun");
                    }
                    continue;
                }
                //Yukarıda Sütunlar eklendi
                // onun şemasına göre yeni bir satır oluşturuyoruz. 
                //Okunan verileri yan yana sıralamak için
                var yeniSatir = table.NewRow();
                for (int j = 1; j <= cols; j++)
                {
                    //Sütunların içeriği boş mu kontrolü yapılmaktadır.
                    if (range.Cells[i, j] != null && range.Cells[i, j].Value2 != null)
                        yeniSatir[j - 1] = range.Cells[i, j].Value2.ToString();
                    else // İçeriği boş hücrede hata vermesini önlemek için
                        yeniSatir[j - 1] = String.Empty;
                }
                table.Rows.Add(yeniSatir);
            }
            return table;


        }

        private void buttonXmlKaydet_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    dt.WriteXml(sfd.FileName);
                    if (MessageBox.Show("XML dosyası hazırlandı. Dosya açılsın mı?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 AnaForm = new Form1();
            AnaForm.Show();
        }
    }
}
