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
    public partial class Form4 : Form
    {
        public static string name;
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请输入用户名及密码！");

            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-0C840N2\\SQLEXPRESS;Initial Catalog=hotel;Integrated Security=True";
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "exec admincheck '" + textBox1.Text + "','" + textBox2.Text + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string power = dr[0].ToString();
                    MessageBox.Show("欢迎你，管理员");
                    Form5 f5 = new Form5();
                    name = textBox1.Text;
                    this.Visible = false;
                    f5.ShowDialog();
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
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        
    }
}
