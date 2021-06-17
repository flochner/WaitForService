using System;
using System.ServiceProcess;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WaitForService
{
    public partial class Form1 : Form
    {
        private int exitStatus = -1;
        private string svcName;// = "postgresql-x64-9.3";
        private string appName;// = @"C:\Program Files (x86)\Fluke Calibration\LogWare III Client\LogWare3.exe";
        private string appVis;

        public Form1()
        {
            InitializeComponent();
            if (LoadSaveSettings() == true)
                BackgroundWorker1.RunWorkerAsync();
            else
                Environment.Exit(exitStatus);
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int startAttempts = 0;
            string currentStatus;

            Invoke(new MethodInvoker(() => { this.Text = svcName; }));

            do
            {
                currentStatus = GetStatus(svcName);
                switch (currentStatus)
                {
                    case "Running":
                        Invoke(new MethodInvoker(() => { label1.Text = currentStatus; }));
                        try
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo
                            {
                                FileName = appName,
                                WindowStyle = (ProcessWindowStyle)int.Parse(appVis)
                            };
                            Process.Start(startInfo);
                        }
                        catch (Exception ex)
                        {
                            ModalMessageBox(ex.Message, ex.Source);
                            Exit(1);
                            break;
                        }
                        Exit(0);
                        break;
                    case "Stopped":
                        Invoke(new MethodInvoker(() => { label1.Text = currentStatus; }));
                        var svc = new ServiceController(svcName);
                        try
                        {
                            svc.Start();
                        }
                        catch (Exception ex)
                        {
                            ModalMessageBox(ex.Message, ex.Source);
                            Exit(2);
                        }
                        break;
                    case "StartPending":
                        Invoke(new MethodInvoker(() => { label1.Text = currentStatus; }));
                        Invoke(new MethodInvoker(() => { progressBar1.MarqueeAnimationSpeed = 10; }));
                        Thread.Sleep(10);
                        if (startAttempts++ > 10)
                        {
                            ModalMessageBox("Too many start attempts.", svcName);
                            Exit(3);
                        }
                        break;
                    default:
                        ModalMessageBox(currentStatus, svcName);
                        Exit(4);
                        break;
                }

                do
                {
                    Thread.Sleep(10);
                    if (BackgroundWorker1.CancellationPending == true) return;
                } while (GetStatus(svcName).Equals(currentStatus));

                Invoke(new MethodInvoker(() => { progressBar1.MarqueeAnimationSpeed = 0; }));

            } while (true);
        }

        private bool LoadSaveSettings()
        {
            string installPath;
            bool isInCVRun;
            bool configComplete = false;
            RegistryKey regKeyConfig = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ConRes\WaitForService", true);
            RegistryKey regKeyRun = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (regKeyConfig == null)
            {
                MessageBox.Show("Please use Add/Remove Programs to Modify/Repair the installation for the current user.");
                return false;
            }

            isInCVRun = !string.IsNullOrEmpty((string)regKeyRun.GetValue("WaitForService"));
            installPath = (string)regKeyConfig.GetValue("wfsInstallPath");
            svcName = (string)regKeyConfig.GetValue("Service");
            appName = (string)regKeyConfig.GetValue("Application");
            appVis = (string)regKeyConfig.GetValue("Visibility");

            Thread.Sleep(1000);
            if (string.IsNullOrEmpty(svcName) ||
                string.IsNullOrEmpty(appName) ||
                string.IsNullOrEmpty(appVis) ||
                Control.ModifierKeys == Keys.Shift)
            {
                Form2 settings = new Form2(svcName, appName, appVis, isInCVRun);
                settings.ShowDialog(this);

                if (settings.DialogResult == DialogResult.OK)
                {
                    svcName = settings.ServiceName;
                    appName = settings.AppName;
                    appVis = settings.AppVisibility;
                    if (settings.SaveSettings)
                    {
                        regKeyConfig.SetValue("Service", svcName);
                        regKeyConfig.SetValue("Application", appName);
                        regKeyConfig.SetValue("Visibility", appVis);
                        if (settings.RunAtLogon == false)
                        {
                            try { regKeyRun.DeleteValue("WaitForService"); }
                            catch { }
                        }
                        else
                        {
                            regKeyRun.SetValue("WaitForService", installPath);
                        }
                    }
                    configComplete = true;
                }
                settings.Dispose();
            }
            regKeyRun.Close();
            regKeyConfig.Close();
            return configComplete;
        }

        private string GetStatus(string serviceName)
        {
            ServiceController sc = new ServiceController(serviceName);
            try
            {
                return sc.Status.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void ModalMessageBox(string message, string source)
        {
            Invoke(new MethodInvoker(() =>
            {
                MessageBox.Show(this, message, source);
            }));
        }

        private void Exit(int status)
        {
            exitStatus = status;
            BackgroundWorker1.CancelAsync();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Environment.Exit(exitStatus);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            { 
                BackgroundWorker1.CancelAsync();
            }
        }
    }
}

