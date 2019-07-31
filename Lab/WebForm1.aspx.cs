using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Windows.Forms;

namespace Lab
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string sqlconn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\Database1.mdf';";//连接数据库字符串
        public static string[] colName = new string[10];//存储数据表的列名

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowData("设备信息");
            }
        }

        //显示资料至GridView
        void ShowData(String table)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = sqlconn;
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM " + table, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                GridView1.DataSource = dr;
                GridView1.DataBind();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    colName[i] = dr.GetName(i);//将得到的列名存储至数组colName中
                }
                ShowName();
            }
        }

        //显示数据表的列名
        void ShowName()
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label2.ForeColor = System.Drawing.Color.White;
            Label3.ForeColor = System.Drawing.Color.White;
            Label4.ForeColor = System.Drawing.Color.White;
            Label5.ForeColor = System.Drawing.Color.White;
            Label6.ForeColor = System.Drawing.Color.White;
            Label1.Text = colName[0] + " *";//主键.不可为NULL
            if (RadioButtonList1.SelectedValue == "0")
            {
                Label2.Text = colName[1];
                Label3.Text = colName[2];
                Label5.Text = colName[4];
            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                Label2.ForeColor = System.Drawing.Color.Red;
                Label3.ForeColor = System.Drawing.Color.Red;
                Label2.Text = colName[1] + " *";//主键.不可为NULL
                Label3.Text = colName[2] + " *";//主键.不可为NULL
                Label5.Text = colName[4] + "（1为是,0为否）";
            }
            Label4.Text = colName[3];
            Label6.Text = colName[5];
        }

        //通过RadioButton按钮判断所需获取的信息
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "0")
            {
                ShowData("设备信息");
                //显示文本
                Label6.Visible = true;
                TextBox6.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                ShowData("设备借还信息");
                //隐藏文本
                Label6.Visible = false;
                TextBox6.Visible = false;
            }
            empty();
        }

        //清空文本框
        void empty()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
        }

        //增加数据功能
        protected void Button1_Click(object sender, EventArgs e)
        {
            string sqlstr = null;
            if (TextBox1.Text.Trim() == "" || TextBox2.Text.Trim() == "" || TextBox3.Text.Trim() == "" || TextBox4.Text.Trim() == "" || TextBox5.Text.Trim() == "")
            {
                MessageBox.Show("文本域不可为空", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (RadioButtonList1.SelectedValue == "0")
                {
                    if (TextBox6.Text.Trim() != "")
                    {
                        sqlstr = string.Format("INSERT INTO 设备信息 " +
                            "VALUES('{0}',N'{1}',N'{2}','{3}','{4}',N'{5}')", TextBox1.Text.Trim(), TextBox2.Text.Trim(), TextBox3.Text.Trim(), TextBox4.Text.Trim(), TextBox5.Text.Trim(), TextBox6.Text.Trim());
                    }
                    else
                    {
                        MessageBox.Show("文本域不可为空", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (RadioButtonList1.SelectedValue == "1")
                {
                    sqlstr = string.Format("INSERT INTO 设备借还信息 " +
                        "VALUES('{0}','{1}',N'{2}',N'{3}','{4}')", TextBox1.Text.Trim(), TextBox2.Text.Trim(), TextBox3.Text.Trim(), TextBox4.Text.Trim(), TextBox5.Text.Trim());
                }
            }
            executeNonQuery(sqlstr);
        }

        //删除数据功能（按照主键执行删除操作）
        protected void Button2_Click(object sender, EventArgs e)
        {
            string sqlstr = null;
            if (RadioButtonList1.SelectedValue == "0")
            {
                sqlstr = string.Format("DELETE FROM 设备信息 WHERE 设备编号='{0}'", TextBox1.Text.Trim());
            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                sqlstr = string.Format("DELETE FROM 设备借还信息 " +
                    "WHERE 设备编号='{0}' AND 借出时间='{1}' AND 借用人姓名=N'{2}'", TextBox1.Text.Trim(), TextBox2.Text.Trim(), TextBox3.Text.Trim());
            }
            executeNonQuery(sqlstr);
        }

        //修改功能(可修改任意非主键的一个或多个的数据信息)
        protected void Button3_Click(object sender, EventArgs e)
        {
            string str = "";
            string sqlstr = null;
            if (RadioButtonList1.SelectedValue == "0")
            {
                sqlstr = "UPDATE 设备信息 SET ";
                if (TextBox2.Text.Trim().Length > 0)
                {
                    sqlstr += colName[1] + "= N'" + TextBox2.Text.Trim() + "' ";
                }
                if (TextBox3.Text.Trim().Length > 0)
                {
                    sqlstr += colName[2] + "= N'" + TextBox3.Text.Trim() + "' ";
                }
            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                sqlstr = "UPDATE 设备借还信息 SET ";
                str = " AND " + colName[1] + "= '" + TextBox2.Text.Trim() + "' AND " + colName[2] + "= N'" + TextBox3.Text.Trim() + "'";
            }
            if (TextBox4.Text.Trim().Length > 0)
            {
                sqlstr += colName[3] + "= '" + TextBox4.Text.Trim() + "' ";
            }
            if (TextBox5.Text.Trim().Length > 0)
            {
                sqlstr += colName[4] + "= '" + TextBox5.Text.Trim() + "' ";
            }
            if (TextBox6.Text.Trim().Length > 0)
            {
                sqlstr += colName[5] + "= N'" + TextBox6.Text.Trim() + "' ";
            }
            sqlstr += "WHERE " + colName[0] + "='" + TextBox1.Text.Trim() + "'" + str;
            executeNonQuery(sqlstr);
            Label2.Text = sqlstr;
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
                }
                catch (Exception sqlExc)
                {
                    MessageBox.Show(sqlExc.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);//捕获异常
                }
                if (RadioButtonList1.SelectedValue == "0")
                {
                    ShowData("设备信息");
                }
                else if (RadioButtonList1.SelectedValue == "1")
                {
                    ShowData("设备借还信息");
                }
            }
        }

        //多条件模糊查询功能
        protected void Button4_Click(object sender, EventArgs e)
        {
            string table = null;
            if (RadioButtonList1.SelectedValue == "0")
            {
                table = "设备信息";
            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                table = "设备借还信息";
            }
            string sql = "select * from " + table;
            //使用List拼接where条件
            List<string> wheres = new List<string>();
            if (TextBox1.Text.Trim().Length > 0)
            {
                wheres.Add(colName[0] + " LIKE N'%" + TextBox1.Text.Trim() + "%'");
            }
            if (TextBox2.Text.Trim().Length > 0)
            {
                wheres.Add(colName[1] + " LIKE N'%" + TextBox2.Text.Trim() + "%'");
            }
            if (TextBox3.Text.Trim().Length > 0)
            {
                wheres.Add(colName[2] + " LIKE N'%" + TextBox3.Text.Trim() + "%'");
            }
            if (TextBox4.Text.Trim().Length > 0)
            {
                wheres.Add(colName[3] + " LIKE N'%" + TextBox4.Text.Trim() + "%'");
            }
            if (TextBox5.Text.Trim().Length > 0)
            {
                wheres.Add(colName[4] + " LIKE N'%" + TextBox5.Text.Trim() + "%'");
            }
            if (TextBox6.Text.Trim().Length > 0)
            {
                wheres.Add(colName[5] + " LIKE N'%" + TextBox6.Text.Trim() + "%'");
            }
            //判断用户是否选择了条件
            if (wheres.Count > 0)
            {
                string wh = string.Join(" AND ", wheres.ToArray());//使用"and"关键字将List间隔并存储至字符串中
                sql += (" WHERE " + wh);//拼接sql语句
            }
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = sqlconn;
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    //输出至GridView中
                    if (!dr.HasRows)
                    {
                        throw new Exception("输入数据有误，请重新输入数据！！");
                    }
                    GridView1.DataSource = dr;
                    GridView1.DataBind();
                    empty();
                }
                catch (Exception sqlExc)
                {
                    MessageBox.Show(sqlExc.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }
    }
}