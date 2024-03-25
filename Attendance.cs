using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Payroll_Management_System
{
    public partial class Attendance : Form
    {
        public Attendance()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-V6C3CKB6\\SQLEXPRESS02;Initial Catalog=PMS2;Integrated Security=True");

        private void hOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show(this);
            this.Hide();
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.Show(this);
            this.Hide();
        }

        private void bonusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bonus bonus = new Bonus();
            bonus.Show(this);
            this.Hide();
        }

        private void advanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Advance advance = new Advance();
            advance.Show(this);
            this.Hide();
        }

        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Attendance attendance = new Attendance();
            attendance.Show(this);
            this.Hide();
        }

        private void issueSalaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueSalary issueSalary = new IssueSalary();
            issueSalary.Show(this);
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Attendance_Load(object sender, EventArgs e)
        {
            display();
            getEmployees();
          
        }

        private void display()
        {
            SqlCommand cmd = new SqlCommand("select * from pmsAttendanceTbl",conn);
            DataTable dt = new DataTable();

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
            
        }

        private bool isValid()
        {
            if (txtAbsentDay.Text == ""|| txtExcusedDay.Text == ""|| txtName.Text == "" || txtPresentDay.Text == "" || cmbEmpId.SelectedIndex == -1 || dtpPeroid.Value == DateTime.Now)
            {
                MessageBox.Show("Erro");
                return false;
            }
            return true;
        }
        private void getEmployees()
        {
            conn.Open();
            SqlCommand vmd = new SqlCommand("select * from pmsEmployeeTbl", conn);
            SqlDataReader sqlDataReader = vmd.ExecuteReader();

            DataTable dtr =new DataTable();
            dtr.Columns.Add("Id",typeof(int));
            dtr.Load(sqlDataReader);
            cmbEmpId.ValueMember = "Id";
            cmbEmpId.DataSource= dtr;

            conn.Close();
        }

        private void getEmployeeName()
        {
            conn.Open();
            string querry = "Select * from pmsEmployeeTbl where Id=" + cmbEmpId.SelectedValue.ToString() + "";
            SqlCommand vmd = new SqlCommand(querry,conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(vmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtName.Text = dr["Name"].ToString();
            }

            conn.Close();
        }

        private void CancleData()
        {
            txtAbsentDay.Text = "";
            txtExcusedDay.Text = "";
            txtName.Text = "";
            txtPresentDay.Text = "";
            cmbEmpId.SelectedIndex = -1;
            dtpPeroid.Value=DateTime.Now;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            CancleData();
        }

        private void cmbEmpId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getEmployeeName();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(isValid())
                {
                    conn.Open();
                    SqlCommand sql = new SqlCommand("Insert into pmsAttendanceTbl(E_Id,Name,Present_days,Absent_days,Excused_days,peroid) values(@Eid,@EN,@PD,@AD,@ED,@EP)",conn);
                    sql.Parameters.AddWithValue("@Eid",cmbEmpId.Text);
                    sql.Parameters.AddWithValue("@EN",txtName.Text);
                    sql.Parameters.AddWithValue("@PD", txtPresentDay.Text);
                    sql.Parameters.AddWithValue("@AD",txtAbsentDay.Text);
                    sql.Parameters.AddWithValue("@ED", txtExcusedDay.Text);
                    sql.Parameters.AddWithValue("@EP", dtpPeroid.Value.Date);
                   
                    sql.ExecuteNonQuery();
                    MessageBox.Show("INSERTED");
                    conn.Close();
                    display();
                    CancleData();
                }
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        int AttId;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AttId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            cmbEmpId.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtPresentDay.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtAbsentDay.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtExcusedDay.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            dtpPeroid.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValid())
                {
                    conn.Open();
                    SqlCommand sql = new SqlCommand("update pmsAttendanceTbl set E_Id=@Eid,Name=@EN,Present_days=@PD,Absent_days=@AD,Excused_days=@ED,peroid=@EP where Att_ID=@Attkey", conn);
                    sql.Parameters.AddWithValue("@Eid", cmbEmpId.Text);
                    sql.Parameters.AddWithValue("@EN", txtName.Text);
                    sql.Parameters.AddWithValue("@PD", txtPresentDay.Text);
                    sql.Parameters.AddWithValue("@AD", txtAbsentDay.Text);
                    sql.Parameters.AddWithValue("@ED", txtExcusedDay.Text);
                    sql.Parameters.AddWithValue("@EP", dtpPeroid.Value.Date);
                    sql.Parameters.AddWithValue("@Attkey",AttId);

                    sql.ExecuteNonQuery();
                    MessageBox.Show("UPDATED");
                    conn.Close();
                    display();
                    CancleData();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValid())
                {
                    conn.Open();
                    SqlCommand sql = new SqlCommand("delete from pmsAttendanceTbl where Att_ID=@Attkey", conn);
                    
                    sql.Parameters.AddWithValue("@Attkey", AttId);

                    sql.ExecuteNonQuery();
                    MessageBox.Show("Deleted");
                    conn.Close();
                    display();
                    CancleData();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackColor = Color.Lime;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.Transparent;
        }
    }
}
