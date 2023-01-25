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
    public partial class paket : Form
    {
        public paket()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=ditacucimobil;Integrated Security=True");
        void tampil()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from paketcuci", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "paketcuci");
            dgvpaket.DataSource = ds;
            dgvpaket.DataMember = "paketcuci";
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
            txttarif.Text = "";
        }
        void nootomatis()
        {
            long hitung;
            string nourutan;
            SqlDataReader rd;
            conn.Open();
            SqlCommand cmd = new SqlCommand("select idjasa from paketcuci where idjasa in(select max(idjasa)from paketcuci) order by idjasa desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["idjasa"].ToString().Length - 1, 1)) + 1;
                string kodeurutan = "0" + hitung;
                nourutan = "PK0" + kodeurutan.Substring(kodeurutan.Length - 1, 1);
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

        private void paket_Load(object sender, EventArgs e)
        {
            tampil();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            nootomatis();
        }

        private void dgvpaket_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dgvpaket.SelectedRows[0].Cells[0].Value.ToString();
            txtnama.Text = dgvpaket.SelectedRows[0].Cells[1].Value.ToString();
            txttarif.Text = dgvpaket.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            bersih();
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
                string dita = "insert into paketcuci values('" + txtid.Text + "','" + txtnama.Text + "','" + txttarif.Text + "')";
                SqlCommand cmd = new SqlCommand(dita, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil disimpan");
                conn.Close();
                tampil();
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
                string aku = "update paketcuci set namajasacuci='" + txtnama.Text + "',tarif='" + txttarif.Text + "' where idjasa='" + txtid.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil di update");
                conn.Close();
                tampil();
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
                string aku = "delete from paketcuci where idjasa='" + txtid.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil di hapus");
                conn.Close();
                tampil();
                bersih();
            }
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from paketcuci where namajasacuci like '%" + bunifuTextbox1.text + "%'", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "paketcuci");
            dgvpaket.DataSource = ds;
            dgvpaket.DataMember = "paketcuci";
            conn.Close();
        }
        }
}
