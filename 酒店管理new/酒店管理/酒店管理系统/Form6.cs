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
    public partial class Form6 : Form
    {
        string a = null;
        string b = null;
        string c = null;
        string xingming = null;
        string cardid = null;
        public Form6(string rid,string rname,string rprice,string xm,string cid)
        {
            InitializeComponent();
            a = rid;
            b = rname;
            c = rprice;
            xingming = xm;
            cardid = cid;
        }
        

        private void Form6_Load(object sender, EventArgs e)
        {
            textBox1.Text = a;
            textBox2.Text = xingming;
            textBox5.Text = cardid;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("请填入完整信息！");
            }
            else 
            {
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                    MessageBox.Show("离店时间不可早于抵店时间");
                else
                {
                    DialogResult dr = MessageBox.Show("客房编号：" + textBox1.Text + "\n姓名：" + textBox2.Text + "\n证件号：" + textBox5.Text + 
                        "\n手机号："+textBox3.Text+"\n入住时间："+dateTimePicker1.Value.ToString()+"\n退房时间："+dateTimePicker2.Value.ToString(),
                        "请确认信息：", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        SqlConnection con = new SqlConnection();//建立数据库连接
                        con.ConnectionString = "Data Source=DESKTOP-0C840N2\\SQLEXPRESS;Initial Catalog=hotel;Integrated Security=True";
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandText = "exec 预约房间 @rno='" + textBox1.Text + "',@id='" + textBox5.Text + "',@name='" + textBox2.Text + "',@rname='" + b
                            + "',@adate='" + dateTimePicker1.Value.ToString() + "',@ldate='" + dateTimePicker2.Value.ToString() + "',@phone='" + textBox3.Text +
                            "',@rprice='" + c + "',@rnum='" + textBox4.Text + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "exec zhuangtaixiugai @id='" + textBox1.Text + "'";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("房间预订成功！");
                        Form7 f7 = new Form7(xingming, cardid);
                        f7.ShowDialog();
                        this.Close();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(xingming ,cardid);
            f2.ShowDialog();
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        
    }
}
