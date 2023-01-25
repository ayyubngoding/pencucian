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
    public partial class transaksi : Form
    {
        public transaksi()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=ditacucimobil;Integrated Security=True");
        void tampil()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from pencucian", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "pencucian");
            dgvtr.DataSource = ds;
            dgvtr.DataMember = "pencucian";
            conn.Close();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void bersih()
        {
            idtransaksi.Text = "";
            cbidpel.Text = "";
            txtnamapel.Text = "";
            txtnamaken.Text = "";
            cbidjasa.Text = "";
            cbjenisken.Text = "";
            txttotal.Text = "";
            txtbayar.Text = "";
            txtkembalian.Text = "";
            tgl.Text = "";
            cbidpet.Text = "";
            txttarif.Text = "";
            txtbiaya.Text = "";
        }
        void nootomatis()
        {
            long hitung;
            string nourutan;
            SqlDataReader rd;
            conn.Open();
            SqlCommand cmd = new SqlCommand("select idtransaksi from pencucian where idtransaksi in(select max(idtransaksi)from pencucian) order by idtransaksi desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["idtransaksi"].ToString().Length - 1, 1)) + 1;
                string kodeurutan = "0" + hitung;
                nourutan = "PK0" + kodeurutan.Substring(kodeurutan.Length - 1, 1);
            }
            else
            {
                nourutan = "1";
            }
            rd.Close();
            idtransaksi.Text = nourutan;
            conn.Close();
        }
        void idpelanggan()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select idpelanggan from pelanggan", conn);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("idpelanggan", typeof(string));
            dt.Load(rd);
            cbidpel.ValueMember = "idpelanggan";
            cbidpel.DataSource = dt;
            conn.Close();
        }
        void namapel()
        {
            conn.Open();
            var ditaamaliya = "select * from pelanggan where idpelanggan ='" + cbidpel.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(ditaamaliya, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow ditadf in dt.Rows)
            {
                txtnamapel.Text = ditadf["namapelanggan"].ToString();

                txtnamaken.Text = ditadf["namakendaraan"].ToString();
            }
            conn.Close();
        }
        void idcuci()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select idjasa from paketcuci", conn);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("idjasa", typeof(string));
            dt.Load(rd);
            cbidjasa.ValueMember = "idjasa";
            cbidjasa.DataSource = dt;
            conn.Close();
        }
        void namacuci()
        {
            conn.Open();
            var ditaamaliya = "select * from paketcuci where idjasa ='" + cbidjasa.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(ditaamaliya, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow ditadf in dt.Rows)
            {
                txtnamacuci.Text = ditadf["namajasacuci"].ToString();

                txttarif.Text = ditadf["tarif"].ToString();
            }
            conn.Close();
        }
        void idpetugas()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select idpetugas from petugass", conn);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("idpetugas", typeof(string));
            dt.Load(rd);
            cbidpet.ValueMember = "idpetugas";
            cbidpet.DataSource = dt;
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

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            bersih();
        }

        private void dgvtr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            idtransaksi.Text = dgvtr.SelectedRows[0].Cells[0].Value.ToString();
            cbidpel.Text = dgvtr.SelectedRows[0].Cells[1].Value.ToString();
            txtnamapel.Text = dgvtr.SelectedRows[0].Cells[2].Value.ToString();
            txtnamaken.Text = dgvtr.SelectedRows[0].Cells[3].Value.ToString();
            cbidjasa.Text = dgvtr.SelectedRows[0].Cells[4].Value.ToString();
            txtnamacuci.Text = dgvtr.SelectedRows[0].Cells[5].Value.ToString();
            cbjenisken.Text = dgvtr.SelectedRows[0].Cells[6].Value.ToString();
            txttotal.Text = dgvtr.SelectedRows[0].Cells[7].Value.ToString();
            txtbayar.Text = dgvtr.SelectedRows[0].Cells[8].Value.ToString();
            txtkembalian.Text = dgvtr.SelectedRows[0].Cells[9].Value.ToString();
            tgl.Text = dgvtr.SelectedRows[0].Cells[10].Value.ToString();
            cbidpet.Text = dgvtr.SelectedRows[0].Cells[11].Value.ToString();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (idtransaksi.Text == "")
            {
                MessageBox.Show("isi data terlebih dahulu");
            }
            else
            {
                conn.Open();
                string dita = "insert into pencucian values('" + idtransaksi.Text + "','" + cbidpel.SelectedValue.ToString() + "','" + txtnamapel.Text + "','" + txtnamaken.Text + "','" + cbidjasa.SelectedValue.ToString() + "','" + txtnamacuci.Text + "','" + cbjenisken.SelectedItem.ToString() + "','" + txttotal.Text + "','" + txtbayar.Text + "','" + txtkembalian.Text + "','" + tgl.Value.Date.ToString("yyyy-MM-dd") + "','" + cbidpet.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(dita, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil disimpan");
                conn.Close();
                tampil();
                bersih();
            }
        }

        private void transaksi_Load(object sender, EventArgs e)
        {
            tampil();
            idcuci();
            idpelanggan();
            idpetugas();
        }

        private void cbidpel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            namapel();
        }

        private void cbidjasa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            namacuci();
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtbayar.Text) < Convert.ToInt32(txttotal.Text))
            {
                MessageBox.Show("uang yang anda bayarkan kurang");
            }
            else
            {
                double bil1,bil2, hasil;
                bil1 = double.Parse(txttotal.Text);
                bil2 = double.Parse(txtbayar.Text);
                hasil = bil2 - bil1;
                txtkembalian.Text= hasil.ToString();
            }
            
        }
        void combobox()
        {
            if (cbjenisken.Text=="Mobil")
            {
                txtbiaya.Text = "20000";
            }
            else if (cbjenisken.Text=="Truck")
            {
                txtbiaya.Text = "25000";
            }
            else if (cbjenisken.Text == "Bus")
            {
                txtbiaya.Text = "35000";
            }
            else if (cbjenisken.Text == "Motor")
            {
                txtbiaya.Text = "10000";
            }
          
        }

        private void cbjenisken_SelectionChangeCommitted(object sender, EventArgs e)
        {
            combobox();
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            if (txttarif.Text=="asdfgh")
            {
                txttarif.Text = "df";
            }
            else
            {
                double bil1, bil2, hasiltotal;
                bil1 = double.Parse(txttarif.Text);
                bil2 = double.Parse(txtbiaya.Text);

                hasiltotal = bil1 + bil2;
                txttotal.Text = hasiltotal.ToString();
            }
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from pencucian where namapelanggan like '%" + bunifuTextbox1.text + "%'", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "pencucian");
            dgvtr.DataSource = ds;
            dgvtr.DataMember = "pencucian";
            conn.Close();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            if (idtransaksi.Text == "")
            {
                MessageBox.Show("pilih data yang akan dihapus");
            }
            else if (MessageBox.Show("apakah Anda akan menghapus : " + txtnamapel.Text + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                conn.Open();
                string aku = "delete from petugass where idpetugas='" + idtransaksi.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil di hapus");
                conn.Close();
                tampil();
                bersih();
            }
        }
    }
}
