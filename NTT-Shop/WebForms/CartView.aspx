<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="CartView.aspx.cs" Inherits="NTT_Shop.WebForms.CartView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
                <article>
                    <header>
                        <h1>
                            <asp:Label ID="Carrito" Text="" runat="server" />
                        </h1>
                    </header>
                    <section>
                        <h2><asp:Label ID="NoProducts" text="No hay productos en el carrito" runat="server" Visible="true"/></h2>
                        <p>
                            <asp:Repeater id="RepeaterDescID" runat="server">
                            <ItemTemplate>
                                <div class="card">
                                    <asp:Label ID="TitleLabelID" runat="server" Text='<%# Eval("product.descriptions[0].title")%>' /> <br />
                                    <asp:Label ID="DescLabelID" runat="server" Text='<%# Eval("product.descriptions[0].description")%>' /> <br />
                                    x<asp:Label ID="Label1" runat="server" Text='<%# Eval("quantity")%>' /> =
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("totalPrice")%>' /> €
                                    <asp:Button CssClass="buto" text="Eliminar del carrito" CommandName="PasarId" CommandArgument='<%# Eval("product.idProduct")%>' OnClick="btnRemoveCart_Click" runat="server" />
                                    <br /> <br /> 
                                </div>
                            </ItemTemplate>
                            </asp:Repeater>
                        </p>
                        <p>
                            <asp:Button ID="FinButton" Text="Finalizar compra" runat="server" OnClick="Compra_Click"/>
                        </p>
                    </section>
                </article>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
