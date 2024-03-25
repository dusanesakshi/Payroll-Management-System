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
    public partial class Bonus : Form
    {
        public Bonus()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-V6C3CKB6\\SQLEXPRESS02;Initial Catalog=PMS2;Integrated Security=True");
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

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.Show(this);
            this.Hide();
        }

        private void bonusToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Bonus_Load(object sender, EventArgs e)
        {
            display();
        }

        private void display()
        {
            SqlCommand cmd = new SqlCommand("select * from pmsBonusTbl",con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr= cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();
            dataGridView1.DataSource = dt;

        }
        private void CancleData()
        {
            txtBonusAmt.Text = "";
            txtBonusName.Text = "";
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            CancleData();
        }

        private bool isValid()
        {
            if(txtBonusAmt.Text=="" || txtBonusName.Text=="")
            {
                MessageBox.Show("Please fill all details", "failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(isValid())
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("insert into pmsBonusTbl(Name,Amount) values(@BN,@BA)", con);
                    com.Parameters.AddWithValue("@BN",txtBonusName.Text);
                    com.Parameters.AddWithValue("@BA", txtBonusAmt.Text);
                    com.ExecuteNonQuery();
                    MessageBox.Show("INSERTED");
                    con.Close();
                    display();
                    CancleData();
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int BonusID;
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValid())
                {
                    if (BonusID > 0)
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("Update pmsBonusTbl set Name=@BN, Amount=@BA where Bid=@Bonus", con);
                        com.Parameters.AddWithValue("@BN", txtBonusName.Text);
                        com.Parameters.AddWithValue("@BA", txtBonusAmt.Text);
                        com.Parameters.AddWithValue("Bonus", BonusID);
                        com.ExecuteNonQuery();
                        MessageBox.Show("UPDATED");
                        con.Close();
                        display();
                        CancleData();
                    }
                    else
                    {
                        MessageBox.Show("ID NOT FOUND");
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BonusID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            txtBonusName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtBonusAmt.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(isValid())
                {
                    if(BonusID>0)
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("delete from pmsBonusTbl where Bid=@Bonus",con);
                        com.Parameters.AddWithValue("@Bonus",BonusID);
                        com.ExecuteNonQuery();
                        MessageBox.Show("UPDATED");
                        con.Close();
                        display();
                        CancleData();
                    }
                    else
                    {
                        MessageBox.Show("ERROR");
                    }
                }
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
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
