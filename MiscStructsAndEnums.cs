using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwSearch {
    class MiscStructsAndEnums {
        public struct SearchQuery {
            public string path;
            public string name;
            public string inFile;
            public string type;
            public long minSize;
            public long maxSize;
            public DateTime minDateMod;
            public DateTime maxDateMod;
            public DateTime minDateCre;
            public DateTime maxDateCre;
        }

        public enum SearchType {
            NeedsInFile,
            CanBeInFile,
            DoesntNeedSearch
        }

        public struct SearchResult {
            public string path;
            public string name;
            public string type;
            public string linePosition;
            public long size;
            public DateTime dateModified;
            public DateTime dateCreated;
        }

        public struct DisplayResult {
            public object path { set; get; }
            public object name { set; get; }
            public object type { set; get; }
            public object lineIndex { set; get; }
            public object size { set; get; }
            public object dateModified { set; get; }
            public object dateCreated { set; get; }
        }

        public struct LogEntry {
            public string time { set; get; }
            public string data { set; get; }
        }
    }
}
