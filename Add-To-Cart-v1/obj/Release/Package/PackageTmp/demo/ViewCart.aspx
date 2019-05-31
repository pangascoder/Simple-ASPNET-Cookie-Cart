<%@ Page Title="" Language="C#" MasterPageFile="~/demo/AddToCartDemo.Master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="Add_To_Cart_v1.demo.WebForm2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="Products.aspx">Back to Products</a>
    <br />
    <br />
    <asp:GridView runat="server" ID="GVCart" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="80%" HorizontalAlign="Center" AutoGenerateColumns="false" OnRowCommand="GVCart_RowCommand">
        <AlternatingRowStyle BackColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField HeaderText="Product Name">
                <ItemTemplate>
                    <p><%# Eval("product_name") %></p>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Product Price">
                <ItemTemplate>
                    <p><%# string.Format("{0:C}", Convert.ToDecimal(Eval("product_price"))) %></p>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <p><%# Eval("product_quantity") %></p>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price Total">
                <ItemTemplate>
                    <p><%# string.Format("{0:C}", Convert.ToDecimal(Eval("product_item_total"))) %></p>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="BtnRemoveFromCart" CommandArgument='<%# Eval("product_id") %>' CommandName="Remove From Cart" Text="Remove"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
</asp:Content>
