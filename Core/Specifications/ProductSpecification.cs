using Core.Entities;

namespace Core.Specifications
{
    public class ProductSpecification: BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecificationParams specificationParams) : base(p =>
            (string.IsNullOrEmpty(specificationParams.Search) || p.Name.ToLower().Contains(specificationParams.Search)) && 
            (!specificationParams.Brands.Any() || specificationParams.Brands.Contains(p.Brand)) && 
            (!specificationParams.Types.Any() || specificationParams.Types.Contains(p.Type)))
        {
            ApplyPaging(specificationParams.PageSize * (specificationParams.PageIndex - 1), specificationParams.PageSize);

            switch(specificationParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }   
        }
    }
}