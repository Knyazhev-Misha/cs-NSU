using CardPickStrategy;
namespace Task2
{
    public class RandomDeckShuffler : IDeckShuffler
    {
        private Deck deck;

        public RandomDeckShuffler(Deck deck){
            this.deck = deck;
        }

        public void ShuffleDeck()
        {
            Random rand = new Random();
            int numChangeCard1, numChangeCard2;
            Card tmp;
            Card[] cards = deck.cards;
            for(int i = 0; i < cards.Length; i += 1){
                numChangeCard1 = rand.Next(cards.Length);
                numChangeCard2 = rand.Next(cards.Length);

                while(numChangeCard1 == numChangeCard2){
                    numChangeCard2 = rand.Next(cards.Length);
                }

                tmp = cards[numChangeCard1];
                cards[numChangeCard1] = cards[numChangeCard2];
                cards[numChangeCard2] = tmp;
            }
        }

        public IPartner[] GiveDeckforPlayer(IPartner[] players)
        {
            int numberPlayerCards =  deck.cards.Length / players.Length;

            Card[] cardsPlayer = new Card[numberPlayerCards];

            for(int i = 0; i < players.Length; i += 1){
                cardsPlayer = new Card[numberPlayerCards];
                for(int t = 0; t < numberPlayerCards; t += 1)
                {
                    cardsPlayer[t] = deck.cards[t + i * numberPlayerCards];
                }
                players[i].cards = cardsPlayer;
            }

            return players;
        }
    }
}