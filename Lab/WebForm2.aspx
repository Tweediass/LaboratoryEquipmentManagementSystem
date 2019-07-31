<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Lab.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .background {
            background-image: url(resource/background2.png);
            background-size: 100% 100%;
            background-repeat: no-repeat
        }

        .auto-style1 {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="background">
        <br />
        <br />
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="请选择需要操作的数据表：" ForeColor="#CC3300" Font-Size="X-Large"></asp:Label>
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True" ForeColor="#FF9966" Font-Size="X-Large">
                                            <asp:ListItem Value="设备信息" Selected="True">设备信息</asp:ListItem>
                                            <asp:ListItem Value="设备借还信息">设备借还信息</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="Label1" runat="server" Text="Label1" Font-Size="X-Large"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <br />
                                        <asp:Label ID="Label2" runat="server" Text="Label2" Font-Size="X-Large"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <br />
                                        <asp:Label ID="Label3" runat="server" Text="Label3" Font-Size="X-Large"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <br />
                                        <asp:Label ID="Label4" runat="server" Text="Label4" Font-Size="X-Large"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <br />
                                        <asp:Label ID="Label5" runat="server" Text="Label5" Font-Size="X-Large"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <br />
                                        <asp:Label ID="Label6" runat="server" Text="Label6" Font-Size="X-Large"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        <asp:Button ID="Button1" runat="server" Text="添加" BackColor="#663300" ForeColor="White" Font-Size="X-Large" OnClick="Button1_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button2" runat="server" Text="删除" BackColor="#663300" ForeColor="White" Font-Size="X-Large" OnClick="Button2_Click" />
                                        <br />
                                        <br />
                                        <asp:Button ID="Button3" runat="server" Text="修改" BackColor="#663300" ForeColor="White" Font-Size="X-Large" OnClick="Button3_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button4" runat="server" Text="查询" BackColor="#663300" ForeColor="White" Font-Size="X-Large" OnClick="Button4_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left">
                            <asp:Label ID="Label8" runat="server" Text="使用DateSet实现" ForeColor="Red" Font-Size="X-Large"></asp:Label>
                            <br />
                            <br />
                            <div class="auto-style1">
                                <asp:GridView ID="GridView1" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderWidth="1px" CellPadding="3" Height="351px" Width="590px" BorderStyle="None" CellSpacing="2">
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                    <SelectedRowStyle BackColor="#738A9C" ForeColor="White" Font-Bold="True" />
                                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                                </asp:GridView>
                            </div>
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
