using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace WaitForService
{
    public partial class WaitForService : Form
    {
        private int exitStatus = -1;
        private int appVis;
        private string svcName;// = "postgresql-x64-9.3";
        private string appName;// = @"C:\Program Files (x86)\Fluke Calibration\LogWare III Client\LogWare3.exe";

        public WaitForService()
        {
            InitializeComponent();
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int startAttempts = 0;
            string currentStatus;

            Invoke(new MethodInvoker(() => { this.Text = svcName; }));

            if (LoadSettings() == false)
                BackgroundWorker1.CancelAsync();
            Thread.Sleep(10);

            do
            {
                if (BackgroundWorker1.CancellationPending == true) return;

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
                                WindowStyle = (ProcessWindowStyle)appVis
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

        private bool LoadSettings()
        {
            RegistryKey regKeyConfig = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ConRes\WaitForService");
            svcName = (string)regKeyConfig.GetValue("Service");
            appName = (string)regKeyConfig.GetValue("Application");
            appVis = (int)regKeyConfig.GetValue("Visibility");
            
            Thread.Sleep(1000);
            if (string.IsNullOrEmpty(svcName) ||
                string.IsNullOrEmpty(appName) ||
                appVis < 0 ||
                Control.ModifierKeys == Keys.Shift)
            {
                using (Process config = Process.Start("Configure.exe"))
                {
                    config.WaitForExit();
                    if (config.ExitCode != 0)
                        return false;
                }
                svcName = (string)regKeyConfig.GetValue("Service");
                appName = (string)regKeyConfig.GetValue("Application");
                appVis = (int)regKeyConfig.GetValue("Visibility");
            }

            regKeyConfig.Close();
            return true;
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

        private void WaitForService_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BackgroundWorker1.CancelAsync();
            }
        }
    }
}

