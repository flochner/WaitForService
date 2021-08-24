using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Configure
{
    public partial class Configure : Form
    {
        private readonly List<string> serviceList = new List<string>();

        public Configure()
        {
            InitializeComponent();
            PopulateServices();
#if !DEBUG
            LoadSettings();
#endif
        }

        private void LoadSettings()
        {
            RegistryKey regKeyConfig = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ConRes\WaitForService");
            RegistryKey regKeyRun = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");

            if (regKeyConfig == null)
            {
                MessageBox.Show("Application not installed properly.\nPlease repair/modify installation.",
                                "WaitForService", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(-1);
            }

            comboBoxService.SelectedItem = (string)regKeyConfig.GetValue("Service");
            textBoxApp.Text = (string)regKeyConfig.GetValue("Application");
            comboBoxVisibility.SelectedIndex = (int)regKeyConfig.GetValue("Visibility");
            checkBoxRunAtLogon.Checked = !string.IsNullOrEmpty((string)regKeyRun.GetValue("WaitForService"));
            checkBoxLockWorkstation.Checked = Convert.ToBoolean(regKeyConfig.GetValue("LockWorkstation"));

            regKeyRun.Close();
            regKeyConfig.Close();
        }

        private void PopulateServices()
        {
            comboBoxService.Items.Clear();
            serviceList.Clear();

            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController service in services)
            {
                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\services\" + service.ServiceName);
                string imagePath = regKey.GetValue("ImagePath").ToString().ToLower();

                if (checkBoxMSsvcs.Checked &&
                    (imagePath.Contains("svchost") || imagePath.Contains("windows") || imagePath.Contains("microsoft")) &&
                    !imagePath.Contains("driverstore"))
                {
                    continue;
                }

                comboBoxService.Items.Add(service.ServiceName);
                serviceList.Add(service.DisplayName);
            }
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            string filePath;

            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.InitialDirectory = @"C:\Program Files";
                openFile.Filter = "Programs (*.exe;*.com)|*.exe;*.com|Batch Files (*.bat;*.cmd)|*.bat;*.cmd|All files (*.*)|*.*";
                openFile.FilterIndex = 1;
                openFile.RestoreDirectory = true;
                try
                {
                    if (openFile.ShowDialog(this) == DialogResult.OK)
                    {
                        filePath = openFile.FileName;
                        textBoxApp.Text = filePath;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.Source);
                }
            }
        }

        private void CheckBoxMSsvcs_CheckedChanged(object sender, EventArgs e)
        {
            PopulateServices();
            SetOKbuttonStatus();
        }

        private void ComboBoxService_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;

            e.DrawBackground();

            if (e.State.ToString().Contains("Selected"))
            {
                e.Graphics.DrawString(comboBoxService.Items[e.Index].ToString(), e.Font, Brushes.White, new Point(e.Bounds.X, e.Bounds.Y));
                this.Text = serviceList[e.Index];
            }
            else
            {
                e.Graphics.DrawString(comboBoxService.Items[e.Index].ToString(), e.Font, Brushes.Black, new Point(e.Bounds.X, e.Bounds.Y));
            }
        }

        private void ComboBoxService_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOKbuttonStatus();
        }

        private void TextBoxApp_TextChanged(object sender, EventArgs e)
        {
            SetOKbuttonStatus();
        }

        private void ComboBoxVisibility_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOKbuttonStatus();
        }

        private void CheckBoxLockWorkstation_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SetOKbuttonStatus()
        {
            buttonOK.Enabled = !string.IsNullOrEmpty(comboBoxService.Text) &&
                               !string.IsNullOrEmpty(textBoxApp.Text) &&
                               !string.IsNullOrEmpty(comboBoxVisibility.Text);
        }

        private void ComboBoxService_Leave(object sender, EventArgs e)
        {
            this.Text = "WaitForService - Configuration";
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            RegistryKey regKeyConfig = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ConRes\WaitForService", true);
            RegistryKey regKeyRun = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

#if !DEBUG
            regKeyConfig.SetValue("Service", comboBoxService.SelectedItem);
            regKeyConfig.SetValue("Application", textBoxApp.Text);
            regKeyConfig.SetValue("Visibility", comboBoxVisibility.SelectedIndex);
            regKeyConfig.SetValue("LockWorkstation", checkBoxLockWorkstation.Checked.ToString());
            if (checkBoxRunAtLogon.Checked == true)
                regKeyRun.SetValue("WaitForService", (string)regKeyConfig.GetValue("wfsInstallPath"));
            else
                try { regKeyRun.DeleteValue("WaitForService"); }
                catch { }

            regKeyRun.Close();
            regKeyConfig.Close();
#endif 

            Environment.Exit(0);
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }

        private void CheckBoxRunAtLogon_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxLockWorkstation.Enabled = checkBoxRunAtLogon.Checked;
            if (checkBoxRunAtLogon.Checked == false)
            {
                checkBoxLockWorkstation.Checked = false;
            }
        }
    }
}
