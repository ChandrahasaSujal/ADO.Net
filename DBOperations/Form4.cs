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
using static Microsoft.VisualBasic.Interaction;
namespace DBOperations
{
    public partial class Form4 : Form
    {
        SqlDataAdapter da;
        DataSet ds;
        int rno = 0;
        SqlCommandBuilder cb;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("select eno,ename,job,salary from employee", new SqlConnection("Data Source=Server;User Id=Sa;Password=123;Database=CS4DB"));
            ds = new DataSet();
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Fill(ds, "Employee");
            ShowData();
        }
        private void ShowData()
        {
            textBox2.Focus();
            textBox1.Text = ds.Tables["Employee"].Rows[rno]["Eno"].ToString();
            textBox2.Text = ds.Tables["Employee"].Rows[rno]["Ename"].ToString();
            textBox3.Text = ds.Tables["Employee"].Rows[rno]["Job"].ToString();
            textBox4.Text = ds.Tables["Employee"].Rows[rno]["Salary"].ToString();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            rno = 0;
            ShowData();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (rno < ds.Tables[0].Rows.Count - 1)
            {
                rno += 1;
                ShowData();
            }
            else
            {
                MessageBox.Show("You are at the last record", "Information", MessageBoxButtons.OK);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            rno = ds.Tables[0].Rows.Count - 1;
            ShowData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if(rno>0)
            { 
            rno -= 1;
            ShowData();
            }
            else
            {
                MessageBox.Show("There are no records", "Information",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox3.Text = textBox4.Text = "";
            textBox2.Focus();
            int index = ds.Tables[0].Rows.Count - 1;
            int Eno = Convert.ToInt32(ds.Tables[0].Rows[index]["Eno"]) + 1;
            textBox1.Text = Eno.ToString();
            btnInsert.Enabled = true;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = textBox1.Text;
            dr[1] = textBox2.Text;
            dr[2] = textBox3.Text;
            dr[3] = textBox4.Text;
            ds.Tables[0].Rows.Add(dr);
            rno = ds.Tables[0].Rows.Count - 1;
            btnInsert.Enabled = false;
            MessageBox.Show("DataRow added to DataTable in Dataset.");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ds.Tables[0].Rows[rno]["Ename"] = textBox2.Text;
            ds.Tables[0].Rows[rno]["Job"] = textBox3.Text;
            ds.Tables[0].Rows[rno]["Salary"] = textBox4.Text;
            MessageBox.Show("DataRow is updated.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds.Tables[0].Rows[rno].Delete();
            MessageBox.Show("Rowdata is deleted in DataTable of DataSet.");
            rno++;
            ShowData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int eno;
            string value = InputBox("Enter Employee Number to Search", "Employee Search", "", 150, 150);
            if(int.TryParse(value,out eno))
            {
                DataRow dr = ds.Tables["Employee"].Rows.Find(eno);
                if(dr!=null)
                {
                    textBox1.Text = dr[0].ToString();
                    textBox2.Text = dr[1].ToString();
                    textBox3.Text = dr[2].ToString();
                    textBox4.Text = dr[3].ToString();
                }
                else
                {
                    MessageBox.Show("Employee doesn't exist for given Employee number.");
                }
            }
            else
            {
                MessageBox.Show("Enter Integer value as Employee number.");
            }
            
        }

        private void btnSaveDb_Click(object sender, EventArgs e)
        {
            cb = new SqlCommandBuilder(da);
            da.Update(ds, "Employee");
            MessageBox.Show("Data is Saved to Database.");
        }
    }
}
