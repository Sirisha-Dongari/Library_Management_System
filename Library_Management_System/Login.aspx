<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="Library_Management_System.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Library Management System - Login</title>

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
            width: 400px;
            margin: 80px auto;
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
            padding: 10px;
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
            width: 120px;
            height: 38px;
            background-color: darkblue;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 15px;
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

        .register {
            text-align: center;
            margin-top: 18px;
            font-size: 15px;
        }

        .register a {
            color: #087ff5;
            text-decoration: none;
            font-weight: bold;
        }

        .register a:hover {
            text-decoration: underline;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <h2>Library Management System</h2>

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
                <td colspan="2" align="center">

                    <asp:Button
                        ID="btnLogin"
                        runat="server"
                        Text="Login"
                        CssClass="btn"
                        OnClick="btnLogin_Click" />

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

        <div class="register">

            New user?

            <asp:HyperLink
                ID="lnkRegister"
                runat="server"
                NavigateUrl="Register.aspx"
                Text="Register here">
            </asp:HyperLink>

        </div>

    </div>

</form>

</body>

</html>