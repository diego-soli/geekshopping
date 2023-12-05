using System.Threading.Tasks;
namespace GeekShoppingMessageBus
{
    public interface IMessageBus
    {
        Task PublicMessage(BaseMessage message, string queueName);
    }
}