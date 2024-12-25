using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Cache;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Display();
        }









        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public List<NhanVien> GetAll()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44342/api/")
            };
            var response = client.GetAsync("NhanVien").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<List<NhanVien>>().Result : null;
        }
        public void Display()
        {
            dgvNhanVien.DataSource = GetAll();
        }
        private void dgvNhanVien_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                txtMaNV.Text = dgvNhanVien.Rows[index].Cells[0].Value.ToString();
                txtTen.Text = dgvNhanVien.Rows[index].Cells[1].Value.ToString();
                txtTrinhDo.Text = dgvNhanVien.Rows[index].Cells[3].Value.ToString();
                txtLuong.Text = dgvNhanVien.Rows[index].Cells[2].Value.ToString();
            }
        }
        public NhanVien GetNhanVien()
        {
            return new NhanVien() { MaNV = txtMaNV.Text, HoTen = txtTen.Text, TrinhDoHoc = txtTrinhDo.Text, Luong = decimal.Parse(txtLuong.Text) };
        }
        public bool Add(NhanVien x)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44342/api/")
            };
            var response = client.PostAsJsonAsync("NhanVien", x).Result;
            return response.IsSuccessStatusCode;

        }
        public bool Edit(NhanVien x)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44342/api/")
            };
            var response = client.PutAsJsonAsync("NhanVien", x).Result;
            return response.IsSuccessStatusCode;

        }
        public bool Delete(string id)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44342/api/")
            };
            var response = client.DeleteAsync("NhanVien?id=" + id).Result;
            return response.IsSuccessStatusCode;
        }
        public NhanVien Search(string id)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44342/api/")
            };
            var response = client.GetAsync("NhanVien?id=" + id).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<NhanVien>().Result : null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Add(GetNhanVien()))
            {
                MessageBox.Show("Không thêm được nv");
                return;
            }
            Display();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Edit(GetNhanVien()))
            {
                MessageBox.Show("Không sửa được nv");
                return;
            }
            Display();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            if (!Delete(txtMaNV.Text))
            {
                MessageBox.Show("Không xóa được nv");
                return;
            }
            Display();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<NhanVien> li = new List<NhanVien>();
            li.Add(Search(txtMaNV.Text));
            dgvNhanVien.DataSource = li.ToList();
            if (Search(txtMaNV.Text) == null)
            {
                MessageBox.Show("Không tìm thấy nv");
            }

        }
    }
}
