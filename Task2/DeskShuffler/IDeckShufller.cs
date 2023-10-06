using CardPickStrategy;
namespace Task2
{
    public interface IDeckShuffler
    {
        public void ShuffleDeck();
        public IPartner[] GiveDeckforPlayer(IPartner[] players);
    }
}