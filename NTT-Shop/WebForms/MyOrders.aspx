<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="NTT_Shop.WebForms.MyOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
                <article>
                    <header>
                        <h1>Pedidos</h1>
                    </header>
                    <p>
                        <asp:Repeater ID="OrdersRepeater" runat="server">
                            <ItemTemplate>
                                <div class="card">
                                    ESTADO : <asp:Label Text='<%# Eval("orderStatus.orderStatusName")%>' runat="server" />  <br />
                                    Pedido nº: <asp:Label Text='<%# Eval("idOrder")%>' runat="server" /> 
                                    a las <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("dateTime")%>' /> <br /> 
                                    <asp:Label ID="StatusLabel" runat="server" Text="" /> 
                                    <asp:Label ID="TotalPriceLabel" runat="server" Text='<%# Eval("totalPrice")%>' /> € <br /> 
                                    <asp:Repeater id="RepeaterDetailID" runat="server" DataSource='<%# Eval("orderDetails")%>'>
                                        <ItemTemplate>
                                            <asp:Label ID="TitleLabelID" runat="server" Text='<%# Eval("product.descriptions[0].title")%>' /> <br />
                                            x<asp:Label ID="Label1" runat="server" Text='<%# Eval("units")%>' /> =
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("price")%>' /> €
                                            <br /> <br /> 
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <br />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </p>
                </article>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
