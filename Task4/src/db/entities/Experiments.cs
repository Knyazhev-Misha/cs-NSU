using Task2;
using CardPickStrategy;

namespace Task4
{
    public class Experiment
    {
        public int Id { get; set; }
        public int pickNumCardofMark { get; set; }
        public int pickNumCardofIlon { get; set; }
        public CardColor[] cardsMark { get; set; }
        public CardColor[] cardsIlon { get; set; }
        public bool result { get; set; }
    }
}