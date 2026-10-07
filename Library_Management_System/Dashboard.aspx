<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="Library_Management_System.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Library Dashboard</title>

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
            background-color: transparent;
            border: none;
            color: #d5dbe5;
            text-decoration: none;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 15px;
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
            font-size: 25px;
            color: #172033;
        }

        .user-info {
            color: #555;
            font-size: 15px;
            font-weight: bold;
        }

        /* ================= CONTENT ================= */

        .content {
            padding: 40px;
        }

        .page-title {
            margin-bottom: 30px;
        }

        .page-title h1 {
            margin: 0;
            color: #172033;
            font-size: 32px;
        }

        .page-title p {
            margin-top: 8px;
            color: #777;
            font-size: 15px;
        }

        /* ================= DASHBOARD CARDS ================= */

        .cards {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 25px;
            margin-bottom: 40px;
        }

        .dashboard-card {
            background-color: white;
            border-radius: 12px;
            padding: 25px;
            min-height: 145px;
            display: flex;
            align-items: center;
            box-shadow: 0 3px 12px rgba(0,0,0,0.08);
        }

        .card-icon {
            width: 60px;
            height: 60px;
            min-width: 60px;
            border-radius: 12px;
            background-color: #e7f0ff;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 28px;
            margin-right: 18px;
        }

        .card-info h3 {
            margin: 0 0 7px 0;
            font-size: 17px;
            color: #666;
        }

        .card-info h2 {
            margin: 0;
            font-size: 34px;
            color: #111827;
        }

        .card-info p {
            margin: 7px 0 0 0;
            color: #888;
            font-size: 13px;
        }

        /* ================= WELCOME ================= */

        .welcome {
            background-color: white;
            border-radius: 12px;
            padding: 35px;
            box-shadow: 0 3px 12px rgba(0,0,0,0.08);
            margin-bottom: 30px;
        }

        .welcome h2 {
            margin: 0 0 18px 0;
            color: #172033;
            font-size: 25px;
        }

        .welcome p {
            color: #666;
            font-size: 16px;
            line-height: 1.7;
            margin: 8px 0;
        }

        /* ================= QUICK ACTIONS ================= */

        .quick-actions {
            background-color: white;
            border-radius: 12px;
            padding: 30px;
            box-shadow: 0 3px 12px rgba(0,0,0,0.08);
        }

        .quick-actions h2 {
            margin: 0 0 20px 0;
            color: #172033;
        }

        .action-btn {
            display: inline-block;
            padding: 13px 23px;
            margin-right: 10px;
            margin-bottom: 10px;
            border: none;
            border-radius: 6px;
            color: white;
            text-decoration: none;
            font-size: 14px;
            font-weight: bold;
            cursor: pointer;
        }

        .action-blue {
            background-color: #087ff5;
        }

        .action-green {
            background-color: #28a745;
        }

        .action-purple {
            background-color: #6f42c1;
        }

        .action-orange {
            background-color: #fd7e14;
        }

        .action-btn:hover {
            opacity: 0.85;
        }

        /* ================= LOGOUT ================= */

        .logout-area {
            margin-top: 30px;
        }

        .logout-btn {
            width: 100%;
            padding: 14px;
            background-color: #dc3545;
            color: white;
            border: none;
            border-radius: 6px;
            font-size: 15px;
            font-weight: bold;
            cursor: pointer;
        }

        .logout-btn:hover {
            background-color: #c82333;
        }

        /* ================= RESPONSIVE ================= */

        @media screen and (max-width: 1100px) {

            .cards {
                grid-template-columns: repeat(2, 1fr);
            }
        }

        @media screen and (max-width: 700px) {

            .sidebar {
                position: relative;
                width: 100%;
                height: auto;
            }

            .main {
                margin-left: 0;
            }

            .cards {
                grid-template-columns: 1fr;
            }

            .content {
                padding: 20px;
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
                CssClass="menu-link active"
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

        <!-- TOP BAR -->

        <div class="topbar">

            <h2>Dashboard</h2>

            <div class="user-info">

                Welcome,
                
                <asp:Label
                    ID="lblUsername"
                    runat="server">
                </asp:Label>

            </div>

        </div>


        <!-- CONTENT -->

        <div class="content">

            <!-- PAGE TITLE -->

            <div class="page-title">

                <h1>Dashboard</h1>

                <p>
                    Overview of your library management system
                </p>

            </div>


            <!-- ================= CARDS ================= -->

            <div class="cards">

                <!-- TOTAL BOOKS -->

                <div class="dashboard-card">

                    <div class="card-icon">
                        📚
                    </div>

                    <div class="card-info">

                        <h3>
                            Total Books
                        </h3>

                        <h2>

                            <asp:Label
                                ID="lblTotalBooks"
                                runat="server"
                                Text="0">
                            </asp:Label>

                        </h2>

                        <p>
                            Books in library
                        </p>

                    </div>

                </div>


                <!-- TOTAL MEMBERS -->

                <div class="dashboard-card">

                    <div class="card-icon">
                        👥
                    </div>

                    <div class="card-info">

                        <h3>
                            Total Members
                        </h3>

                        <h2>

                            <asp:Label
                                ID="lblTotalMembers"
                                runat="server"
                                Text="0">
                            </asp:Label>

                        </h2>

                        <p>
                            Registered members
                        </p>

                    </div>

                </div>


                <!-- ISSUED BOOKS -->

                <div class="dashboard-card">

                    <div class="card-icon">
                        📖
                    </div>

                    <div class="card-info">

                        <h3>
                            Issued Books
                        </h3>

                        <h2>

                            <asp:Label
                                ID="lblIssuedBooks"
                                runat="server"
                                Text="0">
                            </asp:Label>

                        </h2>

                        <p>
                            Currently issued
                        </p>

                    </div>

                </div>


                <!-- RETURNED BOOKS -->

                <div class="dashboard-card">

                    <div class="card-icon">
                        ↩️
                    </div>

                    <div class="card-info">

                        <h3>
                            Returned Books
                        </h3>

                        <h2>

                            <asp:Label
                                ID="lblReturnedBooks"
                                runat="server"
                                Text="0">
                            </asp:Label>

                        </h2>

                        <p>
                            Returned books
                        </p>

                    </div>

                </div>

            </div>


            <!-- ================= WELCOME ================= -->

            <div class="welcome">

                <h2>
                    Library Management Dashboard
                </h2>

                <p>
                    Welcome to the Library Management System.
                </p>

                <p>
                    Manage books, members, issued books and
                    returned books from the navigation menu.
                </p>

                <p>
                    Select a module from the sidebar or use
                    Quick Actions below.
                </p>

            </div>


            <!-- ================= QUICK ACTIONS ================= -->

            <div class="quick-actions">

                <h2>
                    Quick Actions
                </h2>

                <asp:LinkButton
                    ID="btnBooks"
                    runat="server"
                    Text="Manage Books"
                    CssClass="action-btn action-blue"
                    PostBackUrl="~/Book.aspx">
                </asp:LinkButton>

                <asp:LinkButton
                    ID="btnMembers"
                    runat="server"
                    Text="Members"
                    CssClass="action-btn action-green"
                    PostBackUrl="~/Members.aspx">
                </asp:LinkButton>

                <asp:LinkButton
                    ID="btnIssue"
                    runat="server"
                    Text="Issue Book"
                    CssClass="action-btn action-purple"
                    PostBackUrl="~/IssueBooks.aspx">
                </asp:LinkButton>

                <asp:LinkButton
                    ID="btnReturn"
                    runat="server"
                    Text="Return Book"
                    CssClass="action-btn action-orange"
                    PostBackUrl="~/ReturnBooks.aspx">
                </asp:LinkButton>

            </div>


            <!-- ================= LOGOUT ================= -->

            <div class="logout-area">

                <asp:Button
                    ID="btnLogout"
                    runat="server"
                    Text="Logout"
                    CssClass="logout-btn"
                    OnClick="btnLogout_Click" />

            </div>

        </div>

    </div>

</form>

</body>

</html>