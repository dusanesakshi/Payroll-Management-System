using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll_Management_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="Admin" && textBox2.Text=="Password")
            {
                Home home = new Home();
                home.Show(this);
                this.Hide();
            }
            else if (textBox1.Text!="Admin" && textBox2.Text=="Password")
            {
                MessageBox.Show("Invalid Username");
            }
            else if(textBox1.Text=="Admin" && textBox2.Text!="Password")
            {
                MessageBox.Show("Invalid Password");
            }
            else 
            {
                MessageBox.Show("INVALID CREDENTIAL");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackColor=Color.Lime;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.Transparent;
        }
    }
}
