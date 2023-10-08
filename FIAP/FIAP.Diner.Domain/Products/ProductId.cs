namespace FIAP.Diner.Domain.Products
{
    public record ProductId()
    {
        public Guid Value = Guid.NewGuid();
    }
}
