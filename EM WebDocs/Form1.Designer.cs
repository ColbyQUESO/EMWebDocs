namespace EM_WebDocs
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbURL = new System.Windows.Forms.TextBox();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.gbDocsImgs = new System.Windows.Forms.GroupBox();
            this.cbImgs = new System.Windows.Forms.CheckBox();
            this.cbDocs = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbIMG = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbIDHigh = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbIDLow = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.gbDups = new System.Windows.Forms.GroupBox();
            this.rbOverwriteDups = new System.Windows.Forms.RadioButton();
            this.rbSkipDups = new System.Windows.Forms.RadioButton();
            this.tbOutputFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbWriteOut = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFailed = new System.Windows.Forms.Label();
            this.lblExist = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblDL = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.BGW_DownloadFile = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbWriteOutOrg = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowseOutputOrg = new System.Windows.Forms.Button();
            this.cbSource = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbOCR = new System.Windows.Forms.CheckBox();
            this.tbOutputOrg = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnOrg = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tbBaseFolder = new System.Windows.Forms.TextBox();
            this.loadingPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BGW_Organize = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.browseProgramDataDirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.browseBaseFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.browseCurrentSaveFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fbdOutputOrg = new System.Windows.Forms.FolderBrowserDialog();
            this.label14 = new System.Windows.Forms.Label();
            this.tbProjManual = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.gbMain.SuspendLayout();
            this.gbDocsImgs.SuspendLayout();
            this.gbDups.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.loadingPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(9, 32);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(415, 20);
            this.tbURL.TabIndex = 0;
            this.tbURL.Text = "http://www.eaglemountaincity.org/Home/ShowDocument?id=";
            // 
            // gbMain
            // 
            this.gbMain.Controls.Add(this.gbDocsImgs);
            this.gbMain.Controls.Add(this.label6);
            this.gbMain.Controls.Add(this.tbIMG);
            this.gbMain.Controls.Add(this.label5);
            this.gbMain.Controls.Add(this.tbIDHigh);
            this.gbMain.Controls.Add(this.label1);
            this.gbMain.Controls.Add(this.tbIDLow);
            this.gbMain.Controls.Add(this.label4);
            this.gbMain.Controls.Add(this.btnGo);
            this.gbMain.Controls.Add(this.gbDups);
            this.gbMain.Controls.Add(this.tbOutputFolder);
            this.gbMain.Controls.Add(this.label3);
            this.gbMain.Controls.Add(this.label2);
            this.gbMain.Controls.Add(this.tbURL);
            this.gbMain.Location = new System.Drawing.Point(6, 6);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(433, 444);
            this.gbMain.TabIndex = 2;
            this.gbMain.TabStop = false;
            // 
            // gbDocsImgs
            // 
            this.gbDocsImgs.Controls.Add(this.cbImgs);
            this.gbDocsImgs.Controls.Add(this.cbDocs);
            this.gbDocsImgs.Location = new System.Drawing.Point(9, 212);
            this.gbDocsImgs.Name = "gbDocsImgs";
            this.gbDocsImgs.Size = new System.Drawing.Size(197, 76);
            this.gbDocsImgs.TabIndex = 7;
            this.gbDocsImgs.TabStop = false;
            this.gbDocsImgs.Text = "Download Parameters";
            // 
            // cbImgs
            // 
            this.cbImgs.AutoSize = true;
            this.cbImgs.Location = new System.Drawing.Point(17, 43);
            this.cbImgs.Name = "cbImgs";
            this.cbImgs.Size = new System.Drawing.Size(111, 17);
            this.cbImgs.TabIndex = 1;
            this.cbImgs.Text = "Download Images";
            this.cbImgs.UseVisualStyleBackColor = true;
            // 
            // cbDocs
            // 
            this.cbDocs.AutoSize = true;
            this.cbDocs.Checked = true;
            this.cbDocs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDocs.Location = new System.Drawing.Point(17, 20);
            this.cbDocs.Name = "cbDocs";
            this.cbDocs.Size = new System.Drawing.Size(108, 17);
            this.cbDocs.TabIndex = 0;
            this.cbDocs.Text = "Download Docs?";
            this.cbDocs.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "URL for Images";
            // 
            // tbIMG
            // 
            this.tbIMG.Location = new System.Drawing.Point(9, 82);
            this.tbIMG.Name = "tbIMG";
            this.tbIMG.Size = new System.Drawing.Size(415, 20);
            this.tbIMG.TabIndex = 14;
            this.tbIMG.Text = "http://www.eaglemountaincity.org/Home/ShowImage?id=";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(175, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "ID Range High";
            // 
            // tbIDHigh
            // 
            this.tbIDHigh.Location = new System.Drawing.Point(257, 125);
            this.tbIDHigh.Name = "tbIDHigh";
            this.tbIDHigh.Size = new System.Drawing.Size(69, 20);
            this.tbIDHigh.TabIndex = 12;
            this.tbIDHigh.Text = "7000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "ID Range Low";
            // 
            // tbIDLow
            // 
            this.tbIDLow.Location = new System.Drawing.Point(88, 125);
            this.tbIDLow.Name = "tbIDLow";
            this.tbIDLow.Size = new System.Drawing.Size(69, 20);
            this.tbIDLow.TabIndex = 10;
            this.tbIDLow.Text = "6800";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(321, 365);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 26);
            this.label4.TabIndex = 9;
            this.label4.Text = "Collect and Rename\r\nWebsite Documents";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(349, 394);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 8;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // gbDups
            // 
            this.gbDups.Controls.Add(this.rbOverwriteDups);
            this.gbDups.Controls.Add(this.rbSkipDups);
            this.gbDups.Enabled = false;
            this.gbDups.Location = new System.Drawing.Point(227, 212);
            this.gbDups.Name = "gbDups";
            this.gbDups.Size = new System.Drawing.Size(197, 76);
            this.gbDups.TabIndex = 6;
            this.gbDups.TabStop = false;
            this.gbDups.Text = "Duplicate Management";
            this.gbDups.Visible = false;
            // 
            // rbOverwriteDups
            // 
            this.rbOverwriteDups.AutoSize = true;
            this.rbOverwriteDups.Location = new System.Drawing.Point(21, 42);
            this.rbOverwriteDups.Name = "rbOverwriteDups";
            this.rbOverwriteDups.Size = new System.Drawing.Size(70, 17);
            this.rbOverwriteDups.TabIndex = 3;
            this.rbOverwriteDups.Text = "Overwrite";
            this.rbOverwriteDups.UseVisualStyleBackColor = true;
            // 
            // rbSkipDups
            // 
            this.rbSkipDups.AutoSize = true;
            this.rbSkipDups.Checked = true;
            this.rbSkipDups.Location = new System.Drawing.Point(21, 19);
            this.rbSkipDups.Name = "rbSkipDups";
            this.rbSkipDups.Size = new System.Drawing.Size(74, 17);
            this.rbSkipDups.TabIndex = 2;
            this.rbSkipDups.TabStop = true;
            this.rbSkipDups.Text = "Skip Dups";
            this.rbSkipDups.UseVisualStyleBackColor = true;
            // 
            // tbOutputFolder
            // 
            this.tbOutputFolder.Location = new System.Drawing.Point(9, 174);
            this.tbOutputFolder.Name = "tbOutputFolder";
            this.tbOutputFolder.Size = new System.Drawing.Size(415, 20);
            this.tbOutputFolder.TabIndex = 5;
            this.tbOutputFolder.Tag = "";
            this.tbOutputFolder.Text = "E:\\EMCity Docs\\Test";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Output Document Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "URL for Docs";
            // 
            // tbWriteOut
            // 
            this.tbWriteOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.tbWriteOut.Location = new System.Drawing.Point(445, 12);
            this.tbWriteOut.Multiline = true;
            this.tbWriteOut.Name = "tbWriteOut";
            this.tbWriteOut.ReadOnly = true;
            this.tbWriteOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbWriteOut.Size = new System.Drawing.Size(539, 382);
            this.tbWriteOut.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(909, 400);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear Log";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(742, 405);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Failed";
            // 
            // lblFailed
            // 
            this.lblFailed.AutoSize = true;
            this.lblFailed.Location = new System.Drawing.Point(783, 405);
            this.lblFailed.Name = "lblFailed";
            this.lblFailed.Size = new System.Drawing.Size(13, 13);
            this.lblFailed.TabIndex = 18;
            this.lblFailed.Text = "0";
            // 
            // lblExist
            // 
            this.lblExist.AutoSize = true;
            this.lblExist.Location = new System.Drawing.Point(668, 405);
            this.lblExist.Name = "lblExist";
            this.lblExist.Size = new System.Drawing.Size(13, 13);
            this.lblExist.TabIndex = 20;
            this.lblExist.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(605, 405);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Don\'t Exist";
            // 
            // lblDL
            // 
            this.lblDL.AutoSize = true;
            this.lblDL.Location = new System.Drawing.Point(532, 405);
            this.lblDL.Name = "lblDL";
            this.lblDL.Size = new System.Drawing.Size(13, 13);
            this.lblDL.TabIndex = 22;
            this.lblDL.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(459, 405);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Downloaded";
            // 
            // BGW_DownloadFile
            // 
            this.BGW_DownloadFile.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGW_DownloadFile_DoWork);
            this.BGW_DownloadFile.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGW_DownloadFile_RunWorkerCompleted);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(997, 482);
            this.tabControl1.TabIndex = 23;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbMain);
            this.tabPage1.Controls.Add(this.lblDL);
            this.tabPage1.Controls.Add(this.tbWriteOut);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.btnClear);
            this.tabPage1.Controls.Add(this.lblExist);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.lblFailed);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(989, 456);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Download";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbWriteOutOrg);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(989, 456);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Organize";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbWriteOutOrg
            // 
            this.tbWriteOutOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.tbWriteOutOrg.Location = new System.Drawing.Point(444, 12);
            this.tbWriteOutOrg.Multiline = true;
            this.tbWriteOutOrg.Name = "tbWriteOutOrg";
            this.tbWriteOutOrg.ReadOnly = true;
            this.tbWriteOutOrg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbWriteOutOrg.Size = new System.Drawing.Size(539, 424);
            this.tbWriteOutOrg.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.tbProjManual);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.btnBrowseOutputOrg);
            this.groupBox1.Controls.Add(this.cbSource);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cbOCR);
            this.groupBox1.Controls.Add(this.tbOutputOrg);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btnOrg);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbBaseFolder);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 482);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // btnBrowseOutputOrg
            // 
            this.btnBrowseOutputOrg.Location = new System.Drawing.Point(394, 129);
            this.btnBrowseOutputOrg.Name = "btnBrowseOutputOrg";
            this.btnBrowseOutputOrg.Size = new System.Drawing.Size(34, 22);
            this.btnBrowseOutputOrg.TabIndex = 17;
            this.btnBrowseOutputOrg.Text = "...";
            this.btnBrowseOutputOrg.UseVisualStyleBackColor = true;
            this.btnBrowseOutputOrg.Click += new System.EventHandler(this.btnBrowseOutputOrg_Click);
            // 
            // cbSource
            // 
            this.cbSource.FormattingEnabled = true;
            this.cbSource.Items.AddRange(new object[] {
            "emcity.org",
            "GRAMA",
            "Resolution"});
            this.cbSource.Location = new System.Drawing.Point(9, 32);
            this.cbSource.Name = "cbSource";
            this.cbSource.Size = new System.Drawing.Size(121, 21);
            this.cbSource.TabIndex = 16;
            this.cbSource.Text = "EMCity.org";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Source Type";
            // 
            // cbOCR
            // 
            this.cbOCR.AutoSize = true;
            this.cbOCR.Location = new System.Drawing.Point(9, 167);
            this.cbOCR.Name = "cbOCR";
            this.cbOCR.Size = new System.Drawing.Size(173, 17);
            this.cbOCR.TabIndex = 14;
            this.cbOCR.Text = "Extract (OCR) Text from PDFs?";
            this.cbOCR.UseVisualStyleBackColor = true;
            // 
            // tbOutputOrg
            // 
            this.tbOutputOrg.Location = new System.Drawing.Point(9, 130);
            this.tbOutputOrg.Name = "tbOutputOrg";
            this.tbOutputOrg.Size = new System.Drawing.Size(379, 20);
            this.tbOutputOrg.TabIndex = 13;
            this.tbOutputOrg.Tag = "E:\\EMCity Docs\\www.eaglemountaincity.org\\Home";
            this.tbOutputOrg.Text = "E:\\EMCity Docs\\Test";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 114);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(123, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Output Document Folder";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(300, 378);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 26);
            this.label10.TabIndex = 11;
            this.label10.Text = "Organize and Index\r\nDownloaded Documents";
            // 
            // btnOrg
            // 
            this.btnOrg.Location = new System.Drawing.Point(349, 407);
            this.btnOrg.Name = "btnOrg";
            this.btnOrg.Size = new System.Drawing.Size(75, 23);
            this.btnOrg.TabIndex = 10;
            this.btnOrg.Text = "Go";
            this.btnOrg.UseVisualStyleBackColor = true;
            this.btnOrg.Click += new System.EventHandler(this.btnOrg_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Base Folder for Docs to Organize";
            // 
            // tbBaseFolder
            // 
            this.tbBaseFolder.Location = new System.Drawing.Point(9, 81);
            this.tbBaseFolder.Name = "tbBaseFolder";
            this.tbBaseFolder.Size = new System.Drawing.Size(418, 20);
            this.tbBaseFolder.TabIndex = 7;
            this.tbBaseFolder.Text = "E:\\EMCity Docs\\www.eaglemountaincity.org\\Home";
            // 
            // loadingPanel
            // 
            this.loadingPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.loadingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loadingPanel.Controls.Add(this.panel1);
            this.loadingPanel.Location = new System.Drawing.Point(660, 233);
            this.loadingPanel.Name = "loadingPanel";
            this.loadingPanel.Size = new System.Drawing.Size(294, 100);
            this.loadingPanel.TabIndex = 24;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(5, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 87);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.statusLabel);
            this.panel2.Location = new System.Drawing.Point(90, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 75);
            this.panel2.TabIndex = 2;
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Location = new System.Drawing.Point(0, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(185, 75);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Please wait...";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::EM_WebDocs.Properties.Resources.Main_Logo_Gif;
            this.pictureBox1.Location = new System.Drawing.Point(7, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // BGW_Organize
            // 
            this.BGW_Organize.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGW_Organize_DoWork);
            this.BGW_Organize.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGW_Organize_RunWorkerCompleted);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1018, 31);
            this.toolStrip1.TabIndex = 25;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browseProgramDataDirToolStripMenuItem1,
            this.browseBaseFolder,
            this.browseCurrentSaveFolderToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::EM_WebDocs.Properties.Resources.Document_Folder_B;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(37, 28);
            this.toolStripDropDownButton1.Text = "Browse HDD";
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // browseProgramDataDirToolStripMenuItem1
            // 
            this.browseProgramDataDirToolStripMenuItem1.Name = "browseProgramDataDirToolStripMenuItem1";
            this.browseProgramDataDirToolStripMenuItem1.Size = new System.Drawing.Size(236, 22);
            this.browseProgramDataDirToolStripMenuItem1.Text = "Browse ProgramData Directory";
            this.browseProgramDataDirToolStripMenuItem1.Click += new System.EventHandler(this.browseProgramDataDirToolStripMenuItem1_Click);
            // 
            // browseBaseFolder
            // 
            this.browseBaseFolder.Name = "browseBaseFolder";
            this.browseBaseFolder.Size = new System.Drawing.Size(236, 22);
            this.browseBaseFolder.Text = "Browse Base Folder";
            this.browseBaseFolder.Click += new System.EventHandler(this.browseBaseFolder_Click);
            // 
            // browseCurrentSaveFolderToolStripMenuItem
            // 
            this.browseCurrentSaveFolderToolStripMenuItem.Name = "browseCurrentSaveFolderToolStripMenuItem";
            this.browseCurrentSaveFolderToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.browseCurrentSaveFolderToolStripMenuItem.Text = "Browse Current Save Folder";
            this.browseCurrentSaveFolderToolStripMenuItem.Click += new System.EventHandler(this.browseCurrentSaveFolderToolStripMenuItem_Click);
            // 
            // fbdOutputOrg
            // 
            this.fbdOutputOrg.SelectedPath = "tbOutputOrg.Text";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 200);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(221, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Manually Fill in Empty Fields (Inactive if blank)";
            // 
            // tbProjManual
            // 
            this.tbProjManual.Location = new System.Drawing.Point(73, 224);
            this.tbProjManual.Name = "tbProjManual";
            this.tbProjManual.Size = new System.Drawing.Size(122, 20);
            this.tbProjManual.TabIndex = 19;
            this.tbProjManual.Tag = "";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(24, 227);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 13);
            this.label15.TabIndex = 20;
            this.label15.Tag = "Project";
            this.label15.Text = "Project";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 544);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.loadingPanel);
            this.Controls.Add(this.tabControl1);
            this.MaximumSize = new System.Drawing.Size(1034, 583);
            this.MinimumSize = new System.Drawing.Size(1034, 583);
            this.Name = "Form1";
            this.Text = "EM WebDocs";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            this.gbDocsImgs.ResumeLayout(false);
            this.gbDocsImgs.PerformLayout();
            this.gbDups.ResumeLayout(false);
            this.gbDups.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.loadingPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.GroupBox gbMain;
        private System.Windows.Forms.GroupBox gbDups;
        private System.Windows.Forms.RadioButton rbOverwriteDups;
        private System.Windows.Forms.RadioButton rbSkipDups;
        private System.Windows.Forms.TextBox tbOutputFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbIDHigh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIDLow;
        private System.Windows.Forms.TextBox tbWriteOut;
        private System.Windows.Forms.GroupBox gbDocsImgs;
        private System.Windows.Forms.CheckBox cbImgs;
        private System.Windows.Forms.CheckBox cbDocs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbIMG;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFailed;
        private System.Windows.Forms.Label lblExist;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblDL;
        private System.Windows.Forms.Label label11;
        private System.ComponentModel.BackgroundWorker BGW_DownloadFile;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbBaseFolder;
        private System.Windows.Forms.TextBox tbWriteOutOrg;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnOrg;
        private System.Windows.Forms.TextBox tbOutputOrg;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel loadingPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label statusLabel;
        private System.ComponentModel.BackgroundWorker BGW_Organize;
        private System.Windows.Forms.CheckBox cbOCR;
        private System.Windows.Forms.ComboBox cbSource;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem browseProgramDataDirToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem browseCurrentSaveFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseBaseFolder;
        private System.Windows.Forms.Button btnBrowseOutputOrg;
        private System.Windows.Forms.FolderBrowserDialog fbdOutputOrg;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbProjManual;
        private System.Windows.Forms.Label label14;
    }
}

