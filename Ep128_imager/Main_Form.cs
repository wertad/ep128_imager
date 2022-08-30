using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ep128_imager
{
    public partial class Main_Form : Form
    {
        public static Main_Form _Main_Form;

        public static string watchFolderPath = Functions.ReadAppConfig("watchFolder");
        public static List<string> floppyDrives = new List<string>();
        public static string selectedFloppyDrive = "";

        public Main_Form()
        {
            InitializeComponent();
            _Main_Form = this;
            
            wtiteToConsole($"Monitoring of watch folder has started...");

            listFloppyDrives();
            textBox_watchFolder.Text = watchFolderPath;
            startWatching();
            //Functions.clearFloppy();
            
        }
        public void wtiteToConsole(string message)
        {
            ThreadProcSafe(message);
        }
        private void ThreadProcSafe(string message)
        {
            string now = DateTime.Now.ToString("HH:mm:ss");
            ThreadHelperClass.SetText(this, richTextBox_console, $"{now}> {message}\n");
        }

        private void listFloppyDrives()
        {
            // belistázza a Floppy Drive comboboxba azokat a driveokat amelyek pontosan 809984 byte méretűek
            // azaz 800KB-os floppy drive-ok
            // ez azért szükséges, mert meg akarom előzni, hogy véletlenül más drive-ról töröljön a program
            Functions.DiskInfo();
            foreach (var item in floppyDrives)
            {
                comboBox_ImageDrives.Items.Add(item);
            }
            if (comboBox_ImageDrives.Items.Count > 0)
                comboBox_ImageDrives.SelectedIndex = 0;
        }

        private void textBox_watchFolder_TextChanged(object sender, EventArgs e)
        {
            watchFolderPath = textBox_watchFolder.Text;
            Functions.WriteAppConfig("watchFolder", watchFolderPath);
        }

        private void button_watchBrowser_Click(object sender, EventArgs e)
        {
            browseFolder();
        }

        public void browseFolder()
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_watchFolder.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        public void startWatching()
        {
            // Ha a megadott folder nem létezik, akkor meg kell adni. 

            if (Directory.Exists(watchFolderPath))
            {
                Functions.watchFolder();
            }
            else
            {
                MessageBox.Show("Watch folder dosen't exist!");
                browseFolder();
                startWatching();
            }
        }

        private void comboBox_ImageDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFloppyDrive = comboBox_ImageDrives.Text;
        }
    }

    public static class ThreadHelperClass
    {
        delegate void SetTextCallback(Form f, Control ctrl, string text);
        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(Form form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text += text;
            }
        }
    }
}
