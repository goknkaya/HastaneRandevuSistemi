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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string tc;

        SqlCon con = new SqlCon();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;

            //Ekranda TC Kimlik numarası görünen sekreterin adını ve soyadını getiren kod

            SqlCommand command1 = new SqlCommand("Select SekreterAdSoyad from Tbl_Sekreterler where SekreterTC = @p1",con.connection());
            command1.Parameters.AddWithValue("@p1",lblTC.Text);
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();
            }
            con.connection().Close();

            //Branşları Datagridview' e aktarma

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Tbl_Branslar",con.connection());
            da1.Fill(dt1);
            dtBrans.DataSource = dt1;

            //Doktorları Datagridview' e aktarma (Doktorların isimlerini ad boşluk soyad şeklinde Doktorlar sütunu içerisine yazar

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as Doktorlar, DoktorBrans from Tbl_Doktorlar", con.connection());
            da2.Fill(dt2);
            dtDoktor.DataSource = dt2;

            //Comboboxa Branşları aktarma

            SqlCommand command2 = new SqlCommand("Select BransAd from Tbl_Branslar", con.connection());
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBranch.Items.Add(dr2[0]);
            }
            con.connection().Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris fr = new FrmSekreterGiris();
            fr.Show();
            this.Hide();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Insert into Tbl_Randevular (RandevuTarih, RandevuSaat, RandevuBrans, RandevuDoktor) values (@p1, @p2, @p3, @p4)", con.connection());
            command.Parameters.AddWithValue("@p1", maskDate.Text);
            command.Parameters.AddWithValue("@p2", maskTime.Text);
            command.Parameters.AddWithValue("@p3", cmbBranch.Text);
            command.Parameters.AddWithValue("@p4", cmbDoctor.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Randevu oluşturuldu.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Branş Comboboxına tıklanınca direkt ilgili branşa ait doktorlar listelensin
            cmbDoctor.Items.Clear();
            SqlCommand command3 = new SqlCommand("Select DoktorAd, DoktorSoyad from Tbl_Doktorlar where DoktorBrans = @p1", con.connection());
            command3.Parameters.AddWithValue("@p1", cmbBranch.Text);
            SqlDataReader dr3 = command3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoctor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            con.connection().Close();
        }

        private void btnDuyuru_Click(object sender, EventArgs e)
        {
            //Duyuru oluşturma

            SqlCommand command = new SqlCommand("Insert into Tbl_Duyurular (Duyuru) values (@p1)",con.connection());
            command.Parameters.AddWithValue("@p1",rtbDuyuru.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Duyuru oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            rtbDuyuru.Clear();
        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli fr = new FrmDoktorPaneli();
            fr.Show();
            this.Hide();
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBransPaneli fr = new FrmBransPaneli();
            fr.Show();
            this.Hide();
        }

        private void btnRandevuPanel_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr = new FrmRandevuListesi();
            fr.Show();
            this.Hide();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
            this.Hide();
        }
    }
}
