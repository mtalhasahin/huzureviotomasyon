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
    public partial class Doktor : Form
    {
        public Doktor()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(" Data Source=.;Initial Catalog=HuzurEviOtomasyonu;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        DataTable dt = new DataTable();
        public void Listele()
    {
        dt.Clear();
        SqlDataAdapter da = new SqlDataAdapter("Select *From Yenikayit", con);
        da.Fill(dt);
        dataGridView1.DataSource = dt;
    }
        private void button1_Click(object sender, EventArgs e)
        {
            IlacTakipKaydı Itk = new IlacTakipKaydı();
            Itk.ShowDialog();
        }
        public void Tarih()
        {
            DateTime tarih = DateTime.Now;
            label20.Text = tarih.ToString();


        }
        private void Doktor_Load(object sender, EventArgs e)
        {
            Listele();
            Tarih();
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
                SqlDataAdapter da = new SqlDataAdapter("Select *From Yenikayit where KimlikNo Like '%" + textBox4.Text + "%'", con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

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

        private void Doktor_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Giris g1 = new Giris();
            g1.Show();
            this.Hide();
        }
    }
}
