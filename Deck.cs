using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerEvaluator
{
    public class Deck
    {
        private List<Card> deck;
        private const int totalCards = 52;
        private const int suits = 4;
        private const int ranks = 13;

        public Deck()
        {
            deck = new List<Card>();
            List<int> rankValue = new List<int>();
            List<int> suitValue = new List<int>();

            // Create the 13 rank values
            for (int i = 1; i <= ranks; i++)
            {
                rankValue.Add(i);
            }
            // Create the 4 suit values
            for (int i = 1; i <= suits; i++)
            {
                suitValue.Add(i);
            }

            // Create each of the 52 cards
            for (int i = 0; i < totalCards; i++)
            {
                // suit[i / 13] because there are 52 cards in the deck, 52/13 is 4
                // i/13 will always round down, therefore returning each suit until all 11 cards are made
                Card card = new Card(rankValue[i % 11], suitValue[i / 13]);
                deck.Add(card);
            }
        }

        // Remove the last card
        public Card DrawCard()
        {
            Card card = deck.Last();
            deck.Remove(card);
            return card;
        }

        // Simple implementation of the Fisher-Yates shuffle method.
        public void Shuffle()
        {
            Random randomNum = new Random();

            for (int first = 0; first < deck.Count; first++)
            {
                int second = randomNum.Next(totalCards);
                Card tempCard = deck[first];
                deck[first] = deck[second];
                deck[second] = tempCard;
            }
        }
    }
}
