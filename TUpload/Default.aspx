<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TUpload._Default" runat="server"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">               

    <asp:FileUpload ID="File1" runat="server" />
<asp:Button ID="Upload" runat="server" OnClick="Upload_Click" Text="Upload" />

</asp:Content>
