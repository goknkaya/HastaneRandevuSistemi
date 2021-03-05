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
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmHastaGiris frmHG = new FrmHastaGiris();
            this.Hide();
            frmHG.Show();
        }

        SqlCon con = new SqlCon();

        private void btnRegister_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Insert into Tbl_Hastalar (HastaAd, HastaSoyad, HastaTC, HastaTelefon, HastaSifre, HastaCinsiyet) values (@p1, @p2, @p3, @p4, @p5, @p6)",con.connection());
            command.Parameters.AddWithValue("@p1", txtFirstName.Text);
            command.Parameters.AddWithValue("@p2", txtLastName.Text);
            command.Parameters.AddWithValue("@p3", maskTC.Text);
            command.Parameters.AddWithValue("@p4", maskPhone.Text);
            command.Parameters.AddWithValue("@p5", txtPassword.Text);
            command.Parameters.AddWithValue("@p6", cmbGender.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Kayıt işlemi başarılı. Şifreniz: " + txtPassword.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
