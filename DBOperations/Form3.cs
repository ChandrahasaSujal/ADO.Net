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
namespace DBOperations
{
    public partial class Form3 : Form
    {
        SqlDataReader dr;
        SqlCommand cmd;
        SqlConnection con;
        string SqlStmt;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=Server\\Server;User Id=Sa;Password=123;Database=ADODb");
            con.Open();
            cmd = new SqlCommand("select eno,ename,job,salary,Status from Employee", con);
            dr = cmd.ExecuteReader();
            ShowData();
        }
        private void ShowData()
        {
            if (dr.Read())
            {
                textBox1.Text = dr["Eno"].ToString();
                textBox2.Text = dr["Ename"].ToString();
                textBox3.Text = dr["Job"].ToString();
                textBox4.Text = dr["Salary"].ToString();
                checkBox1.Checked = Convert.ToBoolean(dr["Status"]);
            }
            else
            {
                MessageBox.Show("You are at the last record.");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dr.Close();
            textBox2.Text = textBox3.Text = textBox4.Text = "";
            checkBox1.Checked = false;
            cmd = new SqlCommand();
            cmd.CommandText = "select max(eno)+1 from employee";
            cmd.Connection = con;
            textBox1.Text = cmd.ExecuteScalar().ToString();
            btnInsert.Enabled = true;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            SqlStmt = $"insert into Employee(Eno,Ename,Job,Salary,Status) values({textBox1.Text},'{textBox2.Text}','{textBox3.Text}',{textBox4.Text},{Convert.ToInt32(checkBox1.Checked)});";
            ExecuteDml();
            btnInsert.Enabled = false;
        }

        private void ExecuteDml()
        {
            dr.Close();
            DialogResult d = MessageBox.Show("Are you sure of executing below Sql Statement??\n\n" + SqlStmt, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                cmd = new SqlCommand();
                cmd.CommandText = SqlStmt;
                cmd.Connection = con;
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                    MessageBox.Show("Successfully.");
                else
                    MessageBox.Show("fail.");
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlStmt = $"update employee set ename='{textBox2.Text}',job='{textBox3.Text}',salary={textBox4.Text},status={Convert.ToInt32(checkBox1.Checked)} where eno={textBox1.Text}";
            ExecuteDml();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlStmt = "delete from employee where eno="+textBox1.Text;
            ExecuteDml();
        }
    }
}
