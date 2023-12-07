using System;
using System.Threading.Tasks;
using MassTransit;
using CardPickStrategy; 

namespace Gods
{
    public class GetMessage
    {
        public int PickCard { get; set; }
    }
}