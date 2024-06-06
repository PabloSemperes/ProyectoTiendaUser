<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="NTT_Shop.WebForms.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel ID="updPanel" class="gray_bg" runat="server" UpdateMode="Always">
     <ContentTemplate>
         <section id="main-content">	
             <article>
                 <header>
                     <h1>Perfil</h1>
                 </header>
                 <table border="0" cellpadding="10" cellspacing="0" >
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server">Usuario</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLogin" MaxLength="100" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <div class="col-4 d-grid gap-2" style="margin-top: 10px;">
                            <asp:Button runat="server" ID="btnCambiarC" Text="Cambiar Contraseña"  OnClick="btnCambiarC_Click"  class="btn btn-outline-secondary"/>
                        </div>
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
                            <asp:Label ID="Label6" runat="server">Segundo apellido</asp:Label>
                        </td>
                        <td style="height: 57px">
                            <asp:TextBox ID="txtSurname2" MaxLength="100" runat="server" Height="29px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="height: 57px">
                            <asp:Label ID="Label7" runat="server">Dirección</asp:Label>
                        </td>
                        <td style="height: 57px">
                            <asp:TextBox ID="txtAdress" MaxLength="200" runat="server" Height="29px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="height: 57px">
                            <asp:Label ID="Label8" runat="server">Provincia</asp:Label>
                        </td>
                        <td style="height: 57px">
                            <asp:TextBox ID="txtProvince" MaxLength="50" runat="server" Height="29px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="height: 57px">
                            <asp:Label ID="Label9" runat="server">Pueblo/Ciudad</asp:Label>
                        </td>
                        <td style="height: 57px">
                            <asp:TextBox ID="txtCity" MaxLength="50" runat="server" Height="29px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="height: 57px">
                            <asp:Label ID="Label10" runat="server">Código postal</asp:Label>
                        </td>
                        <td style="height: 57px; vertical-align:central;">
                            <asp:TextBox ID="txtPostalCode" MaxLength="5" runat="server" Height="29px"></asp:TextBox>
                            <asp:Label ID="labelPostalCode" CssClass="userError" Text="Introduzca solo números" runat="server"/>
                        </td>
                    </tr>
                      <tr>
                        <td style="height: 57px">
                            <asp:Label ID="Label11" runat="server">Teléfono</asp:Label>
                        </td>
                        <td style="height: 57px">
                            <asp:TextBox ID="txtPhone" MaxLength="12" runat="server" Height="29px"></asp:TextBox>
                            <asp:Label ID="labelPhone" CssClass="userError" Text="Introduzca solo números" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 57px">
                            <asp:Label ID="Label5" runat="server">Email</asp:Label>
                        </td>
                        <td style="height: 57px">
                            <asp:TextBox ID="txtMail" type="email" MaxLength="100" runat="server" Height="29px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td style="height: 57px">
                            <asp:Label ID="Label12" runat="server">Idioma</asp:Label>
                        </td>
                        <td style="height: 57px">
                             <asp:DropDownList ID="cboxLanguage" runat="server" style="width: 200px; float: left;" ></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td class="CELDA7 width4" align="left" style="height: 57px">
                           <asp:Button id="btnNewRegister" runat="server" OnClick="btnUpdate_Click" Text="Actualizar datos" />
                        </td>
                    </tr>
                      
                </table>
             </article>
        </section>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
