using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Windows.Forms;

namespace Lab
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string sqlconn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\Database1.mdf';";//连接数据库字符串
        public static string[] colName = new string[10];//存储数据表的列名
        SqlConnection conn = null;//定义连接的全局变量
        SqlCommand comm = null;//定义操作命令的全局变量
        SqlDataAdapter Adapter = null;//定义适配器

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
            if (RadioButtonList1.SelectedValue == "设备信息")
            {
                Label2.Text = colName[1];
                Label3.Text = colName[2];
                Label5.Text = colName[4];
            }
            else if (RadioButtonList1.SelectedValue == "设备借还信息")
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
            ShowData(RadioButtonList1.SelectedValue);
            if (RadioButtonList1.SelectedValue == "设备信息")
            {
                //显示文本
                Label6.Visible = true;
                TextBox6.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "设备借还信息")
            {
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

        //数据库查询方法，DataSet为返回类型
        public DataSet fillSet(string table)
        {
            conn = new SqlConnection(sqlconn);
            comm = new SqlCommand();
            Adapter = new SqlDataAdapter();
            conn.Open();//打开数据库
            comm.CommandType = CommandType.Text;//定义发送到数据库的SQL语句
            comm.CommandText = "SELECT * FROM " + table;//定义查询语句
            comm.Connection = conn;//指定SQL命令所指定的数据库连接类
            Adapter.SelectCommand = comm;//定义查询命令
            DataSet ds = new DataSet();
            Adapter.Fill(ds, table);//填充数据集
            if (table.Equals("设备信息"))
            {
                ds.Tables[table].PrimaryKey = new DataColumn[] { ds.Tables[table].Columns[colName[0]] };
            }
            else
            {
                ds.Tables[table].PrimaryKey = new DataColumn[] { ds.Tables[table].Columns[colName[0]], ds.Tables[table].Columns[colName[1]], ds.Tables[table].Columns[colName[2]] };
            }
            return ds;
        }

        //更新，查询，删除和添加数据库后执行统一释放资源
        public void release()
        {
            conn.Dispose();
            comm.Dispose();
            Adapter.Dispose();
        }

        //增加数据功能
        protected void Button1_Click(object sender, EventArgs e)
        {
            string table = RadioButtonList1.SelectedValue;
            DataSet MyDataSet = fillSet(table);//填充数据集
            DataRow dr = MyDataSet.Tables[table].NewRow();//新建一行
            SqlCommandBuilder scb = new SqlCommandBuilder(Adapter);
            try
            {
                if (table.Equals("设备信息"))
                {
                    dr[colName[5]] = TextBox6.Text.Trim();
                    dr[colName[4]] = TextBox5.Text.Trim();
                }
                else
                {
                    if (TextBox5.Text.Trim().Equals("0") || TextBox5.Text.Trim().Equals("否"))
                    {
                        dr[colName[4]] = false;
                    }
                    else if ((TextBox5.Text.Trim().Equals("1") || TextBox5.Text.Trim().Equals("是")))
                    {
                        dr[colName[4]] = true;
                    }
                    else
                    {
                        dr[colName[4]] = TextBox5.Text.Trim();
                    }
                }
                dr[colName[0]] = TextBox1.Text.Trim();
                dr[colName[1]] = TextBox2.Text.Trim();
                dr[colName[2]] = TextBox3.Text.Trim();
                dr[colName[3]] = TextBox4.Text.Trim();
                MyDataSet.Tables[table].Rows.Add(dr);//数据集中添加行
                Adapter.Update(MyDataSet, table);//提交更改
                empty();
            }
            catch (Exception sqlExc)
            {
                MessageBox.Show(sqlExc.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);//捕获异常
            }
            finally
            {
                release();//释放资源
            }
            ShowData(table);
        }

        //删除数据功能（按照主键执行删除操作）
        protected void Button2_Click(object sender, EventArgs e)
        {
            string table = RadioButtonList1.SelectedValue;
            DataSet MyDataSet = fillSet(table);//填充数据集
            DataTable dt = MyDataSet.Tables[table];//选定数据表
            SqlCommandBuilder scb = new SqlCommandBuilder(Adapter);
            string str = "";
            try
            {
                if (table.Equals("设备借还信息"))
                {
                    str = " AND 借出时间='" + TextBox2.Text.Trim() + "' AND 借用人姓名='" + TextBox3.Text.Trim() + "'";
                }
                DataRow[] dr = dt.Select("设备编号= '" + TextBox1.Text.Trim() + "'" + str);//查询数据;
                if (dr.Length == 0)
                {
                    throw new Exception("删除数据失败，请检查输入数据！！");
                }
                dr[0].Delete();
                Adapter.Update(MyDataSet, table);//提交更改
                MyDataSet.Tables[table].AcceptChanges();
                empty();
            }
            catch (Exception sqlExc)
            {
                MessageBox.Show(sqlExc.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);//捕获异常
            }
            finally
            {
                release();//释放资源
            }
            ShowData(table);
        }

        //修改功能(可修改任意非主键的一个或多个的数据信息)
        protected void Button3_Click(object sender, EventArgs e)
        {
            string str = "";
            string table = RadioButtonList1.SelectedValue;
            DataSet MyDataSet = fillSet(table);//填充数据集
            DataTable dt = MyDataSet.Tables[table];//选定数据表
            SqlCommandBuilder scb = new SqlCommandBuilder(Adapter);
            try
            {
                if (table.Equals("设备借还信息"))
                {
                    str = " AND 借出时间='" + TextBox2.Text.Trim() + "' AND 借用人姓名='" + TextBox3.Text.Trim() + "'";
                }
                DataRow[] dr = dt.Select("设备编号= '" + TextBox1.Text.Trim() + "'" + str);//查询数据;
                if (dr.Length == 0)
                {
                    throw new Exception("修改数据失败，请检查输入数据！！");
                }
                if (table.Equals("设备信息"))
                {
                    if (TextBox2.Text.Trim().Length > 0)
                    {
                        dr[0][colName[1]] = TextBox2.Text.Trim();
                    }
                    if (TextBox3.Text.Trim().Length > 0)
                    {
                        dr[0][colName[2]] = TextBox3.Text.Trim();
                    }
                    if (TextBox6.Text.Trim().Length > 0)
                    {
                        dr[0][colName[5]] = TextBox6.Text.Trim();
                    }
                }
                else
                {
                    if (TextBox5.Text.Trim().Equals("0") || TextBox5.Text.Trim().Equals("否"))
                    {
                        dr[0][colName[4]] = false;
                    }
                    else if ((TextBox5.Text.Trim().Equals("1") || TextBox5.Text.Trim().Equals("是")))
                    {
                        dr[0][colName[4]] = true;
                    }
                    else if (TextBox5.Text.Trim().Length > 0)
                    {
                        dr[0][colName[4]] = TextBox5.Text.Trim();
                    }
                }
                if (TextBox4.Text.Trim().Length > 0)
                {
                    dr[0][colName[3]] = TextBox4.Text.Trim();
                }
                Adapter.Update(MyDataSet, table);//提交更改
                empty();
            }
            catch (Exception sqlExc)
            {
                MessageBox.Show(sqlExc.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);//捕获异常
            }
            finally
            {
                release();//释放资源
            }
            ShowData(table);
        }

        //多条件模糊查询功能
        protected void Button4_Click(object sender, EventArgs e)
        {
            string table = RadioButtonList1.SelectedValue;
            string str = "";
            //使用List拼接where条件
            List<string> wheres = new List<string>();
            if (TextBox1.Text.Trim().Length > 0)
            {
                wheres.Add(colName[0] + " = '" + TextBox1.Text.Trim() + "'");
            }
            if (TextBox2.Text.Trim().Length > 0)
            {
                wheres.Add(colName[1] + " = '" + TextBox2.Text.Trim() + "'");
            }
            if (TextBox3.Text.Trim().Length > 0)
            {
                wheres.Add(colName[2] + " LIKE '%" + TextBox3.Text.Trim() + "%'");
            }
            if (TextBox4.Text.Trim().Length > 0)
            {
                wheres.Add(colName[3] + " LIKE '%" + TextBox4.Text.Trim() + "%'");
            }
            if (TextBox5.Text.Trim().Length > 0)
            {
                wheres.Add(colName[4] + " = '" + TextBox5.Text.Trim() + "'");
            }
            if (TextBox6.Text.Trim().Length > 0)
            {
                wheres.Add(colName[5] + " LIKE '%" + TextBox6.Text.Trim() + "%'");
            }
            //判断用户是否选择了条件
            if (wheres.Count > 0)
            {
                str = string.Join(" AND ", wheres.ToArray());//使用"AND"关键字将List间隔并存储至字符串str
            }
            DataSet MyDataSet = fillSet(table);//填充数据集
            DataTable dt = MyDataSet.Tables[table];//选定数据表
            SqlCommandBuilder scb = new SqlCommandBuilder(Adapter);
            try
            {
                DataRow[] dr = dt.Select(str);//查询数据;
                DataSet ds = new DataSet();//建立一个临时的数据集用来存放筛选的项
                ds = MyDataSet.Clone();//使用Clone方法会创建具有相同结构的新DataSet，但不包含任何行。
                if (dr.Length == 0)
                {
                    throw new Exception("查询数据失败，请检查输入数据！！");
                }
                foreach (DataRow row in dr)
                {
                    ds.Tables[table].NewRow();//NewRow() 创建与该表具有相同架构的新DataRow
                    ds.Tables[table].Rows.Add(row.ItemArray);//ItemArray：获取或设置行中所有列的值。
                }
                MyDataSet.Tables[table].AcceptChanges();//应用更改
                //查询结果展示于GridView中
                GridView1.DataSource = ds.Tables[table].DefaultView;
                GridView1.DataBind();
                empty();
            }
            catch (Exception sqlExc)
            {
                MessageBox.Show(sqlExc.ToString(), "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            finally
            {
                release();//释放资源
            }
        }
    }
}