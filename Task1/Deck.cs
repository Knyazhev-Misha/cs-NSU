// Deck.cs
using System;
using System.Collections.Generic;
using CardPickStrategy;

namespace Task1
{
    public class Deck
    {
        private Card[] cards;
        private int numberCards = 36;
        private int numberPlayer = 2;

        public Deck()
        {
            InitializeDeck();
        }

        private void InitializeDeck()
        {
             cards = new Card[numberCards];

            for (int i = 0; i < numberCards; i += 2)
            {
                cards[i] = Card.Red;
                cards[i + 1] = Card.Black;
            }

            ShuffleCards();
        }

        public void ShuffleCards()
        {
            Random rand = new Random();
            int numChangeCard1, numChangeCard2;
            Card tmp;
            for(int i = 0; i < numberCards; i += 1){
                numChangeCard1 = rand.Next(numberCards);
                numChangeCard2 = rand.Next(numberCards);

                while(numChangeCard1 == numChangeCard2){
                    numChangeCard2 = rand.Next(numberCards);
                }

                tmp = cards[numChangeCard1];
                cards[numChangeCard1] = cards[numChangeCard2];
                cards[numChangeCard2] = tmp;
            }
        }

        public Card[] GetCards(int numPlayer)
        {
            int numberPlayerCards =  numberCards / numberPlayer;
            int numStartCard = numberPlayerCards * (numPlayer - 1);

            Card[] cardsPlayer = new Card[numberPlayerCards];

            for(int i = 0; i < numberPlayerCards; i += 1){
                cardsPlayer[i] = cards[i + numStartCard];
            }

            return cardsPlayer;
        }

    }
}
