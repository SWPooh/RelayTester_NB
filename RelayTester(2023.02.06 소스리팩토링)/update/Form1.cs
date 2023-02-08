using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.IO;
using Ionic.Zip;
using System.Diagnostics;
using System.Threading;
using Daeati;

namespace update
{
    //Procesing
    //Remove previous download file
    //Download file
    //Unzip file contents to temp folder
    //Remove files from destination folder present in temp folder
    //Move unzipped files to destination folder
    //Remove download file
    //Remove temp folder
    public partial class Form1 : Form
    {
        bool called = true;

        private string tempDownloadFolder = "";
        private string processToEnd = "";
        private string downloadFile = "";
        private string URL = "";
        private string destinationFolder = "";
        private string updateFolder = Application.StartupPath + @"\updates\";
        private string postProcessFile = "";
        private string postProcessCommand = "";

        delegate void SetLabelCallback(Label label, string text);
        public void SetLabel(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                SetLabelCallback d = new SetLabelCallback(SetLabel);
                label.Invoke(d, new object[] { label, text });
            }
            else
            {
                label.Text = text;
                label.Refresh();
                Invalidate();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Hide();

            if (called)
            {
                WindowState = FormWindowState.Normal;
                //XP에서는 화면이 잘리는것 같아서 처리
                if (this.ClientRectangle.Height < progressBar1.Location.Y + progressBar1.Size.Height)
                {
                    this.Height = 21 + progressBar1.Location.Y + progressBar1.Size.Height * 2;
                }
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                progressBar1.Step = 1;
                progressBar1.Value = 0;

                //Show();

                BackgroundWorker bw = new BackgroundWorker();

                bw.DoWork -= new DoWorkEventHandler(backgroundWorker);
                bw.DoWork += new DoWorkEventHandler(backgroundWorker);
                bw.WorkerSupportsCancellation = true;
                bw.RunWorkerAsync();

            }
        }
        private void backgroundWorker(object sender, DoWorkEventArgs e)
        {

            preDownload();

            if (called)
            {
                WindowState = FormWindowState.Normal;
                SetLabel(line1, "앱 실행 종료중 - " + processToEnd);
                Thread.Sleep(1000);

                try
                {
                    Process[] processes = Process.GetProcesses();
                    foreach (Process process in processes)
                    {
                        if (process.ProcessName == processToEnd)
                        {
                            process.Kill();
                        }
                    }
                }
                catch (Exception)
                { }
                
                //webdata.bytesDownloaded += Bytesdownloaded;
                SetLabel(line1, "업데이트 파일 다운로드중...");
                int filesize = webdata.downloadFromWeb_Size(URL, downloadFile, tempDownloadFolder);
                webdata.downloadFromWeb(URL, downloadFile, tempDownloadFolder, filesize);

                int i = 0;
                while (i++ < 50)
                {
                    progressBar1.PerformStep();
                    Thread.Sleep(100);
                }
                SetLabel(line1, "업데이트 파일 다운로드 완료");
                Thread.Sleep(500);

                unZip(tempDownloadFolder + downloadFile, tempDownloadFolder);
                SetLabel(line1, "압축 해제중...");
                while (i++ < 70)
                {
                    progressBar1.PerformStep();
                    Thread.Sleep(100);
                }

                moveFiles();
                SetLabel(line1, "파일 복사중...");
                while (i++ < 90)
                {
                    progressBar1.PerformStep();
                    Thread.Sleep(100);
                }

                wrapUp();
                SetLabel(line1, "임시파일 삭제중...");
                while (i++ < 100)
                {
                    progressBar1.PerformStep();
                    Thread.Sleep(100);
                }
                

                SetLabel(line1, "업데이트 완료");
                Thread.Sleep(500);

                if (postProcessFile != "") postDownload();

            }

            Close();


        }



        private void unpackCommandline()
        {

            string cmdLn = "";

            foreach (string arg in Environment.GetCommandLineArgs())
            {
                cmdLn += arg;

            }

            if (cmdLn.IndexOf('|') == -1)
            {
                called = false;
                info info = new info();
                info.ShowDialog();
                Close();
            }


            string[] tmpCmd = cmdLn.Split('|');

            for (int i = 1; i < tmpCmd.GetLength(0); i++)
            {
                if (tmpCmd[i] == "downloadFile") downloadFile = tmpCmd[i + 1];
                if (tmpCmd[i] == "URL") URL = tmpCmd[i + 1];
                if (tmpCmd[i] == "destinationFolder") destinationFolder = tmpCmd[i + 1];
                if (tmpCmd[i] == "processToEnd") processToEnd = tmpCmd[i + 1];
                if (tmpCmd[i] == "postProcess") postProcessFile = tmpCmd[i + 1];
                if (tmpCmd[i] == "command") postProcessCommand += @" /" + tmpCmd[i + 1];
                i++;
            }

        }

        private void unZip(string file, string unZipTo)
        {
            try
            {
                // Specifying Console.Out here causes diagnostic msgs to be sent to the Console
                // In a WinForms or WPF or Web app, you could specify nothing, or an alternate
                // TextWriter to capture diagnostic messages. 

                using (ZipFile zip = ZipFile.Read(file))
                {
                    // This call to ExtractAll() assumes:
                    //   - none of the entries are password-protected.
                    //   - want to extract all entries to current working directory
                    //   - none of the files in the zip already exist in the directory;
                    //     if they do, the method will throw.
                    zip.ExtractAll(unZipTo);
                }
            }
            catch (System.Exception)
            {
            }
        }


        private void preDownload()
        {

            if (!Directory.Exists(updateFolder)) Directory.CreateDirectory(updateFolder);

            tempDownloadFolder = updateFolder + DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) + @"\";

            if (Directory.Exists(tempDownloadFolder))
            {
                Directory.Delete(tempDownloadFolder, true);
            }

            Directory.CreateDirectory(tempDownloadFolder);

            unpackCommandline();

        }

        private void postDownload()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = postProcessFile;
            //startInfo.Arguments = postProcessCommand;
            startInfo.Arguments = "Updated";
            //MessageBox.Show(startInfo.FileName + "::::" + startInfo.Arguments);
            Process.Start(startInfo);
        }


        private void wrapUp()
        {
            if (Directory.Exists(tempDownloadFolder))
            {
                Directory.Delete(tempDownloadFolder, true);
            }

        }


        private void moveFiles()
        {
            DirectoryInfo di = new DirectoryInfo(tempDownloadFolder);
            FileInfo[] files = di.GetFiles();

            foreach (FileInfo fi in files)
            {
                if (fi.Name != downloadFile) File.Copy(tempDownloadFolder + fi.Name, destinationFolder + fi.Name, true);
            }

        }

        private void Bytesdownloaded(ByteArgs e)
        {
            progressBar1.Maximum = e.total;

            if (progressBar1.Value + e.downloaded <= progressBar1.Maximum)
            {
                progressBar1.Value += e.downloaded;
                SetLabel(line1, "업데이트 다운로드중...");
            }
            else
            {
                SetLabel(line1, "업데이트 다운로드 완료");
            }

            progressBar1.Refresh();
            Invalidate();

        }
    }
}
