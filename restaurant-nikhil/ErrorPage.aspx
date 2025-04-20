<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="restaurant_nikhil.ErrorPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f8f8;
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
            margin: 0;
        }

        .container {
            text-align: center;
            padding: 30px;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            width: 400px;
        }

        .title {
            font-size: 24px;
            color: #e74c3c;
            margin-bottom: 10px;
        }

        .error-message {
            font-size: 16px;
            color: #333;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="title">Oops! Something went wrong.</div>
            <asp:Label ID="ErrorMessageLabel" runat="server" CssClass="error-message" Text="An unexpected error occurred. Please try again later."></asp:Label>
        </div>
    </form>
</body>
</html>
