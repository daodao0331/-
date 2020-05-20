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

namespace 酒店管理系统
{
    public partial class Form9 : Form
    {
        string a = null;
        string b = null;
        string c = null;
        string d = null;

        public Form9(string username,string password,string name,string cardid)
        {
            InitializeComponent();
            a = username;
            b = password;
            c = name;
            d = cardid;
        }

        string CString = @"Data Source=.\sqlexpress;Initial Catalog=hotel;Integrated Security=True;uid=sa;pwd=123456";
        SqlConnection conn = new SqlConnection();
        string strSql;
        SqlDataAdapter adap = new SqlDataAdapter();
        DataSet ds = new DataSet();
        

        Form8 f8 = new Form8();

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form9_Load(object sender, EventArgs e)
        {
            textBox1.Text = a;
            textBox2.Text = b;
            textBox3.Text = c;
            textBox4.Text = d;
            conn.ConnectionString = CString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strSql = "exec yonghuxiugai @id= '" + textBox1.Text + "',@pwd='"+textBox2.Text+"',@name='"+textBox3.Text+"',@cardid='"+textBox4.Text+"'";
            exeSelectCmd(strSql);
            MessageBox.Show("修改成功！");
            this.Close();

        }
        private void exeSelectCmd(string sql)
        {
            adap.SelectCommand = new SqlCommand(sql, conn);
            ds.Clear();
            adap.Fill(ds, "用户表");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
