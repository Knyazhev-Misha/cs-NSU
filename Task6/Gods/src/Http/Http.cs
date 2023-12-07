using Task2;
using CardPickStrategy; 
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gods
{
    public class Http
    {
    
        public static async Task Send(IPartner ilon, IPartner mark, int pickCardIlon, int pickCardMark){
            string ilonJsonCards = Newtonsoft.Json.JsonConvert.SerializeObject(ilon.cards);
            string markJsonCards = Newtonsoft.Json.JsonConvert.SerializeObject(mark.cards);

            string ilonURL = "http://localhost:1114/IlonRoom";
            string markURL = "http://localhost:1112/MarkRoom";
            
            using (HttpClient client = new HttpClient())
            {

                var ilonContent = new PlayerDTO
                {
                    pickNumCard = pickCardIlon,
                    cardsPlayer = markJsonCards
                };

                var markContent = new PlayerDTO
                {
                    pickNumCard = pickCardMark,
                    cardsPlayer = ilonJsonCards
                };

                StringContent ilonContentJson = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(ilonContent), Encoding.UTF8, "application/json");
                StringContent markContentJson = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(markContent), Encoding.UTF8, "application/json");

                HttpResponseMessage responseIlonRoom = await client.PostAsync(ilonURL, ilonContentJson);
                HttpResponseMessage responseMarkRoom = await client.PostAsync(markURL, markContentJson);

                if (responseIlonRoom.IsSuccessStatusCode && responseMarkRoom.IsSuccessStatusCode)
                {
                    string ilonAnswer = await responseIlonRoom.Content.ReadAsStringAsync();
                    string markAnswer = await responseMarkRoom.Content.ReadAsStringAsync();
                    
                    int ilonColourNum, markColourNum;

                    int.TryParse(ilonAnswer, out ilonColourNum);
                    int.TryParse(markAnswer, out markColourNum);

                    CardColor ilonColour, markColour;
                    if(ilonColourNum == 0)
                    {
                        ilonColour = CardColor.Red;
                    }
                    else
                    {
                        ilonColour = CardColor.Black;
                    }

                    if(markColourNum == 0)
                    {
                        markColour = CardColor.Red;
                    }
                    else
                    {
                        markColour = CardColor.Black;
                    }
                   
                    bool result;

                    if(ilonColour == markColour)
                    {
                        result = true;
                    }

                    else
                    {
                        result = false;
                    }

                    Console.Write("Ilon cards: ");
                    for(int i = 0; i < ilon.cards.Length; i += 1)
                    {
                        Console.Write($"{i}({ilon.cards[i].colour}) ");
                    }

                     Console.Write("\nMark cards: ");

                    for(int i = 0; i < mark.cards.Length; i += 1)
                    {
                        Console.Write($"{i}({mark.cards[i].colour}) ");
                    }

                    Console.Write("\n");

                    Console.WriteLine($"Pick Ilon: {pickCardIlon}");
                    Console.WriteLine($"Pick Mark: {pickCardMark}");
                    Console.WriteLine($"Ilon pick colour: {ilonColour}");
                    Console.WriteLine($"Mark pick colour: {markColour}");
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