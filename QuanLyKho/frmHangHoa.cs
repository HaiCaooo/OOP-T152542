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
    public partial class frmHangHoa : Form
    {
        public frmHangHoa()
        {
            InitializeComponent();

        }
        QuanLyKhoHangEntities db = new QuanLyKhoHangEntities();
        int chosen = -1;
        private void frmHangHoa_Load(object sender, EventArgs e)
        {
            ShowHangHoa();
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            dgvHangHoa.CellDoubleClick += DgvHangHoa_CellDoubleClick;
            btnXoa.Click += BtnXoa_Click;
            btnNCC.Click += BtnNCC_Click;
        }

        private void BtnNCC_Click(object sender, EventArgs e)
        {
            frmNhaCungCap form = new frmNhaCungCap();
            form.ShowDialog();
            ShowHangHoa();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa sản phẩm không", "thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (dgvHangHoa.SelectedRows.Count == 1)
                {
                    var id = (int)dgvHangHoa.SelectedRows[0].Cells[0].Value;
                    var sp = db.SanPhams.Find(id);
                    db.SanPhams.Remove(sp);
                    db.SaveChanges();
                    ShowHangHoa();

                    txtMoTa.Text = "";
                    txtTenHH.Text = "";
                    nudGia.Value = 0; 
                }
            }
        }

        private void DgvHangHoa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHangHoa.SelectedRows.Count == 1)
            {
                var row = dgvHangHoa.SelectedRows[0];
                var cell = row.Cells["ID"];
                int id = (int)cell.Value;
                var sp = db.SanPhams.Find(id);
                txtTenHH.Text = sp.TenSP;
                txtMoTa.Text = sp.MoTa;
                nudGia.Value = sp.DonGia;
                cboNCC.SelectedValue = sp.ID_NCC;
                chosen = id;
            }
            ShowHangHoa();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvHangHoa.SelectedRows.Count == 1)
            {
                if (chosen != -1)
                {
                    var sp = db.SanPhams.Find(chosen);

                    sp.TenSP = txtTenHH.Text;
                    sp.MoTa = txtMoTa.Text;
                    sp.DonGia = (int)nudGia.Value;
                    sp.ID_NCC = (int)cboNCC.SelectedValue;
                    txtMoTa.Text = "";
                    txtTenHH.Text = "";
                    nudGia.Value = 0;
                    try
                    {
                        db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
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

                ShowHangHoa();
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            SanPham sp = new SanPham();
            sp.DonGia = (int)nudGia.Value;
            sp.TenSP = txtTenHH.Text;
            sp.MoTa = txtMoTa.Text;
            sp.ID_NCC = (int)cboNCC.SelectedValue;

            try
            {
                db.SanPhams.Add(sp);
                db.SaveChanges();
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
            ShowHangHoa();

        }

        private void ShowHangHoa()
        {
            //List<SanPham> hanghoa = db.SanPhams.ToList();
            var hanghoa = db.SanPhams.Select(m => new {
                ID = m.ID,
                TenSP = m.TenSP,
                DonGia = m.DonGia,
                Mota = m.MoTa,
                NhaCungCap = m.NhaCungCap.TenNCC,
            }).ToList();
            this.dgvHangHoa.DataSource = hanghoa;
            dgvHangHoa.Columns["id"].Width = 50;
            //dgvHangHoa.Columns["id_NCC"].Visible = false;
            dgvHangHoa.Columns["NhaCungCap"].DisplayIndex = 3;
            dgvHangHoa.Columns["ID"].HeaderText = "Mã Hàng Hóa";
            dgvHangHoa.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
            dgvHangHoa.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvHangHoa.Columns["NhaCungCap"].HeaderText = "Nhà Cung Cấp";
            dgvHangHoa.Columns["MoTa"].HeaderText = "Mô Tả Sản Phẩm";
            List<NhaCungCap> ncc = db.NhaCungCaps.ToList();
            this.cboNCC.DataSource = ncc;
            cboNCC.DisplayMember = "TenNCC";
            cboNCC.ValueMember = "ID";

        }

    }
}
