using System;
using System.ServiceProcess;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace WaitForService
{
    public partial class Form1 : Form
    {
        int exitStatus = -1;
        string serviceName = "postgresql-x64-9.3";
        string programName = @"C:\Program Files (x86)\Fluke Calibration\LogWare III Client\LogWare3.exe";

        public Form1()
        {
            InitializeComponent();
            this.Text = serviceName;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int startAttempts = 0;
            string currentStatus;

            do
            {
                currentStatus = GetStatus(serviceName);
                switch (currentStatus)
                {
                    case "Running":
                        label1.Text = currentStatus;
                        try
                        {
                            Process.Start(programName);
                        }
                        catch (Exception ex)
                        {
                            ModalMessageBox(ex.Message);
                            Exit(1);
                        }
                        Exit(0);
                        break;
                    case "Stopped":
                        label1.Text = currentStatus;
                        var svc = new ServiceController(serviceName);
                        try
                        {
                            svc.Start();
                        }
                        catch (Exception ex)
                        {
                            ModalMessageBox(ex.Message);
                            Exit(2);
                        }
                        break;
                    case "StartPending":
                        label1.Text = "Starting";
                        progressBar1.MarqueeAnimationSpeed = 10;
                        if (startAttempts++ > 3)
                        {
                            ModalMessageBox("Too many start attempts.");
                            Exit(3);
                        }
                        break;
                    default:
                        ModalMessageBox(currentStatus);
                        Exit(4);
                        break;
                }

                do
                {
                    if (backgroundWorker1.CancellationPending == true) return;
                    Thread.Sleep(10);
                } while (GetStatus(serviceName).Equals(currentStatus));

                progressBar1.MarqueeAnimationSpeed = 0;

            } while (true);
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

        private void ModalMessageBox(string message)
        {
            Invoke(new MethodInvoker(() =>
            {
                MessageBox.Show(this, message);
            }));
        }

        private void Exit(int status)
        {
            exitStatus = status;
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Environment.Exit(exitStatus);
        }
   }
}

