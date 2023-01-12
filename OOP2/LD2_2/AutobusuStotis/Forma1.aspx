﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="AutobusuStotis.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autobusų stotis</title>
    <link rel="stylesheet" type="text/css" href="Lab2Style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Autobusų stotis" Font-Bold="True" Font-Size="X-Large"></asp:Label>
            <br />
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="XmlDataSource1" DataTextField="pavadinimas" DataValueField="pavadinimas">
            </asp:DropDownList>
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/BusFiles.xml"></asp:XmlDataSource>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Nuskaityti duomenys" CausesValidation="False" />
            <br />
            <br />
            <asp:Label ID="Label3" runat="server"></asp:Label>
            <br />
            <asp:Label ID="Label6" runat="server"></asp:Label>
            <br />
            <asp:Table ID="Table1" runat="server" CssClass="table">
            </asp:Table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ForeColor="Red" />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Atvykimo miestas:" Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox1" runat="server" placeholder="Miestas" Visible="False"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Miesto pavadnimas privalomas" ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:Label ID="Label8" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Laiko intervalas" Visible="False"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server" placeholder="00:00" Visible="False"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="Reikialingas pradinis laikas" ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:TextBox ID="TextBox3" runat="server" placeholder="23:59" Visible="False"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3" ErrorMessage="Reikalingas galutinis laikas" ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:Label ID="Label7" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Savaitės diena:" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="Button2" runat="server" Text="Ieškoti" OnClick="Button2_Click" Visible="False" />
            <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="XmlDataSource2" DataTextField="pavadinimas" DataValueField="pavadinimas" Visible="False">
            </asp:DropDownList>
            <asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/App_Data/WeekDays.xml"></asp:XmlDataSource>
            <br />
            <br />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Parodyti populiariausią miestą" Visible="False" CausesValidation="False" />
            <br />
            <asp:Table ID="Table2" runat="server" CssClass="table">
            </asp:Table>
            <br />
        </div>
    </form>
</body>
</html>
