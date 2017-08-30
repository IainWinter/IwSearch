using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static IwSearch.MiscStructsAndEnums;

namespace IwSearch {
    class Setup {
        public void Start(MainWindow mainWindow, Options options) {
            CreateSaveFileIfNeeded(options);
            LoadOptions(options);
            if (!options.hasBeenOpend) AddOrRemoveRegistryKey("Add");
            options.hasBeenOpend = true;
            DisplayOptions(mainWindow, options);
        }

        private void CreateSaveFileIfNeeded(Options options) {
            Directory.CreateDirectory(options.varsSave);
            if (!File.Exists(options.varsSave + "options.txt")) options.Save();
        }

        private void LoadOptions(Options options) {
            options.Load();
        }

        private void DisplayOptions(MainWindow mainWindow, Options options) {
            mainWindow.searchOnlyInTextFiles_checkBox.IsChecked = options.searchOnlyInTextFiles;
            mainWindow.returnOnFirstInstanceOfLine_checkBox.IsChecked = options.returnMultipleLinesInFile;
            mainWindow.searchType_comboBox.SelectedIndex = (int)options.searchType;
            mainWindow.searchLargeFiles_checkBox.IsChecked = options.searchLargeFilesToEnd;
        }

        public void AddOrRemoveRegistryKey(string option) {
            var process = new ProcessStartInfo(Environment.CurrentDirectory + "\\AddOrRemoveIwSearchFromContextMenus.exe", option);
            process.UseShellExecute = true;
            process.Verb = "runas";
            try {
                var action = Process.Start(process);
            } catch(Exception e) {
                Console.WriteLine(e);
                MessageBox.Show("Admin privileges are required to add or remove the option to search " +
                    "folders with IwSearch from Explorer. However, IwSearch will work " +
                    "normally without admin privileges. This can be enabled " +
                    "again from the 'Options' tab.", "Alert");
            } 
        }
    }
}
