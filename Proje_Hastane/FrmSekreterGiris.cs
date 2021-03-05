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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            FrmGirisler fr = new FrmGirisler();
            this.Hide();
            fr.Show();
        }

        SqlCon con = new SqlCon();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * from Tbl_Sekreterler where SekreterTC = @p1 and SekreterSifre = @p2",con.connection());
            command.Parameters.AddWithValue("@p1", maskTC.Text);
            command.Parameters.AddWithValue("@p2", txtPassword.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                FrmSekreterDetay fr = new FrmSekreterDetay();
                fr.tc = maskTC.Text; //MaskedTextBox' taki TC' yi açılacak Sekreter Detay Sayfasında gösterme
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("TC Kimlik Numaranız veya şifreniz hatalı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            con.connection().Close();
        }
    }
}
