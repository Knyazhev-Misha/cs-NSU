using Xunit;
using CardPickStrategy; 
using Task2;
using Task4;
using Moq;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class DeckTests
{
    [Fact]
    public void DeckShouldHave18RedAnd18BlackCards()
    {

        Deck deck = new Deck(); 

        int redCardCount = deck.cards.Count(card => card.colour == CardColor.Red);
        int blackCardCount = deck.cards.Count(card => card.colour == CardColor.Black);

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
            cardRed18[i] = new Card(CardColor.Red);
            cardBlack18[i] = new Card(CardColor.Black);
        }

        int pickedCardRed = strategy.Pick(cardRed18);
        int pickedCardBlack = strategy.Pick(cardBlack18);

        Assert.Equal(cardRed18[0].colour, CardColor.Red);
        Assert.Equal(pickedCardRed, 0);
        Assert.Equal(cardBlack18[0].colour, CardColor.Black);
        Assert.Equal(pickedCardBlack, 0);
    }
}

 public class CollisiumSandboxTests
{
    [Fact]
    public void RunExperiment_ShouldCallShuffleDeckOnce()
    {
            
        var mockShuffler = new Mock<IDeckShuffler>();
        var mockDeck = new Mock<Deck>();
        
        var mockMark = new Mock<IPartner>();
        var mockIlon = new Mock<IPartner>();

        mockMark.Setup(m => m.Play(It.IsAny<Card[]>())).Returns(0);
        mockIlon.Setup(m => m.Play(It.IsAny<Card[]>())).Returns(0);

        Card[] mockMarkCards = new Card[18];
        Card[] mockIlonCards = new Card[18];

        for(int i = 0; i < 18; i += 1){
            mockMarkCards[i] = new Card(CardColor.Red);
            mockIlonCards[i] = new Card(CardColor.Black);
        }

        mockMark.Setup(m => m.cards).Returns(mockMarkCards);
        mockIlon.Setup(m => m.cards).Returns(mockIlonCards);        
        
        var partners = new List<IPartner> { mockMark.Object, mockIlon.Object };
        
        var mockFirstCardStrategy = new Mock<FirstCardStrategy>();
        
        var strategies = new List<ICardPickStrategy> { mockFirstCardStrategy.Object, 
        mockFirstCardStrategy.Object };
        
        IPartner[] players = new IPartner[]{mockMark.Object, mockIlon.Object};
        
        mockShuffler.Setup(s => s.GiveDeckforPlayer(It.IsAny<IPartner[]>()))
    .Returns((IPartner[] players) => players);

        var sandbox = new Mock<CollisiumSandbox>(partners, strategies, mockShuffler.Object);


        bool result = sandbox.Object.RunExperiment(true);

        mockShuffler.Verify(s => s.ShuffleDeck(), Times.Once);
        Assert.Equal(result, false);
    }
}

public class DBTests
{
    [Fact]
    public void TestDateBaseOnSavwRead()
    {
        int number = 100;

        DB db = new DB();

        db.CreateExperements(number); 

        int succesSave = db.CountSuccesOfExperements();

        List<Experiment> experimentsSave = db.experiments;

        db.Save();

        db.Read();

        List<Experiment> experimentsRead = db.experiments;

        for(int i = 0; i < number; i += 1)
        {
            Assert.Equal(experimentsSave[i].result, experimentsRead[i].result);
        }

        List<CollisiumSandbox> sandboxes = db.MappExperementsToSandbox();

        int succesRead = db.CountSuccesOfExperements(sandboxes);
        
        Assert.Equal(succesRead, succesSave);
    }
}





