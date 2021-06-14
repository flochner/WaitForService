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
        public string AppName { get => appName; }
        public string AppVis { get => appVis; }
        public string ServiceName { get => serviceName; }

        private bool saveSettings;
        private string appName;
        private string appVis;
        private string serviceName;
        private List<string> serviceList = new List<string>();

        public Form2(string sN, string aN, string aV)
        {
            InitializeComponent();

            serviceName = sN; appName = aN; appVis = aV;

            comboBoxService.Text = serviceName;
            textBoxApp.Text = appName;
            if (string.IsNullOrEmpty(appVis))
                comboBoxVisibility.SelectedIndex = -1;
            else
                comboBoxVisibility.SelectedIndex = int.Parse(appVis);

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
            appVis = comboBoxVisibility.SelectedIndex.ToString();
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

        private void comboBoxVisibility_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOKbuttonStatus();
        }

        private void SetOKbuttonStatus()
        {
            if (string.IsNullOrEmpty(comboBoxService.Text) || string.IsNullOrEmpty(textBoxApp.Text) || string.IsNullOrEmpty(comboBoxVisibility.Text))
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
