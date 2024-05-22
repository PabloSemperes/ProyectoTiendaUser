using API_NTT_SHOP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Configuration;

namespace NTT_Shop.WebForms
{
    public partial class LanguagePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["session-id"] != null)
            {
                LoadGridView();
            }
            else
            {
                Response.Redirect("http://localhost:63664/WebForms/LoginForm.aspx");
            }
        }

        public void LoadGridView()
        {
            //List<Language> exampleList = new List<Language>();
            
            GridViewEjemplo.DataSource = GetAllLanguages();
            GridViewEjemplo.DataBind();
        }

        protected void GridViewEjemplo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewEjemplo.PageIndex = e.NewPageIndex;
            LoadGridView();
        }

        protected void GridViewEjemplo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GridViewEjemplo.EditIndex = e.NewEditIndex;
            LoadGridView();
        }

        protected void GridViewEjemplo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Obtener objeto fila seleccionado para actualizar:
            GridViewRow objFila = this.GridViewEjemplo.Rows[e.RowIndex];

            //Obtener valores del objeto fila modificados en pantalla:
            HiddenField hddIdLanguage = (HiddenField)objFila.FindControl("hddIdLanguage");
            TextBox description = (TextBox)objFila.FindControl("txtDescription");
            TextBox iso = (TextBox)objFila.FindControl("txtISO");

            //En este punto, llamar a métodos de API/Base de datos para actualizar los datos.
            sbyte result = UpdateLanguage(int.Parse(hddIdLanguage.Value),description.Text,iso.Text);

            // Mostrar mensaje:
            string script = "alert(\"Error\");";
            switch (result)
            {
                case 0:
                    script = "alert(\"Formato de datos incorrectos\");";
                    break;
                case 1:
                    script = "alert(\"Actualizado Correctamente\");";
                    break;
                case 2:
                    script = "alert(\"ISO repetida\");";
                    break;
                default:
                    break;
            }
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);

            //Recargar Grid en modo no-edición
            this.GridViewEjemplo.EditIndex = -1;
            LoadGridView();
        }

        protected void GridViewEjemplo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.GridViewEjemplo.EditIndex = -1;
            GridViewRow objFila = this.GridViewEjemplo.Rows[e.RowIndex];
            HiddenField hddIdLanguage = (HiddenField)objFila.FindControl("hddIdLanguage");

            //Borrar invocando a método de api de borrado con el id:
            sbyte result = DeleteLanguage(int.Parse(hddIdLanguage.Value));

            // Mostrar mensaje:
            string script = "alert(\"Error\");";
            switch (result)
            {
                case 0:
                    script = "alert(\"Formato de datos incorrectos\");";
                    break;
                case 1:
                    script = "alert(\"Eliminado Correctamente\");";
                    //Refrescar Grid
                    LoadGridView();
                    break;
                case 2:
                    script = "alert(\"No existe ese lenguaje\");";
                    break;
                default:
                    break;
            }
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }

        protected void btnNewLanguage_Click(object sender, EventArgs e)
        {
            //Recoger valores formulario:
            string description = txtDescription.Text;
            string iso = txtISO.Text;

            //Crear nuevo elemento invocando método API de inserción
            sbyte result = InsertLanguage(description, iso);

            //Borrar campos
            txtDescription.Text = string.Empty;
            txtISO.Text = string.Empty;

            // Mostrar mensaje:
            string script = "alert(\"Error\");";
            switch (result)
            {
                case 0:
                    script = "alert(\"Formato de datos incorrectos\");";
                    break;
                case 1:
                    script = "alert(\"Insertado Correctamente\");";
                    break;
                case 2:
                    script = "alert(\"Ese ISO ya existe\");";
                    break;
                default:
                    break;
            }
            
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);

            //Refrescar Grid
            LoadGridView();
        }

        private List<Language> GetAllLanguages() 
        {
            List<Language> list = new List<Language>();

            string url = @"https://localhost:7204/api/Language/getAllLanguages";

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject jsonObject = JObject.Parse(result);
                    JArray jsonArray = jsonObject["languageList"].ToObject<JArray>();
                    list = jsonArray.ToObject<List<Language>>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return list;
        }
        private sbyte InsertLanguage(string description, string iso) 
        {
            sbyte result=-1;
            string url = @"https://localhost:7204/api/Language/insertLanguage";
            var languageData = new { language = new Language(1, description, iso) };
            string json = JsonConvert.SerializeObject(languageData);

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
            }
            return result;
        }
        private sbyte UpdateLanguage(int idLanguage, string description, string iso)
        {
            sbyte result = -1;
            string url = @"https://localhost:7204/api/Language/updateLanguage";
            var languageData = new { language = new Language(idLanguage, description, iso) };
            string json = JsonConvert.SerializeObject(languageData);

            HttpWebResponse httpResponse = null;

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
                if (ex.Message.Contains("409")) result = 2;
                else if (ex.Message.Contains("400")) result = 0;
            }
            return result;
        }
        private sbyte DeleteLanguage(int id) 
        {
            sbyte result = -1;
            string url = @"https://localhost:7204/api/Language/DeleteLanguage";
            var idData = new { id };
            string json = JsonConvert.SerializeObject(idData);

            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "DELETE";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                result = 1;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("409")) result = 2;
                else if (ex.Message.Contains("400")) result = 0;
            }
            return result;
        }
    }
}