<%@ Page Title="" Language="C#" MasterPageFile="MasterLogin.Master" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="NTT_Shop.WebForms.LoginForm" %>
<asp:Content ID="Register" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
                <article>
                    <header>
                        <h1>Iniciar Sesión</h1>
                    </header>
                    <table class="login" border="0" cellpadding="10" cellspacing="0" >
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server">Usuario</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLogin" MaxLength="100" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 57px">
                                <asp:Label ID="Label2" runat="server">Contraseña</asp:Label>
                            </td>
                            <td style="height: 57px">
                                <asp:TextBox ID="txtPass" MaxLength="100" runat="server" Height="29px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="CELDA7 width4" align="left" style="height: 57px">
                               <asp:Button id="btnNewLogin" runat="server" OnClick="btnNewLogin_Click" Text="Iniciar sesión" />
                            </td>
                        </tr>
                    </table>
                </article>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
