namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class ProductWithExtraDataDto
    {
        public ProductDto Product { get; set; }
        
        public long Inventory { get; set; }
        
        public long Sold { get; set; }
        
        public string ProductGroupDisplayName { get; set; }
    }
}