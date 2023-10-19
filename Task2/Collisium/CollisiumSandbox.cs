using CardPickStrategy;
namespace Task2
{
    public class CollisiumSandbox
    {

        private IPartner mark;
        private IPartner ilon;
        private ICardPickStrategy randomStrategy;
        private ICardPickStrategy firstStrategy;
        private Deck deck;
        private IDeckShuffler shufller;





        public CollisiumSandbox(IEnumerable<IPartner> partners, 
        IEnumerable<ICardPickStrategy> strategys, Deck deck, IDeckShuffler shufller){
        
            var partnersArray = partners.ToArray();

            this.mark = partnersArray[0];
            this.ilon = partnersArray[1];
            
            var strategysArray = strategys.ToArray();

            this.firstStrategy = strategysArray[0];
            this.randomStrategy = strategysArray[1];

            this.deck = deck;

            this.shufller = shufller;            
        }

        public bool RunExperiment()
        {
            shufller.ShuffleDeck();
            IPartner[] players = new IPartner[]{mark, ilon};
            players = shufller.GiveDeckforPlayer(players);
            mark = players[0];
            ilon = players[1];


            int pickNumCardofMark = mark.Play(ilon.cards);
            int pickNumCardofIlon = ilon.Play(mark.cards);

            Card cardIlon = ilon.cards[pickNumCardofMark];
            Card cardMark = mark.cards[pickNumCardofIlon];

            bool isSuccessful = cardIlon.colour.Equals(cardMark.colour);
            
            return isSuccessful;
        }
    }
}