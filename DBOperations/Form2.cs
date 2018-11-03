using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace DBOperations
{
    public partial class Form2 : Form
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataReader dr;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=SqloleDb;Data Source=SERVER;User Id=Sa;Password=123;DataBase=CS4DB");
            con.Open();
            cmd = new OleDbCommand("select * from dept order by deptno",con);
            dr = cmd.ExecuteReader();
            lblDno.Text = dr.GetName(0)+":";
            lblDname.Text = dr.GetName(1)+":";
            lblLoc.Text = dr.GetName(2)+":";
            Show_Data();
        }
        private void Show_Data()
        {
            if (dr.Read())
            { 
            textBox1.Text = dr.GetValue(0).ToString();
            textBox2.Text = dr.GetValue(1).ToString();
            textBox3.Text = dr.GetValue(2).ToString();
            }
            else
            {
                MessageBox.Show("You are at the last record of Database.");
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if(con.State!=ConnectionState.Closed)
            {
                con.Close();
                this.Close();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Show_Data();
        }
    }
}
