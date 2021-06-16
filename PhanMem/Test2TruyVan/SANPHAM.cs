using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
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
    public partial class SANPHAM : Form
    {
        XuLy xl = new XuLy();
       
        public SANPHAM()
        {
            
            InitializeComponent();
            
        }

        public void load()
        {
            dataGridView1.Rows.Clear();
            //dataGridView2.Rows.Clear();
            //dataGridView3.Rows.Clear();
         
            xl.LoadTestData(dataGridView1, comboBox1, comboBox2, comboBox3);
            //xl.LoadTestData2(dataGridView2);
            //xl.LoadTestData3(dataGridView3);
            
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            xl.LoadTestData(dataGridView1, comboBox1, comboBox2, comboBox3); 
            //xl.LoadTestData2(dataGridView2); 
            //xl.LoadTestData3(dataGridView3);

        }
        //button them
        private void button1_Click(object sender, EventArgs e)
        {
            
            xl.Them(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, comboBox1, comboBox2);
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            //foreach (var r in rec)
            //{
            //    dataGridView1.DataSource = rec;
            //    if (r.TENLOAI == comboBox1.SelectedValue.ToString())
            //    {
            //        DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[0].Clone();
            //        row.Cells[0].Value = r.Id;
            //        row.Cells[1].Value = r.TENLOAI;
            //        dataGridView2.Rows.Add(row);
            //    }

            //}
           
        }
        
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
          
            xl.SortData(dataGridView1,comboBox1);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            xl.SortData2(dataGridView1,comboBox2);
        }
        //button truy van
        private void button3_Click_1(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            xl.Query(dataGridView4,comboBox3);
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
            //    textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            //    textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            //    textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            //    textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            //    textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            //    textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            //    textBox7.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();

            //}
            int d = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[d].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[d].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[d].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[d].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[d].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.Rows[d].Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.Rows[d].Cells[6].Value.ToString();
        }
        //button xoa
        private void button4_Click(object sender, EventArgs e)
        {
            
            xl.Xoa(comboBox3);
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            load();
        }
        //button sua
        private void button2_Click_1(object sender, EventArgs e)
        {
             //xl.Sua(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, comboBox1, comboBox2, comboBox3);
            xl.Xoa(comboBox3);
            //CH.Sua(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, comboBox1, comboBox2,comboBox3);
            xl.Them(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6,textBox7, comboBox1, comboBox2);
            load();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            dataGridView1.Rows.Clear();
            xl.LoadTestData(dataGridView1, comboBox1, comboBox2, comboBox3);
            comboBox3.SelectedIndex = 0;
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView4.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView4.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView4.SelectedRows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView4.SelectedRows[0].Cells[3].Value.ToString();
                textBox5.Text = dataGridView4.SelectedRows[0].Cells[4].Value.ToString();
                textBox6.Text = dataGridView4.SelectedRows[0].Cells[5].Value.ToString();
                textBox7.Text = dataGridView4.SelectedRows[0].Cells[6].Value.ToString();

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int d = e.RowIndex;
            //comboBox1.Text = dataGridView2.Rows[d].Cells[1].Value.ToString();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int d = e.RowIndex;
            //comboBox2.Text = dataGridView3.Rows[d].Cells[1].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            double a = double.Parse(textBox8.Text);
            xl.Sort(dataGridView4,a);
           
        }

        private void SANPHAM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                TRANGCHU f = new TRANGCHU();
                f.Show();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            xl.L(dataGridView4,int.Parse(textBox10.Text));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            xl.S(dataGridView4, int.Parse(textBox9.Text));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            xl.LVS(dataGridView4, int.Parse(textBox11.Text));
        }


    }
}
