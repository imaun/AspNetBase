namespace AspNetBase.Core.Models
{

    public class QueryFilterModel
    {
        public QueryFilterModel() {

        }

        public string Search { get; set; }
        public string Columns { get; set; }
        public string OrderBy { get; set; }
        public bool OrderDesc { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public int StartIndex => (PageNumber * PageSize) - PageSize;
    }
}
