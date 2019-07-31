<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="Lab.Comment" %>

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
                意见回复
                <hr />
                <br />
                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Recommend]"></asp:SqlDataSource>
                荐购设备：<asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Equipment" DataValueField="Rmdid" AutoPostBack="True"></asp:DropDownList>

                <br />
                荐购设备编号：<asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
                <br />
                荐购理由：
                <hr />
                <asp:Label ID="Label2" runat="server" Text=" "></asp:Label>
                <hr />
                <br />
                <br />
                回复意见:
                <br />
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" Text="新增意见回复" OnClick="Button1_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
