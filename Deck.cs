using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models
{
    public class Deck{

    private static Card[] deck;
        private static int currentCard;
        private const int NumberOfCards = 52;
        private Random randomNum = new Random();
        private Card[] initial;
       // private Deck deck;


        public  Deck()
        {
            int[] rankValue =
            {
            1, //ace
            2, //two
            3, //three
            4, //four
            5, //five
            6, //six
            7, //seven
            8, //eight
            9, //nine
            10, //ten
            11, //jack
            12, //queen
            13 //king
            };

            int[] suit =
            {
                0, //Clubs
                1, //Diamonds
                2, //Hearts
                3, //Spades
            };

            deck = new Card[NumberOfCards];
            currentCard = 0;

            for (int count = 0; count < deck.Length; count++)
            {
                deck[count] = new Card(rankValue[count % 11], suit[count / 13]);
                //suit[count / 13] because there are 52 cards in the deck, 52/13 is 4
                //count/13 will always round down, therefore returning each suit until all 11 cards are made
            }

        }

        public void Shuffle()
        {
            currentCard = 0;
            for (int first = 0; first < deck.Length; first++)
            {
                int second = randomNum.Next(NumberOfCards);
                Card tempDeck = deck[first];
                deck[first] = deck[second];
                deck[second] = tempDeck;
                //is the tempDeck going to remain in memory? Shall I null it and let garbage collection do its stuff?
            }
        }



        public Card DealCard()
        {
            if (currentCard < deck.Length)
            {
                return deck[currentCard++];
            }
            else return null;
        }

    }
//{
//    public class Card
//    {
//        public enum Suits
//        {
//            Heart = 0, Club = 1, Spade = 3, Diamond = 2
//        }
//        public enum Values
//        {
//            Ace = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13
//        }

//        private Suits _suit;
//        private Values _value;
//        public Suits Suit
//        {
//            get { return _suit; }
//            set { _suit = value; }
//        }

//        public Values Value
//        {
//            get { return _value; }
//            set { _value = value; }
//        }

//        public Card() { }
//        public Card(Suits s, Values v)
//        {
//            _suit = s;
//            _value = v;
//        }

//        public static Card[] NewDeck()
//        {
//            Card[] Cards = new Card[52];
//            for (int i = 0; i < 52; i++)
//            {
//                Cards[i] = new Card((Suits)(i / 13), (Values)((i % 13) + 1));
//            }
//            return Cards.OrderBy(x => Guid.NewGuid()).ToArray();
//        }
//    }
}
