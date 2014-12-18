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
    public partial class Duzenle : Form
    {
        public Giris grs;
        public Duzenle()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(" Data Source=.;Initial Catalog=HuzurEviOtomasyonu;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        private void Duzenle_Load(object sender, EventArgs e)
        {
            YeniKayit ynk = new YeniKayit();
            textBox1.Text = ynk.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = ynk.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text =ynk.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox4.Text = ynk.dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox8.Text = ynk.dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox12.Text = ynk.dataGridView1.CurrentRow.Cells[7].Value.ToString();
            comboBox1.Text = ynk.dataGridView1.CurrentRow.Cells[8].Value.ToString();
            comboBox2.Text = ynk.dataGridView1.CurrentRow.Cells[9].Value.ToString();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

            
        }
        public void Temizle()
        {
            //Temizle butonu oluşturuldu

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            maskedTextBox3.Text = "";
            comboBox4.SelectedItem = "";
            textBox8.Text = "";
            textBox12.Text = "";
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            maskedTextBox4.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            maskedTextBox2.Text = "";
            maskedTextBox1.Text = "";

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "")
            {


                con.Open();
                komut.Connection = con;
                komut.CommandText = "Update Yenikayit Set Ad='" + textBox2.Text + "',Soyad='" + textBox3.Text + "',DogumYeri='" + comboBox4.Text + "',BabaAdi='" + textBox8.Text + "',AnneAdi='" + textBox12.Text + "',Cinsiyet='" + comboBox1.Text + "',EgitimDurum='" + comboBox2.Text + "' where KimlikNo='" + textBox1.Text + "'";
                komut.ExecuteNonQuery();
                con.Close();
                YeniKayit ynk = new YeniKayit();
                ynk.listele();
                Temizle();
                MessageBox.Show("Güncelleme İşlemi Gerçekleşti...");
                this.Close();
            }
            else 
            {
                MessageBox.Show("Boş alan bırakmayınız");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;

            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;

            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;

            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;

            }
        }
    }
}
