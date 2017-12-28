using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CityScape.Models
{
    public class CSDocType
    {
        [Key]
        public int DocType { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CSDoc> Docs { get; set; }
    }
}