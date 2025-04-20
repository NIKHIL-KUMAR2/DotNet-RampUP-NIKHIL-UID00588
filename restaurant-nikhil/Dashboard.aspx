<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="restaurant_nikhil.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <style>
        .grid {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 15px;
            background-color: #ffffff;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
            border-radius: 10px;
            overflow: hidden;
        }

            .grid th {
                background: linear-gradient(to right, #4b6cb7, #182848);
                color: white;
                text-align: left;
                padding: 14px 16px;
                font-weight: 600;
                letter-spacing: 0.5px;
            }

            .grid td {
                padding: 12px 16px;
                border-top: 1px solid #eaeaea;
                color: #333;
            }

            .grid tr:nth-child(even) {
                background-color: #f9f9f9;
            }

            .grid tr:hover td {
                background-color: #eef3fc;
                transition: background-color 0.3s ease;
            }

            .grid td:first-child,
            .grid th:first-child {
                border-top-left-radius: 10px;
            }

            .grid td:last-child,
            .grid th:last-child {
                border-top-right-radius: 10px;
            }

        .action-btn {
            background-color: #4CAF50;
            color: white;
            padding: 6px 6px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: all 0.2s ease-in-out;
            font-size: 14px;
        }

            .action-btn:hover {
                background-color: #45a049;
            }

        .dropdown-style {
            padding: 6px 8px;
            border-radius: 4px;
            border: 1px solid #ccc;
            background-color: #f7f7f7;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <asp:Label Text="Welcome" runat="server" ID="SignedUserName"></asp:Label>
    <asp:Label Text="" runat="server" ID="SessionTimeLabel"></asp:Label>


    <form id="form1" runat="server">
        <div>

            <asp:GridView ID="RestaurantGrid" runat="server" EnableViewState="true" AutoGenerateColumns="False" CssClass="grid" HeaderStyle-BackColor="#333" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="Restaurant ID" />
                    <asp:BoundField DataField="name" HeaderText="Restaurant Name" />
                    <asp:BoundField DataField="description" HeaderText="Description" />


                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnView" runat="server" Text="View Orders" CommandArgument='<%# Eval("id") %>' OnClick="ViewOrdersBTN_CLICK" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="logoutBTN" runat="server" Text="Logout" OnClick="LogoutBTN_CLICK" />
        </div>
    </form>
</body>
</html>
