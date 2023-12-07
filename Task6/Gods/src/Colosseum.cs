using Task2;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CardPickStrategy;

namespace Gods
{
    public class Colosseum
    {
        private CollisiumSandbox CreateCollisiumSandbox(){
            Deck deck = new Deck();

            IDeckShuffler deckShuffler = new RandomDeckShuffler(deck);

            IPartner mark = new Mark();
            IPartner ilon = new Ilon();
            var partners = new List<IPartner> { mark, ilon };

            ICardPickStrategy cardPickStrategyIlon = new FirstCardStrategy();
            ICardPickStrategy cardPickStrategyMark = new RandomCardStrategy();
            var strategies = new List<ICardPickStrategy> { cardPickStrategyIlon, cardPickStrategyMark };
            
            return new CollisiumSandbox(partners, strategies, deckShuffler);
        }

        public IPartner[] CreateIlonAndMask(){
            CollisiumSandbox collisiumSandbox = CreateCollisiumSandbox();

            collisiumSandbox.RunExperiment(true);

            IPartner[] partner = new IPartner[]{collisiumSandbox.ilon, collisiumSandbox.mark};

            return partner;
        }
    }
}