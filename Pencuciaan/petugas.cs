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
    public partial class petugas : Form
    {
        public petugas()
        {
            InitializeComponent();
            tampildatapetugas();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=ditacucimobil;Integrated Security=True");
        void tampildatapetugas()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from petugass", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "petugass");
            dgvpetugas.DataSource = ds;
            dgvpetugas.DataMember = "petugass";
            conn.Close();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void bersih()
        {
            txtid.Text = "";
            txtuser.Text = "";
            txtpass.Text = "";
            txtphon.Text = "";
           
        }
        void nootomatis()
        {
            long hitung;
            string nourutan;
            SqlDataReader rd;
            conn.Open();
            SqlCommand cmd = new SqlCommand("select idpetugas from petugass where idpetugas in(select max(idpetugas)from petugass) order by idpetugas desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["idpetugas"].ToString().Length - 1, 1)) + 1;
                string kodeurutan = "0" + hitung;
                nourutan = "ID0" + kodeurutan.Substring(kodeurutan.Length - 1, 1);
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

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            bersih();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            nootomatis();
        }

        private void dgvpetugas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dgvpetugas.SelectedRows[0].Cells[0].Value.ToString();
            txtuser.Text = dgvpetugas.SelectedRows[0].Cells[1].Value.ToString();
            txtpass.Text = dgvpetugas.SelectedRows[0].Cells[2].Value.ToString();
            txtphon.Text = dgvpetugas.SelectedRows[0].Cells[3].Value.ToString();
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
                string dita = "insert into petugass values('" + txtid.Text + "','" + txtuser.Text + "','" + txtpass.Text + "','" + txtphon.Text + "')";
                SqlCommand cmd = new SqlCommand(dita, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil disimpan");
                conn.Close();
                tampildatapetugas();
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
                string aku = "update petugass set username='" + txtuser.Text + "',password='" + txtpass.Text + "',nohp='" + txtphon.Text + "' where idpetugas='" + txtid.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil di update");
                conn.Close();
                tampildatapetugas();
                bersih();
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "")
            {
                MessageBox.Show("pilih data yang akan dihapus");
            }
            else if (MessageBox.Show("apakah Anda akan menghapus : " + txtuser.Text + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                conn.Open();
                string aku = "delete from petugass where idpetugas='" + txtid.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil di hapus");
                conn.Close();
                tampildatapetugas();
                bersih();
            }
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from petugass where username like '%" + bunifuTextbox1.text + "%'", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "petugass");
            dgvpetugas.DataSource = ds;
            dgvpetugas.DataMember = "petugass";
            conn.Close();
        }
    }
}
