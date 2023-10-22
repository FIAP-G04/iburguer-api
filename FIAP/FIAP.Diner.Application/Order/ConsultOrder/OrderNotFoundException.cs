using FIAP.Diner.Domain.Abstractions;

namespace FIAP.Diner.Application.Order.ConsultOrder
{
    public class OrderNotFoundException : DomainException
    {
        public const string error = "Não foi encontrado pedido com o Id {0}.";

        public OrderNotFoundException(Guid orderId) : base(string.Format(error, orderId.ToString()))
        {
            
        }
    }
}
