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
            this.upload = new System.Windows.Forms.Button();
            this.showAll = new System.Windows.Forms.Button();
            this.BigRedButton = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.examsView = new System.Windows.Forms.DataGridView();
            this.WordExport = new System.Windows.Forms.Button();
            this.controlsPanel.SuspendLayout();
            this.viewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.examsView)).BeginInit();
            this.SuspendLayout();
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.WordExport);
            this.controlsPanel.Controls.Add(this.upload);
            this.controlsPanel.Controls.Add(this.showAll);
            this.controlsPanel.Controls.Add(this.BigRedButton);
            this.controlsPanel.Controls.Add(this.groupBox);
            this.controlsPanel.Controls.Add(this.label1);
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(751, 80);
            this.controlsPanel.TabIndex = 0;
            // 
            // upload
            // 
            this.upload.Location = new System.Drawing.Point(182, 17);
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
            this.BigRedButton.Location = new System.Drawing.Point(668, 12);
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
            this.viewPanel.Size = new System.Drawing.Size(751, 458);
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
            this.examsView.Size = new System.Drawing.Size(751, 458);
            this.examsView.TabIndex = 0;
            this.examsView.DoubleClick += new System.EventHandler(this.examsView_DoubleClick);
            // 
            // WordExport
            // 
            this.WordExport.Location = new System.Drawing.Point(262, 17);
            this.WordExport.Name = "WordExport";
            this.WordExport.Size = new System.Drawing.Size(77, 47);
            this.WordExport.TabIndex = 5;
            this.WordExport.Text = "Экспорт в Word";
            this.WordExport.UseVisualStyleBackColor = true;
            this.WordExport.Click += new System.EventHandler(this.WordExport_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 538);
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
    }
}

