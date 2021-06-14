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
        private List<string> serviceList = new List<string>();
        public string ServiceName { get => serviceName; }
        public string AppName { get => appName; }
        public string AppStart { get => appStart; }
        public bool SaveSettings { get => saveSettings; }

        private bool saveSettings;
        private string serviceName;
        private string appName;
        private string appStart;

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

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Program Files";
            openFileDialog.Filter = "Programs (*.exe;*.com)|*.exe;*.com|Batch Files (*.bat;*.cmd)|*.bat;*.cmd|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            try
            {
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
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
            if (string.IsNullOrEmpty(comboBoxService.Text) || string.IsNullOrEmpty(textBoxApp.Text) || string.IsNullOrEmpty(comboBoxStartup.Text))
            {
                buttonOK.Enabled = false;
            }
            else
            {
                buttonOK.Enabled = true;
            }
        }
    }
}
