﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using Aspose.Zip.Rar;
using System.Diagnostics;

namespace Ep128_imager
{
    internal class Functions
    {
        //private static bool extracted = false;
        public static void watchFolder()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Main_Form.watchFolderPath;
            watcher.IncludeSubdirectories = false;
            watcher.NotifyFilter = NotifyFilters.CreationTime;
            watcher.Filter = "*.RAR";
            watcher.EnableRaisingEvents = true;
            watcher.InternalBufferSize = 24000;
            watcher.Changed += OnChanged;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            string directoryName = e.Name.Substring(0, e.Name.Length - 4);
            string newDirectory = Main_Form.watchFolderPath + @"\" + directoryName + @"\src";

            Directory.CreateDirectory(newDirectory);
            extractRAR(e.FullPath, newDirectory);
            deleteRAR(e.FullPath);
            clearFloppy(); // letörli a kiválasztott floppy driveról az összes fájlt, kivéve az EXDOS.INI-t
            copyGameFiles(newDirectory); // átmásolja a kitömörített játék fájljait a floppy drive-ra
            generateExdosIni(); // legenerálja a megfelelő INI fájlt.
            createImgFile(directoryName);
        }

        //
        // App.Config fájlba írás
        //
        public static void WriteAppConfig(string key, string value)
        {
            try
            {
                //Ahhoz, hogy használni tujda a ConfigurationManagert a következőket kell tenni:
                //
                //You have also to add the reference to the assembly System.Configuration.dll , by
                //1. - Right - click on the References / Dependencies
                //2. - Choose Add Reference
                //3. - Find and add System.Configuration.

                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        public static string ReadAppConfig(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                return "Error";
            }
        }

        public static void extractRAR(string filePath, string destinationFolder)
        {
            // Ha létezik a megkapott tömörített fájl és nem nagyobb eleve 800KB-nál,
            // akkor kitömöríti a megadott folderbe.
            FileInfo file = new FileInfo(filePath);
            if ((File.Exists(filePath)) && ((file.Length < 809984)))
            {
                using (RarArchive archive = new RarArchive(filePath))
                {
                    archive.ExtractToDirectory(destinationFolder);
                }
            }
            else
                System.Windows.Forms.MessageBox.Show(filePath + " is dosen't exist or archive is too big.");
        }
        private static void deleteRAR(string filePath)
        {
            // csak akkor töröl, ha .rar vagy .RAR kiterjesztésű fájlt kap
            // biztos, ami biztos
            if ((filePath.Substring(filePath.Length - 4, 4) == ".rar") || (filePath.Substring(filePath.Length - 4, 4) == ".RAR"))
            {
                    File.Delete(filePath);
            }
        }

        private static void clearFloppy()
        {
            //letörli a kiválasztott floppy driveról az összes fájlt, kivéve az EXDOS.INI-t

            DirectoryInfo FloppyDrive = new DirectoryInfo(Main_Form.selectedFloppyDrive);

            // biztos, ami biztos csak akkor engedi tovább a programot
            // ha az átadott elérési úton 800KB-nál több adat nincs.
            // mivel nem akarom, hogy akárhogy is egy másik útvonaról töröljön

            long totalSize = FloppyDrive.EnumerateFiles().Sum(f => f.Length);
            if (totalSize > 809984)
            {
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                FileInfo[] FloppyFiles = FloppyDrive.GetFiles();
                foreach (FileInfo FloppyFile in FloppyFiles)
                {
                    if (FloppyFile.Name == "EXDOS.INI")
                        continue;
                    else
                    {
                        // +1 biztonsági lépés, ha nagyobb lenne a törlendő fájl 800KB-nál, akkor azonnal álljon le a folyamat.
                        if (FloppyFile.Length > 809984)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine(FloppyFile.FullName);
                            File.SetAttributes(FloppyFile.FullName, FileAttributes.Normal); // enélkül nincs joga törölni a fájlt
                            File.Delete(FloppyFile.FullName);
                        }
                    }
                }
            }
        }

        public static void DiskInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                //There are more attributes you can use.
                //Check the MSDN link for a complete example.

                if (drive.IsReady && drive.TotalSize == 809984)
                    Main_Form.floppyDrives.Add(drive.Name);
            }
        }

        public static void copyGameFiles(string sourceFolder)
        {
            DirectoryInfo gameFolder = new DirectoryInfo(sourceFolder);
            FileInfo[] gameFiles = gameFolder.GetFiles();
            foreach (var item in gameFiles)
            {
                File.Copy(item.FullName, Main_Form.selectedFloppyDrive + item.Name);
            }
        }

        private static string getBinFileName()
        {
            string binFileName = "";

            DirectoryInfo floppyDrive = new DirectoryInfo(Main_Form.selectedFloppyDrive);
            FileInfo[] floppyFiles = floppyDrive.GetFiles();
            foreach (var item in floppyFiles)
            {
                if (item.Extension == ".COM")
                {
                    binFileName = item.Name;
                    break;
                }
            }

            return binFileName;
        }

        private static void generateExdosIni()
        {
            File.SetAttributes(Main_Form.selectedFloppyDrive + "EXDOS.INI", FileAttributes.Normal);
            File.WriteAllText(Main_Form.selectedFloppyDrive + "EXDOS.INI", "load " + getBinFileName());
            File.SetAttributes(Main_Form.selectedFloppyDrive + "EXDOS.INI", FileAttributes.Hidden);
        }

        public static void createImgFile(string fileName)
        {
            string driveLetter = Main_Form.selectedFloppyDrive.Substring(0, 1);
            string directoryName = fileName;
            string watchFolder = Main_Form.watchFolderPath;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/c start " + baseDirectory + @"\rawcopy.exe -l \\.\" + driveLetter + @": " + watchFolder + @"\" + directoryName + @"\" + fileName + ".img";
            startInfo.Verb = "runas";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
