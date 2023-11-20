<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="Assignment4.Administrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            color: #CC0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>Welcome, Administrator!</p>
<hr />
<h2>Users</h2>
<asp:GridView ID="userView" runat="server" AutoGenerateColumns="False" OnRowDataBound="onUserRowDataBound">
    <Columns>
        <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="true" SortExpression="UserName" />
        <asp:BoundField DataField="FirstName"  HeaderText="First Name" ReadOnly="true" SortExpression="FirstName" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="true" SortExpression="LastName" />
        <asp:BoundField DataField="DateJoined" HeaderText="Date Joined" ReadOnly="true" SortExpression="DateJoined" />
        <asp:BoundField DataField="PhoneNumber"  HeaderText="Phone Number" ReadOnly="true" SortExpression="PhoneNumber" />
        <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="true" SortExpression="Email" />
        <asp:BoundField DataField="UserType" HeaderText="User Type" ReadOnly="true" SortExpression="UserType" />
        <asp:TemplateField HeaderText="Delete User">
            <ItemTemplate>
                <asp:Button ID="deleteUser" Text="Delete" CausesValidation="false" runat="server" OnClick="userDelete_Click"/>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<p>

</p>
<h3>Add User</h3>
<table cellspacing="0" rules="all" border="1" id="ContentPlaceHolder1_memberView" style="border-collapse:collapse;">
	<tr>
		<th scope="col">Username</th><th scope="col">Passsword</th><th scope="col">First Name</th><th scope="col">Last Name</th><th scope="col">Date Joined</th><th scope="col">Phone Number</th><th scope="col">Email</th>
	</tr><tr>
        <td>
            <asp:TextBox ID="userName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="userNameValidator" runat="server" ControlToValidate="userName" ErrorMessage="Username field is required" CssClass="auto-style1"></asp:RequiredFieldValidator>
        </td><td>
            <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="passwordValidator" runat="server" ControlToValidate="password" ErrorMessage="Password is required" CssClass="auto-style1"></asp:RequiredFieldValidator>
        </td><td>
            <asp:TextBox ID="firstName" runat="server"></asp:TextBox>
            <asp:CustomValidator ID="firstNameValidator" runat="server" ErrorMessage="First name is required for type" ValidateEmptyText="true" OnServerValidate="firstNameValidator_ServerValidate" CssClass="auto-style1"></asp:CustomValidator>
        </td><td>
            <asp:TextBox ID="lastName" runat="server"></asp:TextBox>
            <asp:CustomValidator ID="lastNameValidator" runat="server" ErrorMessage="Last name is required for type" OnServerValidate="lastNameValidator_ServerValidate" CssClass="auto-style1"></asp:CustomValidator>
        </td><td>
            <asp:TextBox ID="dateJoined" runat="server" TextMode="DateTime"></asp:TextBox>
            <asp:CustomValidator ID="dateJoinedValidator" runat="server" ErrorMessage="Date joined is required for members" OnServerValidate="dateJoinedValidator_ServerValidate" CssClass="auto-style1"></asp:CustomValidator>
        </td><td>
            <asp:TextBox ID="phone" runat="server" TextMode="Phone"></asp:TextBox>
            <asp:CustomValidator ID="phoneValidator" runat="server" ErrorMessage="Phone number is required for type" OnServerValidate="phoneValidator_ServerValidate" CssClass="auto-style1"></asp:CustomValidator>
        </td><td>
            <asp:TextBox ID="email" runat="server" TextMode="Email"></asp:TextBox>
            <asp:CustomValidator ID="emailValidator" runat="server" ErrorMessage="Email is required for members" OnServerValidate="emailValidator_ServerValidate" CssClass="auto-style1"></asp:CustomValidator>
        </td><td>
            <asp:DropDownList ID="userType" runat="server">
                <asp:ListItem>Member</asp:ListItem>
                <asp:ListItem>Instructor</asp:ListItem>
                <asp:ListItem>Administrator</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="userAdd" Text="Add User" runat="server" OnClick="userAdd_Click"/>
        </td>
</table>
<hr />
<h2>Member Sections</h2>
<asp:GridView ID="memberSectionsView" runat="server" AutoGenerateColumns="false" OnRowDataBound="onSectionRowDataBound">
    <Columns>
        <asp:BoundField DataField="MemberFirstLast" HeaderText="Member" />
        <asp:TemplateField HeaderText="Sections">
            <ItemTemplate>
                <asp:GridView ID="sectionsView" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
                    <Columns>
                        <asp:BoundField DataField="SectionName" HeaderText="Section Name" />
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                        <asp:BoundField DataField="InstructorName" HeaderText="Instructor Name" />
                        <asp:BoundField DataField="SectionFee" HeaderText="Section Fee" />
                    </Columns>
                </asp:GridView>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Assign Section">
            <ItemTemplate>
                <asp:DropDownList ID="sectionsDropDown" runat="server">
                    
                </asp:DropDownList>
                <asp:Button ID="assignSection" Text="Assign" CausesValidation="false" OnClick="assignSection_Click" runat="server"/>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</asp:Content>
