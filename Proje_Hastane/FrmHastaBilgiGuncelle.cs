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
    public partial class FrmHastaBilgiGuncelle : Form
    {
        public FrmHastaBilgiGuncelle()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmHastaDetay fr = new FrmHastaDetay();
            this.Hide();
            fr.Show();
        }

        public string tc;

        SqlCon con = new SqlCon();

        private void FrmHastaBilgiGuncelle_Load(object sender, EventArgs e)
        {
            maskTC.Text = tc; //Form açılırken önceki formdaki tc' nin gelmesini sağladık.
            SqlCommand command = new SqlCommand("Select * from Tbl_Hastalar where HastaTC = @p1", con.connection());
            command.Parameters.AddWithValue("@p1", maskTC.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                txtFirstName.Text = dr[1].ToString();
                txtLastName.Text = dr[2].ToString();
                maskPhone.Text = dr[4].ToString();
                txtPassword.Text = dr[5].ToString();
                cmbGender.Text = dr[6].ToString();
            }
            con.connection().Close();
        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //MaskedTextBox' ta yer alan TC Kimlik Numarasına göre update işlemi
            SqlCommand command = new SqlCommand("Update Tbl_Hastalar set HastaAd = @p1,HastaSoyad = @p2,HastaTelefon = @p3,HastaSifre = @p4,HastaCinsiyet = @p5 where HastaTC = @p6",con.connection());
            command.Parameters.AddWithValue("@p1", txtFirstName.Text);
            command.Parameters.AddWithValue("@p2", txtLastName.Text);
            command.Parameters.AddWithValue("@p3", maskPhone.Text);
            command.Parameters.AddWithValue("@p4", txtPassword.Text);
            command.Parameters.AddWithValue("@p5", cmbGender.Text);
            command.Parameters.AddWithValue("@p6", maskTC.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Bilgileriniz güncellendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Değişiklikler yapıldı. Sisteme tekrar giriş yapmanız gerekmektedir.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            FrmHastaGiris fr = new FrmHastaGiris();
            fr.Show();
            this.Hide();
        }
    }
}
