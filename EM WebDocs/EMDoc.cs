using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM_WebDocs
{
    class EMDoc
    {
        private string id = "-1";
        private string title = "";
        private string agency = "";
        private string type = "";
        private string typedet = "";
        private string officials = "";
        private string staff = "";
        private string proj = "";
        private string filename = "";
        private string ext = "";
        private string src = "emcity.org";
        private string ocrtext = "";
        private string votes = "";
        private DateTime date;

        public string Agency
        {
            get
            {
                return agency;
            }
            set
            {
                agency = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public string Extension
        {
            get
            {
                return ext;
            }
            set
            {
                ext = value;
            }
        }

        public string Filename
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string OCRText
        {
            get
            {
                return ocrtext;
            }
            set
            {
                ocrtext = value;
            }
        }

        public string Officials
        {
            get
            {
                return officials;
            }
            set
            {
                officials = value;
            }
        }

        public string Project
        {
            get
            {
                return proj;
            }
            set
            {
                proj = value;
            }
        }

        public string Source
        {
            get
            {
                return src;
            }
            set
            {
                src = value;
            }
        }

        public string Staff
        {
            get
            {
                return staff;
            }
            set
            {
                staff = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public string TypeDet
        {
            get
            {
                return typedet;
            }
            set
            {
                typedet = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public string Votes
        {
            get
            {
                return votes;
            }
            set
            {
                votes = value;
            }
        }
    }
}
