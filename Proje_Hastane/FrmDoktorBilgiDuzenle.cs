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

namespace Proje_Hastane
{
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmDoktorDetay fr = new FrmDoktorDetay();
            this.Hide();
            fr.Show();
        }
        public string tc;
        SqlCon con = new SqlCon();

        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            maskTC.Text = tc;
            SqlCommand command = new SqlCommand("Select * from Tbl_Doktorlar where DoktorTC = @p1",con.connection());
            command.Parameters.AddWithValue("@p1",maskTC.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                txtFirstName.Text = dr[1].ToString();
                txtLastName.Text = dr[2].ToString();
                cmbBranch.Text = dr[3].ToString();
                txtPassword.Text = dr[5].ToString();
            }
            con.connection().Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update Tbl_Doktorlar set DoktorAd = @p1, DoktorSoyad = @p2, DoktorBrans = @p3, DoktorSifre = @p5 where DoktorTC = @p4",con.connection());
            command.Parameters.AddWithValue("@p1", txtFirstName.Text);
            command.Parameters.AddWithValue("@p2", txtLastName.Text);
            command.Parameters.AddWithValue("@p3", cmbBranch.Text);
            command.Parameters.AddWithValue("@p4", maskTC.Text);
            command.Parameters.AddWithValue("@p5", txtPassword.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Bilgileriniz güncellendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);

        }
    }
}
