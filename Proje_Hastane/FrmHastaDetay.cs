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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            FrmHastaBilgiGuncelle fr = new FrmHastaBilgiGuncelle();
            this.Hide();
            fr.tc = lblTC.Text; //TC' yi diğer forma taşıma işlemi
            fr.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FrmHastaGiris frmHG = new FrmHastaGiris();
            this.Hide();
            frmHG.Show();
        }

        public string tc;

        SqlCon con = new SqlCon();

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc.ToString();
            //HastaTC' si lblTC' deki TC' ye eşit olan hastaların adı ve soyadını çağıran sorgu aşağıdadır.
            SqlCommand command1 = new SqlCommand("Select HastaAd, HastaSoyad from Tbl_Hastalar where HastaTC = @p1",con.connection());
            command1.Parameters.AddWithValue("@p1",tc);
            SqlDataReader dr = command1.ExecuteReader();
            while (dr.Read())
            {
                lblNames.Text = dr[0] + " " + dr[1]; //İsim boşluk Soyisim yazdırma
            }

            //Geçmiş Randevu Çekme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where HastaTC = "+tc, con.connection());
            da.Fill(dt);
            dtRandevuGecmis.DataSource = dt;

            //Branş Çekme
            SqlCommand command2 = new SqlCommand("Select BransAd from Tbl_Branslar",con.connection());
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read()) //Veri okundukça combobox' a branşlar ekleniyor.
            {
                cmbBranch.Items.Add(dr2[0]);
            }
            con.connection().Close();
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Eklemeden önce geçmişte eklenmiş olan isimleri temizleme işlemi yapılıyor.
            cmbDoctor.Items.Clear();
            //Doktorlar tablosunda branşı Combobox'ta yer alan Doktor adları ve doktor soyadlarını gösterir.
            SqlCommand command3 = new SqlCommand("Select DoktorAd, DoktorSoyad from Tbl_Doktorlar where DoktorBrans = @p1", con.connection());
            command3.Parameters.AddWithValue("@p1", cmbBranch.Text);
            SqlDataReader dr3 = command3.ExecuteReader();
            while (dr3.Read()) //Veri okundukça Doktor ismini gösterecek combobox' a İsim boşluk soyisim şeklinde ekleniyor.
            {
                cmbDoctor.Items.Add(dr3[0]+" "+dr3[1]);
            }
            con.connection().Close();
        }
        
        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Aşağıdaki SQL sorgusu başkası tarafından randevu alınmamış olup Randevu branşı ile Randevu doktoru Comboboxlardaki gibi seçili olan doktorları listelemeye yarar.

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuBrans = '" + cmbBranch.Text + "' and RandevuDoktor = '" + cmbDoctor.Text + "' and RandevuDurum = 0", con.connection());
            da.Fill(dt);
            dtAktifRandevu.DataSource = dt;

            //RandevuDurum = 0 (False) durumu dolu olmayan randevuları temsil eder.
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            //Randevu almak için aslında Insert into yapmalıydık ama güncelleme mantığı ile çalışacağı için Update sorgusunu kullandık çünkü zaten aşağıdaki durumlar haricindeki tüm randevu durumları sistem tarafından atanıyor.

            SqlCommand command = new SqlCommand("Update Tbl_Randevular set RandevuDurum = 1, HastaTC = @p1, HastaSikayet = @p2 where RandevuID = @p3",con.connection());
            command.Parameters.AddWithValue("@p1", lblTC.Text);
            command.Parameters.AddWithValue("@p2", rtbSikayet.Text);
            command.Parameters.AddWithValue("@p3", txtID.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Randevu alındı.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            rtbSikayet.Clear();
            txtID.Clear();
            cmbBranch.Text = "";
            cmbDoctor.Text = "";

        }

        private void dtAktifRandevu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Datagridviewde seçili olan randevunun id'sini getiriyor.

            int chosen = dtAktifRandevu.SelectedCells[0].RowIndex;
            txtID.Text = dtAktifRandevu.Rows[chosen].Cells[0].Value.ToString();
        }
    }
}
