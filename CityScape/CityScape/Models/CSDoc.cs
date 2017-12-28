using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CityScape.Models
{
    public class CSDoc
    {
        [Key]
        public int DocId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Name { get; set; } //formerly title
        [Required]
        public int Agency { get; set; }
        [Required]
        public int DocType { get; set; }

        public string Project { get; set; }
        public string Source { get; set; }
        public List<string> Keywords { get; set; }
        [Required]

        public string Filename { get; set; }
        public string Extension { get; set; }

        //public string OCRText { get; set; }
        //public string Officials { get; set; }
        //public string Staff { get; set; }
        //public string TypeDet { get; set; }
        //public string Votes { get; set; }

        public virtual CSAgency CSAgency { get; set; }
        public virtual CSDocType CSDocType { get; set; }
    }
}