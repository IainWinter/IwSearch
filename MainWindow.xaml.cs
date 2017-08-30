using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using static IwSearch.MiscStructsAndEnums;

namespace IwSearch {
    public partial class MainWindow : Window {
        Options options;

        public MainWindow() {
            InitializeComponent();
            options = new Options();
            new Setup().Start(this, options);
            if (Environment.GetCommandLineArgs().Length > 1) {
                SetOriginalValues(Environment.GetCommandLineArgs());
            }
        }

        //Window
        private void Window_Closing(object sender, CancelEventArgs e) { StopSearch(); options.Save(); }

        //Options tab
        private void option_checkBox_Clicked(object sender, RoutedEventArgs e) { options.SetCheckboxOption((CheckBox)sender); }
        private void option_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { options.SetComboBoxOption((ComboBox)sender); }
        private void enableSearching_Button_Click(object sender, RoutedEventArgs e) { new Setup().AddOrRemoveRegistryKey("Add"); }
        private void removeSearching_Button_Click(object sender, RoutedEventArgs e) { new Setup().AddOrRemoveRegistryKey("Remove"); }

        //Search tab
        private void startSearch_Button_Click(object sender, RoutedEventArgs e) { Search(); }
        private void stopSearch_Button_Click(object sender, RoutedEventArgs e) { StopSearch(); }
        private void hyperlink_Click(object sender, RoutedEventArgs e) { OpenResultPathInExplorer(e); }
        private void results_dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { UpdateFileRead((DataGrid)sender); }
        private void browse_button_Click(object sender, RoutedEventArgs e) { Browse(); }

        //Log tab
        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) { /*OpenLogDataInMessageBox();*/ }
        private void clearLog_button_Click(object sender, RoutedEventArgs e) { /*ClearLog();*/ }

        private void SetOriginalValues(string[] args) {
            tabControl.SelectedValue = tab_Search;
            path_TextBox.Text = args[1];
            name_TextBox.Focus();
        }

        //Creates new searcher thread
        private void Search() {
            if (!Searcher._isRunning) {
                //Validate input
                if (minsize_TextBox.Text.Equals("")) minsize_TextBox.Text = "0";
                if (minsize_ComboBox.SelectedIndex == -1) minsize_ComboBox.SelectedIndex = 0;
                if (maxsize_TextBox.Text.Equals("")) maxsize_TextBox.Text = "0";
                if (maxsize_ComboBox.SelectedIndex == -1) maxsize_ComboBox.SelectedIndex = 0;
                if (minDateMod_date.SelectedDate > maxDateMod_date.SelectedDate) {
                    DateTime? tmp = minDateMod_date.SelectedDate;
                    minDateMod_date.SelectedDate = maxDateMod_date.SelectedDate;
                    maxDateMod_date.SelectedDate = tmp;
                }
                if (minDateCre_date.SelectedDate > maxDateCre_date.SelectedDate) {
                    DateTime? tmp = minDateCre_date.SelectedDate;
                    minDateCre_date.SelectedDate = maxDateCre_date.SelectedDate;
                    maxDateCre_date.SelectedDate = tmp;
                }

                long minFileSize = long.Parse(minsize_TextBox.Text) * Convert.ToInt64(Math.Pow(1024, Convert.ToDouble(minsize_ComboBox.SelectedIndex)));
                long maxFileSize = long.Parse(maxsize_TextBox.Text) * Convert.ToInt64(Math.Pow(1024, Convert.ToDouble(maxsize_ComboBox.SelectedIndex)));

                Searcher searcher = SearchBuilder.Searcher()
                    .withPath(path_TextBox.Text.Replace("/", "\\"))
                    .withName(name_TextBox.Text)
                    .withLinesInFile(inFile_TextBox.Text)
                    .withType(type_TextBox.Text)
                    .withMinSize(minFileSize)
                    .withMaxSize(maxFileSize)
                    .withMinDateModified(minDateMod_date.SelectedDate)
                    .withMaxDateModified(maxDateMod_date.SelectedDate)
                    .withMinDateCreated(minDateCre_date.SelectedDate)
                    .withMaxDateCreated(maxDateCre_date.SelectedDate)
                    .Build();

                Results results = new Results(this);

                Thread th = new Thread(() => {
                    searcher.StartSearchWrapper(options, results, this);
                });
                th.Start();
                results_dataGrid.ItemsSource = results.results;
            }
        }

        public void UpdateInformation(long filesSearched, long filesContentSearched, int threadsRunning, int resultsFound, ElapsedTime elapsedTime) {
            try {
                Dispatcher.Invoke(() => {
                    filesSearched_textBlock.Text = filesSearched + "";
                    filescontentsearched_textBlock.Text = filesContentSearched + "";
                    elapsedTime_textBlock.Text = TimeSpan.FromMilliseconds(elapsedTime.MillisSince()).ToString("hh':'mm':'ss'.'fff");
                    resultsRound_textBlock.Text = resultsFound + "";
                    threadsSearching_textBlock.Text = threadsRunning + "";
                });
            } catch (TaskCanceledException e) {
                Console.WriteLine(e);
            }
        }

        private void StopSearch() {
            Searcher.StopSearch();
        }

        private void UpdateFileRead(DataGrid dataGrid) {
            if (dataGrid.SelectedIndex == -1) return;
            string path = (string)((DisplayResult)dataGrid.SelectedValue).path;
            int[] positions = ((string)((DisplayResult)dataGrid.SelectedValue).lineIndex).Split(new char[] { ',' }).Select(c => int.Parse(c)).ToArray();
            IEnumerable<string> fileContents = File.ReadLines(path, Encoding.Default);

            fileRead_textBox.Document.Blocks.Clear();
            if(fileContents.Count() > 0) {
                foreach(int position in positions) {
                    try {
                        string text = fileContents.Skip(position - 1).Take(1).First();
                        Paragraph p = new Paragraph(new Run(position + " ---- " + text));
                        fileRead_textBox.Document.Blocks.Add(p);
                    } catch(Exception e) {
                        Console.WriteLine("An error occured while reading from a file" + e);
                    }
                }
            } else {
                fileRead_textBox.Document.Blocks.Add(new Paragraph(new Run("No Data In File")));
            }
        }

        private void Browse() {
            VistaFolderBrowserDialog vfbd = new VistaFolderBrowserDialog();
            vfbd.ShowDialog();
             path_TextBox.Text = vfbd.SelectedPath;
        }

        //Limit textboxes to numericInput
        private void LimitToNumeric(object sender, TextChangedEventArgs e) {
            Regex regex = new Regex("[^0-9]");
            if (regex.IsMatch(((TextBox)sender).Text)) {
                ((TextBox)sender).Text = regex.Replace(((TextBox)sender).Text, "");
            }
        }

        private void OpenResultPathInExplorer(RoutedEventArgs e) {
            Hyperlink link = (Hyperlink)e.OriginalSource;
            Process.Start("explorer.exe", "/select, \"" + link.NavigateUri.AbsoluteUri + "\"");
        }
    }
}