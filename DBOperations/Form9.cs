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
using System.Configuration;

namespace DBOperations
{
    public partial class Form9 : Form
    {
        DataSet ds;
        SqlDataAdapter da1,da2;
        SqlConnection con;
        DataRelation dr;
        string Constr;

        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            Constr = ConfigurationManager.ConnectionStrings["SConstr"].ConnectionString;
            con = new SqlConnection(Constr);
            ds = new DataSet();
            da1 = new SqlDataAdapter("select * from dept",con);
            da2 = new SqlDataAdapter("select *from emp", con);
            da1.Fill(ds, "Dept");
            da2.Fill(ds, "Emp");
            da2 = new SqlDataAdapter("select *from Emp", con);
            dr = new DataRelation("EmpDept",ds.Tables[0].Columns["Deptno"],ds.Tables[1].Columns["Deptno"]);
            ds.Relations.Add(dr);
            dr.ChildKeyConstraint.DeleteRule = Rule.None;
            dr.ChildKeyConstraint.UpdateRule = Rule.None;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView2.DataSource = ds.Tables[1];
        }
    }
}
