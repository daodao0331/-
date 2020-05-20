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
    public partial class Form7 : Form
    {
        public Form7(string xm,string cid)
        {
            xingming = xm;
            cardid = cid;
            InitializeComponent();
        }
        string xingming, cardid;
        string CString = @"Data Source=.\sqlexpress;Initial Catalog=hotel;Integrated Security=True;uid=sa;pwd=123456";
        SqlConnection conn = new SqlConnection();
        string strSql;
        SqlDataAdapter adap = new SqlDataAdapter();
        DataSet ds = new DataSet();
        
        private void Form7_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;   // 设置网络布置为Fill方式
            conn.ConnectionString = CString;        // 连接设置
            strSql = "exec 我的订单 '" + xingming + "','" + cardid + "'";
            exeSelectCmd(strSql);
            dataGridView1.DataSource = ds.Tables["预订单表"];   // 绑定dataGridView 
        }
        private void exeSelectCmd(string sql)
        {
            adap.SelectCommand = new SqlCommand(sql, conn);
            ds.Clear();
            adap.Fill(ds, "预订单表");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(xingming,cardid);
            this.Hide();
            f2.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string bianhao;
            conn.Open();
            if (dataGridView1.CurrentRow.Cells[5].Value.ToString() == "已预定")
            {
                SqlCommand cmd = conn.CreateCommand();
                SqlCommand cmd1 = conn.CreateCommand(); 
                SqlCommand cmd2= conn.CreateCommand();
                cmd.CommandText = "exec 取消订单 @id=" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) ;
                cmd.ExecuteNonQuery();
                cmd1.CommandText = "exec 编号 @id=" + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                SqlDataReader dr = cmd1.ExecuteReader();
                dr.Read();
                bianhao = dr[0].ToString();
                dr.Close();
                cmd2.CommandText = "exec 状态还原 @id=" + int.Parse(bianhao) ;
                cmd2.ExecuteNonQuery();
                MessageBox.Show("订单取消成功！");
                strSql = "exec 我的订单 '" + xingming + "','" + cardid + "'";
                exeSelectCmd(strSql);            /*再次执行存储过程我的订单，用于刷新datagridview中显示的数据*/
            }
            else
            {
                MessageBox.Show("当前订单已完成或已取消！");
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "所有订单")
            {
                strSql = "exec 所有订单 '" + xingming + "','" + cardid + "'";
                exeSelectCmd(strSql);
                dataGridView1.DataSource = ds.Tables["预订单表"];
                button1.Text = "我的订单";
            }
            else
            {
                strSql = "exec 我的订单 '" + xingming + "','" + cardid + "'";
                exeSelectCmd(strSql);
                button1.Text = "所有订单";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "exec 订单详情 '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            
            
            DialogResult dl = MessageBox.Show("预订单号：" + dr[0].ToString() + "\n姓名：" + dr[3].ToString() + "\n证件号：" + dr[2].ToString() + "\n客房编号：" + dr[1].ToString()
                + "\n客房类型：" + dr[4].ToString() + "\n价格：" + dr[9].ToString() + "\n入住人数：" + dr[10].ToString() + "\n入住日期：" + dr[5].ToString() + "\n退房日期：" + dr[6].ToString() +
                        "\n手机号：" + dr[8].ToString()+"\n订单状态：" + dr[7].ToString() ,"订单详情：", MessageBoxButtons.OK);
            dr.Close();
            conn.Close();
        }

       

        
    }
}
