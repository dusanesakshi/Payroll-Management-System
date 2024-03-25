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

namespace Payroll_Management_System
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            CountEmployee();
            CountManager(); 
            salaryPaid();
            BonusPaid();
        }
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-V6C3CKB6\\SQLEXPRESS02;Initial Catalog=PMS2;Integrated Security=True");
        private void CountEmployee()
        {
            try {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) from pmsEmployeeTbl",conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lblEmployeeNo.Text = dt.Rows[0][0].ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CountManager()
        {
            try
            {
               
                string pos = "MANAGER";
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) from pmsEmployeeTbl where Position= '"+pos+"'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lblManagersNo.Text = dt.Rows[0][0].ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void salaryPaid()
        {
            try
            {
                
                string pos = "MANAGER";
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT SUM(Balance) from pmsSalaryTbl", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                salPaidAmt.Text = dt.Rows[0][0].ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BonusPaid()
        {
            try
            {

                
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT SUM(Bonus) from pmsSalaryTbl", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BonPaidAmt.Text = dt.Rows[0][0].ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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

        private void hOMEToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            hOMEToolStripMenuItem.BackColor = Color.Transparent;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            //CountEmployee();
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
