using Xunit;
using CardPickStrategy; 
using Task2;

public class DeckTests
{
    [Fact]
    public void DeckShouldHave18RedAnd18BlackCards()
    {

        Deck deck = new Deck(); 

        int redCardCount = deck.cards.Count(card => card.colour == "Red");
        int blackCardCount = deck.cards.Count(card => card.colour == "Black");

        Assert.Equal(18, redCardCount); 
        Assert.Equal(18, blackCardCount); 
    }
}

public class ShufflingTests
{
    [Fact]
    public void TestShufflingCreatesDifferentDecks()
    {
        IDeckShuffler shuffler = new RandomDeckShuffler(new Deck());

        var decks = new List<Deck>();

        for (int i = 0; i < 10; i += 1) 
        {
            var deck = new Deck();
            decks.Add(deck);
        }

        foreach (var deck in decks)
        {
            shuffler.deck = deck;
            shuffler.ShuffleDeck();
        }

        for (int i = 0; i < decks.Count; i += 1)
        {
            for (int j = i + 1; j < decks.Count; j += 1)
            {
                Assert.NotEqual(decks[i].cards, decks[j].cards);
            }
        }
    }
}

public class StrategyTests
{
    [Fact]
    public void TestStrategyOnShuffledDeck()
    {
        var deck = new Deck();

        ICardPickStrategy strategy = new FirstCardStrategy(); 

        var cardRed18 = new Card[18];
        var cardBlack18 = new Card[18];

        for(int i = 0; i < 18; i += 1)
        {
            cardRed18[i] = new Card("Red");
            cardBlack18[i] = new Card("Black");
        }

        int pickedCardRed = strategy.Pick(cardRed18);
        int pickedCardBlack = strategy.Pick(cardBlack18);

        Assert.Equal(cardRed18[0].colour, "Red");
        Assert.Equal(pickedCardRed, 0);
        Assert.Equal(cardBlack18[0].colour, "Black");
        Assert.Equal(pickedCardBlack, 0);
    }
}


