using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IwSearch.MiscStructsAndEnums;

namespace IwSearch {
    class SearchBuilder {
        public SearchQuery searchQuery;

        public static SearchBuilder Searcher() {
            return new SearchBuilder();
        }

        public SearchBuilder withPath(string path) {
            searchQuery.path = path;
            return this;
        }

        public SearchBuilder withName(string name) {
            searchQuery.name = name;
            return this;
        }

        public SearchBuilder withLinesInFile(string inFile) {
            searchQuery.inFile = inFile;
            return this;
        }

        public SearchBuilder withType(string type) {
            searchQuery.type = type;
            return this;
        }

        public SearchBuilder withMinSize(long minSize) {
            searchQuery.minSize = minSize;
            return this;
        }

        public SearchBuilder withMaxSize(long maxSize) {
            searchQuery.maxSize = maxSize;
            return this;
        }

        public SearchBuilder withMinDateModified(DateTime? minDateMod) {
            if (minDateMod == null) return this;
            searchQuery.minDateMod = minDateMod.Value;
            return this;
        }

        public SearchBuilder withMaxDateModified(DateTime? maxDateMod) {
            if (maxDateMod == null) return this;
            searchQuery.maxDateMod = maxDateMod.Value;
            return this;
        }

        public SearchBuilder withMinDateCreated(DateTime? minDateCre) {
            if (minDateCre == null) return this;
            searchQuery.minDateCre = minDateCre.Value;
            return this;
        }

        public SearchBuilder withMaxDateCreated(DateTime? maxDateCre) {
            if (maxDateCre == null) return this;
            searchQuery.maxDateCre = maxDateCre.Value;
            return this;
        }

        public Searcher Build() {
            return new Searcher(searchQuery);
        }
    }
}
