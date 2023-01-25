using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pencuciaan
{
    public partial class pelanggan : Form
    {
        public pelanggan()
        {
            InitializeComponent();
            tampildatapelanggan();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=ditacucimobil;Integrated Security=True");
        void tampildatapelanggan()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from pelanggan", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "pelanggan");
            dgvpelanggan.DataSource = ds;
            dgvpelanggan.DataMember = "pelanggan";
            conn.Close();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void bersih()
        {
            txtid.Text = "";
            txtnama.Text = "";
            txtphon.Text = "";
            txtalamat.Text = "";
            txtkendaraan.Text = "";
        }
        void nootomatis()
        {
            long hitung;
            string nourutan;
            SqlDataReader rd;
            conn.Open();
            SqlCommand cmd = new SqlCommand("select idpelanggan from pelanggan where idpelanggan in(select max(idpelanggan)from pelanggan) order by idpelanggan desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["idpelanggan"].ToString().Length - 1, 1)) + 1;
                string kodeurutan = "0" + hitung;
                nourutan = "PL0" + kodeurutan.Substring(kodeurutan.Length - 1, 1);
            }
            else
            {
                nourutan = "1";
            }
            rd.Close();
            txtid.Text = nourutan;
            conn.Close();

        }
        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            menu m = new menu();
            m.Show();
            this.Hide();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            nootomatis();
        }

        private void dgvpelanggan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dgvpelanggan.SelectedRows[0].Cells[0].Value.ToString();
            txtnama.Text = dgvpelanggan.SelectedRows[0].Cells[1].Value.ToString();
            txtphon.Text = dgvpelanggan.SelectedRows[0].Cells[2].Value.ToString();
            txtalamat.Text = dgvpelanggan.SelectedRows[0].Cells[3].Value.ToString();
            txtkendaraan.Text = dgvpelanggan.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "")
            {
                MessageBox.Show("isi data terlebih dahulu");
            }
            else
            {
                conn.Open();
                string dita = "insert into pelanggan values('" + txtid.Text + "','" + txtnama.Text + "','" + txtphon.Text + "','"+txtalamat.Text+"','"+txtkendaraan.Text+"')";
                SqlCommand cmd = new SqlCommand(dita, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil disimpan");
                conn.Close();
                tampildatapelanggan();
                bersih();
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "")
            {
                MessageBox.Show("pilih data yang akan diupdate");
            }
            else
            {
                conn.Open();
                string aku = "update pelanggan set namapelanggan='" + txtnama.Text + "',nohp='" + txtphon.Text + "',alamat='" + txtalamat.Text + "',namakendaraan='"+txtkendaraan.Text+"' where idpelanggan='" + txtid.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil di update");
                conn.Close();
                tampildatapelanggan();
                bersih();
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "")
            {
                MessageBox.Show("pilih data yang akan dihapus");
            }
            else if (MessageBox.Show("apakah Anda akan menghapus : " + txtnama.Text + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                conn.Open();
                string aku = "delete from pelanggan where idpelanggan='" + txtid.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil di hapus");
                conn.Close();
                tampildatapelanggan();
                bersih();
            }
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            bersih();
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from pelanggan where namapelanggan like '%" + bunifuTextbox1.text + "%'", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "pelanggan");
            dgvpelanggan.DataSource = ds;
            dgvpelanggan.DataMember = "pelanggan";
            conn.Close();
        }

        private void pelanggan_Load(object sender, EventArgs e)
        {

        }
    }
}
