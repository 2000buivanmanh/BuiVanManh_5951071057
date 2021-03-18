using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoCRUD
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-UML28IP\MSSQLSERVER01;Initial Catalog=demoCRUD;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudensRecord();
        }
        private void GetStudensRecord()
        {
            
            SqlCommand cmd = new SqlCommand(" SELECT * FROM StudentsTb ", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgvShow.DataSource = dt;
        }

        private bool IsVaLydData()
        {
            if(txtHosv.Text == string.Empty
                || txtDiaChi.Text == string.Empty
                || txtTenSV.Text == string.Empty
                || string.IsNullOrEmpty(txtSoBD.Text)
                || string.IsNullOrEmpty(txtSoDT.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu!!!", "lỗi dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            return true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("bạn có muốn thoát ", "thoát",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (IsVaLydData())
            {
                SqlCommand cmd = new SqlCommand("insert into StudentsTb values " +
                    "(@Name, @FatherName, @RollNumber, @Address , @Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtHosv.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtTenSV.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtSoBD.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtSoDT.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudensRecord();
            }
        }
        public int StudenID;
        private void dgvShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            StudenID = Convert.ToInt32(dgvShow.Rows[index].Cells[0].Value);
            txtHosv.Text = dgvShow.Rows[index].Cells[1].Value.ToString();
            txtTenSV.Text = dgvShow.Rows[index].Cells[2].Value.ToString();
            txtSoBD.Text = dgvShow.Rows[index].Cells[3].Value.ToString();
            txtDiaChi.Text = dgvShow.Rows[index].Cells[4].Value.ToString();
            txtSoDT.Text = dgvShow.Rows[index].Cells[5].Value.ToString();
        }

        private void btnCapnhap_Click(object sender, EventArgs e)
        {
            if (StudenID > 0)
            {
                SqlCommand cmd = new SqlCommand("update StudentsTb set " +
                    " Name = @Name , FatherName = @FatherName , RollNumber = @RollNumber ," +
                    " Address = @Address , Mobile = @Mobile where StudenID = @ID" , con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtHosv.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtTenSV.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtSoBD.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtSoDT.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudenID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudensRecord();
            }
            else
            {
                MessageBox.Show("cập nhập lỗi", "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (StudenID > 0)
            {
                SqlCommand cmd = new SqlCommand("delete from StudentsTb where StudenID = @ID", con);
                cmd.CommandType = CommandType.Text;      
                cmd.Parameters.AddWithValue("@ID", this.StudenID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudensRecord();
            }
            else
            {
                MessageBox.Show("cập nhập lỗi", "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }

}
