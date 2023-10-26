using CardPickStrategy;
namespace Task2
{
    public interface IDeckShuffler
    {
        public void ShuffleDeck();
        public Deck deck{ get; set; }
        public IPartner[] GiveDeckforPlayer(IPartner[] players);
    }
}