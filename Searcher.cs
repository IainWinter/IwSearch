using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static IwSearch.MiscStructsAndEnums;

namespace IwSearch {
    class Searcher {
        private SearchQuery searchQuery;

        private static bool stop;

        public static bool _isRunning = false;

        //Information
        private int threadsRunning;
        private int resultsFound;
        private long filesSearched;
        private long filesContentSearched;
        private ElapsedTime elapsedTime;

        public Searcher(SearchQuery searchQuery) {
            this.searchQuery = searchQuery;
            stop = false;
            threadsRunning = 0;
            filesSearched = 0;
            filesContentSearched = 0;
            elapsedTime = new ElapsedTime();
        }

        public static void StopSearch() {
            stop = true;
        }

        public void StartSearchWrapper(Options options, Results results, MainWindow mainWindow) {
            _isRunning = true;

            StartSearch(options, results);
            while (threadsRunning > 0 && !stop) {
                SendInformation(mainWindow);
            }
            //Update the last thread to 0
            threadsRunning = 0;
            SendInformation(mainWindow);

            _isRunning = false;
        }

        private void SendInformation(MainWindow mainWindow) {
            mainWindow.UpdateInformation(filesSearched, filesContentSearched, threadsRunning, resultsFound, elapsedTime);
        }

        private void StartSearch(Options options, Results results) {
            Thread th = new Thread(() => {
                foreach (string r in GetStartRoot()) {
                    foreach (string d in Directory.GetDirectories(r)) {
                        if (stop) break; //Kill thread when stop is true

                        if (!new DirectoryInfo(d).Attributes.HasFlag(FileAttributes.Hidden)) {
                            if (d.Contains(searchQuery.path)) {
                                StartSearcherThread(d, options, results);
                            }
                        }
                    }
                    StartRootSearcherThread(r, options, results);
                }
                Interlocked.Decrement(ref threadsRunning);
            });
            threadsRunning++;
            th.Start();
        }

        public string[] GetStartRoot() {
            if (Directory.Exists(searchQuery.path)) return new string[] { searchQuery.path };
            else return Directory.GetLogicalDrives();
        }

        private void StartSearcherThread(string d, Options options, Results results) {
            ThreadPool.QueueUserWorkItem(new WaitCallback(SearchWrapper), new object[] { d, options, results });
            Interlocked.Increment(ref threadsRunning);
        }

        private void StartRootSearcherThread(string d, Options options, Results results) {
            ThreadPool.QueueUserWorkItem(new WaitCallback(SearchAllFilesInDirectoryWrapper), new object[] { d, options, results });
            Interlocked.Increment(ref threadsRunning);
        }

        private void SearchWrapper(object state) {
            object[] states = (object[])state;
            string root = (string)states[0];
            Options options = (Options)states[1];
            Results results = (Results)states[2];
            Search(root, options, results);
            Interlocked.Decrement(ref threadsRunning);
        }

        private void Search(string root, Options options, Results results) {
            SearchAllFilesInDirectory(root, options, results);
            foreach (string d in Directory.GetDirectories(root)) {
                if (stop) break; //Kill thread when stop is true

                if (!new DirectoryInfo(d).Attributes.HasFlag(FileAttributes.Hidden)) {
                    try {
                        Search(d, options, results);
                    } catch (Exception e) {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        private void SearchAllFilesInDirectoryWrapper(object state) {
            object[] states = (object[])state;
            string root = (string)states[0];
            Options options = (Options)states[1];
            Results results = (Results)states[2];
            SearchAllFilesInDirectory(root, options, results);
            Interlocked.Decrement(ref threadsRunning);
        }

        private void SearchAllFilesInDirectory(string d, Options options, Results results) {
            foreach (string f in Directory.GetFiles(d)) {
                if (stop) break; //Kill thread when stop is true

                SearchResult sr = GetFileInformationAsSearchResult(f);
                int[] lineIndex = SearchFile(sr, options);
                if (lineIndex.Length > 0) {
                    int[] linePosition = lineIndex.Select(i => i += 1).ToArray();
                    sr.linePosition = string.Join(",", linePosition);
                    results.AddResult(sr);
                    resultsFound++;
                }
            }
        }

        //[-1] not in file
        //[] not right file
        private int[] SearchFile(SearchResult sr, Options options) {
            filesSearched++;
            if (FileMeetRequirments(sr)) {
                switch (options.searchType) {
                    case SearchType.DoesntNeedSearch: return new int[] { -1 };
                    case SearchType.CanBeInFile: return SearchInFile(sr, options);
                    case SearchType.NeedsInFile: {
                            if (searchQuery.inFile.Length == 0) return new int[] { 0 }; 
                            int[] index = SearchInFile(sr, options);
                            if (index[0] > -1) return index;
                            else return new int[0];
                        }
                }
            }
            return new int[0];
        }

        private SearchResult GetFileInformationAsSearchResult(string filePath) {
            return new SearchResult {
                path = filePath,
                name = filePath.ToLower().Substring(filePath.LastIndexOf("\\") + 1),
                type = filePath.Contains(".") ? filePath.ToLower().Substring(filePath.LastIndexOf(".")) : "File",
                size = new FileInfo(filePath).Length,
                dateModified = File.GetLastAccessTime(filePath),
                dateCreated = File.GetCreationTime(filePath)
            };
        }

        private bool FileMeetRequirments(SearchResult sr) {
            //Path
            if (!sr.path.ToLower().Contains(searchQuery.path.ToLower()))
                return false;

            //Name
            else if (!sr.name.ToLower().Contains(searchQuery.name.ToLower()))
                return false;

            //Type
            else if (!sr.type.Contains(searchQuery.type.ToLower()))
                return false;

            //Min size
            else if (!(searchQuery.minSize == 0 || searchQuery.minSize <= sr.size))
                return false;

            //Max size
            else if (!(searchQuery.maxSize == 0 || sr.size <= searchQuery.maxSize))
                return false;

            //Min date mod
            else if (!(searchQuery.minDateMod == DateTime.MinValue || (searchQuery.minDateMod <= sr.dateModified)))
                return false;

            //Max date mod
            else if (!(searchQuery.maxDateMod == DateTime.MinValue || (searchQuery.maxDateMod >= sr.dateModified)))
                return false;

            //Min date cre
            else if (!(searchQuery.minDateCre == DateTime.MinValue || (searchQuery.minDateCre <= sr.dateCreated)))
                return false;

            //Max date cre
            else if (!(searchQuery.maxDateCre == DateTime.MinValue || (searchQuery.maxDateCre >= sr.dateCreated)))
                return false;

            //Pass all
            else
                return true;
        }

        private int[] SearchInFile(SearchResult searchResult, Options options) {
            List<int> indices = new List<int>();
            if (!options.searchOnlyInTextFiles || searchResult.type.Equals(".txt")) {
                try {
                    filesContentSearched++;
                    char[] buffer = new char[options.maxBufferSize];
                    int index = 0;
                    using (StreamReader streamReader = new StreamReader(searchResult.path)) {
                        int currentIterations = 0;
                        while (!streamReader.EndOfStream && !stop) {
                            //Don;t search large files to the end
                            if (!options.searchLargeFilesToEnd && currentIterations++ > options.maxSearchIterations) break;

                            streamReader.ReadBlock(buffer, 0, options.maxBufferSize);
                            buffer = ValidateBufferBridge(buffer, streamReader);

                            string bufferAsString = new string(buffer);
                            string[] lines = Regex.Split(bufferAsString.Replace("\r\n", "\r").Replace("\n", "\r"), @"(?=\r)");
                            foreach (string line in lines) {
                                if (line.Contains("\r"))
                                    index++;
                                if (line.Contains(searchQuery.inFile)) {
                                    indices.Add(index);
                                    if (!options.returnMultipleLinesInFile) return indices.ToArray();
                                }
                            }
                        }
                    }
                } catch (Exception e) {
                    Console.WriteLine(e);
                }
            }
            if (indices.Count == 0) indices.Add(-1);
            return indices.ToArray();
        }

        private char[] ValidateBufferBridge(char[] buffer, StreamReader streamReader) {
            //Check for a break in a return. 
            //Windows uses /r/n, which is 2 characters so they will get counted twice if in the middle of 2 blocks
            if (buffer[buffer.Length - 1] == '\r' && streamReader.Peek() == '\n') {
                Array.Resize<char>(ref buffer, buffer.Length);
                buffer[buffer.Length - 1] = (char)streamReader.Read();
            }
            return buffer;
        }
    }
}