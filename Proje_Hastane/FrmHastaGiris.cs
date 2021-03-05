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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            FrmGirisler fr = new FrmGirisler();
            this.Hide();
            fr.Show();
        }

        private void linkSign_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit fr = new FrmHastaKayit();
            this.Hide();
            fr.Show();
        }

        SqlCon con = new SqlCon();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * from Tbl_Hastalar where HastaTC = @p1 and HastaSifre = @p2",con.connection());
            command.Parameters.AddWithValue("@p1", maskTC.Text);
            command.Parameters.AddWithValue("@p2", txtPassword.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                FrmHastaDetay fr = new FrmHastaDetay();
                this.Hide();
                fr.tc = maskTC.Text;
                fr.Show();
            }
            else
            {
                MessageBox.Show("Kimlik numaranız veya şifreniz hatalı");
                maskTC.Clear();
                txtPassword.Clear();
                maskTC.Focus();
            }
            con.connection().Close();
        }
    }
}
