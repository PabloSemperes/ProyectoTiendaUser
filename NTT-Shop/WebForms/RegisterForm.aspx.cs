using API_NTT_SHOP.Models;
using Newtonsoft.Json;
using NTT_Shop.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTT_Shop.WebForms
{
    public partial class RegisterForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnNewRegister_Click(object sender, EventArgs e)
        {
            //Recoger valores formulario:
            string login = txtLogin.Text, pass = txtPass.Text,
            name = txtName.Text, surname = txtSurname.Text, email = txtMail.Text; 

            //Crear nuevo elemento invocando método API de inserción
            sbyte result = InsertUser(login, pass, name, surname, email);
            

            // Mostrar mensaje:
            string script = "alert(\"Error desconocido\");";
            switch (result)
            {
                case 0:
                    script = "alert(\"Formato de datos incorrectos\");";
                    txtLogin.Text = string.Empty;
                    txtPass.Text = string.Empty;
                    txtName.Text = string.Empty;
                    txtSurname.Text = string.Empty;
                    txtMail.Text = string.Empty;
                    break;
                case 1:
                    txtLogin.Text = string.Empty;
                    txtPass.Text = string.Empty;
                    txtName.Text = string.Empty;
                    txtSurname.Text = string.Empty;
                    txtMail.Text = string.Empty;
                    script = "alert(\"Usuario registrado correctamente\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
                    Response.Redirect("http://localhost:63664/WebForms/LoginForm.aspx");
                    break;
                case 2:
                    script = "alert(\"Introduzca una contraseña con al menos una mayúscula, una minúscula y un número. Debe contener al menos 10 caracteres\");";
                    txtPass.Text = string.Empty;
                    break;
                case 3:
                    script = "alert(\"Error, ese usuario/correo ya existe\");";
                    txtLogin.Text = string.Empty;
                    txtMail.Text = string.Empty;
                    break;
                default:
                    txtLogin.Text = string.Empty;
                    txtPass.Text = string.Empty;
                    txtName.Text = string.Empty;
                    txtSurname.Text = string.Empty;
                    txtMail.Text = string.Empty;
                    break;
            }

            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }
        private sbyte InsertUser(string login, string pass, string name, string surname, string email)
        {
            sbyte result = -1;
            string url = @"https://localhost:7204/api/User/InsertUser";
            var userData = new { user = new User(login,pass,name,surname,email) };
            string json = JsonConvert.SerializeObject(userData);

            HttpWebResponse httpResponse = null;

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                result = 1;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("409")) result = 2;
                else if (ex.Message.Contains("400")) result = 0;
                else if (ex.Message.Contains("404")) result = 3;
            }
            return result;
        }
    }
}