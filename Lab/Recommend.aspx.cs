using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Windows.Forms;

namespace Lab
{
    public partial class Recommend : System.Web.UI.Page
    {

        string sqlconn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\Database1.mdf';";//连接数据库字符串

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //显示资料至GridView
        void ShowData(String table)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = sqlconn;
                cn.Open();
                SqlCommand cmd = new SqlCommand("Select a.Rmdid,a.Username,a.Rmddate,a.Equipment,a.Descript,b.Comment From Recommend AS a INNER JOIN Comments AS b ON a.Rmdid=b.Rmdid ");
                SqlDataReader dr = cmd.ExecuteReader();
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
        }

        //清空文本框
        void empty()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string sqlstr = null;
            if (TextBox1.Text.Trim() == "" || TextBox2.Text.Trim() == "" || TextBox3.Text.Trim() == "")
            {
                MessageBox.Show("文本域不可为空", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                sqlstr = string.Format("INSERT INTO Recommend (Username,Rmddate,Equipment,Descript)" +
                                "VALUES(N'{0}','{1}',N'{2}',N'{3}')", TextBox1.Text.Trim(), DateTime.Now.ToString(), TextBox2.Text.Trim(), TextBox3.Text.Trim());
                executeNonQuery(sqlstr);
            }
        }

        //清除
        protected void Button2_Click(object sender, EventArgs e)
        {
            empty();
        }

        //用于执行增、删、改三个功能
        void executeNonQuery(string sqlstr)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = sqlconn;
                cn.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, cn);
                try
                {
                    int affRows = cmd.ExecuteNonQuery();
                    empty();
                    if (affRows == 0)//判断受影响的行数是否为0
                    {
                        throw new Exception("受影响行数为" + affRows + "，输入数据有误，请重新输入数据！！");
                    }
                    else
                    {
                        MessageBox.Show("荐购成功", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                }
                catch (Exception sqlExc)
                {
                    MessageBox.Show(sqlExc.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);//捕获异常
                }
            }
        }
    }
}
