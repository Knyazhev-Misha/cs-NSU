// Deck.cs
using System;
using System.Collections.Generic;
using CardPickStrategy;

namespace Task2
{
    public class Deck
    {
        private Card[] _cards;
        public Card[] cards
        {
            get => _cards;
            set => _cards = value;
        }
        private int numberCards = 36;

        public Deck()
        {
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            cards = new Card[numberCards];
            
            for (int i = 0; i < numberCards; i += 2)
            { 
                cards[i] = new Card(CardColor.Red);
                cards[i + 1] = new Card(CardColor.Black);
            }
        }

    }
}
