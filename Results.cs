using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using static IwSearch.MiscStructsAndEnums;

namespace IwSearch {
    class Results {
        public BindingList<DisplayResult> results;
        private MainWindow mainWindow;

        private static string[] formats = { "B", "KB"};

        public Results(MainWindow mainwindow) {
            results = new BindingList<DisplayResult>();
            this.mainWindow = mainwindow;
        }

        public void AddResult(SearchResult sr) {
            mainWindow.Dispatcher.InvokeAsync(() => {
                results.Add(new DisplayResult {
                    path = sr.path,
                    name = sr.name,
                    type = sr.type,
                    lineIndex = sr.linePosition,
                    size = FormatFileSize(sr.size),
                    dateModified = sr.dateModified.ToString("MM/dd/yyyy"),
                    dateCreated = sr.dateCreated.ToString("MM/dd/yyyy")
                });
            });
        }

        public static string FormatFileSize(long sizeInBytes) {
            int count = 0;
            if (sizeInBytes > 1024) {
                sizeInBytes /= 1024;
                count++;
            }
            return string.Format("{0} {1}", sizeInBytes, formats[count]);
        }
    }
}
