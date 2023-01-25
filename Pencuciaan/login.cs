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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=ditacucimobil;Integrated Security=True");
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            bunifuMaterialTextbox2.isPassword = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                bunifuMaterialTextbox2.isPassword = false;
            }
            else
            {
                bunifuMaterialTextbox2.isPassword = true;
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select count(*) from petugass where Username='" + bunifuMaterialTextbox1.Text + "' and password='" + bunifuMaterialTextbox2.Text + "'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
               
                menu m = new menu();
                m.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("salah");
            }
            conn.Close();
        }
    }
}
