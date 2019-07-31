<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lab.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
        .background {
            background-image: url(resource/background1.png);
            background-size: 100% 100%;
            background-repeat: no-repeat
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <br />
        <br />
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr align="center">
                        <td style="font-size: xx-large; color: #800080;">这是首页</td>
                    </tr>
                    <tr align="center">
                        <td style="font-size: xx-large; color: #FF0000;">
                            <asp:Label ID="Label1" runat="server" Text="时间："></asp:Label>
                            <%=DateTime.Now.ToString() %>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp</td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="刷新" BackColor="Black" Font-Size="Large" ForeColor="White" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
