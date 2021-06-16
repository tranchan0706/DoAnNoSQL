using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2TruyVan
{
    public partial class TRANGCHU : Form
    {
        public TRANGCHU()
        {
            InitializeComponent();
        }
        public TRANGCHU(string a)
        {
            InitializeComponent();
            button2.Text = a;

        }
        private void cửaHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CUAHANG ch = new CUAHANG();
            ch.Show();
            this.Hide();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KHACH ch = new KHACH();
            ch.Show();
            this.Hide();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SANPHAM ch = new SANPHAM();
            ch.Show();
            this.Hide();
        }

        private void TRANGCHU_Load(object sender, EventArgs e)
        {
            if (button2.Text == "ADMIN")
            {
                CUAHANG.Enabled = true;

            }
            else
            {
                CUAHANG.Enabled = false;

            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
