using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerEvaluator
{
    class Hand
    {

        private List<Card> cards;

        

        public Hand()
        {
            cards = new List<Card>(); //using list as hand will vary in size.
        }

        public void GiveCard(Card card)
        {
            cards.Add(card);
        }

        public List<Card> Cards()
        {
            return cards;
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