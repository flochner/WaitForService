using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.ServiceProcess;
using System.Windows.Forms;


namespace WaitForService
{
    public partial class Form2 : Form
    {
        public bool SaveSettings { get => saveSettings; }
        public bool RunatLogon { get => runatLogon; }
        public string ServiceName { get => serviceName; }
        public string AppName { get => appName; }
        public string AppStart { get => appStart; }
        public string RunatLogonUser { get => runatLogonUser; }

        private bool saveSettings;
        private bool runatLogon;
        private string runatLogonUser;
        private string serviceName;
        private string appName;
        private string appStart;
        private List<string> serviceList = new List<string>();
        private Dictionary<string, string> profiles = new Dictionary<string, string>();

        public Form2(string sN, string aN, string aS)
        {
            InitializeComponent();

            serviceName = sN; appName = aN; appStart = aS;

            comboBoxService.Text = serviceName;
            textBoxApp.Text = appName;
            if (string.IsNullOrEmpty(appStart))
                comboBoxStartup.SelectedIndex = -1;
            else
                comboBoxStartup.SelectedIndex = int.Parse(appStart);

            SetStartupUser();

            PopulateServices();
        }

        private void PopulateServices()
        {
            string imagePath;
            RegistryKey regKey;

            comboBoxService.Items.Clear();
            serviceList.Clear();
            this.Text = "Configuration";

            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController service in services)
            {
                regKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + service.ServiceName);
                imagePath = regKey.GetValue("ImagePath").ToString().ToLower();

                if (checkBoxMSsvcs.Checked &&
                    (imagePath.Contains("svchost") || imagePath.Contains("windows") || imagePath.Contains("microsoft")) &&
                    !imagePath.Contains("driverstore"))
                {
                    continue;
                }

                comboBoxService.Items.Add(service.ServiceName);
                serviceList.Add(service.DisplayName);

                if (service.ServiceName == this.serviceName)
                    comboBoxService.SelectedItem = service.ServiceName;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            serviceName = comboBoxService.Text;
            appName = textBoxApp.Text;
            appStart = comboBoxStartup.SelectedIndex.ToString();
            saveSettings = checkBoxSave.Checked;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            string filePath;

            OpenFileDialog openFile = new OpenFileDialog();
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

        private void checkBoxMSsvcs_CheckedChanged(object sender, EventArgs e)
        {
            PopulateServices();
        }

        private void comboBoxService_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
            {
                this.Text = "Configuration";
                return;
            }

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

        private void comboBoxService_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOKbuttonStatus();
        }

        private void textBoxApp_TextChanged(object sender, EventArgs e)
        {
            SetOKbuttonStatus();
        }

        private void comboBoxStartup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOKbuttonStatus();
        }

        private void SetOKbuttonStatus()
        {
            if (string.IsNullOrEmpty(comboBoxService.Text) || 
                string.IsNullOrEmpty(textBoxApp.Text) || 
                string.IsNullOrEmpty(comboBoxStartup.Text) ||
                (checkBoxRunatLogon.Checked == true && comboBoxUser.SelectedIndex == -1)
                )
            {
                buttonOK.Enabled = false;
            }
            else
            {
                buttonOK.Enabled = true;
            }
        }

        private void checkBoxRunatLogon_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRunatLogon.Checked == false)
            {
                comboBoxUser.SelectedIndex = -1;
            }
            comboBoxUser.Enabled = checkBoxRunatLogon.Checked;
            label4.Enabled = checkBoxRunatLogon.Checked;
            runatLogon = checkBoxRunatLogon.Checked;
            SetOKbuttonStatus();
        }

        private void SetStartupUser()
        {
            const string REGISTRY_ROOT = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList\";

            profiles.Add("All Users", ".DEFAULT");
            using (RegistryKey rootKey = Registry.LocalMachine.OpenSubKey(REGISTRY_ROOT))
            {
                string[] profileSIDs = rootKey.GetSubKeyNames();
                foreach (string currSID in profileSIDs)
                {
                    if (currSID.StartsWith("S-1-5-21"))
                    {
                        using (RegistryKey currSubKey = Registry.LocalMachine.OpenSubKey(REGISTRY_ROOT + currSID))
                        {
                            string value = currSubKey.GetValue("ProfileImagePath").ToString();
                            string[] path = value.Split('\\');
                            string username = path[path.Length - 1];
                            profiles.Add(username, currSID);
                            currSubKey.Close();
                        }
                    }
                }
                rootKey.Close();
            }
            foreach (string key in profiles.Keys)
            {
                comboBoxUser.Items.Add(key);
            }
        }

        private void comboBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUser.SelectedItem != null)
            {
                runatLogonUser = profiles[comboBoxUser.SelectedItem.ToString()];
            }
            else
            {
                runatLogonUser = "";
                checkBoxRunatLogon.Checked = false;
            }
            SetOKbuttonStatus();
        }
    }
}
