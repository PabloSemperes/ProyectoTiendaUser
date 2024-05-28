using API_NTT_SHOP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnNewLogin_Click(object sender, EventArgs e)
        {
            //Recoger valores formulario:
            string login = txtLogin.Text;
            string pass = txtPass.Text;

            //Crear nuevo elemento invocando método API de inserción
            sbyte result = UserLogin(login, pass);

            //Borrar campos
            txtLogin.Text = string.Empty;
            txtPass.Text = string.Empty;

            // Mostrar mensaje:
            string script = "alert(\"Error\");";
            switch (result)
            {
                case 0:
                    script = "alert(\"Formato de datos incorrectos\");";
                    break;
                case 1:
                    script = "alert(\"Sesión iniciada correctamente\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
                    Response.Redirect("http://localhost:63664/WebForms/ProductsPage.aspx");
                    break;
                case 2:
                    script = "alert(\"Usuario/contraseña incorrecto\");";
                    break;
                default:
                    break;
            }

            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }
        private sbyte UserLogin(string login, string password)
        {
            sbyte result = -1;
            string url = @"https://localhost:7204/api/User/userLogin";
            var userData = new { user = new User(login, password, "a", "a", "a","a") };
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

                User user = GetUser(login);

                if (user != null && user.PkUser > 0)
                {
                    Session["session-id"] = user.PkUser;
                    Session["session-language"] = user.Language;
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("409")) result = 2;
                else if (ex.Message.Contains("400")) result = 0;
            }
            return result;
        }
        private User GetUser(string login) 
        {
            string url = @"https://localhost:7204/api/User/getAllUsers";
            User user = null;
            List<User> users = new List<User>();

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject jsonObject = JObject.Parse(result);
                    JArray jsonArray = jsonObject["usersList"].ToObject<JArray>();
                    users = jsonArray.ToObject<List<User>>();
                }

                int count = 0; bool finish = false;
                while (count < users.Count && !finish)
                {
                    if (users[count].Login == login) 
                    {
                        user = users[count];
                        finish = true;
                    }
                    count++;
                }
            }
            catch (Exception ex)
            {

            }
            return user;
        }
    }
}