using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace huzurEviotomasyon
{
    public partial class YeniKayit : Form
    {
        
        SqlConnection con = new SqlConnection(" Data Source=.;Initial Catalog=HuzurEviOtomasyonu;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        DataTable dt = new DataTable();
       
        
        public YeniKayit()
        {
            InitializeComponent();
            
        }
        public void listele()
        {
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("Select *From Yenikayit",con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }
        
        public void Tarih()
        {
            DateTime tarih = DateTime.Now;
            label20.Text = tarih.ToString();
            
        
        }
        TckimlikNo Sorgulama = new TckimlikNo();
        private void YeniKayit_Load(object sender, EventArgs e)
        {

            //datagridview deki isimler düzeltildi
            listele();
            dataGridView1.Columns[1].HeaderText = "Kimlik Numarası";
            dataGridView1.Columns[4].HeaderText = "Doğum Tarihi";
            dataGridView1.Columns[5].HeaderText = "Doğum Yeri";
            dataGridView1.Columns[6].HeaderText = "Baba Adı";
            dataGridView1.Columns[7].HeaderText = "Anne Adı";
            dataGridView1.Columns[9].HeaderText = "Eğitim Durumu";
            dataGridView1.Columns[13].HeaderText = "Kullandığı ilaçlar";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Tarih();
        }// Bu kod ile de datagridview'in tüm satırlarını seçilmesi amaçlandı

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            
        }

        public void Temizle()
        {
            //Temizle butonu oluşturuldu

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            maskedTextBox3.Text = "";
            comboBox4.SelectedItem = null;
            textBox8.Text = "";
            textBox12.Text = "";
            comboBox1.SelectedItem =null;
            comboBox2.SelectedItem =null;
            maskedTextBox4.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            maskedTextBox2.Text = "";
            maskedTextBox1.Text = "";
            
        }
        
        public void Kaydet(){
               

        
         if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && Sorgulama.TcNoSorgula(textBox1.Text))
            { //Trim() kodu ile boşlukları sayması engellendi


                
                try
                {
                    
                    con.Open();
                    string DogumTarihi = maskedTextBox3.Text;
                    string Ceptel = maskedTextBox2.Text;
                    string Evtel = maskedTextBox1.Text;
                    komut.Connection = con;
                    komut.CommandText = "Insert Into Yenikayit(KimlikNo,Ad,Soyad,DogumYeri,BabaAdi,AnneAdi,Cinsiyet,EgitimDurum,Hobiler,Fobiler,KullandigiIlac,Rahatsızlıklar,DogumTarihi,Ceptel)Values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox4.Text + "','" + textBox8.Text + "','" + textBox12.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','"+maskedTextBox3.Text+"','"+maskedTextBox2.Text+"')";
                    komut.ExecuteNonQuery();
                    
                 //   for (int i = 0; i < this.Controls.Count; i++)
                 //   {

                 //       if (Controls[i] is TextBox) Controls[i].Enabled = false;
                 //   }
                    con.Close();
                    listele();
                    Temizle();
                    MessageBox.Show("kayıt işlemi gerçekleştirildi.");
                    
                    
                    
                }
                
                catch (Exception)
                {

                    con.Close();
                    MessageBox.Show("Alanları Doğru Doldurunuz...");


                }
            }
            else 
             MessageBox.Show("Tc kimlik Numarasını Yanlış Girdiniz");
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            
            Kaydet();
            Temizle();
            
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Kimlik No'da bulunan textbox'a rakam hariç başka giriş yapılmaması engellendi
            if ((int)e.KeyChar >= 47 && (int)e.KeyChar <= 58)
            {
                e.Handled = false;
            }
            else if ((int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Silme Menüsü", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Delete From Yenikayit where KimlikNo='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "' "; //Silme işlemi Kimlik No ya göre gerçekleşti
            cmd.ExecuteNonQuery();
            con.Close();
            listele();
            MessageBox.Show("Silme işlemi gerçekleştirdi...");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim() == "")
            {//tüm kayıtları göster

                dt.Clear();
                SqlDataAdapter da = new SqlDataAdapter("Select *From Yenikayit", con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                //Kimlik No'ya göre arama gerçekleşti 'Like' kodu ile de aramada eleme yapıldı
                dt.Clear();
                SqlDataAdapter da = new SqlDataAdapter("Select *From Yenikayit where KimlikNo Like '%"+textBox4.Text+"%'", con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Trim() == "")
            {//tüm kayıtları göster

                dt.Clear();
                SqlDataAdapter da = new SqlDataAdapter("Select *From Yenikayit", con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                //Ad'a göre arama işlemi gerçekleşti
                dt.Clear();
                SqlDataAdapter da = new SqlDataAdapter("Select *From Yenikayit where Ad Like'%" + textBox5.Text + "%'", con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (textBox13.Text.Trim() == "")
            {//tüm kayıtları göster

                dt.Clear();
                SqlDataAdapter da = new SqlDataAdapter("Select *From Yenikayit", con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                //soyada göre arama yap işlemi gerçekleşti
                dt.Clear();
                SqlDataAdapter da = new SqlDataAdapter("Select *From Yenikayit where Soyad Like'%" + textBox13.Text + "%'", con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
        }

        private void btnKapat_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)

        {
            Duzenle dzn = new Duzenle();
           dzn.ShowDialog();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //rakam girişi engellendi ve backspace tuş kontrolü yapıldı
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;
            
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //rakam girişi engellendi ve backspace tuş kontrolü yapıldı
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;

            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            //rakam girişi engellendi ve backspace tuş kontrolü yapıldı
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;

            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            //rakam girişi engellendi ve backspace tuş kontrolü yapıldı
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar!=(char) 08)
            {
                e.Handled = true;

            }
        }

        private void textBox2_Leave(object sender, System.EventArgs e)
        {
               

        }

        private void YeniKayit_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Kapatmak istediğinize emin misiniz", "Kapatma Ekranı", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (sonuc == DialogResult.OK)
            {
                Environment.Exit(0);
               
            }
            else
            {
                e.Cancel = true;
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int secilen = comboBox3.SelectedIndex;
            switch (secilen)
            {
                case 0: pictureBox1.Image = Resimler.resim_1; break;
                case 1: pictureBox1.Image = Resimler.resim_2; break;
                case 2: pictureBox1.Image = Resimler.resim_3; break;
                case 3: pictureBox1.Image = Resimler.resim_4; break;
                case 4: pictureBox1.Image = Resimler.resim_5; break;
                default:
                    pictureBox1.Image = Resimler.resim_6; break;
            
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;

            }
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;

            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 47 && (int)e.KeyChar <= 58)
            {
                e.Handled = false;
            }
            else if ((int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {

                if (Controls[i] is TextBox) Controls[i].Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ZiyaretciTakip zt = new ZiyaretciTakip();
            zt.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            IlacTakipKaydı Itk = new IlacTakipKaydı();
            Itk.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = " Data Source=.;Initial Catalog=HuzurEviOtomasyonu;Integrated Security=True";
            string komutcumle = "backup database HuzurEviOtomasyonu to disk='yedekle'";
            SqlCommand komut = new SqlCommand(komutcumle, baglanti);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Giris g1 =new Giris();
            g1.Show();
            this.Hide();
            
        }

        private void maskedTextBox4_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnKaydet_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Raporla rapor = new Raporla();
            rapor.ShowDialog();
            
        }
    }
}
