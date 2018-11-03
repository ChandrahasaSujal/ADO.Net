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

namespace DBOperations
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string Cname = ConfigurationManager.AppSettings.Get("Cname");
            string Address = ConfigurationManager.AppSettings.Get("Address");
            string Phone = ConfigurationManager.AppSettings.Get("Phone");
            string Fax = ConfigurationManager.AppSettings.Get("Fax");

            label1.Text = "Comapany Name:"+Cname;
            label2.Text ="Address:"+ Address;
            label3.Text = "Phone:"+Phone + "; Fax:" + Fax;
        }
    }
}
