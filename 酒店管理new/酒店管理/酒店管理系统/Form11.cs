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
    public partial class Form11 : Form
    {
        public Form11()
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
            cmd.CommandText = "exec 订单详情 '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            DialogResult dl = MessageBox.Show("预订单号：" + dr[0].ToString() + "\n姓名：" + dr[3].ToString() + "\n证件号：" + dr[2].ToString() + "\n客房编号：" + dr[1].ToString()
                + "\n客房类型：" + dr[4].ToString() + "\n价格：" + dr[9].ToString() + "\n入住人数：" + dr[10].ToString() + "\n入住日期：" + dr[5].ToString() + "\n退房日期：" + dr[6].ToString() +
                        "\n手机号：" + dr[8].ToString() + "\n订单状态：" + dr[7].ToString(), "订单详情：", MessageBoxButtons.OK);
            dr.Close();
            conn.Close();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;   // 设置网络布置为Fill方式
            conn.ConnectionString = CString;        // 连接设置
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string strSql = "exec 姓名搜索 '" + textBox1.Text + "'";
                exeSelectCmd(strSql);
                dataGridView1.DataSource = ds.Tables["预订单表"];
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "请输入姓名用于查询")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }

        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "请输入姓名用于查询";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void exeSelectCmd(string sql)
        {
            adap.SelectCommand = new SqlCommand(sql, conn);
            ds.Clear();
            adap.Fill(ds, "预订单表");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSql = "exec 全部订单 ";
            exeSelectCmd(strSql);
            dataGridView1.DataSource = ds.Tables["预订单表"];
            textBox1.Text = "请输入姓名用于查询";
            textBox1.ForeColor = Color.Silver;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string bianhao;
            conn.Open();
            if (dataGridView1.CurrentRow.Cells[6].Value.ToString() == "已预定")
            {
                SqlCommand cmd = conn.CreateCommand();
                SqlCommand cmd1 = conn.CreateCommand();
                SqlCommand cmd2 = conn.CreateCommand();
                cmd.CommandText = "exec 取消订单 @id=" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd.ExecuteNonQuery();
                cmd1.CommandText = "exec 编号 @id=" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                SqlDataReader dr = cmd1.ExecuteReader();
                dr.Read();
                bianhao = dr[0].ToString();
                dr.Close();
                cmd2.CommandText = "exec 状态还原 @id=" + int.Parse(bianhao);
                cmd2.ExecuteNonQuery();
                MessageBox.Show("订单取消成功！");
                strSql = "exec 全部订单";
                exeSelectCmd(strSql);            
            }
            else
            {
                MessageBox.Show("当前订单已完成或已取消！");
            }
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            this.Close();
            f5.Show();
        }

    }
}
