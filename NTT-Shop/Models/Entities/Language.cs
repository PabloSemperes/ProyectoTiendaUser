using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_NTT_SHOP.Models
{
    public class Language
    {
        public int idLanguage { get; set; }
        public string description { get; set; }
        public string iso { get; set; }

        public Language() { }

        public Language(int id, string desc, string iso) 
        {
            this.idLanguage = id;
            this.description = desc;
            this.iso = iso;
        }
    }
}
