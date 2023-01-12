<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="Serveris.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Serveris</title>
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" CssClass="label1" Text="Serveris"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" CssClass="errorColor" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Nuskaityti failus" />
            <br />
            <div id="topdiv">
                <asp:Panel runat="server" ID="Panel1"></asp:Panel>
            </div>
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server" Visible="False">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Sudaryti sąrašą" OnClick="Button2_Click" Visible="False" />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Sudaryti surikiuotą sąrašą" Visible="False" />
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" CssClass="label1" Visible="False"></asp:Label>
            <asp:TreeView ID="TreeView1" runat="server" Visible="False">
            </asp:TreeView>
        </div>
    </form>
</body>
</html>
