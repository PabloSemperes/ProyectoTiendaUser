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
using System.Xml.Linq;

namespace NTT_Shop.WebForms
{
    public partial class CambiarContrasenya : System.Web.UI.Page
    {
        private string generalUrl = "https://localhost:7204/api/";

        private void MostrarAlerts(bool error, bool correcto)
        {
            if (!correcto)
            {
                lblCorrecto.Text = string.Empty;
            }
            if (!error)
            {
                lblError.Text = string.Empty;
            }
            lblCorrecto.Enabled = correcto;
            lblError.Enabled = error;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["session-id"] != null)
            {
                MostrarAlerts(false, false);
                MostrarDatos();
            }
            else
            {
                Response.Redirect("LoginForm.aspx");
            }
        }

        private void MostrarDatos()
        {
            User usuario = GetUsuario();
            txtUser.Text = usuario.Login.ToString();
        }

        private User GetUsuario()
        {
            User usuario = new User();
            string url = generalUrl + "User/getUser/" + Session["session-id"].ToString();
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var resultado = streamReader.ReadToEnd();
                    var json = JObject.Parse(resultado);
                    usuario = json["user"].ToObject<User>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                usuario = null;
            }
            return usuario;
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {

            bool correcto = Cambiar();
            if (correcto)
            {
                MostrarAlerts(false, true);
                lblCorrecto.Text = "Se ha actualizado correctamente la contraseña";
            }
            else
            {
                MostrarAlerts(true, false);
                lblError.Text = "La contraseña no puede ser la misma que la que tenías antes.";
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx");
        }

        private bool Cambiar()
        {
            bool correcto = false;
            try
            {
                User usuario = GetUsuario();
                usuario.Password = txtContrasenya.Text;
                string url = generalUrl + "User/UpdateUserPassword";
                var userData = new { user = usuario };
                string json = JsonConvert.SerializeObject(userData);

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";

                httpRequest.ContentType = "application/json";
                httpRequest.Accept = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                HttpStatusCode httpStatus = httpResponse.StatusCode;

                if (httpStatus == HttpStatusCode.OK)
                {
                    correcto = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                correcto = false;
            }
            return correcto;
        }
    }
}