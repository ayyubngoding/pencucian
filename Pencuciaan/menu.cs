using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pencuciaan
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void pELANGGANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pelanggan pl = new pelanggan();
            pl.ShowDialog();
        }

        private void pETUGASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            petugas pl = new petugas();
            pl.ShowDialog();
        }

        private void jASACUCIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paket pl = new paket();
            pl.ShowDialog();
        }

        private void pENCUCIANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transaksi pl = new transaksi();
            pl.ShowDialog();
        }

        private void lOGOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
