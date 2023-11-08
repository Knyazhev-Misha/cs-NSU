namespace CardPickStrategy;
public class Card
{
    private CardColor _colour;  // the name field
    public CardColor colour   // the Name property
    {
        get => _colour;
        set => _colour = value;
    }

    public Card(CardColor colour){
        _colour = colour;
    }
}