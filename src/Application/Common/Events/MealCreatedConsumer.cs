using MassTransit;

namespace NutriTrack.src.Application.Common.Events
{
    public class MealCreatedConsumer : IConsumer<MealCreatedEvent>
    {

        public Task Consume(ConsumeContext<MealCreatedEvent> context)

        {

            var message = context.Message;

            Console.WriteLine($"[RabbitMQ] Processando refeição {message.MealId} do usuário {message.UserId}");

            return Task.CompletedTask;
        }
    }
}
