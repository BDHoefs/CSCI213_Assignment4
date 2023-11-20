<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Instructors.aspx.cs" Inherits="Assignment4.Instructors.Instructors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
        <asp:Label ID="lbl_first" runat="server" Text="Label"></asp:Label>
&nbsp;<asp:Label ID="lbl_last" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GridView_Instructor" runat="server">
        </asp:GridView>
    </p>
</asp:Content>
