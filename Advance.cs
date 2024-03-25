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
    public partial class Advance : Form
    {
        public Advance()
        {
            InitializeComponent();
        }
        
        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-V6C3CKB6\\SQLEXPRESS02;Initial Catalog=PMS2;Integrated Security=True");

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

        private void Advance_Load(object sender, EventArgs e)
        {
            display();
        }
        private void display()
        {
            SqlCommand cmd = new SqlCommand("select * from pmsAdvanceTbl",connection);
            DataTable dt = new DataTable();
            connection.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            connection.Close();
            dataGridView1.DataSource = dt;
        }

        private void CancleData()
        {
            txtAdvAmt.Text = "";
            txtAdvName.Text = "";
        }
        private Boolean isValid()
        {
            if (txtAdvAmt.Text==""|| txtAdvName.Text=="")
            {
                MessageBox.Show("Please fill all detail","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        private void btnCancle_Click(object sender, EventArgs e)
        {
            CancleData();
        }

        private void hOMEToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show(this);
            this.Hide();
        }

        private void employeesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.Show(this);
            this.Hide();
        }

        private void bonusToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Bonus bonus = new Bonus();
            bonus.Show(this);
            this.Hide();
        }

        private void attendanceToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Attendance attendance = new Attendance();
            attendance.Show(this);
            this.Hide();
        }

        private void issueSalaryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            IssueSalary issueSalary = new IssueSalary();
            issueSalary.Show(this);
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(isValid())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Insert into pmsAdvanceTbl(Name,Amount) values(@AN, @AA)", connection);
                    cmd.Parameters.AddWithValue("@AN",txtAdvName.Text);
                    cmd.Parameters.AddWithValue("@AA",txtAdvAmt.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("INSERTED");
                    connection.Close();
                    display();
                    CancleData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
             }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if(isValid())
                {
                    if (AmtKey > 0)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("update pmsAdvanceTbl set Name=@AN, Amount=@AA where Aid=@amt",connection);
                        cmd.Parameters.AddWithValue("@AN",txtAdvName.Text);
                        cmd.Parameters.AddWithValue("@AA", txtAdvAmt.Text);
                        cmd.Parameters.AddWithValue("@amt",AmtKey);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("UPDATED");
                        connection.Close();
                        display();
                        CancleData();
                    }
                    else
                    {
                        MessageBox.Show("ERROR");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }
        int AmtKey = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AmtKey = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            txtAdvName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtAdvAmt.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(isValid())
                {
                    if(AmtKey>0)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("delete from pmsAdvanceTbl where Aid=@amt",connection);
                        cmd.Parameters.AddWithValue("amt",AmtKey);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Deleted");
                        connection.Close();
                        display();
                        CancleData();

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
