using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKho
{
    public partial class frmNhaCungCap : Form
    {
        QuanLyKhoHangEntities db = new QuanLyKhoHangEntities();
        int chosen = -1;
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            ShowNhaCungCap();
            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnSua.Click += BtnSua_Click;
            dgvNhaCungCap.CellDoubleClick += DgvNhaCungCap_CellDoubleClick;
        }

        private void DgvNhaCungCap_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNhaCungCap.SelectedRows.Count == 1)
            {
                var row = dgvNhaCungCap.SelectedRows[0];
                var cell = row.Cells["ID"];
                int id = (int)cell.Value;
                var sp = db.NhaCungCaps.Find(id);
                txtTenNCC.Text = sp.TenNCC;
                txtDiaChi.Text = sp.DiaChi;
                txtSDTNCC.Text = sp.DienThoai.ToString();
                chosen = id;
            }
            ShowNhaCungCap();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhaCungCap.SelectedRows.Count == 1)
            {
                if (chosen != -1)
                {
                    var ncc = db.NhaCungCaps.Find(chosen);

                    ncc.TenNCC = txtTenNCC.Text;
                    ncc.DiaChi = txtDiaChi.Text;
                    ncc.DienThoai = int.Parse(txtSDTNCC.Text);
                   
                    txtTenNCC.Text = "";
                    txtDiaChi.Text = "";
                    txtSDTNCC.Text = "";
                    try
                    {
                        db.Entry(ncc).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        chosen = -1;
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please choose item");
                }

            }
            ShowNhaCungCap();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa sản phẩm không", "thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (dgvNhaCungCap.SelectedRows.Count == 1)
                {
                    var id = (int)dgvNhaCungCap.SelectedRows[0].Cells[0].Value;
                    var ncc = db.NhaCungCaps.Find(id);
                    db.NhaCungCaps.Remove(ncc);
                    db.SaveChanges();                  

                    txtDiaChi.Text = "";
                    txtTenNCC.Text = "";
                    txtSDTNCC.Text = "";
                }
                ShowNhaCungCap();
            }
        }
        private void BtnThem_Click(object sender, EventArgs e)
        {
            NhaCungCap ncc = new NhaCungCap();
            ncc.TenNCC = txtTenNCC.Text;
            ncc.DiaChi = txtDiaChi.Text;
            ncc.DienThoai = int.Parse(txtSDTNCC.Text);
            try
            {
                db.NhaCungCaps.Add(ncc);
                db.SaveChanges();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void ShowNhaCungCap()
        {
            var ncc = db.NhaCungCaps.ToList();
            dgvNhaCungCap.DataSource = ncc;
            dgvNhaCungCap.Columns["id"].Width = 50;
            dgvNhaCungCap.Columns["ID"].HeaderText = "Mã Nhà Cung Cấp";
            dgvNhaCungCap.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
            dgvNhaCungCap.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvNhaCungCap.Columns["DienThoai"].HeaderText = "Số Điện Thoại";
            dgvNhaCungCap.Columns["SanPhams"].Visible = false;
        }

        private void txtSDTNCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Chỉ Được Nhập Số Trong Mục Số Điện Thoại");
            }
        }

    }
}
