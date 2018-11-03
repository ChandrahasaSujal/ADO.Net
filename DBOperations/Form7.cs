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
    public partial class Form7 : Form
    {
        SqlDataAdapter da;
        SqlCommandBuilder cb;
        DataSet ds;
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["SConstr"].ConnectionString;
            da = new SqlDataAdapter("select Eno,Ename,Salary,Job from Employee", constr);
            ds = new DataSet();
            da.Fill(ds, "Employee");
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
