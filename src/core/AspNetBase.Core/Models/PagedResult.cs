namespace AspNetBase.Core.Models {

    public class PagedResult<T> where T: class {

        public PagedResult() {
            Items = new List<T>();
        }

        public PagedResult(IEnumerable<T> items) {
            Items = items;
        }

        public IEnumerable<T> Items { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public int TotalCount { get; set; }
        public int ItemsCount => Items?.Count() ?? 0;
    }

}
