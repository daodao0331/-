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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }
        string CString = @"Data Source=.\sqlexpress;Initial Catalog=hotel;Integrated Security=True;uid=sa;pwd=123456";
        SqlConnection conn = new SqlConnection();
        string strSql;
        SqlDataAdapter adap = new SqlDataAdapter();
        DataSet ds = new DataSet();
        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "exec 入住单详情 '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            DialogResult dl = MessageBox.Show("入住单号：" + dr[0].ToString() + "\n姓名：" + dr[2].ToString() + "\n证件号：" + dr[3].ToString() + "\n客房编号：" + dr[1].ToString()
                + "\n客房类型：" + dr[4].ToString() + "\n价格：" + dr[7].ToString() + "\n入住人数：" + dr[8].ToString() + "\n入住日期：" + dr[5].ToString() + "\n退房日期：" + dr[6].ToString() +
                        "\n订单状态：" + dr[9].ToString(), "订单详情：", MessageBoxButtons.OK);
            dr.Close();
            conn.Close();
        }
        private void exeSelectCmd(string sql)
        {
            adap.SelectCommand = new SqlCommand(sql, conn);
            ds.Clear();
            adap.Fill(ds, "入住单表");
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            conn.ConnectionString = CString;                        // 连接
            string strSql = "exec 入住单";
            exeSelectCmd(strSql);                          // 填充
            dataGridView1.DataSource = ds.Tables["入住单表"];       // 绑定
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            this.Close();
            f5.Show();
        }
    }
}
