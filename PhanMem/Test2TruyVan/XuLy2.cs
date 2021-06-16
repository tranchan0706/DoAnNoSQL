using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2TruyVan
{
    public class XuLy2
    {
        public class MongoCRUD
        {
            private IMongoDatabase db;
            public MongoCRUD(string database)
            {
                var client = new MongoClient();
                db = client.GetDatabase(database);
            }
            public void InsertRecord<T>(string table, T record)
            {
                var collection = db.GetCollection<T>(table);
                collection.InsertOne(record);
            }
            //Load DATA
            public List<T> LoadTest<T>(string table)
            {
                var col = db.GetCollection<T>(table);

                return col.Find(new BsonDocument()).ToList();
            }
            //Truy vấn
            public List<T> TruyVanID<T>(string table, ObjectId id)
            {
                var col = db.GetCollection<T>(table);

                var filter = Builders<T>.Filter.Eq("Id", id);
                return col.Find(filter).ToList();
            }
            //Xóa Khách
            public void DeleteRecord<T>(string table, ObjectId id)
            {
                var coll = db.GetCollection<T>(table);
                var filter = Builders<T>.Filter.Eq("Id", id);
                coll.DeleteOne(filter);

            }
            //Sua
            public void UpdateRecord<T>(string table, ObjectId id, T record)
            {
                var coll = db.GetCollection<T>(table);
                var kq = coll.ReplaceOne(new BsonDocument("Id", id), record, new UpdateOptions { IsUpsert = true });
            }
            //public T LoadTest2<T>(string table, string tenloai)
            //{
            //    var col = db.GetCollection<T>(table);

            //    var filter = Builders<T>.Filter.Eq("TENLOAI", tenloai);
            //    return col.Find(filter).First();
            //}
            public List<T> DiemCaoNhat<T>(string table, double a)
            {
                var col = db.GetCollection<T>(table);
                var filter = new BsonDocument() { { "HOADON.TONGTIEN", new BsonDocument() { { "$gt", a } } } };
                
                
                return col.Find(filter).ToList();
            }
            public List<T>ThongTinKhach<T>(string table)
            {
                var col = db.GetCollection<T>(table);
                var pp = new[]{new BsonDocument(){{"$project",new BsonDocument(){{"_id",0},{"MAKHACH",1},{"TENKHACH",1},{"HOADON.TONGTIEN",1}}}}};
                return col.Aggregate<T>(pp).ToList();
            }
        }

        MongoCRUD db = new MongoCRUD("QLTHETHAO");
        public class KHACH
        {
            public ObjectId Id { get; set; }
            public string MAKHACH { get; set; }
            public string TENKHACH { get; set; }
            public string GIOITINH { get; set; }
            public DateTime NGAYSINH { get; set; }
            public string DIACHI { get; set; }
            public string SDT { get; set; }
            public LOAIKHACH LOAIKHACH{ get; set; }
            public HOADON HOADON { get; set; }
        }
        public class LOAIKHACH
        {
            public string MALOAIKHACH { get;set; }
            public string TENLOAIKHACH { get; set; }
        }
        public class HOADON
        {
            public string MAHD { get; set; }
            public DateTime NGAYIN { get; set; }
            public string TRANGTHAI { get; set; }
            public int TONGTIEN { get; set; }
            public string CHUTHICH { get; set; }
        }
        public void LoadTestData(DataGridView data, ComboBox cb1,ComboBox cb2)
        {
            data.ColumnCount = 8;
            data.Columns[0].Name = "Mã khách";
            data.Columns[1].Name = "Tên khách";
            data.Columns[2].Name = "Giới tính";
            data.Columns[3].Name = "Ngày sinh";
            data.Columns[4].Name = "Địa chỉ";
            data.Columns[5].Name = "SĐT";
            data.Columns[6].Name = "Loại khách";
            data.Columns[7].Name = "Hóa đơn";
            var rec = db.LoadTest<KHACH>("KHACHHANG");
            foreach (var r in rec)
            {

                if (r.MAKHACH != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.MAKHACH, r.TENKHACH, r.GIOITINH, r.NGAYSINH, r.DIACHI, r.SDT, r.LOAIKHACH.TENLOAIKHACH, r.HOADON.MAHD);

                    data.Rows.Add(row);
                    cb1.Items.Add(r.LOAIKHACH.TENLOAIKHACH);
                    cb2.Items.Add(r.Id);
                }
            }

        }
        public void Them(TextBox txt1, TextBox txt2, TextBox txt3, TextBox txt4, TextBox txt5, TextBox txt6,TextBox txt7,TextBox txt8,TextBox txt9,TextBox txt10, ComboBox cb1)
        {
            
                DateTime a = DateTime.ParseExact(txt4.Text, "d", null);
                DateTime b = DateTime.Now;
                db.InsertRecord("KHACHHANG", new KHACH { MAKHACH = "" + txt1.Text + "", TENKHACH = "" + txt2.Text + "", GIOITINH = "" + txt3.Text + "", NGAYSINH = a, DIACHI = "" + txt5.Text + "", SDT = "" + txt6.Text + "", LOAIKHACH = new LOAIKHACH { TENLOAIKHACH = "" + cb1.Text + "" }, HOADON = new HOADON { MAHD = "" + txt7.Text + "", NGAYIN = b, TRANGTHAI = "" + txt8.Text + "", TONGTIEN = int.Parse(txt9.Text), CHUTHICH = txt10.Text } });
            
            
        }
        //Truy van Khach
        public void Query(DataGridView data, ComboBox cb)
        {
            
            data.ColumnCount = 8;
            data.Columns[0].Name = "Mã khách";
            data.Columns[1].Name = "Tên khách";
            data.Columns[2].Name = "Giới tính";
            data.Columns[3].Name = "Ngày sinh";
            data.Columns[4].Name = "Địa chỉ";
            data.Columns[5].Name = "SĐT";
            data.Columns[6].Name = "Loại khách";
            data.Columns[7].Name = "Hóa đơn";
            var rec = db.TruyVanID<KHACH>("KHACHHANG", new ObjectId("" + cb.Text + ""));
            foreach (var r in rec)
            {

                if (r.MAKHACH != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.MAKHACH, r.TENKHACH, r.GIOITINH, r.NGAYSINH, r.DIACHI, r.SDT, r.LOAIKHACH.TENLOAIKHACH, r.HOADON.MAHD);

                    data.Rows.Add(row);
                   
                }
            }
        }
        public void GT(DataGridView data, int a)
        {
            data.ColumnCount = 8;
            data.Columns[0].Name = "Mã khách";
            data.Columns[1].Name = "Tên khách";
            data.Columns[2].Name = "Giới tính";
            data.Columns[3].Name = "Ngày sinh";
            data.Columns[4].Name = "Địa chỉ";
            data.Columns[5].Name = "SĐT";
            data.Columns[6].Name = "Loại khách";
            data.Columns[7].Name = "Hóa đơn";
            var rec = db.DiemCaoNhat<KHACH>("KHACHHANG", a);
            foreach (var r in rec)
            {

                if (r.MAKHACH != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.MAKHACH, r.TENKHACH, r.GIOITINH, r.NGAYSINH, r.DIACHI, r.SDT, r.LOAIKHACH.TENLOAIKHACH, r.HOADON.MAHD);

                    data.Rows.Add(row);

                }
            }
        }
        public void TT(DataGridView data)
        {
            data.ColumnCount = 3;
            data.Columns[0].Name = "Mã khách";
            data.Columns[1].Name = "Tên khách";
            data.Columns[2].Name = "Tổng tiền";
            var rec = db.ThongTinKhach<KHACH>("KHACHHANG");
            foreach (var r in rec)
            {

                if (r.MAKHACH != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.MAKHACH, r.TENKHACH, r.HOADON.TONGTIEN);

                    data.Rows.Add(row);

                }
            }
        }
        //Xoa
        public void Xoa(ComboBox cb)
        {
            
                db.DeleteRecord<KHACH>("KHACHHANG", new ObjectId("" + cb.Text + ""));
               

        }
        public void Sua(TextBox txt1, TextBox txt2, TextBox txt3, TextBox txt4, TextBox txt5, TextBox txt6, TextBox txt7, TextBox txt8, TextBox txt9, TextBox txt10, ComboBox cb1,ComboBox id)
        {
            
                DateTime a = DateTime.ParseExact(txt4.Text, "d", null);
                DateTime b = DateTime.Now;
                db.UpdateRecord("KHACHHANG", new ObjectId("" + id.Text + ""), new KHACH { MAKHACH = "" + txt1.Text + "", TENKHACH = "" + txt2.Text + "", GIOITINH = "" + txt3.Text + "", NGAYSINH = a, DIACHI = "" + txt5.Text + "", SDT = "" + txt6.Text + "", LOAIKHACH = new LOAIKHACH { TENLOAIKHACH = "" + cb1.Text + "" }, HOADON = new HOADON { MAHD = "" + txt7.Text + "", NGAYIN = b, TRANGTHAI = "" + txt8.Text + "", TONGTIEN = int.Parse(txt9.Text), CHUTHICH = txt10.Text } });
               

        }
    }
}
