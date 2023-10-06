namespace CardPickStrategy;
public class Card
{
    private string _colour;  // the name field
    public string colour   // the Name property
    {
        get => _colour;
        set => _colour = value;
    }

    public Card(string colour){
        _colour = colour;
    }
}