﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerEvaluator
{
    public class Card
    {
        private enum SuitValues
        {
            Clubs = 0,
            Diamonds = 1,
            Hearts = 2,
            Spades = 3
        }

        private int value;
        private int suit;
        private string cardName;

        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public int Suit
        {
            get { return suit; }
            set { suit = value; }
        }

        public string CardName
        {
            get { return cardName; }
            set { cardName = value; }
        }

        // Change to use enum
        public Card(int cardValue, int cardSuit)
        {
            Value = cardValue;
            Suit = cardSuit;
            //string tempSuit = AssignCardName(cardSuit);
            string suitName;
            if (cardSuit == 0)
                suitName = "Clubs";
            else if (cardSuit == 1)
                suitName = "Diamonds";
            else if (cardSuit == 2)
                suitName = "Hearts";
            else
                suitName = "Spades";
            CardName = cardValue.ToString() + " of " + suitName;
        }

       
    }
}
