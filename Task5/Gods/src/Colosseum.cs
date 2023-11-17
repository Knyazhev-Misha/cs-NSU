using Task2;
using CardPickStrategy; 
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gods
{
    public class Colosseum
    {
        private CollisiumSandbox CreateCollisiumSandbox(){
            Deck deck = new Deck();

            IDeckShuffler deckShuffler = new RandomDeckShuffler(deck);

            IPartner mark = new Mark();
            IPartner ilon = new Ilon();
            var partners = new List<IPartner> { mark, ilon };

            ICardPickStrategy cardPickStrategyIlon = new FirstCardStrategy();
            ICardPickStrategy cardPickStrategyMark = new RandomCardStrategy();
            var strategies = new List<ICardPickStrategy> { cardPickStrategyIlon, cardPickStrategyMark };
            
            return new CollisiumSandbox(partners, strategies, deckShuffler);
        }

        public async Task Send(){
            CollisiumSandbox sandbox = CreateCollisiumSandbox();
            sandbox.RunExperiment(true);

            IPartner ilon = sandbox.ilon;
            IPartner mark = sandbox.mark;   

            string ilonJson = Newtonsoft.Json.JsonConvert.SerializeObject(ilon.cards);
            string markJson = Newtonsoft.Json.JsonConvert.SerializeObject(mark.cards);

            string ilonURL = "http://localhost:1112/IlonRoom";
            string markURL = "http://localhost:1113/MarkRoom";
            
            using (HttpClient client = new HttpClient())
            {
                StringContent ilonContent = new StringContent(ilonJson, Encoding.UTF8, "application/json");
                StringContent markContent = new StringContent(markJson, Encoding.UTF8, "application/json");

                HttpResponseMessage responseIlonRoom = await client.PostAsync(ilonURL, ilonContent);
                HttpResponseMessage responseMarkRoom = await client.PostAsync(markURL, markContent);

                if (responseIlonRoom.IsSuccessStatusCode && responseMarkRoom.IsSuccessStatusCode)
                {
                    string ilonResult = await responseIlonRoom.Content.ReadAsStringAsync();
                    string markResult = await responseMarkRoom.Content.ReadAsStringAsync();
                    
                    int ilonPick;
                    int markPick;

                    int.TryParse(ilonResult, out ilonPick);
                    int.TryParse(markResult, out markPick);

                    Card cardIlon = ilon.cards[markPick];
                    Card cardMark = mark.cards[ilonPick];

                    bool result;

                    if(cardIlon.colour == cardMark.colour)
                    {
                        result = true;
                    }

                    else
                    {
                        result = false;
                    }

                    Console.WriteLine($"Pick Ilon: {ilonPick}");
                    Console.WriteLine($"Pick Mark: {markPick}");
                    Console.WriteLine($"Card Ilon: {cardIlon.colour}");
                    Console.WriteLine($"Card Mark: {cardMark.colour}");
                    Console.WriteLine($"Result: {result}");
                }
                else
                {
                    Console.WriteLine($"Ошибка IlonRoom: {responseIlonRoom.StatusCode} " +
                    $"или ошибка MarkRoom: {responseMarkRoom.StatusCode}");
                }

            }
        }

    }
}