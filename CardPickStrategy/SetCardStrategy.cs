using System;
namespace CardPickStrategy;
public class SetCardStrategy : ICardPickStrategy
{
    private int num;
    public SetCardStrategy(int num)
    {
        this.num = num;
    }

    public int Pick(Card[] cards)
    {
        return num;
    }
}