using System;
using Microsoft.Extensions.Configuration;

namespace Gods
{
    class Program
    {
        static void Main(string[] args)
            {
                Colosseum colosseum = new Colosseum();

                int num = 1;

                while(num > 0)
                {
                    colosseum.Send();

                    int.TryParse(Console.ReadLine(), out num);
                }
            }
    }
}
