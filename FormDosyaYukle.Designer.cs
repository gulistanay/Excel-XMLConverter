
namespace FormXmlKullanimi
{
    partial class FormDosyaYukle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonDosyaSec = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonXmlKaydet = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonDosyaSec
            // 
            this.buttonDosyaSec.Location = new System.Drawing.Point(571, 53);
            this.buttonDosyaSec.Name = "buttonDosyaSec";
            this.buttonDosyaSec.Size = new System.Drawing.Size(173, 33);
            this.buttonDosyaSec.TabIndex = 0;
            this.buttonDosyaSec.Text = "Dosya Seç";
            this.buttonDosyaSec.UseVisualStyleBackColor = true;
            this.buttonDosyaSec.Click += new System.EventHandler(this.buttonDosyaSec_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(54, 111);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(690, 239);
            this.dataGridView1.TabIndex = 1;
            // 
            // buttonXmlKaydet
            // 
            this.buttonXmlKaydet.Location = new System.Drawing.Point(283, 377);
            this.buttonXmlKaydet.Name = "buttonXmlKaydet";
            this.buttonXmlKaydet.Size = new System.Drawing.Size(263, 61);
            this.buttonXmlKaydet.TabIndex = 2;
            this.buttonXmlKaydet.Text = "XML Olarak Kaydet";
            this.buttonXmlKaydet.UseVisualStyleBackColor = true;
            this.buttonXmlKaydet.Click += new System.EventHandler(this.buttonXmlKaydet_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(688, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 75);
            this.button1.TabIndex = 3;
            this.button1.Text = "Ana Forma Dön";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormDosyaYukle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(799, 464);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonXmlKaydet);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonDosyaSec);
            this.Name = "FormDosyaYukle";
            this.Text = "FormDosyaYukle";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDosyaSec;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonXmlKaydet;
        private System.Windows.Forms.Button button1;
    }
}