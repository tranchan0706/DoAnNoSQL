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
    class CuaHangBus
    {
        //Class CUAHANG
        public class CUAHANG
        {
            public ObjectId Id { get; set; }
            public string TENCUAHANG { get; set; }
            public string DIACHI { get; set; }
            public string SDT { get; set; }
            public KHUVUC KHUVUC { get; set; }
            public QUANLY QUANLY { get; set; }

        }
        //Class KHUVUC
        public class KHUVUC
        {
            public double Id { get; set; }
            public string TENKV { get; set; }
            public THANHPHO THANHPHO { get; set; }

        }
        //Class THANHPHO
        public class THANHPHO
        {
            public double Id { get; set; }
            public string TENTP { get; set; }
        }
        //CLASS QUANLY
        public class QUANLY
        {
            public double Id { get; set; }
            public string TENQL { get; set; }
            public string SDT { get; set; }
            public string DIACHI { get; set; }
            public string NGAYVAOLAM { get; set; }
            public double HESOLUONG { get; set; }
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
            public void UpdateRecord<T>(string table, ObjectId id, T record)
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
            //Limit&Skip
            public List<T> LVS<T>(string table, int a,int b)
            {
                var col = db.GetCollection<T>(table);
                return col.Find(new BsonDocument()).Skip(a).Limit(b).ToList();

            }
            //Skip
            public List<T> S<T>(string table, int a)
            {
                var col = db.GetCollection<T>(table);
                return col.Find(new BsonDocument()).Skip(a).ToList();

            }
            //Limit
            public List<T> L<T>(string table, int a)
            {
                var col = db.GetCollection<T>(table);

                return col.Find(new BsonDocument()).Limit(a).ToList();
            }
            //tim theo khuvuc
            public List<T> Seach_TENKV<T>(string table, string a)
            {
                var col = db.GetCollection<T>(table);
                var filter = Builders<T>.Filter.Eq("KHUVUC.TENKV",a);
                return col.Find(filter).ToList();
            }
            //tim theo khuvuc va nguoi quan ly
            public List<T> Seach_TENKV_NQL<T>(string table, string a, string b)
            {
                var col = db.GetCollection<T>(table);
                var filter = new BsonDocument() { { "$and", new BsonArray() { new BsonDocument() { {"KHUVUC.TENKV", a} }, new BsonDocument() {{ "QUANLY.TENQL", b} } } } };
                return col.Find(filter).ToList();
            }

        }
        public void Them(TextBox txt1, TextBox txt2, TextBox txt3, TextBox txt4, TextBox txt5, TextBox txt6, TextBox txt7, TextBox txt8, ComboBox cb1, ComboBox cb2)
        {
            db.InsertRecord("CUAHANG", new CUAHANG { TENCUAHANG = "" + txt1.Text + "", DIACHI = "" + txt2.Text + "", SDT = "" + txt3.Text + "", KHUVUC = new KHUVUC { TENKV = "" + cb1.Text + "", THANHPHO = new THANHPHO { TENTP = "" + cb2.Text + "" } }, QUANLY = new QUANLY { TENQL = "" + txt4.Text + "", SDT = "" + txt5.Text + "", DIACHI = "" + txt6.Text + "", NGAYVAOLAM = "" + txt7.Text + "", HESOLUONG = double.Parse(txt8.Text) } });

        }
        public void Xoa(ComboBox cb)
        {
            db.DeleteRecord<CUAHANG>("CUAHANG", new ObjectId("" + cb.Text + ""));
        }
        public void Sua(TextBox txt1, TextBox txt2, TextBox txt3, TextBox txt4, TextBox txt5, TextBox txt6, TextBox txt7, TextBox txt8, ComboBox cb1, ComboBox cb2, ComboBox id)
        {
            db.UpdateRecord("CUAHANG", new ObjectId("" + id.Text + ""), new CUAHANG { TENCUAHANG = "" + txt1.Text + "", DIACHI = "" + txt2.Text + "", SDT = "" + txt3.Text + "", KHUVUC = new KHUVUC { TENKV = "" + cb1.Text + "", THANHPHO = new THANHPHO { TENTP = "" + cb2.Text + "" } }, QUANLY = new QUANLY { TENQL = "" + txt4.Text + "", SDT = "" + txt5.Text + "", DIACHI = "" + txt6.Text + "", NGAYVAOLAM = "" + txt7.Text + "", HESOLUONG = double.Parse(txt8.Text) } });
        }
        //tuy van ID
        public void Query(DataGridView data, ComboBox cb)
        {
            data.ColumnCount = 6;
            data.Columns[0].Name = "Tên cửa hàng";
            data.Columns[1].Name = "địa chỉ";
            data.Columns[2].Name = "số điện thoại";
            data.Columns[3].Name = "khu vực";
            data.Columns[4].Name = "thành phố";
            data.Columns[5].Name = "Quản lý";
            var rec = db.TruyVanID<CUAHANG>("CUAHANG", new ObjectId("" + cb.Text + ""));
            foreach (var r in rec)
            {

                if (r.TENCUAHANG != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.TENCUAHANG, r.DIACHI, r.SDT, r.KHUVUC.TENKV, r.KHUVUC.THANHPHO.TENTP, r.QUANLY.TENQL);

                    data.Rows.Add(row);

                }
            }


        }


        //Load dataa
        public void LoadTestData(DataGridView data, ComboBox cb1, ComboBox cb2, ComboBox cb3)
        {
            data.ColumnCount = 7;
            data.Columns[0].Name = "_id";
            data.Columns[1].Name = "Tên cửa hàng";
            data.Columns[2].Name = "địa chỉ";
            data.Columns[3].Name = "số điện thoại";
            data.Columns[4].Name = "khu vực";
            data.Columns[5].Name = "thành phố";
            data.Columns[6].Name = "Quản lý";
            var rec = db.LoadDataGrid<CUAHANG>("CUAHANG");
            foreach (var r in rec)
            {

                if (r.KHUVUC != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.Id, r.TENCUAHANG, r.DIACHI, r.SDT, r.KHUVUC.TENKV, r.KHUVUC.THANHPHO.TENTP, r.QUANLY.TENQL);

                    data.Rows.Add(row);
                    cb1.Items.Add(r.KHUVUC.TENKV);
                    cb2.Items.Add(r.KHUVUC.THANHPHO.TENTP);
                    cb3.Items.Add(r.Id);
                }
            }

        }
        //Load loai khu vuc
        public void LoadTestData2(DataGridView data)
        {
            data.ColumnCount = 4;
            data.Columns[0].Name = "Mã loại";
            data.Columns[1].Name = "Tên loại";
            data.Columns[2].Name = "Mã thành phố";
            data.Columns[3].Name = "Tên thành phố";

            var rec = db.LoadDataGrid<CUAHANG>("CUAHANG");
            foreach (var r in rec)
            {

                if (r.KHUVUC != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.KHUVUC.Id, r.KHUVUC.TENKV, r.KHUVUC.THANHPHO.Id, r.KHUVUC.THANHPHO.TENTP);

                    data.Rows.Add(row);
                }
            }
        }
        ////Load QUANLY
        public void LoadTestData3(DataGridView data)
        {
            data.ColumnCount = 6;
            data.Columns[0].Name = "Mã quản lý";
            data.Columns[1].Name = "Tên quản lý";
            data.Columns[2].Name = "Số điện thoại";
            data.Columns[3].Name = "Địa chỉ";
            data.Columns[4].Name = "Ngày vào làm";
            data.Columns[5].Name = "Hệ số lương";

            var rec = db.LoadDataGrid<CUAHANG>("CUAHANG");
            foreach (var r in rec)
            {

                if (r.QUANLY != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.QUANLY.Id, r.QUANLY.TENQL, r.QUANLY.SDT, r.QUANLY.DIACHI, r.QUANLY.NGAYVAOLAM, r.QUANLY.HESOLUONG);

                    data.Rows.Add(row);
                }
            }
        }

        //public void SortData(DataGridView data, TextBox cb1)
        //{
        //    String searchValue = cb1.Text;
        //    int rowIndex = -1;
        //    foreach (DataGridViewRow row in data.Rows)
        //    {
        //        if (row.Cells[5].Value.ToString().Equals(searchValue))
        //        {
        //            rowIndex = row.Index;
        //            break;
        //        }
        //    } data.Rows[rowIndex].Selected = true;
        //}
        //public void SortData2(DataGridView data, ComboBox cb2)
        //{

        //    String searchValue = cb2.Text;
        //    int rowIndex = -1;
        //    foreach (DataGridViewRow row in data.Rows)
        //    {
        //        if (row.Cells[8].Value.ToString().Equals(searchValue))
        //        {
        //            rowIndex = row.Index;
        //            break;
        //        }
        //    } data.Rows[rowIndex].Selected = true;


        //}
        //Limit&Skip
        public void LVS(DataGridView data, int a, int b)
        {
            data.ColumnCount = 7;
            data.Columns[0].Name = "_id";
            data.Columns[1].Name = "Tên cửa hàng";
            data.Columns[2].Name = "địa chỉ";
            data.Columns[3].Name = "số điện thoại";
            data.Columns[4].Name = "khu vực";
            data.Columns[5].Name = "thành phố";
            data.Columns[6].Name = "Quản lý";
            var rec = db.LVS<CUAHANG>("CUAHANG", a,b);
            foreach (var r in rec)
            {

                if (r.TENCUAHANG != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.Id, r.TENCUAHANG, r.DIACHI, r.SDT, r.KHUVUC.TENKV, r.KHUVUC.THANHPHO.TENTP, r.QUANLY.TENQL);

                    data.Rows.Add(row);

                }
            }
        }
        //Limit
        public void L(DataGridView data, int a)
        {
            data.ColumnCount = 7;
            data.Columns[0].Name = "_id";
            data.Columns[1].Name = "Tên cửa hàng";
            data.Columns[2].Name = "địa chỉ";
            data.Columns[3].Name = "số điện thoại";
            data.Columns[4].Name = "khu vực";
            data.Columns[5].Name = "thành phố";
            data.Columns[6].Name = "Quản lý";

            var rec = db.L<CUAHANG>("CUAHANG", a);
            foreach (var r in rec)
            {
                if (r.TENCUAHANG != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.Id, r.TENCUAHANG, r.DIACHI, r.SDT, r.KHUVUC.TENKV, r.KHUVUC.THANHPHO.TENTP, r.QUANLY.TENQL);

                    data.Rows.Add(row);

                }
            }
        }
        ////Skip
        public void S(DataGridView data, int a)
        {
            data.ColumnCount = 7;
            data.Columns[0].Name = "_id";
            data.Columns[1].Name = "Tên cửa hàng";
            data.Columns[2].Name = "địa chỉ";
            data.Columns[3].Name = "số điện thoại";
            data.Columns[4].Name = "khu vực";
            data.Columns[5].Name = "thành phố";
            data.Columns[6].Name = "Quản lý";

            var rec = db.S<CUAHANG>("CUAHANG", a);
            foreach (var r in rec)
            {
                if (r.TENCUAHANG != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.Id, r.TENCUAHANG, r.DIACHI, r.SDT, r.KHUVUC.TENKV, r.KHUVUC.THANHPHO.TENTP, r.QUANLY.TENQL);

                    data.Rows.Add(row);

                }
            }
        }
        public void Seach_TENKV_NQL(DataGridView data, string a, string b)
        {

            data.ColumnCount = 7;
            data.Columns[0].Name = "_id";
            data.Columns[1].Name = "Tên cửa hàng";
            data.Columns[2].Name = "địa chỉ";
            data.Columns[3].Name = "số điện thoại";
            data.Columns[4].Name = "khu vực";
            data.Columns[5].Name = "thành phố";
            data.Columns[6].Name = "Quản lý";
            var rec = db.Seach_TENKV_NQL<CUAHANG>("CUAHANG", a,b);
            foreach (var r in rec)
            {

                if (r.TENCUAHANG != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.Id, r.TENCUAHANG, r.DIACHI, r.SDT, r.KHUVUC.TENKV, r.KHUVUC.THANHPHO.TENTP, r.QUANLY.TENQL);

                    data.Rows.Add(row);

                }
            }
        }
        public void Seach_TENKV(DataGridView data, string a)
        {

            data.ColumnCount = 7;
            data.Columns[0].Name = "_id";
            data.Columns[1].Name = "Tên cửa hàng";
            data.Columns[2].Name = "địa chỉ";
            data.Columns[3].Name = "số điện thoại";
            data.Columns[4].Name = "khu vực";
            data.Columns[5].Name = "thành phố";
            data.Columns[6].Name = "Quản lý";
            var rec = db.Seach_TENKV<CUAHANG>("CUAHANG", a);
            foreach (var r in rec)
            {

                if (r.TENCUAHANG != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.Id, r.TENCUAHANG, r.DIACHI, r.SDT, r.KHUVUC.TENKV, r.KHUVUC.THANHPHO.TENTP, r.QUANLY.TENQL);

                    data.Rows.Add(row);

                }
            }
        }

        public void Load1(DataGridView data, ComboBox cb)
        {
            data.ColumnCount = 6;
            data.Columns[0].Name = "Mã quản lý";
            data.Columns[1].Name = "Tên quản lý";
            data.Columns[2].Name = "Số điện thoại";
            data.Columns[3].Name = "Địa chỉ";
            data.Columns[4].Name = "Ngày vào làm";
            data.Columns[5].Name = "Hệ số lương";
            var rec = db.TruyVanID<CUAHANG>("CUAHANG", new ObjectId("" + cb.Text + ""));
            foreach (var r in rec)
            {

                if (r.QUANLY != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.QUANLY.Id, r.QUANLY.TENQL, r.QUANLY.SDT, r.QUANLY.DIACHI, r.QUANLY.NGAYVAOLAM, r.QUANLY.HESOLUONG);

                    data.Rows.Add(row);

                }
            }
        }
        public void Load2(DataGridView data, ComboBox cb)
        {
            data.ColumnCount = 4;
            data.Columns[0].Name = "Mã loại";
            data.Columns[1].Name = "Tên loại";
            data.Columns[2].Name = "Mã thành phố";
            data.Columns[3].Name = "Tên thành phố";

            var rec = db.TruyVanID<CUAHANG>("CUAHANG", new ObjectId("" + cb.Text + ""));
            foreach (var r in rec)
            {

                if (r.KHUVUC != null)
                {
                    DataGridViewRow row = (DataGridViewRow)data.RowTemplate.Clone();
                    row.CreateCells(data, r.KHUVUC.Id, r.KHUVUC.TENKV, r.KHUVUC.THANHPHO.Id, r.KHUVUC.THANHPHO.TENTP);

                    data.Rows.Add(row);
                }
            }
        }
    }
}
