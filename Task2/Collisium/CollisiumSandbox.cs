using CardPickStrategy;
namespace Task2
{
    public class CollisiumSandbox
    {

        private IPartner mark;
        public CollisiumSandbox(IPartner partner){
            mark = partner;
            Console.Write("Mark: " + mark.name + "\n");
        }

        public bool RunExperiment()
        {
            ICardPickStrategy strategy = new FirstCardStrategy();
            IPartner mark = new Mark(strategy);
            IPartner ilon = new Ilon(strategy);
            Deck deck = new Deck();
            IDeckShuffler shufller = new RandomDeckShuffler(deck);
            shufller.ShuffleDeck();
            IPartner[] players = new IPartner[]{mark, ilon};
            players = shufller.GiveDeckforPlayer(players);
            mark = players[0];
            ilon = players[1];

           // Console.Write("Mark: " + mark.name + " Ilon: " + ilon.name + "\n");

            int pickNumCardofMark = mark.Play(ilon.cards);
            int pickNumCardofIlon = ilon.Play(mark.cards);

            Card cardIlon = ilon.cards[pickNumCardofMark];
            Card cardMark = mark.cards[pickNumCardofIlon];

            bool isSuccessful = cardIlon.colour.Equals(cardMark.colour);
            
            return isSuccessful;
        }
    }
}