<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs"
    Inherits="Library_Management_System.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Register - Library Management System</title>

    <style>

        * {
            box-sizing: border-box;
        }

        body {
            margin: 0;
            padding: 0;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #e6f2ff;
        }

        .container {
            width: 430px;
            margin: 70px auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0px 0px 15px rgba(0,0,0,0.20);
        }

        h2 {
            text-align: center;
            color: darkblue;
            margin-bottom: 25px;
        }

        table {
            width: 100%;
        }

        td {
            padding: 9px;
        }

        .label {
            font-weight: bold;
            color: #333;
        }

        .txt {
            width: 230px;
            height: 35px;
            padding: 6px 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 15px;
        }

        .txt:focus {
            outline: none;
            border-color: #087ff5;
        }

        .btn {
            width: 125px;
            height: 38px;
            background-color: darkblue;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            font-weight: bold;
        }

        .btn:hover {
            background-color: #087ff5;
        }

        .message {
            display: block;
            color: red;
            font-weight: bold;
            text-align: center;
            margin-top: 10px;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <h2>User Registration</h2>

        <table>

            <tr>
                <td class="label">
                    Username
                </td>

                <td>
                    <asp:TextBox
                        ID="txtUsername"
                        runat="server"
                        CssClass="txt">
                    </asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="label">
                    Password
                </td>

                <td>
                    <asp:TextBox
                        ID="txtPassword"
                        runat="server"
                        TextMode="Password"
                        CssClass="txt">
                    </asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="label">
                    Confirm Password
                </td>

                <td>
                    <asp:TextBox
                        ID="txtConfirmPassword"
                        runat="server"
                        TextMode="Password"
                        CssClass="txt">
                    </asp:TextBox>
                </td>
            </tr>

            <tr>
                <td colspan="2" align="center">

                    <asp:Button
                        ID="btnRegister"
                        runat="server"
                        Text="Register"
                        CssClass="btn"
                        OnClick="btnRegister_Click" />

                    &nbsp;

                    <asp:Button
                        ID="btnBack"
                        runat="server"
                        Text="Back to Login"
                        CssClass="btn"
                        OnClick="btnBack_Click" />

                </td>
            </tr>

            <tr>
                <td colspan="2">

                    <asp:Label
                        ID="lblMessage"
                        runat="server"
                        CssClass="message">
                    </asp:Label>

                </td>
            </tr>

        </table>

    </div>

</form>

</body>

</html>