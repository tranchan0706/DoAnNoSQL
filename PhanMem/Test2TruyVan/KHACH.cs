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
    public partial class KHACH : Form
    {
        XuLy2 xl2 = new XuLy2();
        public KHACH()
        {
            InitializeComponent();
            
        }
        public void load()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            xl2.LoadTestData(dataGridView1, comboBox1, comboBox2);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
        //button them
        private void button1_Click(object sender, EventArgs e)
        {
            xl2.Them(textBox1,textBox2,textBox3,textBox4,textBox5,textBox6,textBox9,textBox11,textBox12,textBox13,comboBox1);
            comboBox2.Items.Clear();
            load();
        }
        
        private void KHACH_Load(object sender, EventArgs e)
        {
            xl2.LoadTestData(dataGridView1,comboBox1,comboBox2);
            DateTime a = DateTime.Now;
            textBox10.Text = a.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            xl2.Query(dataGridView2,comboBox2);
        }
        //button xoa
        private void button3_Click(object sender, EventArgs e)
        {
            xl2.Xoa(comboBox2);
            comboBox2.Items.Clear();
            load();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                textBox9.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();

            }
        }
        //button Sua
        private void button4_Click(object sender, EventArgs e)
        {

            xl2.Xoa(comboBox2);            //CH.Sua(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, comboBox1, comboBox2,comboBox3);
            xl2.Them(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox9, textBox11, textBox12, textBox13, comboBox1);
            load();
            comboBox2.Items.Clear();
            dataGridView1.Rows.Clear();
            xl2.LoadTestData(dataGridView1, comboBox1, comboBox2);
            comboBox2.SelectedIndex = 0;
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void KHACH_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                TRANGCHU f = new TRANGCHU();
                f.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            xl2.GT(dataGridView2,int.Parse(textBox7.Text));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            xl2.TT(dataGridView2);
        }


    }
}
