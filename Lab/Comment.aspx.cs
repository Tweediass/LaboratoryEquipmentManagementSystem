using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Lab
{
    public partial class Comment : System.Web.UI.Page
    {
        string sqlconn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\Database1.mdf';";//连接数据库字符串
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.DropDownList1.DataBind();
                this.DropDownList1.Items.Insert(0, new ListItem("--请选择--", " "));
            }
            Label1.Text = DropDownList1.SelectedValue;
            ShowData("");
        }

        //显示资料至GridView
        void ShowData(String table)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = sqlconn;
                cn.Open();
                int id = 0;
                int.TryParse(DropDownList1.SelectedValue, out id);
                SqlCommand cmd = new SqlCommand("SELECT Descript FROM Recommend where Rmdid='"+id+"'" , cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    for(int i=0;i<dr.FieldCount;i++)
                    {
                        Label2.Text = dr[i].ToString();
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sqlstr = null;
            if (TextBox1.Text.Trim() == "")
            {
                MessageBox.Show("文本域不可为空", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                sqlstr = string.Format("INSERT INTO Comments (Rmdid,Commentdate,Comment)" +
                                "VALUES('{0}','{1}',N'{2}')", DropDownList1.SelectedValue, DateTime.Now.ToString(), TextBox1.Text.Trim());
                executeNonQuery(sqlstr);
            }
        }

        //清空文本框
        void empty()
        {
            TextBox1.Text = "";
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
                        MessageBox.Show("新增意见成功", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
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