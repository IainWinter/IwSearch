using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IwSearch.MiscStructsAndEnums;

namespace IwSearch {
    class Setup {
        public void Start(MainWindow mainWindow, Options options) {
            CreateSaveFileIfNeeded(options);
            LoadOptions(options);
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
    }
}
