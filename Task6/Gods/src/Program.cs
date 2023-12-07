using System;
using System.Threading.Tasks;
using MassTransit;
using Task2;
using CardPickStrategy;

namespace Gods
{
    class Program
    {
        static async Task Main(string[] args)
        {          
            await MassTransit.Run();
            Console.Write("Для начала эксперемента напишите число: ");
        
            bool parse = int.TryParse(Console.ReadLine(), out int num);

            Colosseum colosseum = new Colosseum();

            var ilonQueueUri = new Uri("rabbitmq://localhost:5672/ilon_queue");
            var maskQueueUri = new Uri("rabbitmq://localhost:5672/mask_queue");

            while (parse)
            {
                IPartner[] partner = colosseum.CreateIlonAndMask();
                IPartner ilon = partner[0];
                IPartner mark = partner[1];

                var messageIlon = new SendMessage
                {
                    Cards = ilon.cards
                };

                var messageMark = new SendMessage
                {
                    Cards = mark.cards
                };

                await MassTransit.PublishToIlon(messageIlon);
                await MassTransit.PublishToMark(messageMark);

                DateTime startTime = DateTime.Now;
                while(GodsConsumer.pickCardIlon == -1 || GodsConsumer.pickCardMark == -1){
                    DateTime currentTime = DateTime.Now;
                    TimeSpan elapsedTime = currentTime - startTime;

                    if (elapsedTime.TotalSeconds >= 5)
                    {

                        if(GodsConsumer.pickCardIlon == -1)
                        {
                            await MassTransit.PublishToIlon(messageIlon);
                        }

                        if(GodsConsumer.pickCardMark == -1)
                        {
                            await MassTransit.PublishToMark(messageMark);
                        }
                        startTime = DateTime.Now;
                    }
                }

                await Http.Send(ilon, mark, GodsConsumer.pickCardIlon, GodsConsumer.pickCardMark);

                GodsConsumer.pickCardIlon = -1;
                GodsConsumer.pickCardMark = -1;

                Console.Write("Для продолжения эксперемента напишите число: ");
                parse = int.TryParse(Console.ReadLine(), out num); 
            }  
        }
    }
}
