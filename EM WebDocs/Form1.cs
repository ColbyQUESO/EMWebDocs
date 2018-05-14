using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Web;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace EM_WebDocs
{
    public partial class Form1 : Form
    {
        int notExist = 0;
        int dl = 0;
        int failed = 0;
        private static string SQL_PATH = Directory.GetParent(Application.CommonAppDataPath).ToString() + "\\EMDocsDB.sqlite";
        private static string ID_PATH = Directory.GetParent(Application.CommonAppDataPath).ToString() + "\\EMDocsDBID.sqlite";
        string sSource = "";
        string sProj = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Setup UI
            loadingPanel.Visible = false;
            loadingPanel.Enabled = false;
            loadingPanel.BringToFront();
            loadingPanel.Location = new Point((this.Width - loadingPanel.Width) / 2, (this.Height - loadingPanel.Height) / 2);
            dateTimePicker1.Value = DateTime.Today;

            try
            {
                //copy sqlite file to programdata
                File.WriteAllBytes(SQL_PATH, Properties.Resources.EMDocsDB);

                if (!File.Exists(ID_PATH))
                {
                    File.WriteAllBytes(ID_PATH, Properties.Resources.EMDocsDBID);
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(ex.ToString(), "Error");
            }
        }

        #region Download files from website

        private void btnGo_Click(object sender, EventArgs e)
        {
            UpdateStatusLabel("Downloading...");

            loadingPanel.Visible = true;
            loadingPanel.Enabled = true;

            tbWriteOut.Clear();
            tbWriteOut.AppendText(DateTime.Now.ToLongTimeString() + " Beginning Download...\n");
            Log.AddMessage("Beginning Download...", "Information");

            if (BGW_DownloadFile.IsBusy != true)
            {
                BGW_DownloadFile.RunWorkerAsync();
            }
        }

        private void BGW_DownloadFile_DoWork(object sender, DoWorkEventArgs e)
        {
            notExist = 0;
            dl = 0;
            failed = 0;

            if (!Directory.Exists(tbOutputFolder.Text))
            {
                Directory.CreateDirectory(tbOutputFolder.Text);
            }

            int id = Int32.Parse(tbIDLow.Text);

            while (id <= Int32.Parse(tbIDHigh.Text))
            {
                if (cbDocs.Checked)
                {
                    bool success = DownloadFileFromURL(tbURL.Text + id, id);
                }

                if (cbImgs.Checked)
                {
                    bool success = DownloadFileFromURL(tbIMG.Text + id, id);
                }

                id++;
            }

            if (id == Int32.Parse(tbIDHigh.Text))
            {
                tbWriteOut.AppendText("");
            }
        }

        private bool DownloadFileFromURL(string URL, int id)
        {
            string urlAddress = URL;

            try
            {
                UpdateStatusLabel("Contacting host: " + URL);
                Log.AddMessage("Contacting host: " + URL, "Information");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null || response.CharacterSet == "")
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    // Get the headers associated with the response.
                    WebHeaderCollection myWebHeaderCollection = response.Headers;

                    string mime = "";
                    string name = "";
                    string filename = "";

                    for (int i = 0; i < myWebHeaderCollection.Count; i++)
                    {
                        String header = myWebHeaderCollection.GetKey(i);
                        String[] values = myWebHeaderCollection.GetValues(header);

                        if (header == "Content-Type")
                        {
                            mime = values[0];
                        }

                        if (header == "Content-Disposition")
                        {
                            //&quot;
                            string pattern = "(?:.*filename=\")(.*)\"";
                            Match match = Regex.Match(values[0], pattern);
                            filename = match.Groups[1].Value;
                        }

                        if (header == "Content-DocumentTitle")
                        {
                            name = values[0];
                        }
                    }

                    if (filename == "")
                    {
                        filename = GetDefaultExtension(mime);
                    }

                    using (WebClient webClient = new WebClient())
                    {
                        UpdateStatusLabel("Downloading file: " + URL);
                        Log.AddMessage("Downloading file: " + URL, "Information");
                        webClient.DownloadFile(URL, tbOutputFolder.Text + "\\" + id + "_ _" + name + "_ _" + filename);// + "_" + filename);

                        this.Invoke(new System.Action(() =>
                        {
                            tbWriteOut.AppendText(DateTime.Now.ToLongTimeString() + " " + id + " Downloading: " + name + "_ _" + "_ _" + filename + "\n");
                        }));
                    }

                    response.Close();
                    readStream.Close();

                    dl++;

                    this.Invoke(new System.Action(() =>
                    {
                        // Running on the UI thread
                        lblDL.Text = dl.ToString();
                    }));

                    UpdateStatusLabel("Download complete: " + URL);
                    Log.AddMessage("Download complete: " + URL, "Information");

                    return true;
                }
                else
                {
                    UpdateStatusLabel("Download unsuccesful: " + URL);
                    Log.AddMessage("Download unsuccesful: " + URL, "Error");
                    failed++;

                    this.Invoke(new System.Action(() =>
                    {
                        // Running on the UI thread
                        lblFailed.Text = failed.ToString();
                        tbWriteOut.AppendText(DateTime.Now.ToLongTimeString() + " Failed: !HTTPStatusCode.OK for " + URL + "\n");
                        Log.AddMessage("Failed: !HTTPStatusCode.OK for " + URL, "Error");
                    }));

                    return false;
                }
            }
            catch (WebException eW)
            {
                if (eW.Message == "The remote server returned an error: (404) Not Found.")
                {
                    notExist++;

                    this.Invoke(new System.Action(() =>
                    {
                        // Running on the UI thread
                        lblExist.Text = notExist.ToString();
                        Log.AddMessage("Does Not Exist: " + URL, "Error");
                        //tbWriteOut.AppendText(DateTime.Now.ToLongTimeString() + "Does Not Exist: " + URL + "\n");
                    }));
                }
                else
                {
                    failed++;

                    this.Invoke(new System.Action(() =>
                    {
                        // Running on the UI thread
                        lblFailed.Text = failed.ToString();
                        tbWriteOut.AppendText(DateTime.Now.ToLongTimeString() + " Failed: " + URL + " " + eW.Message + "\n");
                        Log.AddMessage("Failed: " + URL + " " + eW.Message, "Error");
                    }));
                }

                return false;
            }
            catch (Exception e)
            {
                failed++;

                this.Invoke((MethodInvoker)delegate {
                    // Running on the UI thread
                    lblFailed.Text = failed.ToString();
                    tbWriteOut.AppendText(DateTime.Now.ToLongTimeString() + " Failed: " + URL + " " + e.ToString() + "\n");
                    Log.AddMessage("Failed: " + URL + " " + e.Message, "Error");
                });

                return false;
            }
        }

        private void BGW_DownloadFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateStatusLabel("");
            loadingPanel.Visible = false;
            loadingPanel.Enabled = false;
            tbWriteOut.AppendText(DateTime.Now.ToLongTimeString() + " Download Ended\n");
        }

        #endregion Download files from website

        #region Organize files, rename, and create index

        private void btnOrg_Click(object sender, EventArgs e)
        {
            loadingPanel.Visible = true;
            loadingPanel.Enabled = true;
            sSource = cbSource.SelectedItem.ToString();
            sProj = tbProjManual.Text;

            if (BGW_Organize.IsBusy != true)
            {
                BGW_Organize.RunWorkerAsync();
            }            
        }

        private void BGW_Organize_DoWork(object sender, DoWorkEventArgs e)
        {            
            Log.AddMessage("Beginning organization of files...", "Information");
            UpdateStatusLabel("Finding files...");

            EMDocs emdocs = new EMDocs();
            //List<string> eDocs = new List<string>();

            if (!Directory.Exists(tbOutputOrg.Text))
            {
                Directory.CreateDirectory(tbOutputOrg.Text);
            }

            if (Directory.Exists(tbBaseFolder.Text))
            {
                var files = Directory.GetFiles(tbBaseFolder.Text, "*.pdf", SearchOption.AllDirectories);
                int currentEDoc = 1;

                if (files.Length == 0)
                {
                    Log.AddMessage("No files found to organize...", "Information");
                    MessageBox.Show("No files found to organize...", "Information");
                }

                foreach (var f in files)
                {
                    EMDoc eDoc = emdocs.AddDoc(f, sSource, cbOCR.Checked, (cbFillDate.Checked ? dateTimePicker1.Value : DateTime.Now));

                    UpdateStatusLabel("Indexing source " + currentEDoc + " of " + files.Length + ":\n" + eDoc.Id + " - " + eDoc.Title);
                    //eDocs.Add(em.Id + " - " + em.Title);

                    //Filename
                    //string saveFile = eDoc.Id + "_" + eDoc.Date.ToShortDateString() + "_" + eDoc.Agency + "_" + eDoc.Type + "_" + eDoc.Title;
                    string ag = eDoc.Agency;
                    string type = eDoc.Type;
                    int year = eDoc.Date.Year;
                    int month = eDoc.Date.Month;
                    int day = eDoc.Date.Day;
                    string docDir = "";
                    string relDocDir = "";
                    string xmlDir = "";
                    string ocrDir = "";
                    string relOcrDir = "";

                    docDir = tbOutputOrg.Text + "\\docs\\";//"\\" + ag + "\\" + type + "\\" + year + "\\";
                    relDocDir = "\\docs\\";
                    xmlDir = tbOutputOrg.Text + "\\xmls\\";
                    ocrDir = tbOutputOrg.Text + "\\ocr\\";
                    relOcrDir = "\\ocr\\";

                    string shortDate = eDoc.Date.ToShortDateString();

                    if (eDoc.Agency == "" || eDoc.Type == "" || year == 0 || eDoc.Title == "" || shortDate == "1/1/0001")
                    {
                        docDir = tbOutputOrg.Text + "\\_incomplete\\";
                        relDocDir = "\\_incomplete\\";
                        xmlDir = tbOutputOrg.Text + "\\_incomplete\\";
                        ocrDir = tbOutputOrg.Text + "\\_incomplete\\";
                        relOcrDir = "\\_incomplete\\";
                    }

                    UpdateStatusLabel("Saving output " + currentEDoc + " of " + files.Length + ":\n" + eDoc.Id + " - " + eDoc.Title);

                    if (!Directory.Exists(docDir))
                    {
                        Directory.CreateDirectory(docDir);
                    }

                    if (!Directory.Exists(xmlDir))
                    {
                        Directory.CreateDirectory(xmlDir);
                    }

                    if (cbOCR.Checked)
                    {
                        if (!Directory.Exists(ocrDir))
                        {
                            Directory.CreateDirectory(ocrDir);
                        }
                    }

                    //Path to save to
                    File.Copy(eDoc.Filename, docDir + Path.GetFileNameWithoutExtension(eDoc.Filename) + eDoc.Extension, true); //tbOutputOrg.Text + "\\" + ag + "\\" + year + "\\" + month + "\\" + RemoveProbChars(saveFile) + eDoc.Extension, true);

                    UpdateStatusLabel("Building index " + currentEDoc + " of " + files.Length + ":\n" + eDoc.Id + " - " + eDoc.Title);

                    XmlDocument xml = new XmlDocument();// Create the XML Declaration, and append it to XML eDoc
                    XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
                    xml.AppendChild(dec);// Create the root element
                    XmlElement root = xml.CreateElement("EMDoc");
                    xml.AppendChild(root);

                    XmlElement id = xml.CreateElement("WebsiteID");
                    id.InnerText = eDoc.Id.ToString();
                    root.AppendChild(id);

                    XmlElement mime = xml.CreateElement("Mime");
                    mime.InnerText = eDoc.Extension;
                    root.AppendChild(mime);

                    XmlElement source = xml.CreateElement("Source");
                    source.InnerText = eDoc.Source;
                    root.AppendChild(source);

                    XmlElement sourceURL = xml.CreateElement("SourceURL");

                    if (sSource == "emcity.org")
                    {
                        if (eDoc.Extension.ToLower() == ".pdf")
                        {
                            sourceURL.InnerText = @"http://www.eaglemountaincity.org/Home/ShowDocument?id=" + eDoc.Id.ToString();
                        }
                        else
                        {
                            sourceURL.InnerText = @"http://www.eaglemountaincity.org/Home/ShowImage?id=" + eDoc.Id.ToString();
                        }
                    }

                    root.AppendChild(sourceURL);

                    XmlElement path = xml.CreateElement("Path");
                    path.InnerText = relDocDir + Path.GetFileNameWithoutExtension(eDoc.Filename) + eDoc.Extension;
                    //path.InnerText = tbOutputFolder.Text + "\\" + eDoc.Id + "_" + eDoc.Date.ToShortDateString() + "_" + eDoc.Title + eDoc.Extension;
                    root.AppendChild(path);

                    XmlElement filename = xml.CreateElement("Filename");
                    filename.InnerText = Path.GetFileNameWithoutExtension(eDoc.Filename) + eDoc.Extension;
                    root.AppendChild(filename);

                    if (cbOCR.Checked)
                    {
                        XmlElement ocr = xml.CreateElement("OCR");
                        ocr.InnerText = relOcrDir + Path.GetFileNameWithoutExtension(eDoc.Filename) + ".txt";
                        root.AppendChild(ocr);
                    }

                    XmlElement flds = xml.CreateElement("Fields");
                    root.AppendChild(flds);

                    XmlElement docName = xml.CreateElement("Title");
                    docName.InnerText = eDoc.Title;
                    flds.AppendChild(docName);

                    XmlElement date = xml.CreateElement("Date");
                    date.InnerText = eDoc.Date.ToShortDateString();
                    flds.AppendChild(date);

                    XmlElement yr = xml.CreateElement("Year");
                    yr.InnerText = year.ToString();
                    flds.AppendChild(yr);

                    XmlElement agency = xml.CreateElement("Agency");
                    agency.InnerText = eDoc.Agency;
                    flds.AppendChild(agency);

                    XmlElement docType = xml.CreateElement("DocType");
                    docType.InnerText = eDoc.Type;
                    flds.AppendChild(docType);

                    XmlElement docTypeDet = xml.CreateElement("DocTypeDetail");
                    docTypeDet.InnerText = eDoc.TypeDet;
                    flds.AppendChild(docTypeDet);

                    if (sProj != "")
                    {
                        if (eDoc.Project == "")
                        {
                            XmlElement project = xml.CreateElement("Project");
                            project.InnerText = sProj;
                            flds.AppendChild(project);
                        }
                        {
                            XmlElement project = xml.CreateElement("Project");
                            project.InnerText = eDoc.Project;
                            flds.AppendChild(project);
                        }
                    }
                    else
                    {
                        XmlElement project = xml.CreateElement("Project");
                        project.InnerText = eDoc.Project;
                        flds.AppendChild(project);
                    }


                    XmlElement officials = xml.CreateElement("Officials");
                    officials.InnerText = eDoc.Officials;
                    flds.AppendChild(officials);

                    XmlElement staff = xml.CreateElement("Staff");
                    staff.InnerText = eDoc.Staff;
                    flds.AppendChild(staff);

                    XmlElement votes = xml.CreateElement("Votes");
                    votes.InnerText = eDoc.Votes;
                    flds.AppendChild(votes);

                    xml.Save(xmlDir + Path.GetFileNameWithoutExtension(eDoc.Filename) + ".xml"); //tbOutputOrg.Text + "\\" + ag + "\\" + year + "\\" + month + "\\" + RemoveProbChars(saveFile) + ".xml");

                    if (cbOCR.Checked)
                    { 
                        if (eDoc.OCRText != "")
                        {
                            UpdateStatusLabel("Saving OCR " + currentEDoc + " of " + files.Length + ":\n" + eDoc.Id + " - " + eDoc.Title);

                            using (StreamWriter file = new System.IO.StreamWriter(ocrDir + Path.GetFileNameWithoutExtension(eDoc.Filename) + ".txt", false))
                            {
                                file.Write(eDoc.OCRText);
                            }
                        }
                    }

                    currentEDoc++;
                }
            }
            else
            {
                Console.WriteLine("Base Directory Doesn't Exist");
            }

            //int currentEDoc = 1;

            //Log.AddMessage(eDocs.Count + " files found to organize", "Information");

            //foreach (EMDoc eDoc in eDocs)
            //{        
            //    UpdateStatusLabel("Saving files and indexes " + currentEDoc + " of " + emdocs.CollectionCount() + ":\n" + eDoc.Id + " - " + eDoc.Title);

            //    //Filename
            //    //string saveFile = eDoc.Id + "_" + eDoc.Date.ToShortDateString() + "_" + eDoc.Agency + "_" + eDoc.Type + "_" + eDoc.Title;
            //    string ag = eDoc.Agency;
            //    string type = eDoc.Type;
            //    int year = eDoc.Date.Year;
            //    int month = eDoc.Date.Month;
            //    int day = eDoc.Date.Day;
            //    string docDir = "";
            //    string relDocDir = "";
            //    string xmlDir = "";
            //    string ocrDir = "";
            //    string relOcrDir = "";

            //    docDir = tbOutputOrg.Text + "\\docs\\";//"\\" + ag + "\\" + type + "\\" + year + "\\";
            //    relDocDir = "\\docs\\";
            //    xmlDir = tbOutputOrg.Text + "\\xmls\\";
            //    ocrDir = tbOutputOrg.Text + "\\ocr\\";
            //    relOcrDir = "\\ocr\\";

            //    string shortDate = eDoc.Date.ToShortDateString();

            //    if (eDoc.Agency == "" || eDoc.Type == "" || year == 0 || eDoc.Title == "" || shortDate == "1/1/0001")
            //    {
            //        docDir = tbOutputOrg.Text + "\\_incomplete\\";
            //        relDocDir = "\\_incomplete\\";
            //        xmlDir = tbOutputOrg.Text + "\\_incomplete\\";
            //        ocrDir = tbOutputOrg.Text + "\\_incomplete\\";
            //        relOcrDir =  "\\_incomplete\\";
            //    }

            //    if (!Directory.Exists(docDir))
            //    {
            //        Directory.CreateDirectory(docDir);
            //    }

            //    if (!Directory.Exists(xmlDir))
            //    {
            //        Directory.CreateDirectory(xmlDir);
            //    }

            //    if (!Directory.Exists(ocrDir))
            //    {
            //        Directory.CreateDirectory(ocrDir);
            //    }

            //    //Path to save to
            //    File.Copy(eDoc.Filename, docDir + Path.GetFileNameWithoutExtension(eDoc.Filename) + eDoc.Extension, true); //tbOutputOrg.Text + "\\" + ag + "\\" + year + "\\" + month + "\\" + RemoveProbChars(saveFile) + eDoc.Extension, true);

            //    XmlDocument xml = new XmlDocument();// Create the XML Declaration, and append it to XML eDoc
            //    XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            //    xml.AppendChild(dec);// Create the root element
            //    XmlElement root = xml.CreateElement("EMDoc");
            //    xml.AppendChild(root);

            //    XmlElement id = xml.CreateElement("WebsiteID");
            //    id.InnerText = eDoc.Id.ToString();
            //    root.AppendChild(id);

            //    XmlElement mime = xml.CreateElement("Mime");
            //    mime.InnerText = eDoc.Extension;
            //    root.AppendChild(mime);

            //    XmlElement source = xml.CreateElement("Source");
            //    source.InnerText = eDoc.Source;
            //    root.AppendChild(source);

            //    XmlElement sourceURL = xml.CreateElement("SourceURL");

            //    if (eDoc.Extension.ToLower() == ".pdf")
            //    {
            //        sourceURL.InnerText = @"http://www.eaglemountaincity.org/Home/ShowDocument?id=" + eDoc.Id.ToString();
            //    }
            //    else
            //    {
            //        sourceURL.InnerText = @"http://www.eaglemountaincity.org/Home/ShowImage?id=" + eDoc.Id.ToString();
            //    }
                
            //    root.AppendChild(sourceURL);

            //    XmlElement path = xml.CreateElement("Path");
            //    path.InnerText = relDocDir + Path.GetFileNameWithoutExtension(eDoc.Filename) + eDoc.Extension;
            //    //path.InnerText = tbOutputFolder.Text + "\\" + eDoc.Id + "_" + eDoc.Date.ToShortDateString() + "_" + eDoc.Title + eDoc.Extension;
            //    root.AppendChild(path);

            //    XmlElement filename = xml.CreateElement("Filename");
            //    filename.InnerText = Path.GetFileNameWithoutExtension(eDoc.Filename) + eDoc.Extension;
            //    root.AppendChild(filename);

            //    if (cbOCR.Checked)
            //    {
            //        XmlElement ocr = xml.CreateElement("OCR");
            //        ocr.InnerText = relOcrDir + Path.GetFileNameWithoutExtension(eDoc.Filename) + ".txt";
            //        root.AppendChild(ocr);
            //    }

            //    XmlElement flds = xml.CreateElement("Fields");
            //    root.AppendChild(flds);

            //        XmlElement docName = xml.CreateElement("Title");
            //        docName.InnerText = eDoc.Title;
            //        flds.AppendChild(docName);

            //        XmlElement date = xml.CreateElement("Date");
            //        date.InnerText = eDoc.Date.ToShortDateString();
            //        flds.AppendChild(date);

            //        XmlElement yr = xml.CreateElement("Year");
            //        yr.InnerText = year.ToString();
            //        flds.AppendChild(yr);

            //        XmlElement agency = xml.CreateElement("Agency");
            //        agency.InnerText = eDoc.Agency;
            //        flds.AppendChild(agency);

            //        XmlElement docType = xml.CreateElement("DocType");
            //        docType.InnerText = eDoc.Type;
            //        flds.AppendChild(docType);

            //        XmlElement docTypeDet = xml.CreateElement("DocTypeDetail");
            //        docTypeDet.InnerText = eDoc.TypeDet;
            //        flds.AppendChild(docTypeDet);

            //        XmlElement project = xml.CreateElement("Project");
            //        project.InnerText = eDoc.Project;
            //        flds.AppendChild(project);

            //        XmlElement officials = xml.CreateElement("Officials");
            //        officials.InnerText = eDoc.Officials;
            //        flds.AppendChild(officials);

            //        XmlElement staff = xml.CreateElement("Staff");
            //        staff.InnerText = eDoc.Staff;
            //        flds.AppendChild(staff);

            //        XmlElement votes = xml.CreateElement("Votes");
            //        votes.InnerText = eDoc.Votes;
            //        flds.AppendChild(votes);

            //    xml.Save(xmlDir + Path.GetFileNameWithoutExtension(eDoc.Filename) + ".xml"); //tbOutputOrg.Text + "\\" + ag + "\\" + year + "\\" + month + "\\" + RemoveProbChars(saveFile) + ".xml");

            //    if (cbOCR.Checked)
            //    {
            //        if (eDoc.OCRText != "")
            //        {
            //            using (StreamWriter file = new System.IO.StreamWriter(ocrDir + Path.GetFileNameWithoutExtension(eDoc.Filename) + ".txt", false))
            //            {
            //                file.Write(eDoc.OCRText);
            //            }
            //        }
            //    }

            //    currentEDoc++;
            //}
        }

        private void BGW_Organize_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateStatusLabel("");

            loadingPanel.Visible = false;
            loadingPanel.Enabled = false;
        }

        #endregion Organize files, rename, and create index

        //Clear write window on DL tab
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbWriteOut.Clear();
        }

        public static string GetDefaultExtension(string mimeType)
        {
            string result;
            RegistryKey key;
            object value;

            key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimeType, false);
            value = key != null ? key.GetValue("Extension", null) : null;
            result = value != null ? value.ToString() : string.Empty;

            return result;
        }

        //Removes problematic characters from Filename index
        private string RemoveProbChars(string input)
        {
            string ret = input.Replace(@"\", "-").Replace(@"/", "-").Replace(@":", "").Replace(@"<", "").Replace(@">", "");
            return ret;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                tbBaseFolder.Text = tbOutputFolder.Text;
                tbOutputOrg.Text = tbBaseFolder.Text + "Done";
            }
        }

        private void UpdateStatusLabel(string text)
        {
            this.Invoke(new System.Action(() =>
            {
                statusLabel.Text = text;
            }));
        }

        public void WriteToTBWriteOutOrgText(string text)
        {
            this.Invoke(new System.Action(() =>
            {
                tbWriteOutOrg.Text = text;
            }));
        }

        private void browseProgramDataDirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Directory.GetParent(Application.CommonAppDataPath).ToString());
        }

        private void browseCurrentSaveFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1) //Download tab
            {
                string parent = Path.GetFullPath(Path.Combine(tbOutputFolder.Text, ".."));

                if (Directory.Exists(tbOutputFolder.Text)) 
                {
                    System.Diagnostics.Process.Start(tbOutputFolder.Text);
                }
                else if (Directory.Exists(parent))
                {
                    System.Diagnostics.Process.Start(parent);
                }
            }
            else if (tabControl1.SelectedTab == tabPage2) //Organize tab
            {
                string parent = Path.GetFullPath(Path.Combine(tbOutputOrg.Text, ".."));

                if (Directory.Exists(tbOutputOrg.Text))
                {
                    System.Diagnostics.Process.Start(tbOutputOrg.Text);
                }
                else if (Directory.Exists(parent))
                {
                    System.Diagnostics.Process.Start(parent);
                }
            }
        }

        private void browseBaseFolder_Click(object sender, EventArgs e)
        {
            string parent = Path.GetFullPath(Path.Combine(tbBaseFolder.Text, ".."));

            if (Directory.Exists(tbBaseFolder.Text))
            {
                System.Diagnostics.Process.Start(tbBaseFolder.Text);
            }
            else if (Directory.Exists(parent))
            {
                System.Diagnostics.Process.Start(parent);
            }
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                browseBaseFolder.Enabled = false;
            }
            else
            {
                browseBaseFolder.Enabled = true;
            }
        }

        private void btnBrowseOutputOrg_Click(object sender, EventArgs e)
        {
            string parent = Path.GetFullPath(Path.Combine(tbOutputOrg.Text, ".."));

            if (Directory.Exists(tbOutputOrg.Text))
            {
                fbdOutputOrg.SelectedPath = Path.GetFullPath(tbOutputOrg.Text);
                fbdOutputOrg.ShowDialog();
                tbOutputOrg.Text = fbdOutputOrg.SelectedPath;
            }
            else if (Directory.Exists(parent))
            {
                fbdOutputOrg.SelectedPath = Path.GetFullPath(parent);
                fbdOutputOrg.ShowDialog();
                tbOutputOrg.Text = fbdOutputOrg.SelectedPath;
            }
        }
    }
}
