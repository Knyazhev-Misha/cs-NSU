using CardPickStrategy;
namespace Task2
{
    public class CollisiumSandbox
    {

        private IPartner _mark;
        public IPartner mark
        {
            get => _mark;
            set => _mark = value;
        }

        private IPartner _ilon;
        public IPartner ilon
        {
            get => _ilon;
            set => _ilon = value;
        }

        private int _pickNumCardofMark;

        public int pickNumCardofMark
        {
            get => _pickNumCardofMark;
            set => _pickNumCardofMark = value;
        }

        private int _pickNumCardofIlon;

        public int pickNumCardofIlon
        {
            get => _pickNumCardofIlon;
            set => _pickNumCardofIlon = value;
        }

        private ICardPickStrategy randomStrategy;
        private ICardPickStrategy firstStrategy;
        private IDeckShuffler shufller;

        public CollisiumSandbox(IEnumerable<IPartner> partners, 
        IEnumerable<ICardPickStrategy> strategys, IDeckShuffler shufller){
            
            var strategysArray = strategys.ToArray();
            this.firstStrategy = strategysArray[0];
            this.randomStrategy = strategysArray[1];

            var partnersArray = partners.ToArray();
            this._mark = partnersArray[0];
            this._ilon = partnersArray[1];

            this.mark.strategy = firstStrategy;
            this.ilon.strategy = randomStrategy;

            this.shufller = shufller;            
        }

        public bool RunExperiment(bool shuflling)
        {
            if(shuflling)
            {
                shufller.ShuffleDeck();
                IPartner[] players = new IPartner[]{_mark, _ilon};
                players = shufller.GiveDeckforPlayer(players);
                _mark = players[0];
                _ilon = players[1];
            }

            _pickNumCardofMark = _mark.Play(_mark.cards);
            _pickNumCardofIlon = _ilon.Play(_ilon.cards);

            Card cardIlon = _ilon.cards[_pickNumCardofMark];
            Card cardMark = _mark.cards[_pickNumCardofIlon];

            if(cardIlon.colour == cardMark.colour)
            {
                return true;
            }
            
            return false;
        }
    }
}