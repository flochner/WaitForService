﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;


namespace WaitForService
{
    public partial class Form2 : Form
    {
        private List<string> serviceList = new List<string>();

        public Form2(string serviceName, string appName, string windowState)
        {
            InitializeComponent();
            textBoxApp.Text = appName;
            PopulateServices();
        }

        private void PopulateServices()
        {
            comboBoxService.Items.Clear();
            serviceList.Clear();
            this.Text = "Configuration";

            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController service in services)
            {
                RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + service.ServiceName);
                var imagePath = regKey1.GetValue("ImagePath").ToString();
                if (checkBoxMSsvcs.Checked && !(imagePath.Contains("Windows") || imagePath.Contains("windows") || imagePath.Contains("WINDOWS")))
                {
                    comboBoxService.Items.Add(service.ServiceName);
                    if (regKey1.GetValue("DisplayName") != null)
                       serviceList.Add(regKey1.GetValue("DisplayName").ToString()); 
                }
                else if (!checkBoxMSsvcs.Checked)
                {
                    comboBoxService.Items.Add(service.ServiceName);
                    if (regKey1.GetValue("DisplayName") != null)
                        serviceList.Add(regKey1.GetValue("DisplayName").ToString());
                }
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
            try
            {
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Form1.serviceName = comboBoxService.Text;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

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

            //user mouse is hovering over this drop-down item, update its data  
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                this.Text = serviceList[e.Index];
            }

            e.DrawBackground();

            if (e.State.ToString().Contains("Selected"))
            {
                e.Graphics.DrawString(comboBoxService.Items[e.Index].ToString(), e.Font, Brushes.White, new Point(e.Bounds.X, e.Bounds.Y));
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
