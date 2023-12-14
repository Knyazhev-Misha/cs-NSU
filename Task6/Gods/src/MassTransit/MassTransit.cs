using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.MessageData.Values;
using RabbitMQ.Client;

namespace Gods
{
    public static class MassTransit
    {
        private static IBusControl _bus;

        public static async Task Run()
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost:5672/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("gods_queue", e =>
                {
                    e.Consumer<GodsConsumer>();
                });
            });

            try
            {
                await _bus.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запуска шины сообщений: {ex.Message}");
                throw;
            }
        }

        public static async Task PublishToIlon(InfoMessage sendMessage)
        {
            var exchange1Address = new Uri("rabbitmq://localhost:5672/ilon_queue");
            var endpoint = await _bus.GetSendEndpoint(exchange1Address);
            await endpoint.Send(sendMessage);
        }

        public static async Task PublishToMark(InfoMessage sendMessage)
        {
            var exchange2Address = new Uri("rabbitmq://localhost:5672/mark_queue");
            var endpoint = await _bus.GetSendEndpoint(exchange2Address);
            await endpoint.Send(sendMessage);
        }

        public static async Task Stop()
        {
            try
            {
                await _bus?.StopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping MassTransit: {ex.Message}");
                throw;
            }
        }
    }
}
