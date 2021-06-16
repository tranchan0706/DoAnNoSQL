using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Test2TruyVan
{
    public class XuLy
    { 
        //Class SanPham
        public class SANPHAM
        {

            public ObjectId Id { get; set; }
            public string MASP { get; set; }
            public string TENSP { get; set; }
            public double GIABAN { get; set; }
            public string BAOHANH { get; set; }
            public string UUDAITHEM { get; set; }
            public string HINHANH { get; set; }
            public string DACDIEM { get; set; }
            public LOAISP LOAISP { get; set; }

        }
        //Class LoaiSP
        public class LOAISP
        {
            public ObjectId Id { get; set; }
            public string TENLOAI { get; set; }
            public MON MON { get; set; }

        }
        //Class Mon
        public class MON
        {
            public ObjectId Id { get; set; }
            public string TENMON { get; set; }
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
            //Them
            public void InsertRecord<T>(string table, T record)
            {
                var collection = db.GetCollection<T>(table);
                collection.InsertOne(record);
            }
            //Xoa
            public void DeleteRecord<T>(string table, ObjectId id)
            {
                var coll = db.GetCollection<T>(table);
                var filter = Builders<T>.Filter.Eq("Id", id);
                coll.DeleteOne(filter);
                
            }
            //Sua
            public void UpdateRecord<T>(string table,ObjectId id,T record)
            {
                var coll = db.GetCollection<T>(table);
                var kq = coll.ReplaceOne(new BsonDocument("Id", id), record, new UpdateOptions { IsUpsert = true });
            }
            //Load data
            public List<T> LoadDataGrid<T>(string table)
            {
                var col = db.GetCollection<T>(table);

                return col.Find(new BsonDocument()).ToList();
            }
            //Truy van ID
            public List<T> TruyVanID<T>(string table, ObjectId id)
            {
                var col = db.GetCollection<T>(table);

                var filter = Builders<T>.Filter.Eq("Id", id);
                return col.Find(filter).ToList();
            }
            //public List<T> Sort<T>(string table)
            //{
            //    var col = db.GetCollection<T>(table);
            //    var sort = Builders<T>.Sort.Descending("Id");
            //    var highestScore = col.Find()

            //}
            //Gt theo GiaBan
            public List<T> DiemCaoNhat<T>(string table,double a)
            {
                var col = db.GetCollection<T>(table);
                var builder = Builders<T>.Filter.Gt("GIABAN", a);

                return col.Find(builder).ToList();
            }
            //Limit&Skip
            public List<T> LVS<T>(string table,int a)
            {
                var col = db.GetCollection<T>(table);
                return  col.Find(new BsonDocument()).Skip(a).Limit(a).ToList();
                
            }
            //Skip
            public List<T> S<T>(string table, int a)
            {
                var col = db.GetCollection<T>(table);
                return  col.Find(new BsonDocument()).Skip(a).ToList();
               
            }
            //Limit
            public List<T> L<T>(string table, int a)
            {
                var col = db.GetCollection<T>(table);
                
                return col.Find(new BsonDocument()).Limit(a).ToList();
            }
        }
        public void Them(TextBox txt1, TextBox txt2, TextBox txt3, TextBox txt4, TextBox txt5, TextBox txt6, TextBox txt7, ComboBox cb1, ComboBox cb2)
        {
            db.InsertRecord("SANPHAM", new SANPHAM { MASP = "" + txt1.Text + "", TENSP = "" + txt2.Text + "", GIABAN = double.Parse(txt3.Text), BAOHANH = "" + txt4.Text + "", UUDAITHEM = "" + txt5.Text + "", HINHANH = "" + txt6.Text + "", DACDIEM = "" + txt7.Text + "", LOAISP = new LOAISP { TENLOAI = "" + cb1.Text + "", MON = new MON { TENMON = "" + cb2.Text + "" } } });

        }
        public void Xoa(ComboBox cb)
        {
            db.DeleteRecord<SANPHAM>("SANPHAM", new ObjectId("" + cb.Text + ""));
        }
        public void Sua(TextBox txt1, TextBox txt2, TextBox txt3, TextBox txt4, TextBox txt5, TextBox txt6, ComboBox cb1, ComboBox cb2,ComboBox id)
        {

            db.UpdateRecord("SANPHAM", new ObjectId("" + id.Text + ""), new SANPHAM { MASP = "" + txt1.Text + "", TENSP = "" + txt2.Text + "", GIABAN = double.Parse(txt3.Text), BAOHANH = "" + txt4.Text + "", UUDAITHEM = "" + txt5.Text + "", DACDIEM = "" + txt6.Text + "", LOAISP = new LOAISP { TENLOAI = "" + cb1.Text + "", MON = new MON { TENMON = "" + cb2.Text + "" } } });
        }
        //tuy van ID
        public void Query(DataGridView data,ComboBox cb)
        {
            data.ColumnCount = 9;
            data.Columns[0].Name = "Mã";
            data.Columns[1].Name = "Tên sản phẩm";
            data.Columns[2].Name = "Giá bán";
            data.Columns[3].Name = "Bảo hành";
            data.Columns[4].Name = "Ưu đãi thêm";
            data.Columns[5].Name = "Hình ảnh";
            data.Columns[6].Name = "Đặc điểm";
            data.Columns[7].Name = "Loại sản phẩm";
            data.Columns[8].Name = "Môn thể thao";
            var rec = db.TruyVanID<SANPHAM>("SANPHAM", new ObjectId(""+cb.Text+""));
            foreach (var r in rec)
            {

                if (r.TENSP != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.MASP, r.TENSP, r.GIABAN, r.BAOHANH, r.UUDAITHEM, r.HINHANH, r.DACDIEM, r.LOAISP.TENLOAI, r.LOAISP.MON.TENMON);

                    data.Rows.Add(row);
       
                }
            }
            
            
        }
        public void Sort(DataGridView data,double a)
        {
            data.ColumnCount = 9;
            data.Columns[0].Name = "Mã";
            data.Columns[1].Name = "Tên sản phẩm";
            data.Columns[2].Name = "Giá bán";
            data.Columns[3].Name = "Bảo hành";
            data.Columns[4].Name = "Ưu đãi thêm";
            data.Columns[5].Name = "Hình ảnh";
            data.Columns[6].Name = "Đặc điểm";
            data.Columns[7].Name = "Loại sản phẩm";
            data.Columns[8].Name = "Môn thể thao";
            
            var rec = db.DiemCaoNhat<SANPHAM>("SANPHAM",a);
            foreach (var r in rec)
            {

                if (r.TENSP != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.MASP, r.TENSP, r.GIABAN, r.BAOHANH, r.UUDAITHEM, r.HINHANH, r.DACDIEM, r.LOAISP.TENLOAI, r.LOAISP.MON.TENMON);

                    data.Rows.Add(row);

                }
            }
        }
        //Limit&Skip
        public void LVS(DataGridView data, int a)
        {
            data.ColumnCount = 9;
            data.Columns[0].Name = "Mã";
            data.Columns[1].Name = "Tên sản phẩm";
            data.Columns[2].Name = "Giá bán";
            data.Columns[3].Name = "Bảo hành";
            data.Columns[4].Name = "Ưu đãi thêm";
            data.Columns[5].Name = "Hình ảnh";
            data.Columns[6].Name = "Đặc điểm";
            data.Columns[7].Name = "Loại sản phẩm";
            data.Columns[8].Name = "Môn thể thao";

            var rec = db.LVS<SANPHAM>("SANPHAM", a);
            foreach (var r in rec)
            {

                if (r.TENSP != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.MASP, r.TENSP, r.GIABAN, r.BAOHANH, r.UUDAITHEM, r.HINHANH, r.DACDIEM, r.LOAISP.TENLOAI, r.LOAISP.MON.TENMON);

                    data.Rows.Add(row);

                }
            }
        }
        //Limit
        public void L(DataGridView data, int a)
        {
            data.ColumnCount = 9;
            data.Columns[0].Name = "Mã";
            data.Columns[1].Name = "Tên sản phẩm";
            data.Columns[2].Name = "Giá bán";
            data.Columns[3].Name = "Bảo hành";
            data.Columns[4].Name = "Ưu đãi thêm";
            data.Columns[5].Name = "Hình ảnh";
            data.Columns[6].Name = "Đặc điểm";
            data.Columns[7].Name = "Loại sản phẩm";
            data.Columns[8].Name = "Môn thể thao";

            var rec = db.L<SANPHAM>("SANPHAM", a);
            foreach (var r in rec)
            {

                if (r.TENSP != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.MASP, r.TENSP, r.GIABAN, r.BAOHANH, r.UUDAITHEM, r.HINHANH, r.DACDIEM, r.LOAISP.TENLOAI, r.LOAISP.MON.TENMON);

                    data.Rows.Add(row);

                }
            }
        }
        //Skip
        public void S(DataGridView data, int a)
        {
            data.ColumnCount = 9;
            data.Columns[0].Name = "Mã";
            data.Columns[1].Name = "Tên sản phẩm";
            data.Columns[2].Name = "Giá bán";
            data.Columns[3].Name = "Bảo hành";
            data.Columns[4].Name = "Ưu đãi thêm";
            data.Columns[5].Name = "Hình ảnh";
            data.Columns[6].Name = "Đặc điểm";
            data.Columns[7].Name = "Loại sản phẩm";
            data.Columns[8].Name = "Môn thể thao";

            var rec = db.S<SANPHAM>("SANPHAM", a);
            foreach (var r in rec)
            {

                if (r.TENSP != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.MASP, r.TENSP, r.GIABAN, r.BAOHANH, r.UUDAITHEM, r.HINHANH, r.DACDIEM, r.LOAISP.TENLOAI, r.LOAISP.MON.TENMON);

                    data.Rows.Add(row);

                }
            }
        }
        //Load dataa
        public void LoadTestData(DataGridView data,ComboBox cb1,ComboBox cb2,ComboBox cb3)
        {
            data.ColumnCount = 9;
            data.Columns[0].Name = "Mã";
            data.Columns[1].Name = "Tên sản phẩm";
            data.Columns[2].Name = "Giá bán";
            data.Columns[3].Name = "Bảo hành";
            data.Columns[4].Name = "Ưu đãi thêm";
            data.Columns[5].Name = "Hình ảnh";
            data.Columns[6].Name = "Đặc điểm";
            data.Columns[7].Name = "Loại sản phẩm";
            data.Columns[8].Name = "Môn thể thao";
            var rec = db.LoadDataGrid<SANPHAM>("SANPHAM");
            foreach (var r in rec)
            {

                if (r.LOAISP != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.MASP, r.TENSP, r.GIABAN, r.BAOHANH, r.UUDAITHEM, r.HINHANH, r.DACDIEM, r.LOAISP.TENLOAI, r.LOAISP.MON.TENMON);

                    data.Rows.Add(row);
                    cb1.Items.Add(r.LOAISP.TENLOAI);
                    cb2.Items.Add(r.LOAISP.MON.TENMON);
                    cb3.Items.Add(r.Id);
                }
            }

        }
        //Load loai sp
        public void LoadTestData2(DataGridView data)
        {
            data.ColumnCount = 2;
            data.Columns[0].Name = "Mã";
            data.Columns[1].Name = "Tên loại";

            var rec = db.LoadDataGrid<SANPHAM>("SANPHAM");
            foreach (var r in rec)
            {

                if (r.LOAISP != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.LOAISP.Id, r.LOAISP.TENLOAI);

                    data.Rows.Add(row);
                }
            }
        }
        //Load Mon
        public void LoadTestData3(DataGridView data)
        {
            data.ColumnCount = 2;
            data.Columns[0].Name = "Mã";
            data.Columns[1].Name = "Tên môn";

            var rec = db.LoadDataGrid<SANPHAM>("SANPHAM");
            foreach (var r in rec)
            {

                if (r.LOAISP.MON != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.LOAISP.MON.Id, r.LOAISP.MON.TENMON);

                    data.Rows.Add(row);
                }
            }
        }

        public void SortData(DataGridView data,ComboBox  cb1   )
        {

            String searchValue = cb1.Text;
            int rowIndex = -1;
            foreach (DataGridViewRow row in data.Rows)
            {
                if (row.Cells[7].Value.ToString().Equals(searchValue))
                {
                    rowIndex = row.Index;
                    break;
                }
            } data.Rows[rowIndex].Selected = true;


        }
        public void SortData2(DataGridView data, ComboBox cb2)
        {

            String searchValue = cb2.Text;
            int rowIndex = -1;
            foreach (DataGridViewRow row in data.Rows)
            {
                if (row.Cells[8].Value.ToString().Equals(searchValue))
                {
                    rowIndex = row.Index;
                    break;
                }
            } data.Rows[rowIndex].Selected = true;


        }
        
    }
}
