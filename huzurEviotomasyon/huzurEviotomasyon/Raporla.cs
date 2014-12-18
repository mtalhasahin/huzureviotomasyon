using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace huzurEviotomasyon
{
    public partial class Raporla : Form
    {
        public Raporla()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(" Data Source=.;Initial Catalog=HuzurEviOtomasyonu;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        DataTable dt = new DataTable();
        private void Raporla_Load(object sender, EventArgs e)
        {
            dt.Clear();
            SqlDataAdapter adt = new SqlDataAdapter("Select * From Yenikayit",con);
            adt.Fill(dt);
            CrystalReport1 rapor = new CrystalReport1();
            rapor.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rapor;
        }
    }
}
