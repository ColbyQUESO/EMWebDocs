using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;

namespace EM_WebDocs
{
    class EMDocs
    {
        bool OCR = false;
        string source = "";

        //List<EMDoc> collection = new List<EMDoc>();

        public EMDoc AddDoc(string file, string src, bool ocr)
        {
            OCR = ocr;
            source = src;

            EMDoc doc = new EMDoc();
            string text;

            doc.Filename = file;

            //Read the file and look for the info.
            if (OCR) //if OCRing read it all
            {
                text = GetTextFromPDF(file, -1);
                doc.OCRText = text;
            }
            else  //else read first page
            {
                text = GetTextFromPDF(file, 1);
            }

            if (text == null)
            {
                text = "";
            }

            //Identify most commonly used terms in document
            IEnumerable<Tuple<string, int>> terms = GetMostUsedWords(text);
            List<string> keywords = new List<string>();

            foreach (var t in terms)
            {
                //Get keywords from SQLite...need to build the SQLite method and table in file and finish the code below that compares.  Then put keywords most found in XML node
                List<string> kwListText = new List<string>();
                kwListText = SQLite.GetKeyWord("dbo.key_agency_ocrtext");

                foreach (var k in kwListText)
                {
                    if (t.Item1 == k //look in sqlite for terms to remove)
                    Console.WriteLine(t.Item1 + ", " + t.Item2);
                }


            }

            var matchID = Regex.Match(file, @"\\(\d+)_ _"); //match for docs from the website

            //emcity.org
            //GRAMA
            //Other

            if (source != "emcity.org")
            {
                doc.Source = source;
                matchID = Regex.Match(file, @"(\\)(?!.*\\)(.*)\.");
                doc.Id = SQLite.InsertID("dbo." + source.ToLower(), "G", Path.GetFileName(file));
            }
            else //if it is from the website
            {
                doc.Id = matchID.Groups[1].Value;
            }

            doc.Date = GetDate(Path.GetFileName(file), text);
            doc.Agency = GetAgency(file, text);
            doc.Type = GetDocType(file, text);
            doc.Title = GetTitle(file, text);

            doc.Extension = Path.GetExtension(file);

            //Try to fill in the agency based on the doctype or the title since it might have changed from reading the file
            if (doc.Agency == "")
            {
                if (doc.Type == "Agreement" || doc.Type == "Resolution")
                {
                    doc.Agency = "City Council";
                }

                if (doc.Agency == "")
                {
                    doc.Agency = GetAgency(doc.Title, "");
                }
            }

            //if you have the right fields filled out, look for more info
            if (doc.Agency != "" && doc.Type != "" && text != null)
            {
                if (!OCR) //if not OCRing the whole doc, a bit more is needed to find council and staff members
                {
                    text = GetTextFromPDF(file, 2);
                }

                if (text != null)
                {
                    doc.Officials = GetOfficials(doc.Agency, doc.Type, text);
                    doc.Staff = GetStaff(doc.Agency, doc.Type, text);
                    doc.TypeDet = GetTypeDet(doc.Agency, doc.Type, text);

                    if (doc.Agency == "City Council" && doc.Type == "Minutes")
                    {
                        doc.Votes = GetVotes(doc.Agency, doc.Type, text);
                    }
                }
            }

            if (doc.Project == "")
            {
                if (text == null)
                {
                    text = "";
                }

                doc.Project = GetProject(file, text);

                if (doc.Project == "")
                {
                    doc.Project = GetProject(doc.Agency, doc.Type, file, text);
                }

            }

            //Console.WriteLine(text);
            Console.WriteLine(doc.Id);
                     
            //collection.Add(doc);
            return doc;
        }

        //public List<EMDoc> GetCollection()
        //{
        //    return collection;
        //}

        private string GetAgency(string file, string text)
        {
            string agency = "";

            List<Tuple<int, string, string>> agListText = new List<Tuple<int, string, string>>();
            agListText = SQLite.GetAgency("dbo.key_agency_ocrtext");

            foreach (var a in agListText)
            {
                Match projMatch = Regex.Match(text, a.Item2); //Keyword

                if (projMatch.Success)
                {
                    agency = a.Item3; //Agency
                    break;
                }
            }

            if (agency == "")
            {
                List<Tuple<int, string, string>> agListFile = new List<Tuple<int, string, string>>();
                agListFile = SQLite.GetAgency("dbo.key_agency_filename");

                foreach (var a in agListFile)
                {
                    if (file.ToLower().Contains(a.Item2))
                    {
                        agency = a.Item3; //Agency
                        break;
                    }
                }
            }       

            return agency;
        }

        private IEnumerable<Tuple<string, int>> GetMostUsedWords(string text)
        {
            var toIgnore = new List<string> { "the", "of", "and", "to", "a", "-" };

            var orderedWords = text
              .Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries)
              .GroupBy(x => x.ToLower())
              .Select(x => Tuple.Create(x.Key.ToLower(), x.Count()))
              .OrderByDescending(x => x.Item2)
              .Take(100);      

            return orderedWords;
        }

        private DateTime GetDate(string file, string text)
        {
            string docDate = "";
            DateTime dateTime = new DateTime();// = DateTime.Today;

            //Check within file
            if (docDate == "")
            {
                Match date = Regex.Match(text, @"(?i)(January|February|March|April|May|June|July|August|September|October|November|December)\s*(\d{1,2}\w*\s?,?\s{0,5}\d{2,4})|(?i)([r|o][-.]\d{1,2}[.-][19|20]\d{2,3})");

                if (date.Groups[0].Value != "")
                {
                    Match textInDate = Regex.Match(date.Groups[0].Value, @"(?i)(January|February|March|April|May|June|July|August|September|October|November|December)\s*(\s\d{1,2}\w{2}\s?,?\s{0,5}\d{2,4})");

                    if (textInDate.Success)
                    {
                        Match removeTextInDate = Regex.Match(date.Groups[0].Value, @"(?i)(January|February|March|April|May|June|July|August|September|October|November|December)\s*(\s\d{1,2})\w{2}(\s?,?\s{0,5}\d{2,4})");
                        string temp = removeTextInDate.Groups[1].Value + removeTextInDate.Groups[2].Value + removeTextInDate.Groups[3].Value;

                        docDate = temp;
                    }
                    else
                    {
                        docDate = date.Groups[0].Value;
                    }
                }

                if (docDate == "")
                {
                    //Check filenames for date
                    docDate = ParseDate(file);
                }

                try
                {
                    if (docDate.Contains("R-") || docDate.Contains("O-") || docDate.Contains("January|February|March|April|May|June|July|August|September|October|November|December"))
                    {
                        docDate = ParseDate(file);
                    }

                    dateTime = Convert.ToDateTime(docDate);
                }
                catch (Exception e)
                {
                    Log.AddMessage(e.ToString(), "Error");
                    Console.WriteLine(e.ToString() + " : " + e.InnerException);
                    //dateTime = DateTime.Today;
                }
            }
            else
            {
                dateTime = Convert.ToDateTime(docDate);
            }

            if (dateTime < DateTime.Now)
            {
                return dateTime;
            }
            else
            {
                return new DateTime();
            }
        }

        private string GetDocType(string file, string text)
        {
            string docType = "";

            List<Tuple<int, string, string>> docTypeListFile = new List<Tuple<int, string, string>>();
            docTypeListFile = SQLite.GetDocType("dbo.key_doctype_filename");

            foreach (var p in docTypeListFile)
            {
                Match docMatch = Regex.Match(file.ToLower(), p.Item2); //Keyword

                if (docMatch.Success)
                {
                    docType = p.Item3;
                    break; //DocType
                }
            }

            if (docType == "")
            {
                var mResolution = Regex.Match(file.ToLower(), @"r[-.]\d{1,2}[.-][19|20]\d{2,3}");
                var mOrdinance = Regex.Match(file.ToLower(), @"o[-.]\d{1,2}[.-][19|20]\d{2,3}");

                if (mResolution.Success)
                {
                    docType = "Resolution";
                }
                else if (mOrdinance.Success)//Groups.Count > 0)
                {
                    docType = "Ordinance";
                }
            }

            if (docType == "")
            {
                List<Tuple<int, string, string>> docTypeListText = new List<Tuple<int, string, string>>();
                docTypeListText = SQLite.GetDocType("dbo.key_doctype_ocrtext");

                foreach (var p in docTypeListText)
                {
                    Match docMatch = Regex.Match(text, p.Item2); //Keyword

                    if (docMatch.Success)
                    {
                        docType = p.Item3;
                        break; //DocType
                    }
                }
            }


            ////Check within file if not found elsewhere
            //if (docType == "")
            //{
            //    //Check within the document
            //    Match typePacket = Regex.Match(text, @"(?i)Proclamation|Memorandum|CITY\s+COUNCIL\s+MEETING|Planning Commission Staff Report|City Council Staff Report");

            //    //Check if packet
            //    if (typePacket.Success)
            //    {
            //        docType = "Packet";                   
            //    }
            //}

            return docType;
        }

        private string GetStaff(string agency, string type, string text)
        {
            string docStaff = "";

            Match staff = Regex.Match(text, @"(?i)STAFF PRESENT:\s+((?:.*\n[^\s])+?[^\n]*).*\n.*called the meeting|(?i)STAFF PRESENT:\s+((?:.*\n[^\s])+?[^\n]*).*\n.*CALL TO ORDER");
            docStaff = staff.Groups[1].Value; //Assign first match

            if (staff.Groups[1].Value == "") 
            {
                docStaff = staff.Groups[2].Value; //Assign second match, if first is empty
            }

            string replace = Regex.Replace(docStaff, @"\t|\n|\r|\x0d|\x0a", " ").Trim(' ');

            if (replace.Trim(' ').Trim('.').Trim('_') != "")
            {
                string rep = replace.Trim(' ').Trim('.').Trim('_');

                docStaff = rep;
            }

            return docStaff;
        }

        private string GetOfficials(string agency, string type, string text)
        {
            string docOfficials = "";
            List<Tuple<string, string, string, string, string, string>> list = new List<Tuple<string, string, string, string, string, string>>();

            if (agency == "City Council")
            {
                if (type == "Minutes" | type == "Packet")
                {
                    //THIS MAY WORK BETTER - (?i)ELECTED OFFICIALS PRESENT:\s*(.*?)(\n+)?CITY STAFF|(?i)City Council PRESENT:\s*(.*?)(\n+)?CITY STAFF
                    Match officials = Regex.Match(text, @"(?i)ELECTED.*PRESENT:\s+((?:.*\n[^\s])+?[^\n]*)\s*CITY.{1,3}STAFF|(?i)City Council Present:\s+((?:.*\n[^\s])+?[^\n]*)\s*(?:Town|City) staff:|(?i)Council Members present:\s+((?:.*\n[^\s])+?[^\n]*)\s*(?:Town|City) staff:");

                    List<Tuple<string, string, string>> docTypeListText = new List<Tuple<string, string, string>>();
                    docTypeListText = SQLite.GetOfficials("dbo.key_officials_ocrtext");

                    foreach (var p in docTypeListText)
                    {
                        Match docMatch = Regex.Match(officials.Groups[1].Value, p.Item2);

                        if (!docMatch.Success)
                        {
                            //Try second regex match
                            docMatch = Regex.Match(officials.Groups[2].Value, p.Item2);//Keyword
                        }

                        if (!docMatch.Success)
                        {
                            //Try third regex match
                            docMatch = Regex.Match(officials.Groups[3].Value, p.Item2);//Keyword
                        }

                        if (docMatch.Success)
                        {
                            if (!docOfficials.Contains(p.Item3))
                            {
                                docOfficials += p.Item3 + "; "; //Add each official
                            }
                        }
                    }

                    string replace = Regex.Replace(docOfficials, @"\t|\n|\r|\x0d|\x0a", " ").Trim(' ');

                    if (replace.Trim(' ').Trim('.').Trim('_') != "")
                    {
                        string rep = Regex.Replace(replace, "(?i)Councilmembers|Councilmember|Council members|Mayor|Boardmembers|Board members|Pro tempore", "").Trim(' ');

                        docOfficials = rep.Trim(' ').Trim('.').Trim('_');
                    }
                }
            }

            docOfficials = docOfficials.TrimEnd(';');
            return docOfficials;
        }

        //Get project main option 1
        private string GetProject(string file, string text)
        {
            string docProj = "";

            List<Tuple<int, string, string, string, string, string, string>> projList = new List<Tuple<int, string, string, string, string, string, string>>();
            projList = SQLite.GetProj("dbo.key_projects_filename");

            foreach (var p in projList)
            {
                Match projMatch = Regex.Match(file, p.Item2); //Keyword

                if (projMatch.Success)
                {
                    docProj = p.Item3;
                    break;//Name
                }
            }

            return docProj;
        }

        //Get project backup option 2
        private string GetProject(string agency, string type, string file, string text)
        {
            string docProj = "";

            if (agency == "City Council")
            {
                if (type == "Packet")
                {
                    Match project = Regex.Match(text, "(?i)Project:([^\\n]*)\\s+Applicant:");
                    string tempProj = project.Groups[1].Value.TrimEnd(' ').TrimEnd('.').TrimEnd('_').TrimStart(' ').TrimStart('.').TrimStart('_');

                    if (tempProj != "")
                    {
                        List<Tuple<int, string, string, string, string, string, string>> projList = new List<Tuple<int, string, string, string, string, string, string>>();
                        projList = SQLite.GetProj("dbo.key_projects_filename");

                        foreach (var p in projList)
                        {
                            Match projMatch = Regex.Match(tempProj, p.Item2); //Keyword

                            if (projMatch.Success)
                            {
                                docProj = p.Item3;
                                break;//Name
                            }
                            else
                            {
                                docProj = tempProj;
                            }
                        }

                        //docProj = project.Groups[1].Value.TrimEnd(' ').TrimEnd('.').TrimEnd('_').TrimStart(' ').TrimStart('.').TrimStart('_');
                    }
                }
            }

            return docProj;
        }

        private string GetTextFromPDF(string path, int pagesToReturn)
        {
            try
            {
                var strategy = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();//LocationTextExtractionStrategy();

                StringBuilder text = new StringBuilder();
                using (PdfReader reader = new PdfReader(path))
                {
                    if (pagesToReturn >= reader.NumberOfPages || pagesToReturn == -1)
                    {
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            text.Append(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i, strategy));
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= pagesToReturn; i++)
                        {
                            text.Append(iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i, strategy));
                        }
                    }
                }

                return text.ToString();
            }
            catch (Exception e)
            {
                Log.AddMessage(e.ToString(), "Error");
                Console.WriteLine(e.ToString() + " : " + e.InnerException);
                return null;
            }

        }

        private string GetTitle(string file, string text)
        {
            string docTitle = "";

            //add filename doc title to the title
            var matchTitle = Regex.Match(file, @".*_ _(.*)_ _.*\..*");

            docTitle = matchTitle.Groups[1].Value;

            if (docTitle == "")
            {
                //TITLE
                Match title = Regex.Match(text, @"(?i)TITLE: ((?:.*\n[^\s])+[^\n]*)|(?i)EAGLE MOUNTAIN CITY([^\n]*\s+[^\n]*\s+)(?:January|February|March|April|May|June|July|August|September|October|November|December)\s*(?:\s\d{1,2}[\w{2}]*\s?,?\s{0,5}\d{2,4})");

                if (title.Success)
                {
                    if (title.Groups[2].Value.Trim(' ').Trim('.').Trim('_') != "")
                    {
                        //Grab the exact title if possible
                        docTitle = title.Groups[2].Value.Trim(' ').Trim('.').Trim('_');
                        docTitle = Regex.Replace(docTitle, @"\t|\n|\r", "").Trim(' ');
                    }
                    else
                    {
                        //Grab the backup title if that won't do
                        docTitle = title.Groups[0].Value.Trim(' ').Trim('.').Trim('_');
                        docTitle = Regex.Replace(docTitle, @"\t|\n|\r", "").Trim(' ');
                    }
                }
            }

            if (docTitle == "" && source != "emcity.org") //read other filenames
            {
                if (source == "Resolution")
                {
                    var match = Regex.Match(file, @"R-\d{2}-\d{4}--(.*)\.\w{3,4}");
                    docTitle = match.Groups[1].Value;
                }
                else
                {
                    var match = Regex.Match(file, @"(\\)(?!.*\\)(.*)\.\w{3,4}");
                    docTitle = match.Groups[2].Value;
                }


            }

            return docTitle;
        }

        private string GetTypeDet(string agency, string type, string text)
        {
            string typeDet = "";

            if (agency != "" & type != "" & (text != null & text != ""))
            {
                List<string> typeDetList = new List<string>();
                typeDetList = SQLite.GetTypeDet("dbo.key_typedet_ocrtext", agency, type);

                foreach (var str in typeDetList)
                {
                    MatchCollection typeDetReg = Regex.Matches(text, str);

                    foreach (Match m in typeDetReg)
                    {
                        if (m.Groups[1].Value.TrimEnd(' ').TrimEnd('.').TrimEnd('_').TrimStart(' ').TrimStart('.').TrimStart('_') != "")
                        {
                            string replace = Regex.Replace(m.Groups[1].Value, @"\t |\t|\n|\r|\x0d|\x0a", "").Trim(' ');
                            typeDet += replace.Trim(' ').Trim('.').Trim('_') + "; ";
                        }
                    }
                }
            }

            return typeDet.Trim(' ').Trim(';');
        }

        private string GetVotes(string agency, string type, string text)
        {
            string docVotes = "";

            if (agency != "" & type != "" & (text != null & text != ""))
            {
                List<string> docVotesList = new List<string>();
                docVotesList = SQLite.GetVotes("dbo.key_votes_ocrtext", agency, type);

                foreach (var str in docVotesList)
                {
                    MatchCollection docVotesReg = Regex.Matches(text, str);

                    foreach (Match m in docVotesReg)
                    {
                        if (m.Groups[1].Value.TrimEnd(' ').TrimEnd('.').TrimEnd('_').TrimStart(' ').TrimStart('.').TrimStart('_') != "")
                        {
                            string replace = Regex.Replace(m.Groups[1].Value, @"\t |\t|\n|\r|\x0d|\x0a", " ").Trim(' ');
                            docVotes += replace.Trim(' ').Trim('.').Trim('_') + "; ";
                        }
                    }
                }
            }         

            return docVotes.Trim(' ').Trim(';');
        }

        private string ParseDate(string path)
        {
            var matchYear = Regex.Match(path, @"(\d{1,2}[-.]\d{1,2}[-.][19|20]\d{2,4})|[R|r|O|o][-.]\d{1,2}[.-]([19|20]\d{2,3})|([19|20]\d{2,3})[-]?\d{2}|(\d{1,2}-\d{1,2}-\d{2})");

            int result = 0;
            Int32.TryParse(matchYear.Groups[0].Value, out result);

            //if six digit date
            if (matchYear.Groups[0].Value.Length == 6 && result != 0)
            {
                //082415 //(\d{4}20\d{2})|(20\d{2}\d{4})|(\d{4}19\d{2})|(19\d{2}\d{4})
                var str = matchYear.Groups[0].Value.SplitBy(2);

                var mYearFirst = Regex.Match(matchYear.Groups[0].Value, @"");
                var mYearLast = Regex.Match(matchYear.Groups[0].Value, @"");

                int intS = 0;
                int int0 = 0;
                int int1 = 0;
                int int2 = 0;
                string final = "";

                foreach (string s in str)
                {
                    if (intS == 0)
                    {
                        int0 = Int32.Parse(s);
                    }
                    else if (intS == 1)
                    {
                        int1 = Int32.Parse(s);
                    }
                    else if (intS == 2)
                    {
                        int2 = Int32.Parse(s);
                    }

                    intS++;
                }

                if ((int0 > 0 && int0 <= 12) && (int2 > 12))
                {
                    //Assume year is last
                    final = int0 + "-" + int1 + "-" + (int2 >= 96 ? "19" + int2 : "20" + int2);
                }
                else if (int2 > 12)
                {
                    //Year is last
                    final = int0 + "-" + int1 + "-" + (int2 >= 96 ? "19" + int2 : "20" + int2);
                }
                else if ((int0 > 0 && int0 <= 12))
                {
                    //Year is first
                    final = (int0 >= 96 ? "19" + int2 : "20" + int2) + "-" + int2 + "-" + int1;
                }

                return final;
            }
            //if secondary a normal date
            else if (matchYear.Groups[1].Value != "")
            {
                return matchYear.Groups[1].Value; 
            }
            //if ordinance or resolution
            else if (matchYear.Groups[2].Value != "")
            {
                return "12/31/" + matchYear.Groups[2].Value;
            }
            //if date has year first and no day
            else if (matchYear.Groups[3].Value != "")
            {
                return "12/31/" + matchYear.Groups[3].Value;
            }
            //if date has year last but it's only 2 digits
            else if (matchYear.Groups[4].Value != "")
            {
                return matchYear.Groups[4].Value;
            }
            else if (matchYear.Groups[0].Value != "")
            {
                return matchYear.Groups[0].Value;
            }
            else
            {
                //check filename for year only as a last resort
                var matchYr = Regex.Match(path, @"_([19|20]\d{2,3})_");

                if (!matchYr.Success)
                {
                    matchYr = Regex.Match(path, @"_([19|20]\d{2,3}) ");
                }

                if (!matchYr.Success)
                {
                    matchYr = Regex.Match(path, @" ([19|20]\d{2,3})_");
                }

                return "12/31/" + matchYr.Groups[1].Value;
            }
        }

        //public int CollectionCount()
        //{
        //    return collection.Count;
        //}

    }
}
