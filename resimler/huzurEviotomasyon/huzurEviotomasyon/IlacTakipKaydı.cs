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
    public partial class IlacTakipKaydı : Form
    {
       
        SqlConnection con = new SqlConnection(" Data Source=.;Initial Catalog=HuzurEviOtomasyonu;Integrated Security=True");
        SqlCommand kmt = new SqlCommand();
        DataTable dt = new DataTable();
        public IlacTakipKaydı()
        {
            InitializeComponent();
        }
        public void ListeleIlac()
        {
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("Select *From IlacTakip", con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        
        }
       

        public void Sil()
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Silme Menüsü", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {

                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "Delete From IlacTakip where Ad='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "' "; //Silme işlemi Ad'a No ya göre gerçekleşti
                cmd.ExecuteNonQuery();
                con.Close();
                ListeleIlac();
                MessageBox.Show("Silme işlemi gerçekleştirdi...");
            }
        
        }
        public void Temizle()
        {
            //Temizle butonu oluşturuldu

          
            textBox2.Text = "";
            comboBox3.SelectedItem = null;
            comboBox5.SelectedItem = null;
            comboBox4.SelectedItem = null; 
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            maskedTextBox2.Text = "";
            maskedTextBox1.Text = "";

        }

        private void IlacTakipKaydı_Load(object sender, EventArgs e)
        {
            ListeleIlac();
            con.Open();
            SqlCommand com = new SqlCommand("select Ad,Soyad from Yenikayit", con);
            com.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = com.ExecuteReader();
            while (dr.Read())
            {

                comboBox5.Items.Add(dr["Ad"]);
                comboBox4.Items.Add(dr["Soyad"]);


            }
            con.Close();
            
        }

        private void btnYenitakipkaydet_Click(object sender, EventArgs e)
        {
            con.Open();
            string BaslamaTarih=maskedTextBox1.Text;
            string BitirmeTarih = maskedTextBox2.Text;
            kmt.Connection = con;
            kmt.CommandText = "Insert Into IlacTakip(Ad,Soyad,Ilac,ReceteTuru,AcTok,BaslamaTarih,BitirmeTarih,Aciklama)Values('"+comboBox5.Text+"','"+comboBox4.Text+"','"+comboBox1.Text+"','"+comboBox2.Text+"','"+comboBox3.Text+"','"+textBox2.Text+"','"+maskedTextBox1.Text+"','"+maskedTextBox2.Text+"')";
            kmt.ExecuteNonQuery();
            
            ListeleIlac();
            Temizle();
            con.Close();     
            MessageBox.Show("kayıt işlemleri gerçekleşmiştir..");
           
           
        }
            
        

        private void button1_Click(object sender, EventArgs e)
        {
            Sil();
        }

        private void IlacTakipKaydı_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnYenitakipiptal_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
