<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="AutomobiliuParkas.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Automobiliu parkas</title>
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Automobilių parkas" CssClass="label1"></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" CssClass="errorColor"></asp:Label>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Nuskaityti visus failus" />
            <br />
            <asp:Table ID="Table1" runat="server" CssClass="styled-table">
            </asp:Table>
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Rasti geriausias transporto priemones kiekvienoje srityje" Visible="False" />
            <br />
            <br />
            <asp:Table ID="Table2" runat="server" CssClass="styled-table">
            </asp:Table>
            <br />
            <br />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Rasti filialą su seniausiais microautobusais" Visible="False" />
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Filialo su seniausiais microautobusais duomenys:" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Sudaryti kiekvieno filialo krovininių automobilių sąrašus ir juos surūšiuoti." Visible="False" />
            <br />
            <br />
            <asp:Table ID="Table3" runat="server" CssClass="styled-table">
            </asp:Table>
            <br />
            <br />
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Patikrinti techninę apžiūrą" Visible="False" />
            <br />
            <br />
            <asp:Table ID="Table4" runat="server" CssClass="styled-table">
            </asp:Table>
            <br />
        </div>
    </form>
</body>
</html>
