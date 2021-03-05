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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        public string tc;

        SqlCon con = new SqlCon();

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle fr = new FrmDoktorBilgiDuzenle();
            this.Hide();
            fr.tc = lblTC.Text;
            fr.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyuru = new FrmDuyurular();
            this.Hide();
            frmDuyuru.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris frmDRGiris = new FrmDoktorGiris();
            this.Hide();
            frmDRGiris.Show();
        }
        
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;
            //DoktorTC' si lblTC' deki TC' ye eşit olan doktorların adı ve soyadını çağıran sorgu aşağıdadır.
            SqlCommand command1 = new SqlCommand("Select DoktorAd, DoktorSoyad from Tbl_Doktorlar where DoktorTC = @p1", con.connection());
            command1.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0] + " " + dr1[1]; //İsim boşluk Soyisim yazdırma
            }

            //Bu doktora ait randevu listesini getiren sorgu aşağıdadır.
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuDoktor = '"+lblAdSoyad.Text+"'",con.connection());
            da.Fill(dt);
            dtRandevuDetay.DataSource = dt;
        }

        private void dtRandevuDetay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int chosen = dtRandevuDetay.SelectedCells[0].RowIndex;
            rtbRandevuDetay.Text = dtRandevuDetay.Rows[chosen].Cells[7].Value.ToString();
        }
    }
}
