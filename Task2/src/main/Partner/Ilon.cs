using CardPickStrategy;
namespace Task2
{
    public class Ilon : IPartner
    {

        private ICardPickStrategy _strategy;
        public ICardPickStrategy strategy
        {
            get => _strategy;
            set => _strategy = value;
        }

        private Card[] _cards;
        public Card[] cards
        {
            get => _cards;
            set => _cards = value;
        }

        private string _name;
        public string name{
            get => _name;
            set => _name = value;
        }


        public Ilon(){
            _name = "Ilon";
        }

        public int Play(Card[] cards){
            return _strategy.Pick(cards);
        }
    }
}