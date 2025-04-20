<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="restaurant_nikhil.Orders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Orders</title>
    <script>
        function handleStatusChange(dropdown) {
            var originalValue = dropdown.getAttribute('data-original');
            var currentValue = dropdown.value;
            var button = dropdown.closest('td').querySelector('.update-btn');

            if (originalValue !== currentValue) {
                button.style.backgroundColor = '#e74c3c'; 
            } else {
                button.style.backgroundColor = '#4CAF50';
            }
        }
    </script>
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
    <form id="OrdersForm" runat="server">
        <div id="container">
            <asp:TextBox ID="SearchTB" runat="server" Placeholder="Enter Customer name or Order Id" Width="350px"></asp:TextBox>
            <asp:Button ID="SearchBTN" CssClass="action-btn" runat="server" Text="Search" OnClick="SearchOrdersBTN_CLICK" />

            <div>
                <asp:DropDownList ID="FilterStatusDDL" runat="server" CssClass="dropdown-style">
                    <asp:ListItem Text="Pending" Value="pending"></asp:ListItem>
                    <asp:ListItem Text="Confirmed" Value="confirmed"></asp:ListItem>
                    <asp:ListItem Text="Prepared" Value="prepared"></asp:ListItem>
                    <asp:ListItem Text="Delivered" Value="delivered"></asp:ListItem>
                    <asp:ListItem Text="Cancelled" Value="cancelled"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="FilterByStatusBTN" CssClass="action-btn" runat="server" OnClick="FilterOrdersByStatusBTN_CLICK" Text="Apply Filter" />
                <asp:Button ID="ClearFilterBTN" runat="server" CssClass="action-btn" OnClick="ClearFilterBTN_CLICK" Text="Clear Filter" />
            </div>
            <asp:Label ID="FilterInfoLabel" runat="server"></asp:Label>
            <asp:GridView ID="OrdersGrid" runat="server"
                EnableViewState="true"
                AllowSorting="True"
                OnSorting="OrdersGrid_Sorting"
                OnRowCommand="OrdersGrid_RowCommand"
                HeaderStyle-CssClass="grid-header"
                RowStyle-CssClass="grid-row"
                AlternatingRowStyle-BackColor="#f9f9f9"
                AutoGenerateColumns="False"
                CssClass="grid" HeaderStyle-BackColor="#333" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="Order ID" SortExpression="id" />
                    <asp:BoundField DataField="customerName" HeaderText="Ordered By" SortExpression="customerName" />
                    <asp:BoundField DataField="orderValue" HeaderText="Order Value" SortExpression="orderValue" />
                    <asp:BoundField DataField="createdAt" HeaderText="Created At" SortExpression="createdAt" />
                    <asp:BoundField DataField="updatedAt" HeaderText="Last Updated At" SortExpression="updatedAt" />
                    <asp:BoundField DataField="status" HeaderText="Order Status" SortExpression="status" />


                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>


                            <asp:DropDownList ID="StatusUpdateDDL"
                                onchange="handleStatusChange(this)"
                                data-original='<%# Eval("status") %>'
                                CssClass="dropdown-style" runat="server" SelectedValue='<%# Eval("status") %>'>
                                <asp:ListItem Text="Pending" Value="pending" />
                                <asp:ListItem Text="Confirmed" Value="confirmed" />
                                <asp:ListItem Text="Prepared" Value="prepared" />
                                <asp:ListItem Text="Delivered" Value="delivered" />
                                <asp:ListItem Text="Cancelled" Value="cancelled" />
                            </asp:DropDownList>


                            <asp:Button ID="btnUpdateStatus" CssClass="action-btn update-btn" runat="server" Text="Update Status"
                                CommandName="UpdateStatus"
                                CommandArgument='<%# Eval("id") %>' />

                            <asp:Button ID="btnView" runat="server" CssClass="action-btn" Text="View Order Details" CommandArgument='<%# Eval("id") %>' OnClick="OpenOrderDetailsBTN_CLICK" />
                        </ItemTemplate>

                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            

        </div>
        <asp:Label ID="ErrorLabelOrders" runat="server" Text="Hello" Font-Size="Larger" Font-Bold="True" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
