<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="restaurant_nikhil.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <style>
        html {
            height: 100%;
        }

        body {
            margin: 0;
            width: 100%;
            height: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        form {
            width: 300px;
            padding: 20px 10px;
            border: 1px solid black;
        }

        .login {
            text-align: center;
            margin: 0 0 20px 0;
        }

        .input-container {
            margin: 10px 0;
            display: flex;
            justify-content: space-between;
        }

        .btn-container {
            margin: 10px 0;
            display: flex;
            justify-content: center;
        }

        .btn {
            margin-top: 30px;
            padding: 10px 40px;
            color: white;
            background-color: black;
            border-radius: 15px;
            margin: auto;
        }

        .btn:hover {
            background-color: white;
            color: black;
            border: 1px solid black;
        }

        .redirect-label {
            color: blue;
            cursor: pointer;
        }


        .input {
            margin-left: 10px;
        }
        .hide{
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="label1" runat="server" Text="Admin Panel" Font-Size="Larger" Font-Bold="True" CssClass="login" Width="100%"></asp:Label>
        </div>
        <div class="input-container">
            <asp:Label ID="Label2" runat="server" Text="Username: "></asp:Label>
            <asp:TextBox ID="usernameTB" runat="server" CssClass="input"></asp:TextBox>
        </div>
        <div class="input-container">
            <asp:Label ID="Label3" runat="server" Text="Password: "></asp:Label>
            <asp:TextBox ID="passwordTB" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <input type="checkbox" id="showPassCheck" onchange="document.getElementById('passwordTB').type=this.checked ? 'text' :'password'" />
            <label for="showPassCheck">Show Password</label>
        </div>
        <div class="btn-container">
            <asp:Button ID="loginBTN" runat="server" Text="Login" OnClick="loginBTN_Click" CssClass="btn" />
        </div>
        <div class="btn-container hide">
            <asp:Button ID="GoSignup" runat="server" Text="New To Our Platform? Click Here" OnClick="RedirectToSignup" CssClass="redirect-label" />
        </div>
    </form>
</body>
</html>