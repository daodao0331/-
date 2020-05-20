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
    public partial class Form2 : Form
    {
        public Form2(string xingming,string cardid)
        {
            xm = xingming;
            cid = cardid;
            InitializeComponent();
        }
        string rid, rname, rprice,xm,cid;
        string CString = @"Data Source=.\sqlexpress;Initial Catalog=hotel;Integrated Security=True;uid=sa;pwd=123456";
        SqlConnection conn = new SqlConnection();
        string strSql;
        SqlDataAdapter adap = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlCommand cmd;
        SqlDataReader dr;

        private void Form2_Load(object sender, EventArgs e)
        {
            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;   // 设置网络布置为Fill方式
            conn.ConnectionString = CString;        // 连接设置
            strSql = "exec kongxian";
            exeSelectCmd(strSql);
            dataGridView1.DataSource = ds.Tables["客房表"];   // 绑定dataGridView 
            conn.ConnectionString = CString;
            conn.Open();
            strSql = "exec 客房类型";
            SqlCommand cmd = new SqlCommand(strSql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
            conn.Close();
            rid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }
        private void exeSelectCmd(string sql)
        {
            adap.SelectCommand = new SqlCommand(sql, conn);
            ds.Clear();
            adap.Fill(ds, "客房表");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)/*判断checkbox1是否被勾选，若勾选则执行
                                          存储过程kongxian，即筛选状态为空闲的客房*/
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;   // 设置网络布置为Fill方式

                conn.ConnectionString = CString;        // 连接设置
                strSql = "exec kongxian";
                exeSelectCmd(strSql);
                dataGridView1.DataSource = ds.Tables["客房表"];   // 绑定dataGridView 
            }
            else                    //若没有勾选，则执行存储过程kefang，即筛选所有客房
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;   // 设置网络布置为Fill方式

                conn.ConnectionString = CString;        // 连接设置
                strSql = "exec kefang";
                exeSelectCmd(strSql);
                dataGridView1.DataSource = ds.Tables["客房表"];   // 绑定dataGridView 
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")    //判断combobox是否未选中任何东西，若是，则提示
            {
                MessageBox.Show("请指定查询条件！");
                comboBox1.Focus();
                return;
            }
            if (comboBox1.Text != "")          /*若已有选中，再对checkbox进行判断，执行对应存储
                                               过程，筛选相关类型的空闲或所有房间*/
            {
                if (checkBox1.Checked == true)

                    strSql = "exec leixing_kong '"+comboBox1.Text+"'";

                else
                    strSql = "exec leixing '" + comboBox1.Text + "'";
            }


            exeSelectCmd(strSql);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")             /*根据所选的相关查询条件，执行对应的存储过程，
                                                   * 根据价格对空闲或所有客房进行筛选*/
            {
                MessageBox.Show("请指定查询条件！");
                comboBox1.Focus();
                return;
            }
            if (comboBox2.Text != "")
            {
                if (comboBox2.Text == "<200")
                {
                    if (checkBox1.Checked == true)

                        strSql = "exec price_low";

                    else

                        strSql = "exec price2_low";

                }
                else if (comboBox2.Text == "200~500")
                {
                    if (checkBox1.Checked == true)

                        strSql = "exec price_mid";

                    else
                        strSql = "exec price2_mid ";
                }
                else
                {
                    if (checkBox1.Checked == true)

                        strSql = "exec price_high";

                    else
                        strSql = "exec price2_high ";
                }
            }


            exeSelectCmd(strSql);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)     //根据勾选与否，筛选空闲或所有客房

                strSql = "exec kongxian";

            else
                strSql = "exec kefang";
            exeSelectCmd(strSql);
            comboBox1.Text = "";
            comboBox2.Text = "";
            checkBox1.Checked = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6(rid, rname, rprice,xm,cid);
            this.Hide();
            f6.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7(xm,cid);
            this.Hide();
            f7.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 所有房间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            strSql = "exec kefang";          //列出所有客房信息
            exeSelectCmd(strSql);
            checkBox1.Checked = false;
        }

        private void 我的订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7(xm,cid);
            this.Hide();
            f7.ShowDialog();
        }

        private void 预约房间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6(rid,rname,rprice,xm,cid);
            this.Hide();
            f6.ShowDialog();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            rname = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            rprice = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }



    }

}

