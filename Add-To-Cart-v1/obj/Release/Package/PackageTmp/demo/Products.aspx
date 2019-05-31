<%@ Page Title="" Language="C#" MasterPageFile="~/demo/AddToCartDemo.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Add_To_Cart_v1.demo.WebForm1" %>
<%@ MasterType VirtualPath="AddtoCartDemo.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ListView ID="LVProducts" runat="server" GroupItemCount="3" OnItemCommand="LVProducts_ItemCommand">
       <GroupTemplate>
                <div id="itemPlaceholderContainer" class="flex-container" runat="server">
                          <div id="itemPlaceholder" runat="server"></div>
                 </div>
       </GroupTemplate>
       <ItemTemplate>
                 <div class="container">
                       <h2><%# Eval("product_name") %></h2>
                        <p><%# string.Format("{0:C}", Convert.ToDecimal(Eval("product_price"))) %></p>
                     <br />
                     <br />
                     <asp:LinkButton runat="server" ID="BtnAddToCart" CommandArgument='<%# Eval("product_id") %>' CommandName="Add to Cart" Text="Add to Cart"></asp:LinkButton>
                  </div>
       </ItemTemplate>
    </asp:ListView>
    
</asp:Content>
