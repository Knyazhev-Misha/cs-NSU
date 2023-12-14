using System;
using System.Threading.Tasks;
using MassTransit;
using CardPickStrategy; 

namespace Gods
{
    public class InfoMessage
    {
        public int cardPick { get; set; }
        public Card[] cards { get; set; }
    }
}