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
    
        public static async Task Send(){
            string ilonURL = "http://localhost:1114/IlonRoom";
            string markURL = "http://localhost:1113/MarkRoom";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseIlonRoom = await client.PostAsync(ilonURL, null);
                HttpResponseMessage responseMarkRoom = await client.PostAsync(markURL, null);

                if (responseIlonRoom.IsSuccessStatusCode && responseMarkRoom.IsSuccessStatusCode)
                {
                    string ilonAnswer = await responseIlonRoom.Content.ReadAsStringAsync();
                    string markAnswer = await responseMarkRoom.Content.ReadAsStringAsync();
                    
                    int ilonColourNum, markColourNum;

                    int.TryParse(ilonAnswer, out markColourNum);
                    int.TryParse(markAnswer, out ilonColourNum);

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

                    Console.WriteLine($"Ilon pick colour: {ilonColour}");
                    Console.WriteLine($"Mark pick colour: {markColour}");
                    Console.WriteLine($"Result: {result}");
                    Console.WriteLine($"---------------");
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