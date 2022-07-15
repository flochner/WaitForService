using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Configure
{
    public partial class Configure : Form
    {
        private List<string> serviceList = new List<string>();
        private List<string[]> users = new List<string[]>();
        private string startupPathRegistry = "";
        private string installDirRegistry = "";

        public Configure()
        {
            InitializeComponent();
            PopulateServices();
            GetComputerUsers();
            LoadSettings();
        }

        private void LoadSettings()
        {
            RegistryKey regKeyConfig = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ConRes\WaitForService");

            if (regKeyConfig == null)
            {
                MessageBox.Show("Application not installed properly.\nPlease repair/modify installation.",
                                "WaitForService", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(-1);
            }

            comboBoxService.SelectedItem = (string)regKeyConfig.GetValue("Service");
            textBoxApp.Text = (string)regKeyConfig.GetValue("Application");
            comboBoxVisibility.SelectedIndex = (int)regKeyConfig.GetValue("Visibility");
            checkBoxRunAtLogon.Checked = !string.IsNullOrEmpty((string)regKeyConfig.GetValue("Startup User"));
            checkBoxLockWorkstation.Checked = Convert.ToBoolean(regKeyConfig.GetValue("Lock Workstation"));
            comboBoxUsers.Text = (string)regKeyConfig.GetValue("Startup User");
            startupPathRegistry = (string)regKeyConfig.GetValue("Startup Path");
            installDirRegistry = (string)regKeyConfig.GetValue("Install Directory");

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

        private void GetComputerUsers()
        {
            RegistryKey regKeyUsers = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList");
            string[] allUsers = { "All Users", @Environment.GetEnvironmentVariable("ALLUSERSPROFILE") };
        
            users.Clear();
            users.Add(allUsers);
            foreach (var key in regKeyUsers.GetSubKeyNames())
            {
                if (key.StartsWith("S-1-5-21"))
                {
                    string[] values = new string[2];
                    var profile = regKeyUsers.OpenSubKey(key);
                    var path = profile.GetValue("ProfileImagePath");
                    values[0] = path.ToString().Split('\\')[2];
                    values[1] = path.ToString();
                    users.Add(values);
                }
            }
            regKeyUsers.Close();
        }

        private void SetOKbuttonStatus()
        {
            buttonOK.Enabled = !string.IsNullOrEmpty(comboBoxService.Text) &&
                               !string.IsNullOrEmpty(textBoxApp.Text) &&
                               !string.IsNullOrEmpty(comboBoxVisibility.Text) &&
                               ((checkBoxRunAtLogon.Checked && comboBoxUsers.SelectedIndex != -1) ||
                                checkBoxRunAtLogon.Checked == false);
        }

        private string HandleShortcut()
        {
            string startupPath = "";
            string shortcutFile = "WaitForService.lnk";
            string allUsersDir = @Environment.GetEnvironmentVariable("AllUsersProfile");
            string startUpDir = @"Microsoft\Windows\Start Menu\Programs\Startup";
            string shortcutProgdirPath = @Path.Combine(installDirRegistry, shortcutFile).ToString();

            if (startupPathRegistry.Length > 0)
            {
                try
                {
                    new FileInfo(startupPathRegistry).Delete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, ex.Source);
                }
            }

            if (checkBoxRunAtLogon.Checked == true)
            {
                if (comboBoxUsers.Text.Equals("All Users"))
                {
                    startupPath = @Path.Combine(allUsersDir, startUpDir, shortcutFile).ToString();
                }
                else
                {
                    int i = 0;
                    while (!comboBoxUsers.SelectedItem.Equals(users[i][0]))
                        i++;
                    var userDir = Path.Combine(users[i][1], @"AppData\Roaming");
                    startupPath = @Path.Combine(userDir, startUpDir, shortcutFile).ToString();
                }

                try
                {
                    File.Copy(shortcutProgdirPath, startupPath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, ex.Source);
                }
            }
            return startupPath;
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            string filePath;

            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.InitialDirectory = @Environment.GetEnvironmentVariable("ProgramFiles");
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

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            string startupPath = HandleShortcut();
            RegistryKey regKeyConfig = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ConRes\WaitForService", true);

            regKeyConfig.SetValue("Service", comboBoxService.SelectedItem);
            regKeyConfig.SetValue("Application", textBoxApp.Text);
            regKeyConfig.SetValue("Visibility", comboBoxVisibility.SelectedIndex);
            regKeyConfig.SetValue("Lock Workstation", checkBoxLockWorkstation.Checked.ToString());
            if (checkBoxRunAtLogon.Checked == true)
                regKeyConfig.SetValue("Startup User", comboBoxUsers.SelectedItem);
            else
                regKeyConfig.SetValue("Startup User", "");
            regKeyConfig.SetValue("Startup Path", startupPath);
            regKeyConfig.Close();

            Environment.Exit(0);
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

        private void ComboBoxService_Leave(object sender, EventArgs e)
        {
            this.Text = "WaitForService - Configuration";
        }

        private void CheckBoxRunAtLogon_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxLockWorkstation.Enabled = checkBoxRunAtLogon.Checked;
            comboBoxUsers.Enabled = checkBoxRunAtLogon.Checked;
            if (checkBoxRunAtLogon.Checked == false)
            {
                checkBoxLockWorkstation.Checked = false;
                comboBoxUsers.SelectedIndex = -1;
                comboBoxUsers.Text = "";
            }
            else
            {
                comboBoxUsers.Items.Clear();
                comboBoxUsers.Text = "Select User:";
                foreach (string[] user in users)
                    comboBoxUsers.Items.Add(user[0]);
            }
            SetOKbuttonStatus();
        }

        private void ComboBoxUsers_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ComboBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOKbuttonStatus();
        }

        private void ComboBoxUsers_TextUpdate(object sender, EventArgs e)
        {
            comboBoxUsers.SelectedIndex = -1;
        }

        private void ComboBoxVisibility_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ComboBoxVisibility_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOKbuttonStatus();
        }

        private void ComboBoxVisibility_TextUpdate(object sender, EventArgs e)
        {
            comboBoxVisibility.SelectedIndex = -1;
        }

        private void TextBoxApp_TextChanged(object sender, EventArgs e)
        {
            SetOKbuttonStatus();
        }

    }
}
