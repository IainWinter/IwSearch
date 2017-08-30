using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static IwSearch.MiscStructsAndEnums;

namespace IwSearch {
    class Options {
        //Root of save file, gets used to define _fileTypeSave
        private string save;

        //Path to fileType file save
        public string varsSave;

        //If the application has been opend
        public bool hasBeenOpend;

        //If searcher shoul.d only search text file or not
        public bool searchOnlyInTextFiles;

        //How big the buffer can be for searching files
        public int maxBufferSize;

        //Max size of files that will get searched
        public bool searchLargeFilesToEnd;

        //Define "large files"
        public int maxSearchIterations;

        //If the searcher should stop at first match of line in file or keep going
        public bool returnMultipleLinesInFile;

        //Maximuin lines to find in a file
        public int maxReturnLines;

        //NeedsInFile
        //CanBeInFile
        //DoesntNeedSearch
        public SearchType searchType;

        public Options() {
            save = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).Replace("\\", "/") + "/Documents/IwSearch/";
            varsSave = save + "Vars/";
            hasBeenOpend = false;
            searchOnlyInTextFiles = false;
            returnMultipleLinesInFile = false;
            searchType = SearchType.DoesntNeedSearch;
            maxBufferSize = 1024;
            searchLargeFilesToEnd = false;
            maxSearchIterations = 100;
            maxReturnLines = 10;
        }

        public void Save() {
            using (StreamWriter sw = new StreamWriter(varsSave + "options.txt")) {
                sw.WriteLine(hasBeenOpend);
                sw.WriteLine(searchOnlyInTextFiles);
                sw.WriteLine(searchType);
                sw.WriteLine(maxBufferSize);
                sw.WriteLine(returnMultipleLinesInFile);
                sw.WriteLine(maxReturnLines);
                sw.WriteLine(searchLargeFilesToEnd);
                sw.WriteLine(maxSearchIterations);
            }
        }

        public void Load() {
            try {
                string[] options = File.ReadAllLines(varsSave + "options.txt");
                hasBeenOpend = bool.Parse(options[0]);
                searchOnlyInTextFiles = bool.Parse(options[1]);
                searchType = (SearchType)Enum.Parse(typeof(SearchType), options[2]);
                maxBufferSize = int.Parse(options[3]);
                returnMultipleLinesInFile = bool.Parse(options[4]);
                maxReturnLines = int.Parse(options[5]);
                searchLargeFilesToEnd = bool.Parse(options[6]);
                maxSearchIterations = int.Parse(options[7]);
            } catch (Exception e) {
                //Options file is damaged in some form
                Save();
                Load();
            }
        }

        public void SetCheckboxOption(CheckBox checkBox) {
            if (checkBox.Name.Equals("searchOnlyInTextFiles_checkBox"))
                searchOnlyInTextFiles = (bool)checkBox.IsChecked;
            if (checkBox.Name.Equals("returnOnFirstInstanceOfLine_checkBox"))
                returnMultipleLinesInFile = (bool)checkBox.IsChecked;
            if (checkBox.Name.Equals("searchLargeFiles_checkBox"))
                searchLargeFilesToEnd = (bool)checkBox.IsChecked;
        }

        public void SetComboBoxOption(ComboBox comboBox) {
            if (comboBox.Name.Equals("searchType_comboBox")) {
                switch (comboBox.SelectedIndex) {
                    case 0: searchType = SearchType.NeedsInFile; break;
                    case 1: searchType = SearchType.CanBeInFile; break;
                    case 2: searchType = SearchType.DoesntNeedSearch; break;
                }
            }
        }
    }
}
