using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Permissions;
using ParkingServer.Properties;

using System.Linq;

namespace ParkingServer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if(File.Exists(Settings.Default.MonitorFile))
            {
                // wait
                return;
            }

            // get results
            DirectoryInfo di = new DirectoryInfo(Settings.Default.ResultFolder);
            FileInfo[] resultsFiles = di.GetFiles("*.jpg");
            if(resultsFiles.Length == 0)
            {
                return;
            }
            FileInfo[] txtFiles = di.GetFiles(".txt");
            if (resultsFiles.Length == 0)
            {
                return;
            }


            ParkingView.Image = new Bitmap(resultsFiles[0].FullName);

            //process text file
            FileInfo textFile = txtFiles[0];
            // process

            File.Move(resultsFiles[0].FullName, Path.Combine(Settings.Default.ArchiveFolder, resultsFiles[0].Name));
            File.Move(textFile.FullName, Path.Combine(Settings.Default.ArchiveFolder, textFile.Name));
                        
            // push next image
            FileInfo[] nextImages = di.GetFiles();

            var nextFile = (from f in nextImages orderby f.LastWriteTimeUtc descending select f).FirstOrDefault();

            if(nextFile != null)
            {
                File.Move(nextFile.FullName, Settings.Default.MonitorFile, true);
            }

        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            StartBtn.Enabled = false;
            StopBtn.Enabled = true;           
        }

        private void NewResult_Created(object sender, FileSystemEventArgs e)
        {
            string imagePath = Path.Combine(Settings.Default.ResultFolder, e.Name.Replace(".txt", "") + ".jpg");
            if (File.Exists(imagePath))
            {
                ParkingView.Image = new Bitmap(imagePath);
            }

            using (StreamReader file = new StreamReader(e.FullPath))
            {              
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    // to do process boxes                
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            StartBtn.Enabled = true;
            StopBtn.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if(File.Exists(Settings.Default.MonitorFile))
            { 
                ParkingView.Image = new Bitmap(Settings.Default.MonitorFile);
            }
        }
    }
}
