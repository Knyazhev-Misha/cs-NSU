using System;
using System.Threading.Tasks;
using MassTransit;
using CardPickStrategy; 

namespace Gods
{
    public class SendMessage
    {
        public Card[] Cards { get; set; }
    }
}