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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        string CString = @"Data Source=.\sqlexpress;Initial Catalog=hotel;Integrated Security=True;uid=sa;pwd=123456";
        SqlConnection conn = new SqlConnection();
        string strSql;
        SqlDataAdapter adap = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlCommand cmd;
        SqlDataReader dr;
        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            conn.ConnectionString = CString;                        // 连接
            string strSql = "exec 预订中";
            exeSelectCmd(strSql);                          // 填充
            dataGridView1.DataSource = ds.Tables["预订单表"];       // 绑定
        }

        private void exeSelectCmd(string sql)
        {
            adap.SelectCommand = new SqlCommand(sql, conn);
            ds.Clear();
            adap.Fill(ds, "预订单表");
        }

        private void exeSelectCmd1(string sql)
        {
            adap.SelectCommand = new SqlCommand(sql, conn);
            ds.Clear();
            adap.Fill(ds, "入住单表");
        }

        private void 用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 f8=new Form8();
            f8.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form11 f11 = new Form11();
            this.Hide();
            f11.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form10 f10=new Form10();
            this.Hide();
            f10.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string strSql = "exec 手机尾号 '" + textBox1.Text + "'";
            exeSelectCmd(strSql);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[6].Value.ToString() == "已预定")
            {
                conn.Open();
                strSql = "exec 预订信息获取 '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                string id = dr[0].ToString();
                string rid = dr[1].ToString();
                string name = dr[2].ToString();
                string cardid = dr[3].ToString();
                string leixing = dr[4].ToString();
                string price = dr[5].ToString();
                string pnum = dr[6].ToString();
                dr.Close();
                conn.Close();
                strSql = "exec 入住单生成 @rid='" + rid + "',@name='" + name + "',@cardid='" + cardid + "',@leixing='" + leixing + "',@price='" + price + "',@pnum='" + pnum + "',@id='" + id + "'";
                exeSelectCmd(strSql);
                strSql = "exec 预订中";
                exeSelectCmd(strSql);
                MessageBox.Show("入住完成！");
            }
            else
            {
                MessageBox.Show("该订单无法再次入住。");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string strSql = "exec 编号搜索 '" + textBox2.Text + "'";
            exeSelectCmd1(strSql);
            dataGridView1.DataSource = ds.Tables["入住单表"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[6].Value.ToString() == "已入住")
            {
                string strSql = "exec 退房 @name='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "',@rid='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "',@id='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                exeSelectCmd1(strSql);
                MessageBox.Show("退房成功！");
            }
            else
            {
                MessageBox.Show("该订单当前无法办理退房。");
            }
        }

        private void 预订单管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form11 f11 = new Form11();
            this.Hide();
            f11.ShowDialog();
        }

        private void 管理员管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form10 f10 = new Form10();
            this.Hide();
            f10.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form12 f12 = new Form12();
            this.Hide();
            f12.Show();
        }
    }
}
