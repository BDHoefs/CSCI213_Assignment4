<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="Assignment4.Member1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>Welcome, <asp:Label ID="userFirstLastName" runat="server">Test User</asp:Label>!</p>
    <hr />
    <h2>Your Sessions:</h2>
    <asp:GridView ID="memberView" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="SectionName"  HeaderText="Section Name" ReadOnly="true" SortExpression="SectionName" />
            <asp:BoundField DataField="InstructorName" HeaderText="Instructor Name" ReadOnly="true" SortExpression="InstructorName" />
            <asp:BoundField DataField="AmountPaid"  HeaderText="Amount Paid" ReadOnly="true" SortExpression="AmountPaid" />
            <asp:BoundField DataField="PaymentDate"  HeaderText="Payment Date" ReadOnly="true" SortExpression="PaymentDate" />
        </Columns>
    </asp:GridView>
</asp:Content>
