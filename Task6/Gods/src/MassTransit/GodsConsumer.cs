using MassTransit;
using Gods;
using Task2;
using CardPickStrategy;

namespace Gods
{
    public class GodsConsumer : IConsumer<Signal>
    {
        private static List<Boolean> ilonPick = new List<bool>();
        private static List<Boolean> markPick = new List<bool>();
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1);
        
        public async Task Consume(ConsumeContext<Signal> context)
        {     
            var sourceAddress = context.SourceAddress.AbsolutePath; 

            string[] addressParts = sourceAddress.Split('_'); 
            GetSendConsumer(addressParts);
            CheckSend();
        }

        private async Task CheckSend()
        {
            await semaphore.WaitAsync();
            if(ilonPick.Count != 0 && markPick.Count != 0)
                    {
                        Console.WriteLine($"send");
                        await Http.Send();
                        ilonPick.RemoveAt(0);
                        markPick.RemoveAt(0);
                    }
            semaphore.Release();
        }

        private async Task GetSendConsumer(string[] addressParts)
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
                        ilonPick.Add(true);
                    }

                    else if(addressParts[i].IndexOf(markMarker) != -1)
                    {
                        markPick.Add(true);
                    }
                }
            }
        }
    }
}