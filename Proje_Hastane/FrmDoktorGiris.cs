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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            FrmGirisler frmLogin = new FrmGirisler();
            this.Hide();
            frmLogin.Show();
        }

        SqlCon con = new SqlCon();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * from Tbl_Doktorlar where DoktorTC = @p1 and DoktorSifre = @p2",con.connection());
            command.Parameters.AddWithValue("@p1", maskTC.Text);
            command.Parameters.AddWithValue("@p2", txtPassword.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                FrmDoktorDetay fr = new FrmDoktorDetay();
                fr.tc = maskTC.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bilgiler yanlış. Kontrol edip yeniden giriş yapınız.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                maskTC.Clear();
                txtPassword.Clear();
            }
            con.connection().Close();
        }
    }
}
