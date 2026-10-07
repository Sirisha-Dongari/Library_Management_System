<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs"
    Inherits="Library_Management_System.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Library Management System</title>

    <style>

        body {
            margin: 0;
            padding: 0;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f2f2f2;
        }

        .container {
            width: 400px;
            margin: 80px auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px gray;
            text-align: center;
        }

        h2 {
            color: darkblue;
            margin-bottom: 10px;
        }

        .welcome {
            display: block;
            margin-bottom: 20px;
            font-size: 16px;
        }

        .btn {
            width: 220px;
            height: 42px;
            margin: 8px;
            background-color: darkblue;
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
        }

        .btn:hover {
            background-color: green;
        }

        .logout {
            background-color: #dc3545;
        }

        .logout:hover {
            background-color: #b02a37;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <h2>
            Library Management System
        </h2>


        <asp:Label
            ID="lblUser"
            runat="server"
            CssClass="welcome"
            Font-Bold="true">
        </asp:Label>


        <!-- MANAGE BOOKS -->

        <asp:Button
            ID="btnManageBooks"
            runat="server"
            Text="Manage Books"
            CssClass="btn"
            OnClick="btnManageBooks_Click" />

        <br />


        <!-- MEMBERS -->

        <asp:Button
            ID="btnMembers"
            runat="server"
            Text="Members"
            CssClass="btn"
            OnClick="btnMembers_Click" />

        <br />


        <!-- ISSUE BOOKS -->

        <asp:Button
            ID="btnIssueBooks"
            runat="server"
            Text="Issue Books"
            CssClass="btn"
            OnClick="btnIssueBooks_Click" />

        <br />


        <!-- RETURN BOOKS -->

        <asp:Button
            ID="btnReturnBooks"
            runat="server"
            Text="Return Books"
            CssClass="btn"
            OnClick="btnReturnBooks_Click" />

        <br />


        <!-- LOGOUT -->

        <asp:Button
            ID="btnLogout"
            runat="server"
            Text="Logout"
            CssClass="btn logout"
            OnClick="btnLogout_Click" />

    </div>

</form>

</body>

</html>