using MassTransit;
using Gods;
using Task2;
using CardPickStrategy;

namespace Gods
{
    public class GodsConsumer : IConsumer<GetMessage>
    {
        public static int pickCardIlon = -1;
        public static int pickCardMark = -1;
        
        public async Task Consume(ConsumeContext<GetMessage> context)
        {
            var message = context.Message;                 

            var sourceAddress = context.SourceAddress.AbsolutePath; 

            string[] addressParts = sourceAddress.Split('_'); 
            string specificPart = GetSendConsumer(addressParts, message.PickCard);
        }

        private string GetSendConsumer(string[] addressParts, int pickCard)
        {
            string marker = "Room";
            string ilonMarker = "IlonRoom";
            string markMarker = "MarkRoom";

            for (int i = 0; i < addressParts.Length; i++)
            {
                if (addressParts[i].IndexOf(marker) != -1)
                {
                    if(addressParts[i].IndexOf(ilonMarker) != -1)
                    {
                        pickCardIlon = pickCard;
                        Console.WriteLine($"\nПришло число: {pickCard} от Ilon");
                    }

                    else if(addressParts[i].IndexOf(markMarker) != -1)
                    {
                        pickCardMark = pickCard;
                        Console.WriteLine($"\nПришло число: {pickCard} от Mark");
                    }
                }
            }

            return "";
        }
    }
}