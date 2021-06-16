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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        MongoCRUD db = new MongoCRUD("QLTHETHAO");
        //Class MongoCRUD
        public class MongoCRUD
        {
            private IMongoDatabase db;
            public MongoCRUD(string database)
            {
                var client = new MongoClient();
                db = client.GetDatabase(database);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txt1.Text.Trim() == "ADMIN")
            {
                //var tc = new TRANGCHU();
                TRANGCHU tc = new TRANGCHU(txt1.Text.Trim());
                tc.Show();
                this.Hide();
                
                //Program.TK = "AD";

            }
            else if (txt1.Text.Trim() == "NHANVIEN01")
            {
                var tc = new TRANGCHU();
                this.Hide();
                tc.Show();
                
            }
            else
            {
                MessageBox.Show("Tai khoan khong ton tai");
            }
        }
    }
}
