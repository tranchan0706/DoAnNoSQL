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
    public partial class CUAHANG : Form
    {
        CuaHangBus CH = new CuaHangBus();
        public CUAHANG()
        {
            InitializeComponent();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            CH.Them(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, comboBox1, comboBox2);
            comboBox3.Items.Clear();
            load();
            textBox1.Enabled = false;
        }

        private void CUAHANG_Load(object sender, EventArgs e)
        {
            CH.LoadTestData(dataGridView1, comboBox1, comboBox2, comboBox3);
            CH.LoadTestData2(dataGridView2);
            CH.LoadTestData3(dataGridView3);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            textBox1.Enabled = false;
        }
        public void load()
        {
            
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            CH.LoadTestData(dataGridView1, comboBox1, comboBox2, comboBox3);
            CH.LoadTestData2(dataGridView2);
            CH.LoadTestData3(dataGridView3);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CH.Xoa(comboBox3);
            //CH.Sua(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, comboBox1, comboBox2,comboBox3);
            CH.Them(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, comboBox1, comboBox2);
            load();
            comboBox3.Items.Clear();
            dataGridView1.Rows.Clear();
            CH.LoadTestData(dataGridView1, comboBox1, comboBox2, comboBox3);
            comboBox3.SelectedIndex = 0;
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            comboBox3.Text = dataGridView1.Rows[d].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[d].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[d].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[d].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[d].Cells[4].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[d].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.Rows[d].Cells[6].Value.ToString();

            dataGridView3.Rows.Clear();
            CH.Load1(dataGridView3, comboBox3);

            dataGridView2.Rows.Clear();
            CH.Load2(dataGridView2, comboBox3);
        }
        public void loadquanly()
        { }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            textBox4.Text = dataGridView3.Rows[d].Cells[1].Value.ToString();
            textBox5.Text = dataGridView3.Rows[d].Cells[2].Value.ToString();
            textBox6.Text = dataGridView3.Rows[d].Cells[3].Value.ToString();
            textBox7.Text = dataGridView3.Rows[d].Cells[4].Value.ToString();
            textBox8.Text = dataGridView3.Rows[d].Cells[5].Value.ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            comboBox1.Text = dataGridView2.Rows[d].Cells[1].Value.ToString();
            comboBox2.Text = dataGridView2.Rows[d].Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CH.Xoa(comboBox3);
                comboBox3.Items.Clear();
                load();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            comboBox3.Items.Clear();
            load();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //CH.SortData(dataGridView3,textBox4);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            CH.Query(dataGridView1, comboBox3);
        }

        private void button5_Click_1(object sender, EventArgs e)
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
            try
            {
                if (radioButton1.Checked == true)
                {
                    dataGridView1.Rows.Clear();
                    CH.L(dataGridView1, int.Parse(textBox10.Text));
                }
                else if (radioButton2.Checked == true)
                {
                    dataGridView1.Rows.Clear();
                    CH.S(dataGridView1, int.Parse(textBox9.Text));
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    CH.LVS(dataGridView1, int.Parse(textBox9.Text), int.Parse(textBox10.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thất bại! Vui lòng xem lại thông tin nhập.");
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox9.Enabled = false;
            textBox10.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox10.Enabled = false;
            textBox9.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox9.Enabled = true;
            textBox10.Enabled = true;
        }


        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked == true && checkBox2.Checked == false)
                {
                    dataGridView1.Rows.Clear();
                    string a = textBox11.Text.ToString();
                    CH.Seach_TENKV(dataGridView1, a);
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    string a = textBox11.Text.ToString();
                    string b = textBox12.Text.ToString();
                    CH.Seach_TENKV_NQL(dataGridView1, a, b);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thất bại! Vui lòng xem lại thông tin nhập.");
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox12.Enabled = false;
            textBox11.Enabled = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox12.Enabled = true;
            textBox11.Enabled = true;
        }
    }
}
