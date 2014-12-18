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
    public partial class Giris : Form
    {


        int a = 1;
        public Giris()
        {
            InitializeComponent();
            

            
        }
        public string kullanici;
        void baglantim()
        {

            string kadi = txtKadi.Text;
            string sifre = txtSifre.Text;
            if (rdbYonetici.Checked == true)
            {
                if (txtKadi.Text == "" && txtSifre.Text == "" && rdbDoktor.Checked==false && rdbYonetici.Checked==false)
                {
                    MessageBox.Show("Kullanıcı Adı,Şifre veya Mesleği Seçiniz...");
                }
                try
                {
                    if (kadi != "" && sifre != "")
                    {

                        SqlConnection baglanti = new SqlConnection();
                        baglanti.ConnectionString = " Data Source=.;Initial Catalog=HuzurEviOtomasyonu;Integrated Security=True";
                        SqlCommand sorgu = new SqlCommand("SELECT KullaniciAdi,Sifre FROM KullaniciGiris", baglanti);
                        SqlDataReader okuyucu;
                        bool onay = false;
                        try
                        {
                            baglanti.Open();
                            okuyucu = sorgu.ExecuteReader();
                            while (okuyucu.Read())
                            {
                                if ((kadi == okuyucu["KullaniciAdi"].ToString()) && (sifre == okuyucu["Sifre"].ToString()))
                                {

                                    onay = true;
                                    break;// veri tabanıyla doğru ise kullanıcı girişi yap
                                }
                            }
                            baglanti.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (onay == true)
                        {
                            kullanici = kadi;
                            YeniKayit ynk = new YeniKayit();
                            ynk.Show();
                            ynk.label21.Text = kullanici;
                            this.Hide();

                        }
                        else
                        {
                            baglanti.Close();
                            MessageBox.Show("Kullanıcı Adı ve Şifreyi Yanlış Girdiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtKadi.Text = "";
                            txtSifre.Text = "";
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());

                }

            }
            if (rdbYonetici.Checked == false)
            {
                if (txtKadi.Text == "" && txtSifre.Text == "")
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre Boş Girilemez...");
                }
                try
                {
                    if (kadi != "" && sifre != "")
                    {

                        SqlConnection baglanti = new SqlConnection();
                        baglanti.ConnectionString = " Data Source=.;Initial Catalog=HuzurEviOtomasyonu;Integrated Security=True";
                        SqlCommand sorgu = new SqlCommand("SELECT KullaniciAdi,Sifre FROM Doktor", baglanti);
                        SqlDataReader okuyucu;
                        bool onay = false;
                        try
                        {
                            baglanti.Open();
                            okuyucu = sorgu.ExecuteReader();
                            while (okuyucu.Read())
                            {
                                if ((kadi == okuyucu["KullaniciAdi"].ToString()) && (sifre == okuyucu["Sifre"].ToString()))
                                {

                                    onay = true;
                                    break;// veri tabanıyla doğru ise kullanıcı girişi yap
                                }
                            }
                            baglanti.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (onay == true)
                        {
                            kullanici = kadi;
                            Doktor d1 = new Doktor();
                            d1.Show();
                            d1.label22.Text = kullanici;
                            this.Hide();

                        }
                        else
                        {
                            baglanti.Close();
                            MessageBox.Show("Kullanıcı Adı ve Şifreyi Yanlış Girdiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtKadi.Text = "";
                            txtSifre.Text = "";
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());

                }

            }
        }
                       
            
        
        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            baglantim();
        }

        private void Giris_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            a = 2;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            a = 1;
        }

        private void txtKadi_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
