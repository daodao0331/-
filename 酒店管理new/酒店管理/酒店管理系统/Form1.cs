using System;                                                             //vs运行所需头文件
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;                                            //sql所需头文件

namespace 酒店管理系统
{
    public partial class Form1 : Form
    {
        public static string name;                             //定义全局变量name
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")            //判断textbox内容是否为空
            {
                MessageBox.Show("请输入用户名及密码！");

            }
            else 
            {
                SqlConnection con = new SqlConnection();                 //与sql server建立连接
                con.ConnectionString = "Data Source=DESKTOP-0C840N2\\SQLEXPRESS;Initial Catalog=hotel;Integrated Security=True";
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "exec checkin '" + textBox1.Text + "','" + textBox2.Text + "'";
                //执行存储过程checkin 判断输入的用户名密码是否与数据库中相匹配
                SqlDataReader dr = cmd.ExecuteReader();     
                if (dr.Read())
                {
                    string power = dr[0].ToString();
                    string xingming = dr[2].ToString();
                    string cardid = dr[3].ToString();
                    MessageBox.Show("欢迎你，" + textBox1.Text);
                    Form2 f2 = new Form2(xingming,cardid);
                    name = textBox1.Text;
                    this.Visible = false;
                    f2.ShowDialog();
                    this.Close();
                    
                   
                }
                else
                {
                    MessageBox.Show("用户名或密码错误");
                }
                con.Close();
            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            this.Visible = false;
            f3.ShowDialog();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            this.Visible = false;
            f4.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
