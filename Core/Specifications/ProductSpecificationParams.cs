namespace Core.Specifications
{
    public class ProductSpecificationParams : PagingParams
    {
        private List<string> _brands = [];

        private List<string> _types = [];
 

        private string? _search;

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

        public string? Search
        {
            get => _search ?? "";
            set => _search = value?.ToLower();
        }
    }
}