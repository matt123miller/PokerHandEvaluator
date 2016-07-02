using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerEvaluator
{
    // I would like to use an enum if possible to encapsulate a type of hand.
    //public enum WinningHands
    //{
    //    StraightFlush = 'Straight Flush',
    //    FourKind = 'Four of a Kind',
    //    FullHouse = 'Full House',
    //    Flush = 'Flush',
    //    Straight = 'Straight',
    //    ThreeKind = 'Three of a Kind',
    //    TwoPair = 'Two Pairs',
    //    OnePair = 'One Pair'
    //}

    class Table
    {
        Deck deck;

        public List<Card> tableCards;

        private List<Card> tempList = new List<Card>();
        private List<Card> sortedList = new List<Card>();
        private List<Card> returnList = new List<Card>();


        public Table()
        {
            deck = new Deck();
            deck.Shuffle();

            tableCards = new List<Card>();

            // The table begins with 2 cards available to all players
            tableCards.Add(deck.DrawCard());
            tableCards.Add(deck.DrawCard());

        }

        public void DealCardTo(Hand hand)
        {
            Card card = deck.DrawCard();
            hand.GiveCard(card);
        }

        public void DealManyCardsTo(Hand hand, int total)
        {
            for (int i = 0; i < total; i++)
            {
                DealCardTo(hand);
            }
        }

        // TO DO: Must somehow return the type of hand as well.
        public List<Card> EvaluateHand(List<Card> input)
        {
            List<Card> hand = input;
            hand.AddRange(tableCards);

            List<Card> theValueHand = new List<Card>();
            theValueHand = SortByValue(hand);
            List<Card> theSuitHand = new List<Card>();
            theSuitHand = SortBySuit(hand);

            try
            {
                tempList = IsStraightFlush(theValueHand);      //should work
                if (tempList.Count > 0)
                    return tempList;

                tempList = IsFourKind(theValueHand);      //works
                if (tempList.Count > 0)
                    return tempList;

                tempList = IsFullHouse(theValueHand);      //works
                if (tempList.Count > 0)
                    return tempList;

                tempList = IsFlush(theSuitHand);      //works
                if (tempList.Count > 0)
                    return tempList;

                tempList = IsStraight(theValueHand);      //works
                if (tempList.Count > 0)
                    return tempList;

                tempList = IsThreeKind(theValueHand);      //works
                if (tempList.Count > 0)
                    return tempList;

                tempList = IsTwoPair(theValueHand);         //works
                if (tempList.Count > 0)
                    return tempList;

                tempList = IsOnePair(theValueHand);      //works
                if (tempList.Count > 0)
                    return tempList;

                tempList = HighCard(theValueHand);
                if (tempList.Count > 0)
                    return tempList;

                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string DisplayCards(List<Card> input)
        {
            // This was the original implementation. Whilst it's concise and uses flashy LINQ syntax it left a trailing comma.
  
            // Originally the returnStr variable was return straight away
            //string returnStr = input.Aggregate("", (current, card) => current +
            //    (card.CardName + ", "));


            // The updated implementation features a string builder to easily remove the trailing comma
            StringBuilder builder = new StringBuilder(input.Aggregate("", (current, card) => current +
                (card.CardName + ", ")));
            builder.Remove(builder.Length - 2, 1);

            // In C# strings are immutable objects. If you manipulate a string it will create a new string
            // and store the manipulated contents of the original string instance to it.
            // Whilst this is obfuscated from the user it can still have a noticeable performance impact as a new
            // string object has to be instantiated and the original string is discarded for the garbage collector to pick up.
            // However string builders user a pre-allocated memory buffer and a string is dynamically created by appending
            // new data to it.
            // Whilst the performance gains will be unnoticeable in a project of this size it is good practice to use
            // StringBuilders when a program finds itself appending and manipulating a string many times.

            return builder.ToString();
        }

        public List<Card> SortByValue(List<Card> hand)
        {
            return hand.OrderByDescending(CardName => CardName.Value).ToList();
        }


        public List<Card> SortBySuit(List<Card> hand)
        {
            return hand.OrderByDescending(CardName => CardName.Suit).ToList();
        }



        public List<Card> IsStraightFlush(List<Card> hList) //Five cards in numerical order, all of identical suits
        {
            int st = 0;
            tempList.Clear();
            List<Card> tempList2 = new List<Card>();
            List<Card> hList2 = new List<Card>();

            int cardCount = 0;

            //Straight check
            for (int i = 0; i < hList.Count; i++)
            {
                cardCount = hList.Count(card => card.Suit == hList[i].Suit);
                if (cardCount == 5)
                    tempList.Add(hList[i]);
                
                if (tempList.Count == 5)
                    break;
            }
            if (tempList.Count == 0)
                return new List<Card>();

            // Flush check
            for (int i = 0; i < tempList.Count - 1; i++)
            {
                if (tempList[i].Value != tempList[i + 1].Value)
                {
                    if ((tempList[i].Value - tempList[i + 1].Value) == 1)
                    {
                        st++;
                    }
                    else
                        st = 0;
                }
                if (st == 4)
                {
                    tempList.Add(hList[i + 1]);
                    return tempList2;
                }
            }

            return new List<Card>();

        }

        public List<Card> IsFourKind(List<Card> hList) //Four cards of the same rank, and one side card or ‘kicker’.
        {
            int cardCount = 0;
            tempList.Clear();

            for (int i = 0; i < hList.Count; i++)
            {
                cardCount = hList.Count(card => card.Value == hList[i].Value);
                if (cardCount == 4)
                    tempList.Add(hList[i]);
                                
                if (tempList.Count == 4)
                    return tempList;
            }
       
            return new List<Card>();
        }

        public List<Card> IsFullHouse(List<Card> hList) //Three cards of the same rank, and two cards of
        {
            int cardCount = 0;
            tempList.Clear();
            //int i;
            List<Card> tempList2 = new List<Card>();
            List<Card> hList2 = new List<Card>();

            for (int i = 0; i < hList.Count; i++)
            {
                cardCount = hList.Count(card => card.Value == hList[i].Value);
                if (cardCount == 3)
                {
                    tempList.Add(hList[i]);
                    cardCount = 0;
                }
            }
            if (tempList.Count == 0)
                return new List<Card>();

            hList2 = (hList.Except(tempList)).ToList(); 
            //Except works like .Skip() but 
            //skips objects instead of indexes

            for (int i = 0; i < hList2.Count; i++)
            {
                cardCount = hList2.Count(card => card.Value == hList2[i].Value);
                if (cardCount == 2)
                {
                    tempList2.Add(hList2[i]);
                    cardCount = 0;
                }

            }
            if (tempList2.Count == 0)
                return new List<Card>();
            
            tempList.AddRange(tempList2);

            return tempList;
        }

        public List<Card> IsFlush(List<Card> hList) //Five cards of the same suit.
        {
            int cardCount = 0;
            tempList.Clear();

            for (int i = 0; i < hList.Count; i++)
            {
                cardCount = hList.Count(card => card.Suit == hList[i].Suit);
                if (cardCount == 5)
                {
                    tempList.Add(hList[i]);
                    cardCount = 0;
                }
                if (tempList.Count == 5)
                    return tempList;
            }

            return new List<Card>(); ;

        }

        public List<Card> IsStraight(List<Card> hList) //Five cards in sequence.
        {
            int st = 0;
            tempList.Clear();
            sortedList = hList;
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                if (sortedList[i].Value != sortedList[i + 1].Value)
                {
                    if ((sortedList[i].Value - sortedList[i + 1].Value) == 1)
                    {
                        st++;
                        tempList.Add(sortedList[i]);
                    }
                    else
                        st = 0;
                }
                if (st == 4)
                {
                    tempList.Add(sortedList[i + 1]);
                    return tempList;
                }
            }
            return new List<Card>(); ;
        }


        public List<Card> IsThreeKind(List<Card> hList) //Three cards of the same rank, and two 
        {                                               //unrelated side cards
            int cardCount = 0;
            tempList.Clear();

            for (int i = 0; i < hList.Count; i++)
            {
                cardCount = hList.Count(card => card.Value == hList[i].Value);
                if (cardCount == 3)
                {
                    tempList.Add(hList[i]);
                    cardCount = 0;
                }
                if (tempList.Count == 3)
                    return tempList;
            }
            return new List<Card>(); ;
        }

        public List<Card> IsTwoPair(List<Card> hList) //Two cards of a matching rank, another two cards of a different 
        {                                             //matching rank, and one side card.
            int cardCount = 0;
            tempList.Clear();
            //int i;
            List<Card> tempList2 = new List<Card>();
            List<Card> hList2 = new List<Card>();

            for (int i = 0; i < hList.Count; i++)
            {
                cardCount = hList.Count(card => card.Value == hList[i].Value);
                if (cardCount == 2)
                {
                    tempList.Add(hList[i]);
                    cardCount = 0;
                }
                if (tempList.Count == 2)
                    break;
            }
            if (tempList.Count < 2)
                return new List<Card>();

            hList2 = (hList.Except(tempList)).ToList();

            for (int i = 0; i < hList2.Count; i++)
            {
                cardCount = hList2.Count(card => card.Value == hList2[i].Value);
                if (cardCount == 2)
                {
                    tempList2.Add(hList2[i]);
                    cardCount = 0;
                }
                if (tempList2.Count == 2)
                    break;

            }
            if (tempList2.Count != 2)
                return new List<Card>();

            tempList.AddRange(tempList2);

            return tempList;
        }



        public List<Card> IsOnePair(List<Card> hList) //Two cards of a matching rank, and three unrelated side cards.
        {
            int cardCount = 0;
            tempList.Clear();

            for (int i = 0; i < hList.Count; i++)
            {
                cardCount = hList.Count(card => card.Value == hList[i].Value);
                if (cardCount == 2)
                {
                    tempList.Add(hList[i]);
                    cardCount = 0;
                }
                if (tempList.Count == 2)
                {
                    return tempList;
                }
            }

            return new List<Card>();
        }


        private List<Card> HighCard(List<Card> hList)
        {
            tempList.Add(hList[0]);
            return tempList;
        }
    }
}
