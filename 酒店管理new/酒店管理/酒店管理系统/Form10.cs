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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        string CString = @"Data Source=.\sqlexpress;Initial Catalog=hotel;Integrated Security=True;uid=sa;pwd=123456";
        SqlConnection conn = new SqlConnection();
        string strSql;
        SqlDataAdapter adap = new SqlDataAdapter();
        SqlDataAdapter ada = new SqlDataAdapter();
        DataSet ds = new DataSet();

        private void Form10_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = CString;                        // 连接
            string strSql = "exec 管理员查询";
            adap.SelectCommand = new SqlCommand(strSql, conn);    // 适配器
            ds.Clear();
            adap.Fill(ds, "管理员表");                              // 填充
            dataGridView1.DataSource = ds.Tables["管理员表"];       // 绑定
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要保存您对该表所做的修改吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            SqlCommandBuilder cb = new SqlCommandBuilder();     //用于生成SQL语句
            cb.DataAdapter = ada;
            try
            {
                ada.Update(ds.Tables["管理员表"]);
                ds.AcceptChanges();
            }
            catch
            {
                MessageBox.Show("更新失败！可能因为主键重复，或主键为空。请重试！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("您确定要删除该管理员吗？", "请确认：", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                strSql = "exec admindel '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                exeSelectCmd(strSql);
                strSql = "exec 管理员查询";
                exeSelectCmd(strSql);
                MessageBox.Show("删除成功！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            strSql = "exec 管理员查询";
            exeSelectCmd(strSql);
        }

        private void exeSelectCmd(string sql)
        {
            adap.SelectCommand = new SqlCommand(sql, conn);
            ds.Clear();
            adap.Fill(ds, "管理员表");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            this.Close();
            f5.Show();
        }
    }
}
