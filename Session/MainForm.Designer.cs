namespace Session
{
    partial class MainForm
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
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.DownloadRestore = new System.Windows.Forms.Button();
            this.BackupUpload = new System.Windows.Forms.Button();
            this.WordExport = new System.Windows.Forms.Button();
            this.upload = new System.Windows.Forms.Button();
            this.showAll = new System.Windows.Forms.Button();
            this.BigRedButton = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.examsView = new System.Windows.Forms.DataGridView();
            this.Refresh = new System.Windows.Forms.Button();
            this.TeacherList = new System.Windows.Forms.ComboBox();
            this.TeacherSchedule = new System.Windows.Forms.Button();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.examsView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.TeacherSchedule);
            this.controlsPanel.Controls.Add(this.TeacherList);
            this.controlsPanel.Controls.Add(this.Refresh);
            this.controlsPanel.Controls.Add(this.DownloadRestore);
            this.controlsPanel.Controls.Add(this.BackupUpload);
            this.controlsPanel.Controls.Add(this.WordExport);
            this.controlsPanel.Controls.Add(this.upload);
            this.controlsPanel.Controls.Add(this.showAll);
            this.controlsPanel.Controls.Add(this.BigRedButton);
            this.controlsPanel.Controls.Add(this.groupBox);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(939, 80);
            this.controlsPanel.TabIndex = 0;
            // 
            // DownloadRestore
            // 
            this.DownloadRestore.Location = new System.Drawing.Point(467, 16);
            this.DownloadRestore.Name = "DownloadRestore";
            this.DownloadRestore.Size = new System.Drawing.Size(77, 47);
            this.DownloadRestore.TabIndex = 7;
            this.DownloadRestore.Text = "Download + Restore";
            this.DownloadRestore.UseVisualStyleBackColor = true;
            this.DownloadRestore.Click += new System.EventHandler(this.DownloadRestore_Click);
            // 
            // BackupUpload
            // 
            this.BackupUpload.Location = new System.Drawing.Point(384, 16);
            this.BackupUpload.Name = "BackupUpload";
            this.BackupUpload.Size = new System.Drawing.Size(77, 47);
            this.BackupUpload.TabIndex = 6;
            this.BackupUpload.Text = "Backup + Upload";
            this.BackupUpload.UseVisualStyleBackColor = true;
            this.BackupUpload.Click += new System.EventHandler(this.BackupUpload_Click);
            // 
            // WordExport
            // 
            this.WordExport.Location = new System.Drawing.Point(301, 16);
            this.WordExport.Name = "WordExport";
            this.WordExport.Size = new System.Drawing.Size(77, 47);
            this.WordExport.TabIndex = 5;
            this.WordExport.Text = "Экспорт в Word";
            this.WordExport.UseVisualStyleBackColor = true;
            this.WordExport.Click += new System.EventHandler(this.WordExport_Click);
            // 
            // upload
            // 
            this.upload.Location = new System.Drawing.Point(221, 16);
            this.upload.Name = "upload";
            this.upload.Size = new System.Drawing.Size(74, 49);
            this.upload.TabIndex = 4;
            this.upload.Text = "Загрузить на сайт";
            this.upload.UseVisualStyleBackColor = true;
            this.upload.Click += new System.EventHandler(this.UploadClick);
            // 
            // showAll
            // 
            this.showAll.Location = new System.Drawing.Point(15, 43);
            this.showAll.Name = "showAll";
            this.showAll.Size = new System.Drawing.Size(161, 23);
            this.showAll.TabIndex = 3;
            this.showAll.Text = "Показать все";
            this.showAll.UseVisualStyleBackColor = true;
            this.showAll.Click += new System.EventHandler(this.showAll_Click);
            // 
            // BigRedButton
            // 
            this.BigRedButton.Location = new System.Drawing.Point(856, 17);
            this.BigRedButton.Name = "BigRedButton";
            this.BigRedButton.Size = new System.Drawing.Size(71, 52);
            this.BigRedButton.TabIndex = 2;
            this.BigRedButton.Text = "Big Red Button";
            this.BigRedButton.UseVisualStyleBackColor = true;
            this.BigRedButton.Click += new System.EventHandler(this.BigRedButton_Click);
            // 
            // groupBox
            // 
            this.groupBox.FormattingEnabled = true;
            this.groupBox.Location = new System.Drawing.Point(60, 16);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(116, 21);
            this.groupBox.TabIndex = 1;
            this.groupBox.SelectedIndexChanged += new System.EventHandler(this.groupBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Группа";
            // 
            // viewPanel
            // 
            this.viewPanel.Controls.Add(this.examsView);
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(0, 80);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(939, 458);
            this.viewPanel.TabIndex = 1;
            // 
            // examsView
            // 
            this.examsView.AllowUserToAddRows = false;
            this.examsView.AllowUserToDeleteRows = false;
            this.examsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.examsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.examsView.Location = new System.Drawing.Point(0, 0);
            this.examsView.Name = "examsView";
            this.examsView.ReadOnly = true;
            this.examsView.RowHeadersVisible = false;
            this.examsView.Size = new System.Drawing.Size(939, 458);
            this.examsView.TabIndex = 0;
            this.examsView.DoubleClick += new System.EventHandler(this.examsView_DoubleClick);
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(182, 16);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(33, 50);
            this.Refresh.TabIndex = 8;
            this.Refresh.Text = "GO";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // TeacherList
            // 
            this.TeacherList.FormattingEnabled = true;
            this.TeacherList.Location = new System.Drawing.Point(557, 17);
            this.TeacherList.Name = "TeacherList";
            this.TeacherList.Size = new System.Drawing.Size(293, 21);
            this.TeacherList.TabIndex = 9;
            // 
            // TeacherSchedule
            // 
            this.TeacherSchedule.Location = new System.Drawing.Point(557, 43);
            this.TeacherSchedule.Name = "TeacherSchedule";
            this.TeacherSchedule.Size = new System.Drawing.Size(293, 23);
            this.TeacherSchedule.TabIndex = 10;
            this.TeacherSchedule.Text = "GO";
            this.TeacherSchedule.UseVisualStyleBackColor = true;
            this.TeacherSchedule.Click += new System.EventHandler(this.TeacherSchedule_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 538);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.controlsPanel);
            this.Name = "MainForm";
            this.Text = "Расписание сессии";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.viewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.examsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.ComboBox groupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.Button BigRedButton;
        private System.Windows.Forms.Button showAll;
        private System.Windows.Forms.DataGridView examsView;
        private System.Windows.Forms.Button upload;
        private System.Windows.Forms.Button WordExport;
        private System.Windows.Forms.Button DownloadRestore;
        private System.Windows.Forms.Button BackupUpload;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.Button TeacherSchedule;
        private System.Windows.Forms.ComboBox TeacherList;
    }
}

