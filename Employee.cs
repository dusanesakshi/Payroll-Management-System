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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

    SqlConnection conn= new SqlConnection("Data Source=LAPTOP-V6C3CKB6\\SQLEXPRESS02;Initial Catalog=PMS2;Integrated Security=True");

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hOMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show(this);
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

        private void Employee_Load(object sender, EventArgs e)
        {
            display();
        }
        private void display()
        {
            SqlCommand cmd = new SqlCommand("select * from pmsEmployeeTbl",conn);
            DataTable dt = new DataTable();

            conn.Open();
            SqlDataReader sdr =cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();

            dataGridView1.DataSource = dt;  
        }

        private bool isValid()
        {
            if (txtName.Text == "" || txtPhone.Text == "" || txtBaseSalary.Text =="" || txtAddress.Text == ""|| cmbGender.SelectedIndex == -1 || cmbPosition.SelectedIndex == -1 || cmbQualification.SelectedIndex == -1 || dtpJoinDate.Value == DateTime.Now || dtpDob.Value == DateTime.Now)
            {
                MessageBox.Show("Please fill all details","failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void CancleData()
        {
            txtName.Text = "";
            txtPhone.Text = "";
            txtBaseSalary.Text = "";
            txtAddress.Text = "";
            cmbGender.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            cmbQualification.SelectedIndex = -1;
            dtpJoinDate.Value= DateTime.Now;
            dtpDob.Value= DateTime.Now;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CancleData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValid())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into pmsEmployeeTbl(Name,Gender,DOB,Phone,Address,Position,Joining,Qualification,BaseSalary) values(@EN,@EG,@ED,@EP,@EA,@EPos,@JD,@EQ,@EBS)",conn);
                    cmd.Parameters.AddWithValue("@EN", txtName.Text);
                    cmd.Parameters.AddWithValue("@EG", cmbGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ED", dtpDob.Value.Date);
                    cmd.Parameters.AddWithValue("@EP", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@EA",txtAddress.Text);
                    cmd.Parameters.AddWithValue("@EPos",cmbPosition.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@JD",dtpJoinDate.Value.Date);
                    cmd.Parameters.AddWithValue("@EQ",cmbQualification.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EBS", txtBaseSalary.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("INSERTED");
                    conn.Close();
                    CancleData();
                    display();

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int employeeID = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            employeeID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            txtName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            cmbGender.SelectedItem = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            dtpDob.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtPhone.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtAddress.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            cmbPosition.SelectedItem = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            dtpJoinDate.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            cmbQualification.SelectedItem = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            txtBaseSalary.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();    

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValid())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update pmsEmployeeTbl set Name=@EN,Gender=@EG,DOB=@ED,Phone=@EP,Address=@EA,Position=@EPos,Joining=@JD,Qualification=@EQ,BaseSalary=@EBS where Id=@EmpID", conn);
                    cmd.Parameters.AddWithValue("@EN", txtName.Text);
                    cmd.Parameters.AddWithValue("@EG", cmbGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ED", dtpDob.Value.Date);
                    cmd.Parameters.AddWithValue("@EP", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@EA", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@EPos", cmbPosition.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@JD", dtpJoinDate.Value.Date);
                    cmd.Parameters.AddWithValue("@EQ", cmbQualification.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EBS", txtBaseSalary.Text);
                    cmd.Parameters.AddWithValue("@EmpID", employeeID);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("UPDATED");
                    conn.Close();
                    CancleData();
                    display();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValid())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete from pmsEmployeeTbl where Id=@EmpID", conn);
                    cmd.Parameters.AddWithValue("@EmpID",employeeID);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("DELETED");
                    conn.Close();
                    CancleData();
                    display();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
