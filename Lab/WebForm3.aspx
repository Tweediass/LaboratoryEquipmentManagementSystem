<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="Lab.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .background {
            background-image: url(resource/background3.jpg);
            background-size: 100% 100%;
            background-repeat: no-repeat
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="background">
        <br />
        <br />
        <br />
        <table style="width: 100%;">
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server" Text="姓名：张远亮" ForeColor="White" Font-Size="X-Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="Label2" runat="server" Text="班级：16科技三班" ForeColor="White" Font-Size="X-Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="Label3" runat="server" Text="学号：201624131338" ForeColor="White" Font-Size="X-Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr style="font-size: x-large; color: #FFFFFF">
                <td align="center">
                    简介:本网页建立了一个简易WEB实验室设备管理系统，记录实验室所有的实验设备，并及时反应设备的运转情况，以供实验人员合理的安排实验，完成实验任务。
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
