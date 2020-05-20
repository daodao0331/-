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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")//判断是否有输入
            {
                MessageBox.Show("请输入用户名及密码！");
            }

            if (textBox2.Text != textBox3.Text)//判断两次输入密码是否相同
            {
                MessageBox.Show("您两次输入的密码不同，请重新输入！");
                textBox2.Text = "";
                textBox3.Text = "";
                textBox2.Focus();
                return;
            }
            
            
                 SqlConnection con = new SqlConnection();//建立数据库连接
                 con.ConnectionString = "Data Source=DESKTOP-0C840N2\\SQLEXPRESS;Initial Catalog=hotel;Integrated Security=True";
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "exec checkname @id='" + textBox1.Text + "'";
                if (cmd.ExecuteScalar() == null) /*查询数据库中是否存在所填用户名
                                                若没有，则会返回null，并执行存储
                                                过程register，即将输入的用户名以
                                                及密码 上传至数据库中    */
                {
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandText = "exec register @id='" + textBox1.Text + "',@password='" + textBox2.Text + "',@name='" + textBox4.Text + "',@cardid='" + textBox5.Text + "'";
                    cmd1.ExecuteNonQuery();
                 MessageBox.Show("您已成功注册！\n请登录。");
                    Form1 f1 = new Form1();
                    this.Visible = false;
                    f1.ShowDialog();
                    this.Close();
                }
                else
                { 
                    MessageBox.Show("用户注册失败！可能是因为该用户已存在。请重试！");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    
                }


                
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox1.Focus();

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
