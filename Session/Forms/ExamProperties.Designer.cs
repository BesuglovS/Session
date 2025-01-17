﻿namespace Session.Forms
{
    partial class ExamProperties
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
            this.SaveWOLog = new System.Windows.Forms.Button();
            this.ExamAudBox = new System.Windows.Forms.TextBox();
            this.ConsAudBox = new System.Windows.Forms.TextBox();
            this.LabelConsAud = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.ExamDate = new System.Windows.Forms.DateTimePicker();
            this.ConsDate = new System.Windows.Forms.DateTimePicker();
            this.Exam = new System.Windows.Forms.Label();
            this.Cons = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.discipline = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SaveWOLog
            // 
            this.SaveWOLog.Location = new System.Drawing.Point(135, 139);
            this.SaveWOLog.Name = "SaveWOLog";
            this.SaveWOLog.Size = new System.Drawing.Size(99, 49);
            this.SaveWOLog.TabIndex = 50;
            this.SaveWOLog.Text = "Изменить без лога";
            this.SaveWOLog.UseVisualStyleBackColor = true;
            this.SaveWOLog.Click += new System.EventHandler(this.SaveWOLog_Click);
            // 
            // ExamAudBox
            // 
            this.ExamAudBox.Location = new System.Drawing.Point(295, 113);
            this.ExamAudBox.Name = "ExamAudBox";
            this.ExamAudBox.Size = new System.Drawing.Size(148, 20);
            this.ExamAudBox.TabIndex = 46;
            // 
            // ConsAudBox
            // 
            this.ConsAudBox.Location = new System.Drawing.Point(105, 113);
            this.ConsAudBox.Name = "ConsAudBox";
            this.ConsAudBox.Size = new System.Drawing.Size(126, 20);
            this.ConsAudBox.TabIndex = 45;
            // 
            // LabelConsAud
            // 
            this.LabelConsAud.AutoSize = true;
            this.LabelConsAud.Location = new System.Drawing.Point(12, 116);
            this.LabelConsAud.Name = "LabelConsAud";
            this.LabelConsAud.Size = new System.Drawing.Size(60, 13);
            this.LabelConsAud.TabIndex = 48;
            this.LabelConsAud.Text = "Аудитория";
            // 
            // Cancel
            // 
            this.Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Cancel.Location = new System.Drawing.Point(240, 139);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(203, 49);
            this.Cancel.TabIndex = 49;
            this.Cancel.Text = "Отмена";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Save
            // 
            this.Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Save.Location = new System.Drawing.Point(12, 139);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(117, 49);
            this.Save.TabIndex = 47;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // ExamDate
            // 
            this.ExamDate.CustomFormat = "dd.MM.yyyy HH:mm";
            this.ExamDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ExamDate.Location = new System.Drawing.Point(295, 84);
            this.ExamDate.Name = "ExamDate";
            this.ExamDate.Size = new System.Drawing.Size(148, 20);
            this.ExamDate.TabIndex = 44;
            this.ExamDate.Value = new System.DateTime(2014, 1, 9, 10, 0, 0, 0);
            // 
            // ConsDate
            // 
            this.ConsDate.CustomFormat = "dd.MM.yyyy HH:mm";
            this.ConsDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ConsDate.Location = new System.Drawing.Point(105, 84);
            this.ConsDate.Name = "ConsDate";
            this.ConsDate.Size = new System.Drawing.Size(126, 20);
            this.ConsDate.TabIndex = 43;
            this.ConsDate.Value = new System.DateTime(2014, 1, 9, 10, 0, 0, 0);
            // 
            // Exam
            // 
            this.Exam.AutoSize = true;
            this.Exam.Location = new System.Drawing.Point(237, 90);
            this.Exam.Name = "Exam";
            this.Exam.Size = new System.Drawing.Size(52, 13);
            this.Exam.TabIndex = 42;
            this.Exam.Text = "Экзамен";
            // 
            // Cons
            // 
            this.Cons.AutoSize = true;
            this.Cons.Location = new System.Drawing.Point(12, 90);
            this.Cons.Name = "Cons";
            this.Cons.Size = new System.Drawing.Size(78, 13);
            this.Cons.TabIndex = 41;
            this.Cons.Text = "Консультация";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Дисциплина";
            // 
            // discipline
            // 
            this.discipline.Location = new System.Drawing.Point(87, 12);
            this.discipline.Multiline = true;
            this.discipline.Name = "discipline";
            this.discipline.ReadOnly = true;
            this.discipline.Size = new System.Drawing.Size(356, 63);
            this.discipline.TabIndex = 52;
            // 
            // ExamProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 202);
            this.Controls.Add(this.discipline);
            this.Controls.Add(this.SaveWOLog);
            this.Controls.Add(this.ExamAudBox);
            this.Controls.Add(this.ConsAudBox);
            this.Controls.Add(this.LabelConsAud);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.ExamDate);
            this.Controls.Add(this.ConsDate);
            this.Controls.Add(this.Exam);
            this.Controls.Add(this.Cons);
            this.Controls.Add(this.label1);
            this.Name = "ExamProperties";
            this.Text = "Данные экзамена";
            this.Load += new System.EventHandler(this.ExamProperties_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveWOLog;
        private System.Windows.Forms.TextBox ExamAudBox;
        private System.Windows.Forms.TextBox ConsAudBox;
        private System.Windows.Forms.Label LabelConsAud;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.DateTimePicker ExamDate;
        private System.Windows.Forms.DateTimePicker ConsDate;
        private System.Windows.Forms.Label Exam;
        private System.Windows.Forms.Label Cons;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox discipline;
    }
}