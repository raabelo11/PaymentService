namespace PaymentService.Domain.Interfaces
{
    public interface IPaymentPublisher
    {
        Task<bool> SendQueue(string queue, string message);
    }
}
