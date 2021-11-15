namespace Configure
{
    partial class Configure
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxMSsvcs = new System.Windows.Forms.CheckBox();
            this.checkBoxRunAtLogon = new System.Windows.Forms.CheckBox();
            this.checkBoxLockWorkstation = new System.Windows.Forms.CheckBox();
            this.comboBoxUsers = new System.Windows.Forms.ComboBox();
            this.comboBoxVisibility = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Service ";
            // 
            // textBoxApp
            // 
            this.textBoxApp.Location = new System.Drawing.Point(114, 39);
            this.textBoxApp.Name = "textBoxApp";
            this.textBoxApp.Size = new System.Drawing.Size(292, 20);
            this.textBoxApp.TabIndex = 2;
            this.textBoxApp.TextChanged += new System.EventHandler(this.TextBoxApp_TextChanged);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(412, 37);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 3;
            this.buttonBrowse.Text = "Browse ...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(46, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Application ";
            // 
            // comboBoxService
            // 
            this.comboBoxService.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxService.FormattingEnabled = true;
            this.comboBoxService.Location = new System.Drawing.Point(114, 12);
            this.comboBoxService.MaxDropDownItems = 15;
            this.comboBoxService.Name = "comboBoxService";
            this.comboBoxService.Size = new System.Drawing.Size(292, 21);
            this.comboBoxService.TabIndex = 1;
            this.comboBoxService.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBoxService_DrawItem);
            this.comboBoxService.SelectedIndexChanged += new System.EventHandler(this.ComboBoxService_SelectedIndexChanged);
            this.comboBoxService.Leave += new System.EventHandler(this.ComboBoxService_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(62, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Visibility ";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(410, 144);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(491, 144);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // checkBoxMSsvcs
            // 
            this.checkBoxMSsvcs.AutoSize = true;
            this.checkBoxMSsvcs.Location = new System.Drawing.Point(412, 14);
            this.checkBoxMSsvcs.Name = "checkBoxMSsvcs";
            this.checkBoxMSsvcs.Size = new System.Drawing.Size(136, 17);
            this.checkBoxMSsvcs.TabIndex = 0;
            this.checkBoxMSsvcs.Text = "Hide Microsoft services";
            this.checkBoxMSsvcs.UseVisualStyleBackColor = true;
            this.checkBoxMSsvcs.CheckedChanged += new System.EventHandler(this.CheckBoxMSsvcs_CheckedChanged);
            // 
            // checkBoxRunAtLogon
            // 
            this.checkBoxRunAtLogon.AutoSize = true;
            this.checkBoxRunAtLogon.Location = new System.Drawing.Point(21, 95);
            this.checkBoxRunAtLogon.Name = "checkBoxRunAtLogon";
            this.checkBoxRunAtLogon.Size = new System.Drawing.Size(87, 17);
            this.checkBoxRunAtLogon.TabIndex = 9;
            this.checkBoxRunAtLogon.Text = "Run at logon";
            this.checkBoxRunAtLogon.UseVisualStyleBackColor = true;
            this.checkBoxRunAtLogon.CheckedChanged += new System.EventHandler(this.CheckBoxRunAtLogon_CheckedChanged);
            // 
            // checkBoxLockWorkstation
            // 
            this.checkBoxLockWorkstation.AutoSize = true;
            this.checkBoxLockWorkstation.Enabled = false;
            this.checkBoxLockWorkstation.Location = new System.Drawing.Point(114, 120);
            this.checkBoxLockWorkstation.Name = "checkBoxLockWorkstation";
            this.checkBoxLockWorkstation.Size = new System.Drawing.Size(195, 17);
            this.checkBoxLockWorkstation.TabIndex = 10;
            this.checkBoxLockWorkstation.Text = "Lock workstation after program start";
            this.checkBoxLockWorkstation.UseVisualStyleBackColor = true;
            // 
            // comboBoxUsers
            // 
            this.comboBoxUsers.Enabled = false;
            this.comboBoxUsers.FormattingEnabled = true;
            this.comboBoxUsers.Location = new System.Drawing.Point(114, 93);
            this.comboBoxUsers.Name = "comboBoxUsers";
            this.comboBoxUsers.Size = new System.Drawing.Size(121, 21);
            this.comboBoxUsers.TabIndex = 11;
            this.comboBoxUsers.SelectedIndexChanged += new System.EventHandler(this.ComboBoxUsers_SelectedIndexChanged);
            this.comboBoxUsers.TextUpdate += new System.EventHandler(this.ComboBoxUsers_TextUpdate);
            this.comboBoxUsers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxUsers_KeyPress);
            // 
            // comboBoxVisibility
            // 
            this.comboBoxVisibility.FormattingEnabled = true;
            this.comboBoxVisibility.Items.AddRange(new object[] {
            "Normal",
            "Hidden",
            "Minimized",
            "Maximized"});
            this.comboBoxVisibility.Location = new System.Drawing.Point(114, 66);
            this.comboBoxVisibility.Name = "comboBoxVisibility";
            this.comboBoxVisibility.Size = new System.Drawing.Size(121, 21);
            this.comboBoxVisibility.TabIndex = 4;
            this.comboBoxVisibility.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVisibility_SelectedIndexChanged);
            this.comboBoxVisibility.TextUpdate += new System.EventHandler(this.ComboBoxVisibility_TextUpdate);
            this.comboBoxVisibility.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBoxVisibility_KeyPress);
            // 
            // Configure
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(578, 179);
            this.Controls.Add(this.comboBoxUsers);
            this.Controls.Add(this.checkBoxLockWorkstation);
            this.Controls.Add(this.checkBoxRunAtLogon);
            this.Controls.Add(this.checkBoxMSsvcs);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxVisibility);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxService);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxApp);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Configure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WaitForService - Configuration";
            this.TopMost = true;
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
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxMSsvcs;
        private System.Windows.Forms.CheckBox checkBoxRunAtLogon;
        private System.Windows.Forms.CheckBox checkBoxLockWorkstation;
        private System.Windows.Forms.ComboBox comboBoxUsers;
        private System.Windows.Forms.ComboBox comboBoxVisibility;
    }
}