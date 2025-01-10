namespace Core.Specifications
{
    public class ProductSpecificationParams
    {
        private List<string> _brands = [];

        private List<string> _types = [];

        private int _pageSize = 6;

        private const int MaxPageSize = 50;

        private string? _search;

        public int PageIndex { get; set; } = 1;

        public string? Sort { get; set; }

        public List<string> Brands
        {
            get => _brands;
            set
            {
                _brands = value.SelectMany(b => b.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        public List<string> Types
        {
            get => _types;
            set
            {
                _types = value.SelectMany(b => b.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Search
        {
            get => _search ?? "";
            set => _search = value?.ToLower();
        }
    }
}