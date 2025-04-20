<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="restaurant_nikhil.Signup" %>
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

        .redirect-label {
            color: blue;
            cursor: pointer;
        }

        .btn:hover {
            background-color: white;
            color: black;
            border: 1px solid black;
        }

        .input {
            margin-left: 10px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <div>
            <asp:Label ID="label1" runat="server" Text="Signup Form" Font-Size="Larger" Font-Bold="True" CssClass="login" Width="100%"></asp:Label>
        </div>
        <div class="input-container">
            <asp:Label ID="Label4" runat="server" Text="First Name: "></asp:Label>
            <asp:TextBox ID="firstNameTB" runat="server" CssClass="input"></asp:TextBox>
        </div>
        <div class="input-container">
            <asp:Label ID="Label5" runat="server" Text="Last Name: "></asp:Label>
            <asp:TextBox ID="lastNameTB" runat="server" CssClass="input"></asp:TextBox>
        </div>
        <div class="input-container">
            <asp:Label ID="Label2" runat="server" Text="Email: "></asp:Label>
            <asp:TextBox ID="emailTBreg" runat="server" CssClass="input"></asp:TextBox>
        </div>
        <div class="input-container">
            <asp:Label ID="Label3" runat="server" Text="Password: "></asp:Label>
            <asp:TextBox ID="passwordTBreg" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <input type="checkbox" onchange="document.getElementById('passwordTBreg').type=this.checked ? 'text' :'password'" />
            Show Password
        </div>
        <div class="btn-container">
            <asp:Button ID="signupBTN" runat="server" Text="Signup" OnClick="signupBTN_Click" CssClass="btn" />
        </div>
        <div class="btn-container">
            <asp:Button ID="GoSignup" runat="server" Text="Already Registered? Click Here" OnClick="RedirectToLogin" CssClass="redirect-label" />
        </div>
    </form>
</body>
</html>
