<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Recommend.aspx.cs" Inherits="Lab.Recommend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
                <br />
        <br />
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        荐购设备
        <hr />
        所有荐购设备与回复:<br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="Select a.Rmdid,a.Username,a.Rmddate,a.Equipment,a.Descript,b.Comment From Recommend AS a LEFT JOIN Comments AS b ON a.Rmdid=b.Rmdid"></asp:SqlDataSource>
&nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Rmdid" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Rmdid" HeaderText="Rmdid" InsertVisible="False" ReadOnly="True" SortExpression="Rmdid" />
                <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                <asp:BoundField DataField="Rmddate" HeaderText="Rmddate" SortExpression="Rmddate" />
                <asp:BoundField DataField="Equipment" HeaderText="Equipment" SortExpression="Equipment" />
                <asp:BoundField DataField="Descript" HeaderText="Descript" SortExpression="Descript" />
                <asp:BoundField DataField="Comment" HeaderText="Comment" SortExpression="Comment" />
            </Columns>
        </asp:GridView>
        <hr />
        设备荐购:
        <br />
        <br />
        荐购人姓名：
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        设备名称:
        <br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        荐购理由:
        <br />
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="荐购" OnClick="Button1_Click" />
        &nbsp;
        <asp:Button ID="Button2" runat="server" Text="清除" OnClick="Button2_Click" />
                            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
