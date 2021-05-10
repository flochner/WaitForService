using System;
using System.ServiceProcess;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace WaitForService
{
    public partial class Form1 : Form
    {
        int exitStatus = -1;
        string serviceName;// = "postgresql-x64-9.3";
        string programName;// = @"C:\Program Files (x86)\Fluke Calibration\LogWare III Client\LogWare3.exe";
        string windowState;

        public Form1()
        {
            InitializeComponent();            
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int startAttempts = 0;
            string currentStatus;

            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load("WaitForService.exe.config");
            }
            catch (Exception ex)
            {
                ModalMessageBox(ex.Message, ex.Source);
                return;
            }

            programName = xDoc.GetElementsByTagName("Application").Item(0).InnerText;
            serviceName = xDoc.GetElementsByTagName("Service").Item(0).InnerText;
            windowState = xDoc.GetElementsByTagName("WindowState").Item(0).InnerText;

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
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.FileName = @programName;
                            startInfo.WindowStyle = (ProcessWindowStyle)Convert.ToInt32(windowState);
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
                    if (backgroundWorker1.CancellationPending == true) return;
                } while (GetStatus(serviceName).Equals(currentStatus));

                Invoke(new MethodInvoker(() => { progressBar1.MarqueeAnimationSpeed = 0; }));

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
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Environment.Exit(exitStatus);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            { 
                backgroundWorker1.CancelAsync();
            }
        }
    }
}

