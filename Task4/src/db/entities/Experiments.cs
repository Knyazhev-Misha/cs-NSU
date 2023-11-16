using Task2;
using CardPickStrategy;

namespace Task4
{
    public class Experiment
    {
        public int Id { get; set; }
        public int pickNumCardofMark { get; set; }
        public int pickNumCardofIlon { get; set; }
        public string cardsMark { get; set; } // Строка для хранения цветов
        public string cardsIlon { get; set; } // Строка для хранения цветов
        public bool result { get; set; }
    }
}