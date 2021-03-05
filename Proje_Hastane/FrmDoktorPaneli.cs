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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        SqlCon con = new SqlCon();

        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            //Doktorları Datagridview' e aktarma

            DataTable dt = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Tbl_Doktorlar", con.connection());
            da1.Fill(dt);
            dataGridView1.DataSource = dt;

            //Formun ilk açılışında comboboxlara branşları aktarma

            SqlCommand command2 = new SqlCommand("Select BransAd from Tbl_Branslar", con.connection());
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBranch.Items.Add(dr2[0]);
            }
            con.connection().Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmSekreterDetay fr = new FrmSekreterDetay();
            fr.Show();
            this.Hide();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //Doktor ekleme
            SqlCommand command = new SqlCommand("Insert into Tbl_Doktorlar (DoktorAd, DoktorSoyad, DoktorBrans, DoktorTC, DoktorSifre) values (@p1, @p2, @p3, @p4, @p5)", con.connection());
            command.Parameters.AddWithValue("@p1", txtFirstName.Text);
            command.Parameters.AddWithValue("@p2", txtLastName.Text);
            command.Parameters.AddWithValue("@p3", cmbBranch.Text);
            command.Parameters.AddWithValue("@p4", maskTC.Text);
            command.Parameters.AddWithValue("@p5", txtPassword.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Doktor eklendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            txtFirstName.Clear();
            txtLastName.Clear();
            cmbBranch.Text = "";
            maskTC.Clear();
            txtPassword.Clear();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //Datagridviewde bir alana tıklandığında içerisindeki bilgilerin textboxlara aktarılması
            int chosen = dataGridView1.SelectedCells[0].RowIndex;
            txtFirstName.Text = dataGridView1.Rows[chosen].Cells[1].Value.ToString();
            txtLastName.Text = dataGridView1.Rows[chosen].Cells[2].Value.ToString();
            cmbBranch.Text = dataGridView1.Rows[chosen].Cells[3].Value.ToString();
            maskTC.Text = dataGridView1.Rows[chosen].Cells[4].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[chosen].Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Doktor silme
            SqlCommand command = new SqlCommand("Delete from Tbl_Doktorlar where DoktorTC = @p1", con.connection());
            command.Parameters.AddWithValue("@p1", maskTC.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Seçilen doktor silindi.");
            txtFirstName.Clear();
            txtLastName.Clear();
            cmbBranch.Text = "";
            maskTC.Clear();
            txtPassword.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //TC Kimlik Numarası maskedtextbox' ta bulunan doktoru güncelleme
            SqlCommand command = new SqlCommand("Update Tbl_Doktorlar set DoktorAd = @p1, DoktorSoyad = @p2, DoktorBrans = @p3, DoktorSifre = @p5 where DoktorTC = @p4",con.connection());
            command.Parameters.AddWithValue("@p1", txtFirstName.Text);
            command.Parameters.AddWithValue("@p2", txtLastName.Text);
            command.Parameters.AddWithValue("@p3", cmbBranch.Text);
            command.Parameters.AddWithValue("@p4", maskTC.Text);
            command.Parameters.AddWithValue("@p5", txtPassword.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Doktor güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtFirstName.Clear();
            txtLastName.Clear();
            cmbBranch.Text = "";
            maskTC.Clear();
            txtPassword.Clear();
        }
    }
}
