﻿namespace WaitForService
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxApp = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxService = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxStartup = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxMSsvcs = new System.Windows.Forms.CheckBox();
            this.checkBoxSave = new System.Windows.Forms.CheckBox();
            this.comboBoxUser = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxRunatLogon = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Service";
            // 
            // textBoxApp
            // 
            this.textBoxApp.Location = new System.Drawing.Point(74, 39);
            this.textBoxApp.Name = "textBoxApp";
            this.textBoxApp.Size = new System.Drawing.Size(292, 20);
            this.textBoxApp.TabIndex = 2;
            this.textBoxApp.TextChanged += new System.EventHandler(this.textBoxApp_TextChanged);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(372, 37);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 3;
            this.buttonBrowse.Text = "Browse ...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Application";
            // 
            // comboBoxService
            // 
            this.comboBoxService.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxService.FormattingEnabled = true;
            this.comboBoxService.Location = new System.Drawing.Point(74, 12);
            this.comboBoxService.MaxDropDownItems = 15;
            this.comboBoxService.Name = "comboBoxService";
            this.comboBoxService.Size = new System.Drawing.Size(292, 21);
            this.comboBoxService.TabIndex = 1;
            this.comboBoxService.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxService_DrawItem);
            this.comboBoxService.SelectedIndexChanged += new System.EventHandler(this.comboBoxService_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Startup";
            // 
            // comboBoxStartup
            // 
            this.comboBoxStartup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartup.FormattingEnabled = true;
            this.comboBoxStartup.Items.AddRange(new object[] {
            "Normal",
            "Hidden",
            "Minimized",
            "Maximized"});
            this.comboBoxStartup.Location = new System.Drawing.Point(74, 66);
            this.comboBoxStartup.Name = "comboBoxStartup";
            this.comboBoxStartup.Size = new System.Drawing.Size(292, 21);
            this.comboBoxStartup.TabIndex = 4;
            this.comboBoxStartup.SelectedIndexChanged += new System.EventHandler(this.comboBoxStartup_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(362, 140);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(443, 140);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxMSsvcs
            // 
            this.checkBoxMSsvcs.AutoSize = true;
            this.checkBoxMSsvcs.Location = new System.Drawing.Point(373, 15);
            this.checkBoxMSsvcs.Name = "checkBoxMSsvcs";
            this.checkBoxMSsvcs.Size = new System.Drawing.Size(136, 17);
            this.checkBoxMSsvcs.TabIndex = 7;
            this.checkBoxMSsvcs.Text = "Hide Microsoft services";
            this.checkBoxMSsvcs.UseVisualStyleBackColor = true;
            this.checkBoxMSsvcs.CheckedChanged += new System.EventHandler(this.checkBoxMSsvcs_CheckedChanged);
            // 
            // checkBoxSave
            // 
            this.checkBoxSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSave.AutoSize = true;
            this.checkBoxSave.Checked = true;
            this.checkBoxSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSave.Location = new System.Drawing.Point(74, 144);
            this.checkBoxSave.Name = "checkBoxSave";
            this.checkBoxSave.Size = new System.Drawing.Size(90, 17);
            this.checkBoxSave.TabIndex = 8;
            this.checkBoxSave.Text = "Save settings";
            this.checkBoxSave.UseVisualStyleBackColor = true;
            // 
            // comboBoxUser
            // 
            this.comboBoxUser.Enabled = false;
            this.comboBoxUser.FormattingEnabled = true;
            this.comboBoxUser.Location = new System.Drawing.Point(74, 94);
            this.comboBoxUser.Name = "comboBoxUser";
            this.comboBoxUser.Size = new System.Drawing.Size(292, 21);
            this.comboBoxUser.TabIndex = 9;
            this.comboBoxUser.SelectedIndexChanged += new System.EventHandler(this.comboBoxUser_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(39, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "User";
            // 
            // checkBoxRunatLogon
            // 
            this.checkBoxRunatLogon.AutoSize = true;
            this.checkBoxRunatLogon.Location = new System.Drawing.Point(373, 97);
            this.checkBoxRunatLogon.Name = "checkBoxRunatLogon";
            this.checkBoxRunatLogon.Size = new System.Drawing.Size(87, 17);
            this.checkBoxRunatLogon.TabIndex = 11;
            this.checkBoxRunatLogon.Text = "Run at logon";
            this.checkBoxRunatLogon.UseVisualStyleBackColor = true;
            this.checkBoxRunatLogon.CheckedChanged += new System.EventHandler(this.checkBoxRunatLogon_CheckedChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 175);
            this.Controls.Add(this.checkBoxRunatLogon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxUser);
            this.Controls.Add(this.checkBoxSave);
            this.Controls.Add(this.checkBoxMSsvcs);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxStartup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxService);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxApp);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxApp;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxService;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxStartup;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxMSsvcs;
        private System.Windows.Forms.CheckBox checkBoxSave;
        private System.Windows.Forms.ComboBox comboBoxUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxRunatLogon;
    }
}