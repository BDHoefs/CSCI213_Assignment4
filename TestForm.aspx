<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="TestForm.aspx.cs" Inherits="Assignment4.TestForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="testView" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="InstructorID"  HeaderText="InstructorID" ReadOnly="true" SortExpression="InstructorID" />
            <asp:BoundField DataField="InstructorLastName"  HeaderText="InstructorLastName" ReadOnly="true" SortExpression="InstructorLastName" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="Button1" runat="server" Text="Button" />
</asp:Content>
