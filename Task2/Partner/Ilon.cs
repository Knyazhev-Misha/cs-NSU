using CardPickStrategy;
namespace Task2
{
    public class Ilon : IPartner
    {

        private ICardPickStrategy strategy;
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


        public Ilon(ICardPickStrategy strategy){
            this.strategy = strategy;
            _name = "Ilon";
        }

        public int Play(Card[] cards){
            return strategy.Pick(cards);
        }
    }
}