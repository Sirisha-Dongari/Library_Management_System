<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Book.aspx.cs"
    Inherits="Library_Management_System.Book" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Manage Books</title>

    <style>

        * {
            box-sizing: border-box;
        }

        body {
            margin: 0;
            padding: 0;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f4f6f9;
        }

        /* ================= SIDEBAR ================= */

        .sidebar {
            position: fixed;
            left: 0;
            top: 0;
            width: 240px;
            height: 100vh;
            background-color: #172033;
        }

        .logo {
            height: 70px;
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            font-size: 20px;
            font-weight: bold;
            border-bottom: 1px solid #2d3748;
        }

        .menu {
            margin-top: 20px;
        }

        .menu-link {
            display: block;
            width: 100%;
            padding: 16px 25px;
            background-color: transparent;
            border: none;
            color: #cbd5e1;
            text-decoration: none;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 15px;
            text-align: left;
            cursor: pointer;
            transition: 0.2s;
        }

        .menu-link:hover {
            background-color: #007bff;
            color: white;
        }

        .menu-link.active {
            background-color: #007bff;
            color: white;
        }

        /* ================= MAIN ================= */

        .main {
            margin-left: 240px;
            min-height: 100vh;
        }

        /* ================= TOP BAR ================= */

        .topbar {
            height: 70px;
            background-color: white;
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 0 30px;
            box-shadow: 0 2px 6px rgba(0,0,0,0.08);
        }

        .topbar h2 {
            margin: 0;
            color: #172033;
        }

        .admin {
            color: #666;
            font-size: 14px;
        }

        /* ================= CONTENT ================= */

        .content {
            padding: 30px;
        }

        .page-title {
            margin-bottom: 25px;
        }

        .page-title h1 {
            margin: 0;
            color: #172033;
            font-size: 28px;
        }

        .page-title p {
            margin-top: 8px;
            color: #777;
        }

        /* ================= CARD ================= */

        .card {
            background-color: white;
            border-radius: 10px;
            padding: 25px;
            margin-bottom: 25px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.08);
        }

        .card-title {
            margin-bottom: 20px;
            color: #172033;
            font-size: 20px;
            font-weight: bold;
        }

        /* ================= FORM ================= */

        .form-table {
            width: 100%;
            border-collapse: collapse;
        }

        .form-table td {
            padding: 10px;
        }

        .label {
            width: 180px;
            color: #333;
            font-weight: bold;
        }

        .textbox {
            width: 100%;
            max-width: 500px;
            height: 40px;
            padding: 8px 12px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 15px;
        }

        .textbox:focus {
            outline: none;
            border-color: #007bff;
        }

        /* ================= BUTTONS ================= */

        .button-area {
            text-align: center;
            margin-top: 20px;
        }

        .btn {
            min-width: 100px;
            height: 38px;
            padding: 0 18px;
            margin: 5px;
            border: none;
            border-radius: 5px;
            color: white;
            font-size: 14px;
            font-weight: bold;
            cursor: pointer;
        }

        .btn-add {
            background-color: #28a745;
        }

        .btn-search {
            background-color: #6f42c1;
        }

        .btn-update {
            background-color: #007bff;
        }

        .btn-delete {
            background-color: #dc3545;
        }

        .btn-clear {
            background-color: #6c757d;
        }

        .btn-back {
            background-color: #343a40;
        }

        .btn:hover {
            opacity: 0.85;
        }

        /* ================= MESSAGE ================= */

        .message {
            display: block;
            text-align: center;
            margin: 15px 0;
            font-weight: bold;
        }

        /* ================= GRID ================= */

        .grid-container {
            width: 100%;
            overflow-x: auto;
        }

        .grid {
            width: 100%;
            border-collapse: collapse;
        }

        .grid th {
            background-color: #172033;
            color: white;
            padding: 13px;
            text-align: center;
        }

        .grid td {
            padding: 11px;
            text-align: center;
            border: 1px solid #ddd;
        }

        .grid tr:nth-child(even) {
            background-color: #f8f9fa;
        }

        .grid tr:hover {
            background-color: #e8f1ff;
        }

        /* ================= RESPONSIVE ================= */

        @media screen and (max-width: 900px) {

            .sidebar {
                width: 200px;
            }

            .main {
                margin-left: 200px;
            }
        }

        @media screen and (max-width: 600px) {

            .sidebar {
                position: relative;
                width: 100%;
                height: auto;
            }

            .main {
                margin-left: 0;
            }

            .form-table,
            .form-table tbody,
            .form-table tr,
            .form-table td {
                display: block;
                width: 100%;
            }

            .label {
                width: 100%;
            }
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <!-- ================= SIDEBAR ================= -->

    <div class="sidebar">

        <div class="logo">
            📚 Library System
        </div>

        <div class="menu">

            <asp:LinkButton
                ID="lnkDashboard"
                runat="server"
                Text="Dashboard"
                CssClass="menu-link"
                PostBackUrl="~/Dashboard.aspx">
            </asp:LinkButton>

            <asp:LinkButton
                ID="lnkBooks"
                runat="server"
                Text="Manage Books"
                CssClass="menu-link active"
                PostBackUrl="~/Book.aspx">
            </asp:LinkButton>

            <asp:LinkButton
                ID="lnkMembers"
                runat="server"
                Text="Members"
                CssClass="menu-link"
                PostBackUrl="~/Members.aspx">
            </asp:LinkButton>

            <asp:LinkButton
                ID="lnkIssueBooks"
                runat="server"
                Text="Issue Books"
                CssClass="menu-link"
                PostBackUrl="~/IssueBooks.aspx">
            </asp:LinkButton>

            <asp:LinkButton
                ID="lnkReturnBooks"
                runat="server"
                Text="Return Books"
                CssClass="menu-link"
                PostBackUrl="~/ReturnBooks.aspx">
            </asp:LinkButton>

        </div>

    </div>


    <!-- ================= MAIN ================= -->

    <div class="main">

        <div class="topbar">

            <h2>
                Manage Books
            </h2>

            <div class="admin">
                Library Management System
            </div>

        </div>


        <div class="content">

            <div class="page-title">

                <h1>
                    Book Management
                </h1>

                <p>
                    Add, search, update and delete books from the library.
                </p>

            </div>


            <!-- BOOK INFORMATION -->

            <div class="card">

                <div class="card-title">
                    Book Information
                </div>

                <table class="form-table">

                    <tr>

                        <td class="label">
                            Book ID
                        </td>

                        <td>

                            <asp:TextBox
                                ID="txtBookId"
                                runat="server"
                                CssClass="textbox">
                            </asp:TextBox>

                        </td>

                    </tr>


                    <tr>

                        <td class="label">
                            Book Name
                        </td>

                        <td>

                            <asp:TextBox
                                ID="txtBookName"
                                runat="server"
                                CssClass="textbox">
                            </asp:TextBox>

                        </td>

                    </tr>


                    <tr>

                        <td class="label">
                            Author
                        </td>

                        <td>

                            <asp:TextBox
                                ID="txtAuthor"
                                runat="server"
                                CssClass="textbox">
                            </asp:TextBox>

                        </td>

                    </tr>


                    <tr>

                        <td class="label">
                            Quantity
                        </td>

                        <td>

                            <asp:TextBox
                                ID="txtQuantity"
                                runat="server"
                                CssClass="textbox">
                            </asp:TextBox>

                        </td>

                    </tr>

                </table>


                <asp:Label
                    ID="lblMessage"
                    runat="server"
                    CssClass="message">
                </asp:Label>


                <div class="button-area">

                    <asp:Button
                        ID="btnAdd"
                        runat="server"
                        Text="Add"
                        CssClass="btn btn-add"
                        OnClick="btnAdd_Click" />

                    <asp:Button
                        ID="btnSearch"
                        runat="server"
                        Text="Search"
                        CssClass="btn btn-search"
                        OnClick="btnSearch_Click" />

                    <asp:Button
                        ID="btnUpdate"
                        runat="server"
                        Text="Update"
                        CssClass="btn btn-update"
                        OnClick="btnUpdate_Click" />

                    <asp:Button
                        ID="btnDelete"
                        runat="server"
                        Text="Delete"
                        CssClass="btn btn-delete"
                        OnClick="btnDelete_Click"
                        OnClientClick="return confirm('Are you sure you want to delete this book?');" />

                    <asp:Button
                        ID="btnClear"
                        runat="server"
                        Text="Clear"
                        CssClass="btn btn-clear"
                        OnClick="btnClear_Click" />

                    <!-- BACK BUTTON -->

                    <asp:Button
                        ID="btnBack"
                        runat="server"
                        Text="Back"
                        CssClass="btn btn-back"
                        OnClick="btnBack_Click" />

                </div>

            </div>


            <!-- BOOK LIST -->

            <div class="card">

                <div class="card-title">
                    Books List
                </div>

                <div class="grid-container">

                    <asp:GridView
                        ID="GridView1"
                        runat="server"
                        CssClass="grid"
                        AutoGenerateColumns="False"
                        EmptyDataText="No books found.">

                        <Columns>

                            <asp:BoundField
                                DataField="BookId"
                                HeaderText="Book ID" />

                            <asp:BoundField
                                DataField="BookName"
                                HeaderText="Book Name" />

                            <asp:BoundField
                                DataField="Author"
                                HeaderText="Author" />

                            <asp:BoundField
                                DataField="Quantity"
                                HeaderText="Quantity" />

                        </Columns>

                    </asp:GridView>

                </div>

            </div>

        </div>

    </div>

</form>

</body>

</html>