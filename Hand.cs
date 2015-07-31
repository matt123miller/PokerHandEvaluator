using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;

namespace Poker.Models
{
    internal class Hand
    {
        //USE LINQ .Except


        public List<Card> sortedList = new List<Card>();

        public List<Card> handList;

        public List<Card> tempList = new List<Card>();

        public List<Card> returnList = new List<Card>();



        public enum WinningHands
        {
            StraightFlush,
            FourKind,
            FullHouse,
            Flush,
            Straight,
            ThreeKind,
            TwoPair,
            OnePair
        }


        public Hand()
        {
            handList = new List<Card>(); //using list as hand will vary in size.
            //handList.Add(Dealer.deck1.DealCard());
            //handList.Add(Dealer.deck1.DealCard());

            //handList.Add(new Card(10, 2));
            //handList.Add(new Card(2, 1));
            //handList.Add(new Card(6, 2));
            //handList.Add(new Card(7, 2));
            //handList.Add(new Card(8, 2));
            //handList.Add(new Card(9, 2));
            //handList.Add(new Card(8, 2));

        }

        public List<Card> EvalHands()
        {
            List<Card> theValueHand = new List<Card>();
            theValueHand = SortByValue(handList);
            List<Card> theSuitHand = new List<Card>();
            theSuitHand = SortBySuit(handList);

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
            string inputStr = input.Aggregate("", (current, card) => current +
                (card.CardName + ", "));
            return inputStr;
        }

        public List<Card> SortByValue(List<Card> hList)
        {
            return hList.OrderByDescending(CardName => CardName.Value).ToList();
        }
        

        public List<Card> SortBySuit(List<Card> hList)
        {
            return hList.OrderByDescending(CardName => CardName.Suit).ToList();
        }



        public List<Card> IsStraightFlush(List<Card> hList) //Five cards in numerical order, all of identical suits
        {
            //Straight check
            int st = 0;
            tempList.Clear();
            List<Card> tempList2 = new List<Card>();
            List<Card> hList2 = new List<Card>();

            int cardCount = 0;

            for (int i = 0; i < hList.Count; i++)
            {
                cardCount = hList.Count(card => card.Suit == hList[i].Suit);
                if (cardCount == 5)
               // {
                    tempList.Add(hList[i]);
                //    cardCount = 0;
                //}
                if (tempList.Count == 5)
                    break;
            }
            if (tempList.Count == 0)
                return new List<Card>();


            for (int i = 0; i < tempList.Count - 1; i++)
            {
                if (tempList[i].Value != tempList[i + 1].Value)
                {
                    if ((tempList[i].Value - tempList[i + 1].Value) == 1)
                    {
                        st++;
                        //tempList.Add(hList[i]);
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
            //if (tempList.Count == 0)
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
               // {
                    tempList.Add(hList[i]);
                   // cardCount = 0;
                //}
                if (tempList.Count == 4)
                    return tempList;
            }

            return new List<Card>();;

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

            hList2 = (hList.Except(tempList)).ToList(); //Except works like .Skip() but 
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

            foreach (Card card in tempList2)
            {
                tempList.Add(card);
            }

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

            return new List<Card>();;

        }

        public List<Card> IsStraight(List<Card> hList) //Five cards in sequence.
        {
            int st = 0;
            tempList.Clear();
            sortedList = SortByValue(hList);
            for (int i = 0; i < sortedList.Count-1; i++)
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
            return new List<Card>();;
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
            return new List<Card>();;
        }

        public List<Card> IsTwoPair(List<Card> hList) //Two cards of a matching rank, another two cards of a different 
        {
            //matching rank, and one side card.
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

            //foreach (Card card in tempList2)
           // {
                tempList.AddRange(tempList2);
           // }

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
            //if (tempList.Count == 0)
                return new List<Card>();

        }


        private List<Card> HighCard(List<Card> hList)
        {
            tempList.Add(hList[0]);
            return tempList;
        }
        
    }
}

        //private bool CalledTwiceOnePair(List<Card> hList)
        //{
        //    int cardCount = 0;
        //    bool tempBool1 = false;
        //    bool tempBool2 = false;
                
        //    sortedList = SortByValue(hList);
        //    for (int i = 0; i < hList.Count; i++)
        //    {
        //        if (sortedList.Count(card => card.Value == sortedList[i].Value) == 1)
        //        {
        //            sortedList.RemoveAt(i);
        //            cardCount++;
        //        }
        //        if (cardCount == 2)
        //        {
        //            tempBool1 = true;
        //            break;
        //        }
        //    }
        //    cardCount = 0;
        //    foreach (Card c in sortedList)
        //    {
        //        cardCount = sortedList.Count(card => card.Value == c.Value);

        //        if (cardCount == 2)
        //        {
        //            tempBool2 = true;
        //            break;
        //        }
        //    }
        //    if (tempBool2 == true && tempBool1 == true)
        //        return true;
        //    else
        //        return false;
        //}