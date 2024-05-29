using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTT_Shop.Models.Entities
{
    public class User
    {
        public int PkUser { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname1 { get; set; }

        public string Surname2 { get; set; }

        public string Adress { get; set; }

        public string Province { get; set; }

        public string Town { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Language { get; set; }

        public int Rate { get; set; }

        public User() { }
        public User(string login, string pass, string name, string surname, string mail, string language)
        {
            this.PkUser=0; this.Login=login; this.Password=pass; this.Name=name; this.Surname1=surname; this.Surname2="Apellido"; this.Adress="Direccion"; this.Province="Provincia"; this.Town="Ciudad"; this.Phone="000000000000"; this.PostalCode="00000"; this.Email=mail; this.Language=language; this.Rate=1;
        }
    }
}