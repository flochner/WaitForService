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
        private string serviceName;// = "postgresql-x64-9.3";
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


            Invoke(new MethodInvoker(() => { this.Text = serviceName; }));

            do
            {
                currentStatus = GetStatus(serviceName);
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
                        var svc = new ServiceController(serviceName);
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
                            ModalMessageBox("Too many start attempts.", serviceName);
                            Exit(3);
                        }
                        break;
                    default:
                        ModalMessageBox(currentStatus, serviceName);
                        Exit(4);
                        break;
                }

                do
                {
                    Thread.Sleep(10);
                    if (BackgroundWorker1.CancellationPending == true) return;
                } while (GetStatus(serviceName).Equals(currentStatus));

                Invoke(new MethodInvoker(() => { progressBar1.MarqueeAnimationSpeed = 0; }));

            } while (true);
        }

        private bool LoadSaveSettings()
        {
            bool configComplete = true;
            RegistryKey regKeyConfig = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ConRes\WaitForService", true);

            serviceName = (string)regKeyConfig.GetValue("Service");
            appName = (string)regKeyConfig.GetValue("Application");
            appVis = (string)regKeyConfig.GetValue("Visibility");

            if (string.IsNullOrEmpty(serviceName) ||
                string.IsNullOrEmpty(appName) ||
                string.IsNullOrEmpty(appVis))
            {
                Form2 settings = new Form2(serviceName, appName, appVis);
                settings.ShowDialog();

                if (settings.DialogResult == DialogResult.OK)
                {
                    if (settings.SaveSettings)
                    {
                        regKeyConfig.SetValue("Service", settings.ServiceName);
                        regKeyConfig.SetValue("Application", settings.AppName);
                        regKeyConfig.SetValue("Visibility", settings.AppVis);
                    }
                }
                else
                {
                    configComplete = false;
                }
                settings.Dispose();
            }
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

