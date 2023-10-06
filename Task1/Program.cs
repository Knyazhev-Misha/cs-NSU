using System;
using CardPickStrategy;
namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {

            int numberSucces = 0;
            int numberExperiment = 1000000;
            for(int i = 0; i < numberExperiment; i += 1)
            {
                Deck deck = new Deck();
                Card[] cardsPlayer1 = deck.GetCards(1);
                Card[] cardsPlayer2 = deck.GetCards(2);

                ICardPickStrategy strategy = new FirstCardStrategy();

                int numPlayer1Card = strategy.Pick(cardsPlayer1);
                int numPlayer2Card = strategy.Pick(cardsPlayer2);

                if(cardsPlayer1[numPlayer1Card].Equals(cardsPlayer2[numPlayer1Card])){
                    numberSucces += 1;
                }
            }

            double probability = (double)numberSucces / (double)numberExperiment;
            Console.Write("number succes: " + numberSucces + "\nnumber experiment: " 
            +  numberExperiment + "\nprobability: " + probability);
        }
    }
}