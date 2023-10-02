namespace FIAP.Diner.Domain.Products
{
    public static class ProductExceptions
    {
        public readonly static string ProductDoesNotExist = "Product does not exist";
        public readonly static string ProductNameIsRequired = "Product name is required";
        public readonly static string ProductDescriptionIsRequired = "Product description is required";
        public readonly static string ProductImageURLIsRequired = "At least one image is required";
    }
}
