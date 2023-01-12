<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="Kryziazodis.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text="Kryžiažodis"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Pasirinkite kryžiažodį:"></asp:Label>
            <br />          
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="XmlDataSource1" DataTextField="pavadinimas" DataValueField="pavadinimas">
            </asp:DropDownList>
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/Crosswords.xml"></asp:XmlDataSource>
            <br />
            <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Generuoti kryžiažodį" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Generuoti atsakymą" />    
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Nuspalvinti rezultatą" Visible="False" />
            <br />
            <br />
            <asp:Table ID="Table1" runat="server" GridLines="Both" CellPadding="5">
            </asp:Table>
            <br />
            <asp:Table ID="Table2" runat="server" GridLines="Both">
            </asp:Table>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
