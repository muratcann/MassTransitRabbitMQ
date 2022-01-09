using CommonWork;
using MassTransit;
using System.Threading.Tasks;

namespace ReceiverApplication
{
    public class SendMessageConsumer : IConsumer<Product>
    {
        public async Task Consume(ConsumeContext<Product> context)
        {
            var product = context.Message;
        }
    }
}
