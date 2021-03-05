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
    public partial class FrmBransPaneli : Form
    {
        public FrmBransPaneli()
        {
            InitializeComponent();
        }

        SqlCon con = new SqlCon();

        private void FrmBransPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branslar",con.connection());
            da.Fill(dt);
            dataGridView1.DataSource=dt;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmSekreterDetay fr = new FrmSekreterDetay();
            fr.Show();
            this.Hide();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Insert into Tbl_Branslar (BransAd) values (@p1)",con.connection());
            command.Parameters.AddWithValue("@p1",txtBrans.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Branş eklendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            txtBrans.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Datagridviewde bir alana tıklandığında içerisindeki bilgilerin textboxlara aktarılması
            int chosen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[chosen].Cells[0].Value.ToString();
            txtBrans.Text = dataGridView1.Rows[chosen].Cells[1].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Doktor silme
            SqlCommand command = new SqlCommand("Delete from Tbl_Branslar where BransID = @p1", con.connection());
            command.Parameters.AddWithValue("@p1", txtID.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Seçilen branş silindi.");
            txtID.Clear();
            txtBrans.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //ID' si txtID' de bulunan branşı güncelleme
            SqlCommand command = new SqlCommand("Update Tbl_Branslar set BransAd = @p2 where BransID = @p1", con.connection());
            command.Parameters.AddWithValue("@p1", txtID.Text);
            command.Parameters.AddWithValue("@p2", txtBrans.Text);
            command.ExecuteNonQuery();
            con.connection().Close();
            MessageBox.Show("Branş güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtID.Clear();
            txtBrans.Clear();
        }
    }
}
