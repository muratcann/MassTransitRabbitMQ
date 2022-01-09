using CommonWork;
using MassTransit;
using System.Threading.Tasks;

namespace ReceiverApplication
{
    public class PublishMessageConsumer : IConsumer<Person>
    {
        public async Task Consume(ConsumeContext<Person> context)
        {
            var person = context.Message;
        }
    }
}
