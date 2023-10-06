using System;
namespace CardPickStrategy;
public class RandomCardStrategy : ICardPickStrategy
{
    public int Pick(Card[] cards)
    {
        Random random  = new Random();
        int num = random.Next(18);
        return num;
    }
}