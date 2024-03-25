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
using System.Runtime.CompilerServices;

namespace Payroll_Management_System
{
    public partial class IssueSalary : Form
    {
        public IssueSalary()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-V6C3CKB6\\SQLEXPRESS02;Initial Catalog=PMS2;Integrated Security=True");
        private void issueSalaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueSalary issueSalary = new IssueSalary();
            issueSalary.Show(this);
            this.Hide();
        }

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

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hOMEToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {

        }

        private void IssueSalary_Load(object sender, EventArgs e)
        {
            display();
            getEmployees();
            getAttedance();
            getBonus();

        }
        private void display()
        {
            SqlCommand cmd = new SqlCommand("select * from pmsSalaryTbl", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;

        }
        private void getEmployees()
        {
            conn.Open();
            SqlCommand vmd = new SqlCommand("select * from pmsEmployeeTbl", conn);
            SqlDataReader sqlDataReader = vmd.ExecuteReader();

            DataTable dtr = new DataTable();
            dtr.Columns.Add("Id", typeof(int));
            dtr.Load(sqlDataReader);
            cmbEmpID.ValueMember = "Id";
            cmbEmpID.DataSource = dtr;

            conn.Close();
        }
        private void getEmployeeName()
        {
            conn.Open();
            string querry = "Select * from pmsEmployeeTbl where Id=" + cmbEmpID.SelectedValue.ToString() + "";
            SqlCommand vmd = new SqlCommand(querry, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(vmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtName.Text = dr["Name"].ToString();
                txtBaseSal.Text = dr["BaseSalary"].ToString();
            }

            conn.Close();
        }

        private void getBonusDetail()
        {
            try
            {
                conn.Open();
                string querry = "Select * from pmsBonusTbl where Name='" + cmbBonus.SelectedValue.ToString() + "'";
                SqlCommand vmd = new SqlCommand(querry, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(vmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    txtBonusName.Text = dr["Amount"].ToString();

                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void getAttedance()
        {
            conn.Open();
            SqlCommand vmd = new SqlCommand("select * from pmsAttendanceTbl where E_Id=" + cmbEmpID.SelectedValue.ToString() + "", conn);
            SqlDataReader sqlDataReader = vmd.ExecuteReader();

            DataTable dtr = new DataTable();
            dtr.Columns.Add("Att_ID", typeof(int));
            dtr.Load(sqlDataReader);
            cmbAttendance.ValueMember = "Att_ID";
            cmbAttendance.DataSource = dtr;

            conn.Close();
        }
        private void getAttendanceDetails()
        {
            try
            {
                conn.Open();
                string querry = "Select * from pmsAttendanceTbl where Att_ID=" + cmbAttendance.SelectedValue.ToString() + "";
                SqlCommand vmd = new SqlCommand(querry, conn);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(vmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    txtPresentDay.Text = dr["Present_days"].ToString();
                    txtAbsentDays.Text = dr["Absent_days"].ToString();
                    txtExcusedDay.Text = dr["Excused_days"].ToString();
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void getBonus()
        {
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Select * from pmsBonusTbl", conn);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name", typeof(string));
                    dt.Load(sqlDataReader);
                    cmbBonus.ValueMember = "Name";
                    cmbBonus.DataSource = dt;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void CancleData()
        {
            txtAbsentDays.Text = "";
            txtAdvance.Text = "";
            txtBalance.Text = "";
            txtBaseSal.Text = "";
            txtBonusName.Text = "";
            txtExcusedDay.Text = "";
            txtName.Text = "";
            txtPresentDay.Text = "";
            cmbEmpID.SelectedIndex = -1;
            cmbAttendance.SelectedIndex = -1;
            cmbBonus.SelectedIndex = -1;
            dtpPeroid.Value = DateTime.Now;
        }

        private Boolean isValid()
        {
            if (txtAbsentDays.Text == "" || txtAdvance.Text == "" || txtBalance.Text == "" || txtBaseSal.Text == "" || txtBonusName.Text == "" || txtExcusedDay.Text == "" || txtName.Text == "" || txtPresentDay.Text == "" || cmbEmpID.SelectedIndex == -1 || cmbBonus.SelectedIndex == -1 || cmbAttendance.SelectedIndex == -1 || dtpPeroid.Value==DateTime.Now)
            {
                MessageBox.Show("Error");
                return false;
            }
            return true;
        }
        private void btnCancle_Click(object sender, EventArgs e)
        {
            CancleData();
        }

        private void cmbEmpID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getEmployeeName();
            getAttedance();
        }

        private void cmbBonus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getBonusDetail();
        }

        private void cmbAttendance_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getAttendanceDetails();
        }

        int DailyBase = 0, Total = 0, pres = 0, Abs = 0, Exc = 0;

        int salKey=0;

        private void btnEdit_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("PPRNM", 500, 800);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("XYZ Ltd", new Font("Averia", 15, FontStyle.Bold), Brushes.Red, new Point(160, 25));
            e.Graphics.DrawString("Payrol Management System 1.0", new Font("Averia", 12, FontStyle.Bold), Brushes.Red, new Point(160, 50));

            string salNum = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string EmpId = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string EmpName = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            string BasSal = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            string Bonus = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            string Advance = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            string Tax = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            string Balance = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            string Peroid = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();

            e.Graphics.DrawString("Salary Number:" + salNum, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 90));
            e.Graphics.DrawString("Employee ID:" + EmpId, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 120));
            e.Graphics.DrawString("Name:" + EmpName, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 150));
            e.Graphics.DrawString("Base Salary:" + BasSal, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 180));
            e.Graphics.DrawString("Bonus:" + Bonus, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 210));
            e.Graphics.DrawString("Advance:" + Advance, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50,240));
            e.Graphics.DrawString("Tax:" + Tax, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 270));
            e.Graphics.DrawString("Balance" + Balance, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 300));
            e.Graphics.DrawString("Peroid:" + Peroid, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 330));

            e.Graphics.DrawString("By XYZ Ltd", new Font("Bellota", 12, FontStyle.Bold), Brushes.Crimson, new Point(160, 380));
            e.Graphics.DrawString("***** Version 1.0 *****", new Font("Bellota", 12, FontStyle.Bold), Brushes.Crimson, new Point(125, 410));

        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackColor = Color.Lime;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.Transparent;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            salKey = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            cmbEmpID.SelectedValue = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtBaseSal.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            cmbBonus.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtAdvance.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            TotTax = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[6].Value);
            txtBalance.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            dtpPeroid.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                conn.Open();
                string querry = "insert into pmsSalaryTbl(E_ID,Name,BaseSalary,Bonus,Advance,Tax,Balance,Peroid) values(@Eid,@EN,@BS,@EB,@EA,@ET,@Bal,@PD)";
                SqlCommand sql = new SqlCommand(querry,conn);
                sql.Parameters.AddWithValue("@Eid", cmbEmpID.SelectedValue.ToString());
                sql.Parameters.AddWithValue("@EN", txtName.Text);
                sql.Parameters.AddWithValue("@BS",txtBaseSal.Text);
                sql.Parameters.AddWithValue("@EB",txtBonusName.Text);
                sql.Parameters.AddWithValue("@EA",txtAdvance.Text);
                sql.Parameters.AddWithValue("@ET",TotTax);
                sql.Parameters.AddWithValue("@Bal",GrandTotal);
                sql.Parameters.AddWithValue("@PD",dtpPeroid.Value.Date);
                sql.ExecuteNonQuery();
                conn.Close();
                display();
                CancleData();

            }
        }

        double GrandTotal = 0, TotTax = 0;
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtBaseSal.Text == "" || txtBonusName.Text=="" ||txtAdvance.Text=="")
            {
                MessageBox.Show("Select the details");
            }
            else
            {
                pres=Convert.ToInt32(txtPresentDay.Text);
                Abs=Convert.ToInt32(txtAbsentDays.Text);
                Exc = Convert.ToInt32(txtExcusedDay.Text);
                DailyBase=Convert.ToInt32(txtBaseSal.Text)/28;
                Total = ((DailyBase) * pres) + ((DailyBase / 2) * Exc);
                double Tax = Total * 0.16;
                TotTax=Convert.ToInt32(Total- Tax);
                GrandTotal = TotTax + Convert.ToInt32(txtBonusName.Text);
                txtBalance.Text = "RS " + GrandTotal;
            }
        }
    }
}
