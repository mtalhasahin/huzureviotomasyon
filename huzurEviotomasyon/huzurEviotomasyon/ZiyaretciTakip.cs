﻿using System;
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
    public partial class ZiyaretciTakip : Form
    {
        
        SqlConnection con = new SqlConnection(" Data Source=.;Initial Catalog=HuzurEviOtomasyonu;Integrated Security=True");
        SqlCommand kmt = new SqlCommand();
        DataTable dt = new DataTable();
        public ZiyaretciTakip()
        {
            InitializeComponent();
        }
        
        public void ListeleZiyaret()
        {
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("Select *From Ziyaret",con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        TckimlikNo sorgulama = new TckimlikNo();
        public void ZiyaretKaydet()
        {
            if (textBox4.Text.Trim() != "" && textBox2.Text.Trim() != "" && comboBox3.Text.Trim() != "" && textBox6.Text.Trim() != "" && textBox5.Text.Trim() != "" && comboBox1.Text.Trim() != "" && comboBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && sorgulama.TcNoSorgula(textBox4.Text))
            {
                try
                {
                    con.Open();
                    string Tarih = maskedTextBox2.Text;
                    string TelNo = maskedTextBox1.Text;
                    kmt.Connection = con;
                    kmt.CommandText = "Insert Into Ziyaret(KimlikNo,ZiyaretAdSoyad,SakinAdSoyad,Aciklama,Adres,Yakinlik,ZiyaretYeri,ZiyaretAdres)Values('" + textBox4.Text + "','" + textBox2.Text + "','" + comboBox3.Text + "','" + textBox6.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox3.Text + "')";
                    kmt.ExecuteNonQuery();
                    con.Close();
                    ListeleZiyaret();

                    MessageBox.Show("Kayıt İşleminiz Gerçekleşmiştir...");
                }
                catch (Exception)
                {
                    con.Close();
                   
                }
            }
            else
                MessageBox.Show("Tc kimlik Numarası Yanlış..");
        }
        public void Sil() {

            DialogResult cevap;
            cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Silme Menüsü", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {

                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "Delete From Ziyaret where KimlikNo='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "' "; //Silme işlemi Kimlik No ya göre gerçekleşti
                cmd.ExecuteNonQuery();
                con.Close();
                ListeleZiyaret();
                MessageBox.Show("Silme işlemi gerçekleştirdi...");
            }
        }
        public void Temizle()
        {
            //Temizle butonu oluşturuldu

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            textBox6.Text = "";
            maskedTextBox2.Text = "";
            maskedTextBox1.Text = "";

        }
        private void ZiyaretciTakip_Load(object sender, EventArgs e)
        {
            ListeleZiyaret();
            con.Open();
            SqlCommand com = new SqlCommand("select Ad,Soyad from Yenikayit", con);
            com.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = com.ExecuteReader();
            while (dr.Read())
            {

                comboBox3.Items.Add(dr["Ad"]);
                
            }
            con.Close();
            dataGridView1.Columns[1].HeaderText = "Kimlik Numarası";
            dataGridView1.Columns[2].HeaderText = "Ziyaretçi Ad Soyad";
            dataGridView1.Columns[3].HeaderText = "Sakin Ad Soyad";
            dataGridView1.Columns[5].HeaderText = "Açıklama";
            dataGridView1.Columns[6].HeaderText = "Telefon Numarası";
            dataGridView1.Columns[8].HeaderText = "Yakınlık";
            dataGridView1.Columns[9].HeaderText = "Ziyaret Yeri";
            dataGridView1.Columns[10].HeaderText = "Ziyaretçi Adres";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZiyaretKaydet();
            Temizle();
                    
         }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;

            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)
            {
                e.Handled = true;

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Sil();
        }

        private void ZiyaretciTakip_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Kapatmak istediğinize emin misiniz", "Kapatma Ekranı", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (sonuc == DialogResult.OK)
            {
                
            }
            else
            {
                e.Cancel = true;
            }
        }
                
            }
            
        }
    

