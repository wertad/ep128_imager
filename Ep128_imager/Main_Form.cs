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
        public static string watchFolderPath = Functions.ReadAppConfig("watchFolder");
        public static List<string> floppyDrives = new List<string>();
        public static string selectedFloppyDrive = "";

        public Main_Form()
        {
            InitializeComponent();

            listFloppyDrives();
            textBox_watchFolder.Text = watchFolderPath;
            startWatching();
            //Functions.clearFloppy();
            
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
}
