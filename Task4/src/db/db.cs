using System;
using Microsoft.Extensions.Configuration;
using System.Collections;
using Task2;
using CardPickStrategy;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Data.Common;
using System.Text.Json;

namespace Task4
{
    public class DB
    {
        private List<Experiment> _experiments;
        public List<Experiment> experiments
        {
            get => _experiments;
            set => _experiments = value;
        }

        private ApplicationContext db;

        public void createContext(DbName dbName)
        {
            ApplicationContext.SetDb(dbName);

            db = new ApplicationContext();
        }

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

        public void CreateExperements(int number)
        {
            _experiments = new List<Experiment>();

            for(int i = 0; i < number; i += 1)
            {
                    CollisiumSandbox collisiumSandbox = CreateCollisiumSandbox();

                    bool result = collisiumSandbox.RunExperiment(true);

                    CardColor[] cardsMark = new CardColor[18];
                    CardColor[] cardsIlon = new CardColor[18];

                    for(int t = 0; t < 18; t += 1)
                    {
                        cardsMark[t] = collisiumSandbox.mark.cards[t].colour;
                        cardsIlon[t] = collisiumSandbox.ilon.cards[t].colour;
                    }
                    
                    string cardsMarkJson = JsonSerializer.Serialize(cardsMark);
                    string cardsIlonJson = JsonSerializer.Serialize(cardsIlon);

                    Experiment experiment = new Experiment { 
                        pickNumCardofMark = collisiumSandbox.pickNumCardofMark,
                        pickNumCardofIlon = collisiumSandbox.pickNumCardofIlon,
                        cardsMark = cardsMarkJson,
                        cardsIlon = cardsIlonJson,
                        result = result           
                    };

                    _experiments.Add(experiment);
            }
        }

        public void Save()
        {
            {   
                if(_experiments == null)
                {
                    return;
                }

                foreach(Experiment ex in _experiments)
                {
                    db.Experiments.AddRange(ex);
                    db.SaveChanges();
                }
                
            }
        }

        public void Read()
        {
           // using (ApplicationContext db = new ApplicationContext())
            {
                _experiments = db.Experiments.ToList();
            }
        }

        public List<CollisiumSandbox> MappExperementsToSandbox()
        {
            List<CollisiumSandbox> sandboxs = new List<CollisiumSandbox>();

            foreach(Experiment ex in _experiments)
            {
                CollisiumSandbox collisiumSandbox = CreateCollisiumSandbox();
            
                CardColor[] colorCardsIlon = JsonSerializer.Deserialize<CardColor[]>(ex.cardsIlon);
                CardColor[] colorCardsMark = JsonSerializer.Deserialize<CardColor[]>(ex.cardsMark);
                
                Card[] cardsMark = new Card[18];
                Card[] cardsIlon = new Card[18];

                for(int i = 0; i < 18; i += 1)
                {
                    cardsMark[i] = new Card(colorCardsMark[i]);
                    cardsIlon[i] = new Card(colorCardsIlon[i]);
                }

                collisiumSandbox.mark.cards = cardsMark;
                collisiumSandbox.ilon.cards = cardsIlon;

                collisiumSandbox.mark.strategy = new SetCardStrategy(ex.pickNumCardofMark);
                collisiumSandbox.ilon.strategy = new SetCardStrategy(ex.pickNumCardofIlon);

                sandboxs.Add(collisiumSandbox);
            }

            return sandboxs;
        }

        public int CountSuccesOfExperements()
        {
            int count = 0;

            foreach(Experiment ex in _experiments)
            {
                if(ex.result)
                {
                    count += 1;
                }    
            }

            return count;
        }

        public int CountSuccesOfExperements(List<CollisiumSandbox> sandboxs)
        {
            int count = 0;

            foreach(CollisiumSandbox sandbox in sandboxs)
            {
                if(sandbox.RunExperiment(false))
                {
                    count += 1;
                }    
            }

            return count;
        }      
    }
}