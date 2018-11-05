using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace DBOperations
{
    public partial class Form8 : Form
    {
        SqlDataAdapter da;
        DataSet ds;
        bool Flag = false;
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["SConStr"].ConnectionString;
            da = new SqlDataAdapter("select *from Dept",ConStr);
            ds = new DataSet();
            da.Fill(ds, "Dept");
            comboBox1.DataSource = ds.Tables["Dept"];
            comboBox1.DisplayMember = "Dname";
            comboBox1.ValueMember = "Deptno";
            da.SelectCommand.CommandText = "Select * from Emp";
            da.Fill(ds, "Emp");
            dataGridView1.DataSource = ds.Tables["Emp"];
            comboBox1.SelectedIndex = -1;
            comboBox1.Text = "-Select Department-";
            Flag = true;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Flag) { 
            DataView dv = ds.Tables["Emp"].DefaultView;
            dv.RowFilter = "Deptno=" + comboBox1.SelectedValue;
            dv.Sort = "sal";
            }
        }
    }
}
