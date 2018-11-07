using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Configuration;

namespace DBOperations
{
    public partial class Form11 : Form
    {
        string SqlStmt;
        OdbcConnection con;
        OdbcCommand cmd;
        OdbcDataReader dr;
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            con = new OdbcConnection("Dsn=ExcelDsn");
            cmd = new OdbcCommand("Select *from [Student$]",con);
            con.Open();
            dr = cmd.ExecuteReader();
            label2.Text = dr.GetName(0);
            label3.Text = dr.GetName(1);
            label4.Text = dr.GetName(2);
            label5.Text = dr.GetName(3);
            ShowData();
        }
        private void ShowData()
        {
            if (dr.Read())
            {
                textBox1.Text = dr.GetValue(0).ToString();
                textBox2.Text = dr.GetValue(1).ToString();
                textBox3.Text = dr.GetValue(2).ToString();
                textBox4.Text = dr.GetValue(3).ToString();
            }
            else
                MessageBox.Show("there are no records","Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExecuteDml()
        {
            dr.Close();
            cmd.CommandText = SqlStmt;
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Insert or Updated succesfully.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Insert or Updated Fialed","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            textBox1.Focus();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            
            SqlStmt = string.Format("insert into [Student$] Values({0},'{1}',{2},{3})",textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text);
            ExecuteDml();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlStmt = string.Format("Update [Student$] Set name='{0}',class={1},Fees={2} where id={3}",textBox2.Text,textBox3.Text,textBox4.Text,textBox1.Text);
            ExecuteDml();
        }
    }
}
