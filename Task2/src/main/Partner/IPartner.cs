using CardPickStrategy;
namespace Task2
{
    public interface IPartner
    {
        public ICardPickStrategy strategy{ get; set; }
        public Card[] cards{ get; set; }
        public string name{ get; set; }
        public int Play(Card[] cards);
    }
}