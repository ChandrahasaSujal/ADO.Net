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
    public partial class Form10 : Form
    {
        string Constr;
        SqlDataAdapter da;
        DataSet ds;
        SqlConnection con;
        DataRelation dr;

        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            
            Constr = ConfigurationManager.ConnectionStrings["SConstr"].ConnectionString;
            ds = new DataSet();
            con = new SqlConnection(Constr);
            da = new SqlDataAdapter("select *from dept",con);
            da.Fill(ds, "Dept");
            da.SelectCommand.CommandText = "select *from emp";
            da.SelectCommand.Connection = con;
            da.Fill(ds, "Emp");
            dr = new DataRelation("DeptEmp",ds.Tables[0].Columns["Deptno"],ds.Tables[1].Columns["Deptno"]);
            ds.Relations.Add(dr);
            dataGrid1.DataSource=ds;
        }
    }
}
