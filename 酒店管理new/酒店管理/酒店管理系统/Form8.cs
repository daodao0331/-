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
    public partial class Form8 : Form
    {
        public class user
        {
            public static string username, password;
        }
        public Form8()
        {
            InitializeComponent();
        }
        
        string username, password,name,cardid;
        string CString = @"Data Source=.\sqlexpress;Initial Catalog=hotel;Integrated Security=True;uid=sa;pwd=123456";
        SqlConnection conn = new SqlConnection();
        string strSql;
        SqlDataAdapter adap = new SqlDataAdapter();
        DataSet ds = new DataSet();
        
        private void Form8_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;   // 设置网络布置为Fill方式
            conn.ConnectionString = CString;        // 连接设置
            strSql = "exec yonghu";
            exeSelectCmd(strSql);
            dataGridView1.DataSource = ds.Tables["用户表"];   // 绑定dataGridView 
        }

        private void exeSelectCmd(string sql)
        {
            adap.SelectCommand = new SqlCommand(sql, conn);
            ds.Clear();
            adap.Fill(ds, "用户表");
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Form9 f9 = new Form9(username, password,name,cardid);
            f9.ShowDialog();
            strSql = "exec yonghu";
            exeSelectCmd(strSql);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            strSql = "exec yonghu";
            exeSelectCmd(strSql);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            username = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            password = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            name = dataGridView1 .CurrentRow.Cells[2].Value.ToString();
            cardid=dataGridView1 .CurrentRow.Cells[3].Value.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("您确定要删除该用户吗？", "请确认：", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                strSql = "exec shanchu '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                exeSelectCmd(strSql);
                strSql = "exec yonghu";
                exeSelectCmd(strSql);
                MessageBox.Show("删除成功！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
