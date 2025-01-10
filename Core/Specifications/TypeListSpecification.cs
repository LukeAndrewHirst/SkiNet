using Core.Entities;

namespace Core.Specifications
{
    public class TypeListSpecification : BaseSpecification<Product, string>
    {
        public TypeListSpecification()
        {
            AddSelect(b => b.Type);
            ApplyDistinct();
        }
    }
}