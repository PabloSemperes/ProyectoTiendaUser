<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="ProductsPage.aspx.cs" Inherits="NTT_Shop.WebForms.ProductsPage"%>
<asp:Content ID="Content1" class="gray_bg" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
                <article>
                    <header>
                        <h1>Productos</h1>
                    </header>
                    <p>
                         <asp:ListView runat="server"  ID="ProductListView" OnPagePropertiesChanging="ProductListView_PagePropertiesChanging">
                              <LayoutTemplate>
                                <div runat="server" id="lstProducts">
                                  <div runat="server" id="itemPlaceholder" />
                                </div>
                                <asp:DataPager ID="DataPager1" runat="server" PageSize="5" PagedControlID="ProductListView">
                                  <Fields>
                                    <asp:NumericPagerField />
                                  </Fields>
                                </asp:DataPager>
                              </LayoutTemplate>
                              <ItemTemplate>
                                <%--<asp:Image ID="ProductImage" runat="server"
                                  ImageUrl='<%# "~/images/thumbnails/" + Eval("ThumbnailPhotoFileName") %>' />--%>	        
                                <div class="card" runat="server">
                                  <asp:HyperLink CssClass="card-title" ID="ProductLink" runat="server" Text='<%# Eval("descriptions[0].title") %>' 
                                    NavigateUrl='<%# "ProductDisplay.aspx?id=" + Eval("idProduct") %>' /> <br />
                                    <div class="desc">
                                        <asp:Label CssClass="card-desc" ID="DescLabel" runat="server" Text='<%# Eval("descriptions[0].description")%>' /> <br />
                                    </div>
                                    <div class="price">
                                        <asp:Label CssClass="card-desc" ID="PriceLabel" runat="server" Text='<%# Eval("rates[0].price")%>' /> €<br />
                                    </div> 
                                  <%--<asp:Button class="buy--btn" id="btnAddCart" runat="server" CommandName="PasarId" CommandArgument='<%# Eval("idProduct")%>' OnClick="btnAddCart_Click" Text="Añadir al carrito"/>--%>
                                </div>
                                <br />
                              </ItemTemplate>
                        </asp:ListView>
                    </p>
                </article>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
