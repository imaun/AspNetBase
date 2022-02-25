using System.Text.Json.Serialization;

namespace AspNetBase.Web.Common {

    public class DataTableInput<T> where T: class {

        private int _draw;
        private object[] _columns;
        private string _sortColumn;
        private string _sortColumnDir;
        private int _length;
        private int _start;
        private string _searchValue;

        public DataTableInput(
            string draw,
            object[] columns,
            string sortColumn,
            string sortColumnDir,
            string length,
            string start,
            string searchValue) {

            _draw = Convert.ToInt32(draw);
            _columns = columns;
            _sortColumn = sortColumn;
            _sortColumnDir = sortColumnDir;
            _length = Convert.ToInt32(length);
            _start = Convert.ToInt32(start);
            _searchValue = searchValue;
        }

        public void SetFilter(T filter)
            => Filter = filter;

        public int Draw => _draw;

        public string OrderBy => _sortColumn;

        public bool OrderAsc => _sortColumnDir == "asc";

        public bool OrderDesc => _sortColumnDir == "desc";

        public int PageSize => _length;

        public string SearchValue => _searchValue;

        public int PageNumber {
            get {
                if (_start == 0 || _start <= PageSize)
                    return 1;
                return Convert.ToInt32(_start / PageSize);
            }
        }

        public T Filter { get; protected set; }
    }

    public class DataTableResult<T> where T: class {

        public DataTableResult() { }

        public DataTableResult(
            int draw,
            long totalCount,
            int filteredCount,
            IEnumerable<T> data,
            string error = "") {

            Draw = draw;
            RecordsTotal = totalCount;
            RecordsFiltered = filteredCount;
            Data = data;
            Error = error;
        }

        [JsonPropertyName("draw")]
        public int Draw { get; set; }

        [JsonPropertyName("recordsTotal")]
        public long RecordsTotal { get; set; }

        [JsonPropertyName("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

    } 

}
