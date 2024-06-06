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
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null) 
            {
                if (!IsPostBack )
                {
                    int sesion = int.Parse(Session["session-id"].ToString());
                    User user = GetUser(sesion);
                    txtLogin.Text = user.Login;
                    txtName.Text = user.Name;
                    txtSurname.Text = user.Surname1;
                    txtSurname2.Text = user.Surname2;
                    txtAdress.Text = user.Adress;
                    txtProvince.Text = user.Province;
                    txtCity.Text = user.Town;
                    txtPhone.Text = user.Phone;
                    txtPostalCode.Text = user.PostalCode;
                    txtMail.Text = user.Email;
                    labelPhone.Visible = false;
                    labelPostalCode.Visible = false;
                }
                List<Language> languages = GetAllLanguage();
                foreach (Language language in languages)
                {
                    ListItem item = new ListItem(language.description, language.iso);
                    cboxLanguage.Items.Add(item);
                }
            }
            else Response.Redirect("http://localhost:63664/WebForms/LoginForm.aspx");
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            sbyte result = UpdateUser();

            string script = "alert(\"Error\");";
            switch (result)
            {
                case 0:
                    script = "alert(\"Formato de datos incorrectos\");";
                    break;
                case 1:
                    script = "alert(\"Datos actualizados\");";
                    labelPostalCode.Visible = false;
                    labelPhone.Visible = false;
                    break;
                case 2:
                    script = "alert(\"Conflicto\");";
                    break;
                default:
                    break;
            }
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);

            Page_Load(sender, e);
        }

        private User GetUser(int id)
        {
            string url = @"https://localhost:7204/api/User/getUser/";
            url = url + id;
            User user = null;

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject jsonObject = JObject.Parse(result);
                    user = jsonObject["user"].ToObject<User>();
                }
            }
            catch (Exception ex)
            {

            }
            return user;
        }
        private sbyte UpdateUser()
        {
            sbyte result = -1;
            string url = @"https://localhost:7204/api/User/UpdateUser";
            int sesion = int.Parse(Session["session-id"].ToString());
            User userNew = GetUser(sesion);
            userNew.Name = txtName.Text;
            userNew.Surname1 = txtSurname.Text;
            userNew.Surname2 = txtSurname2.Text;
            userNew.Adress = txtAdress.Text;
            userNew.Province = txtProvince.Text;
            userNew.Town = txtCity.Text;
            userNew.PostalCode = txtPostalCode.Text;
            userNew.Phone = txtPhone.Text;
            string language = cboxLanguage.SelectedValue;
            userNew.Language = language;
            var userData = new { user = userNew };
            string json = JsonConvert.SerializeObject(userData);

            HttpWebResponse httpResponse = null;

            if (userNew.PostalCode != null)
            {
                if (!userNew.PostalCode.All(char.IsDigit)) labelPostalCode.Visible = true;
                else labelPostalCode.Visible = false;
            }
            if (userNew.Phone != null)
            {
                if (!userNew.Phone.All(char.IsDigit)) labelPhone.Visible = true;
                else labelPhone.Visible = false;
            }

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";

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
                if (ex.Message.Contains("404")) result = 2;
                else if (ex.Message.Contains("400")) result = 0;
            }
            return result;
        }
        public List<Language> GetAllLanguage()
        {
            string baseUrl = "https://localhost:7204/api/";
            List<Language> languages = new List<Language>();

            try
            {
                string url = baseUrl + "Language/getAllLanguages";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var json = JObject.Parse(result);
                    var languageArray = json["languageList"].ToObject<JArray>();
                    languages = languageArray.ToObject<List<Language>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los idiomas: {ex.Message}");
            }

            return languages;
        }

        protected void btnCambiarC_Click(object sender, EventArgs e)
        {
            Response.Redirect("CambiarContrasenya.aspx");
        }
    }
}