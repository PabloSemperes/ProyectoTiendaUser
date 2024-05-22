<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="ProductDisplay.aspx.cs" Inherits="NTT_Shop.WebForms.ProductDisplay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
                <article class="card">
                    <header>
                        <h1>
                            <asp:Label ID="ProductNameID" Text="" runat="server" />
                        </h1>
                    </header>
                    <p>
                         <%--<asp:image imageurl="imageurl" runat="server" />--%>
                         <div class="price"><asp:Label ID="ProdPriceID" text="" runat="server" /> € <br /></div>
                         <div class="droplist">Selecccionar cantidad : <asp:DropDownList runat="server" ID="DDnumber"> 
                             <asp:ListItem Text="1" Selected="True"/>
                             <asp:ListItem Text="2" />
                             <asp:ListItem Text="3" />
                             <asp:ListItem Text="4" />
                             <asp:ListItem Text="5" />
                         </asp:DropDownList> <br /> </div>
                         <asp:Button class="buto" text="Añadir al carrito" runat="server"  OnClick="btnAddCart_Click"/> <br />
                         <asp:Repeater id="RepeaterDescID" runat="server">
                           <ItemTemplate>
                              <asp:Label ID="DescLabelID" runat="server" Text='<%# Eval("description")%>' /> <br />
                           </ItemTemplate>
                        </asp:Repeater>
                    </p>
                </article>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
