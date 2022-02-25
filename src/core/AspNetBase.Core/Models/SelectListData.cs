namespace AspNetBase.Core.Models
{

    public class SelectListData
    {

        public IEnumerable<SelectListItemData> Items { get; set; }
    }

    public class SelectListItemData
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}
