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

            while (parse)
            {
                IPartner[] partner = colosseum.CreateIlonAndMask();
                IPartner ilon = partner[0];
                IPartner mark = partner[1];

                var messageIlon = new InfoMessage
                {
                    cardPick = -1,
                    cards = ilon.cards
                };

                var messageMark = new InfoMessage
                {
                    cardPick = -1,
                    cards = mark.cards
                };

                await MassTransit.PublishToIlon(messageIlon);
                await MassTransit.PublishToMark(messageMark);

                Console.Write("Для продолжения эксперемента напишите число: ");
                parse = int.TryParse(Console.ReadLine(), out num); 
            }  
        }
    }
}
