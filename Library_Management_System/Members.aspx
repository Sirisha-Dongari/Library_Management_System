<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Members.aspx.cs"
    Inherits="Library_Management_System.Members" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Members Management</title>

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

        .container {
            width: 90%;
            max-width: 1100px;
            margin: 40px auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.15);
        }

        .title {
            text-align: center;
            font-size: 30px;
            font-weight: bold;
            margin-bottom: 30px;
            color: #333;
        }

        .info {
            background-color: #e8f4ff;
            border: 1px solid #b8daff;
            padding: 12px;
            margin-bottom: 20px;
            border-radius: 5px;
            color: #004085;
            line-height: 1.6;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            color: #333;
        }

        .textbox {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 15px;
        }

        .textbox:focus {
            border-color: #007bff;
            outline: none;
        }

        .button-container {
            text-align: center;
            margin-top: 25px;
            margin-bottom: 25px;
        }

        .btn {
            padding: 10px 20px;
            margin: 5px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 15px;
            font-weight: bold;
        }

        .btn:hover {
            opacity: 0.85;
        }

        .btn-add {
            background-color: #28a745;
            color: white;
        }

        .btn-search {
            background-color: #6f42c1;
            color: white;
        }

        .btn-update {
            background-color: #007bff;
            color: white;
        }

        .btn-delete {
            background-color: #dc3545;
            color: white;
        }

        .btn-clear {
            background-color: #6c757d;
            color: white;
        }

        .btn-back {
            background-color: #343a40;
            color: white;
        }

        .message {
            display: block;
            text-align: center;
            font-weight: bold;
            font-size: 16px;
            margin: 15px 0;
            min-height: 22px;
        }

        .grid-container {
            margin-top: 30px;
            overflow-x: auto;
        }

        .grid {
            width: 100%;
            border-collapse: collapse;
        }

        .grid th {
            background-color: #343a40;
            color: white;
            padding: 12px;
            text-align: center;
        }

        .grid td {
            padding: 10px;
            text-align: center;
            border: 1px solid #ddd;
        }

        .grid tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .grid tr:hover {
            background-color: #ddd;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <!-- =====================================================
             TITLE
        ====================================================== -->

        <div class="title">
            Members Management
        </div>


        <!-- =====================================================
             INFORMATION
        ====================================================== -->

        <div class="info">

            <b>Add:</b>
            Leave Member ID empty.
            SQL Server will generate the Member ID automatically.

            <br />

            <b>Update:</b>
            Enter Member ID and fill the fields you want to update.

            <br />

            <b>Search:</b>
            Enter Member ID and click Search.

        </div>


        <!-- =====================================================
             MEMBER ID
        ====================================================== -->

        <div class="form-group">

            <label>
                Member ID
            </label>

            <asp:TextBox
                ID="txtMemberId"
                runat="server"
                CssClass="textbox">
            </asp:TextBox>

        </div>


        <!-- =====================================================
             MEMBER NAME
        ====================================================== -->

        <div class="form-group">

            <label>
                Member Name
            </label>

            <asp:TextBox
                ID="txtMemberName"
                runat="server"
                CssClass="textbox">
            </asp:TextBox>

        </div>


        <!-- =====================================================
             EMAIL
        ====================================================== -->

        <div class="form-group">

            <label>
                Email
            </label>

            <asp:TextBox
                ID="txtEmail"
                runat="server"
                CssClass="textbox">
            </asp:TextBox>

        </div>


        <!-- =====================================================
             PHONE
        ====================================================== -->

        <div class="form-group">

            <label>
                Phone Number (Optional)
            </label>

            <asp:TextBox
                ID="txtPhone"
                runat="server"
                CssClass="textbox">
            </asp:TextBox>

        </div>


        <!-- =====================================================
             MESSAGE
        ====================================================== -->

        <asp:Label
            ID="lblMessage"
            runat="server"
            CssClass="message">
        </asp:Label>


        <!-- =====================================================
             BUTTONS
        ====================================================== -->

        <div class="button-container">

            <asp:Button
                ID="btnAdd"
                runat="server"
                Text="Add Member"
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
                OnClientClick="return confirm('Are you sure you want to delete this member?');" />


            <asp:Button
                ID="btnClear"
                runat="server"
                Text="Clear"
                CssClass="btn btn-clear"
                OnClick="btnClear_Click" />


            <asp:Button
                ID="btnBack"
                runat="server"
                Text="Back"
                CssClass="btn btn-back"
                OnClick="btnBack_Click" />

        </div>


        <!-- =====================================================
             MEMBERS GRID
        ====================================================== -->

        <div class="grid-container">

            <asp:GridView
                ID="GridView1"
                runat="server"
                CssClass="grid"
                AutoGenerateColumns="False"
                EmptyDataText="No members found.">

                <Columns>

                    <asp:BoundField
                        DataField="MemberId"
                        HeaderText="Member ID" />

                    <asp:BoundField
                        DataField="MemberName"
                        HeaderText="Member Name" />

                    <asp:BoundField
                        DataField="Email"
                        HeaderText="Email" />

                    <asp:BoundField
                        DataField="Phone"
                        HeaderText="Phone" />

                </Columns>

            </asp:GridView>

        </div>

    </div>

</form>

</body>

</html>