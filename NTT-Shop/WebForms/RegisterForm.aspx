<%@ Page Title="" Language="C#" MasterPageFile="MasterLogin.Master" AutoEventWireup="true" CodeBehind="RegisterForm.aspx.cs" Inherits="NTT_Shop.WebForms.RegisterForm" %>
<asp:Content ID="Register" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <section id="main-content">	
                <article>
                    <header>
                        <h1>Formulario de registro</h1>
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
                                <asp:TextBox ID="txtPass" MaxLength="100" runat="server" Height="29px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 57px">
                                <asp:Label ID="Label3" runat="server">Nombre</asp:Label>
                            </td>
                            <td style="height: 57px">
                                <asp:TextBox ID="txtName" MaxLength="100" runat="server" Height="29px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 57px">
                                <asp:Label ID="Label4" runat="server">Apellido</asp:Label>
                            </td>
                            <td style="height: 57px">
                                <asp:TextBox ID="txtSurname" MaxLength="100" runat="server" Height="29px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 57px">
                                <asp:Label ID="Label5" runat="server">Email</asp:Label>
                            </td>
                            <td style="height: 57px">
                                <asp:TextBox ID="txtMail" MaxLength="100" runat="server" Height="29px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 57px">
                                <asp:Label ID="Label6" runat="server">Idioma</asp:Label>
                            </td>
                            <td style="height: 57px">
                                 <asp:DropDownList ID="cboxLanguage" runat="server" style="width: 200px; float: left;" ></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="CELDA7 width4" align="left" style="height: 57px">
                               <asp:Button id="btnNewRegister" runat="server" OnClick="btnNewRegister_Click" Text="Registrarse" />
                            </td>
                        </tr>
                    </table>
                </article>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
