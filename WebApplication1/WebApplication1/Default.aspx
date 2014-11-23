<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Label ID="Label1" runat="server" Text="login"></asp:Label>
    <br><asp:TextBox ID="Login" runat="server">yershov.n@outlook.com</asp:TextBox></br>
    <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
    <br><asp:TextBox ID="Password" runat="server">GregToBack94$</asp:TextBox></br>
    <br><asp:Button ID="Button1" runat="server" Text="Sign in" OnClick="Button1_Click" /></br>
    
    <br> <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></br>
    </asp:Content>
