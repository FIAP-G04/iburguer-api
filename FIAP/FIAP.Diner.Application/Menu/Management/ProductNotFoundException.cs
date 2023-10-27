using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.Menu.Management
{
    public class ProductNotFoundException : DomainException
    {
        public const string error = "Não foi encontrado produto com o Id {0}";

        public ProductNotFoundException(Guid ProductId) : base(string.Format(error, ProductId.ToString()))
        {
            
        }
    }
}
