<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="IssueBooks.aspx.cs"
    Inherits="Library_Management_System.IssueBooks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Issue Books</title>

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
            margin-top: 15px;
        }

        .menu-link {
            display: block;
            width: 100%;
            padding: 17px 25px;
            color: #d5dbe5;
            text-decoration: none;
            font-size: 15px;
            background: none;
            border: none;
            text-align: left;
            cursor: pointer;
        }

        .menu-link:hover {
            background-color: #087ff5;
            color: white;
        }

        .menu-link.active {
            background-color: #087ff5;
            color: white;
        }


        /* ================= MAIN ================= */

        .main {
            margin-left: 240px;
            min-height: 100vh;
        }


        /* ================= TOPBAR ================= */

        .topbar {
            height: 70px;
            background-color: white;
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 0 40px;
            box-shadow: 0 2px 7px rgba(0,0,0,0.08);
        }

        .topbar h2 {
            margin: 0;
            color: #172033;
            font-size: 25px;
        }

        .system-name {
            color: #666;
            font-size: 15px;
        }


        /* ================= CONTENT ================= */

        .content {
            padding: 40px;
        }

        .page-title {
            text-align: center;
            color: #172033;
            font-size: 32px;
            margin-bottom: 35px;
        }


        /* ================= FORM CARD ================= */

        .form-card {
            background-color: white;
            max-width: 850px;
            margin: 0 auto 35px auto;
            padding: 35px;
            border-radius: 12px;
            box-shadow: 0 3px 12px rgba(0,0,0,0.08);
        }


        /* ================= INFO ================= */

        .info-box {
            background-color: #e8f3ff;
            border: 1px solid #9dccff;
            color: #064b91;
            padding: 18px;
            border-radius: 6px;
            margin-bottom: 30px;
            font-size: 16px;
            line-height: 1.5;
        }

        .info-box strong {
            font-weight: bold;
        }


        /* ================= FORM ================= */

        .form-group {
            margin-bottom: 25px;
        }

        .form-group label {
            display: block;
            margin-bottom: 9px;
            color: #222;
            font-size: 17px;
            font-weight: bold;
        }

        .txt {
            width: 100%;
            height: 50px;
            padding: 10px 15px;
            border: 1px solid #ccc;
            border-radius: 7px;
            font-size: 16px;
            outline: none;
        }

        .txt:focus {
            border-color: #087ff5;
        }


        /* ================= MESSAGE ================= */

        .message {
            display: block;
            text-align: center;
            min-height: 25px;
            margin: 15px 0;
            font-size: 16px;
            font-weight: bold;
        }


        /* ================= BUTTONS ================= */

        .buttons {
            text-align: center;
            margin-top: 25px;
        }

        .btn {
            min-width: 135px;
            height: 50px;
            margin: 5px;
            border: none;
            border-radius: 7px;
            color: white;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
        }

        .btn-issue {
            background-color: #28a745;
        }

        .btn-search {
            background-color: #6f42c1;
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


        /* ================= TABLE CARD ================= */

        .table-card {
            background-color: white;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 3px 12px rgba(0,0,0,0.08);
        }

        .table-title {
            color: #172033;
            font-size: 25px;
            margin: 0 0 20px 0;
        }


        /* ================= GRIDVIEW ================= */

        .grid {
            width: 100%;
            border-collapse: collapse;
        }

        .grid th {
            background-color: #172033;
            color: white;
            padding: 15px;
            font-size: 15px;
            text-align: center;
        }

        .grid td {
            padding: 14px;
            border: 1px solid #ddd;
            text-align: center;
            font-size: 15px;
        }

        .grid tr:nth-child(even) {
            background-color: #f8f9fa;
        }

        .grid tr:hover {
            background-color: #eef5ff;
        }


        /* ================= RESPONSIVE ================= */

        @media screen and (max-width: 900px) {

            .sidebar {
                width: 200px;
            }

            .main {
                margin-left: 200px;
            }

            .content {
                padding: 25px;
            }
        }


        @media screen and (max-width: 650px) {

            .sidebar {
                position: relative;
                width: 100%;
                height: auto;
            }

            .main {
                margin-left: 0;
            }

            .topbar {
                padding: 0 20px;
            }

            .system-name {
                display: none;
            }

            .content {
                padding: 15px;
            }

            .form-card {
                padding: 20px;
            }

            .table-card {
                overflow-x: auto;
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
                CssClass="menu-link"
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
                CssClass="menu-link active"
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


        <!-- TOP BAR -->

        <div class="topbar">

            <h2>Issue Books</h2>

            <div class="system-name">
                Library Management System
            </div>

        </div>


        <!-- CONTENT -->

        <div class="content">


            <!-- PAGE TITLE -->

            <div class="page-title">
                Issue Books
            </div>


            <!-- ================= FORM ================= -->

            <div class="form-card">


                <!-- INFO -->

                <div class="info-box">

                    <strong>Issue Book:</strong>
                    Enter Member ID and Book ID.

                    <br />

                    Issue ID and Issue Date will be
                    generated automatically.

                </div>


                <!-- MEMBER ID -->

                <div class="form-group">

                    <label for="txtMemberId">
                        Member ID
                    </label>

                    <asp:TextBox
                        ID="txtMemberId"
                        runat="server"
                        CssClass="txt"
                        placeholder="Enter Member ID">
                    </asp:TextBox>

                </div>


                <!-- BOOK ID -->

                <div class="form-group">

                    <label for="txtBookId">
                        Book ID
                    </label>

                    <asp:TextBox
                        ID="txtBookId"
                        runat="server"
                        CssClass="txt"
                        placeholder="Enter Book ID">
                    </asp:TextBox>

                </div>


                <!-- MESSAGE -->

                <asp:Label
                    ID="lblMessage"
                    runat="server"
                    CssClass="message">
                </asp:Label>


                <!-- BUTTONS -->

                <div class="buttons">


                    <asp:Button
                        ID="btnIssue"
                        runat="server"
                        Text="Issue Book"
                        CssClass="btn btn-issue"
                        OnClick="btnIssue_Click">
                    </asp:Button>


                    <asp:Button
                        ID="btnSearch"
                        runat="server"
                        Text="Search"
                        CssClass="btn btn-search"
                        OnClick="btnSearch_Click">
                    </asp:Button>


                    <asp:Button
                        ID="btnClear"
                        runat="server"
                        Text="Clear"
                        CssClass="btn btn-clear"
                        OnClick="btnClear_Click">
                    </asp:Button>


                    <asp:Button
                        ID="btnBack"
                        runat="server"
                        Text="Back"
                        CssClass="btn btn-back"
                        OnClick="btnBack_Click">
                    </asp:Button>

                </div>

            </div>


            <!-- ================= TABLE ================= -->

            <div class="table-card">

                <h2 class="table-title">
                    Issued Books List
                </h2>


                <asp:GridView
                    ID="GridView1"
                    runat="server"
                    CssClass="grid"
                    AutoGenerateColumns="False"
                    EmptyDataText="No issued books found.">

                    <Columns>


                        <asp:BoundField
                            DataField="IssueId"
                            HeaderText="Issue ID" />


                        <asp:BoundField
                            DataField="MemberId"
                            HeaderText="Member ID" />


                        <asp:BoundField
                            DataField="BookId"
                            HeaderText="Book ID" />


                        <asp:BoundField
                            DataField="IssueDate"
                            HeaderText="Issue Date"
                            DataFormatString="{0:dd-MM-yyyy}" />


                    </Columns>

                </asp:GridView>

            </div>


        </div>

    </div>

</form>

</body>

</html>