using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerEvaluator
{
    class Program
    {

        static void Main(string[] args)
        {
            // Create the table and a hand for the user.
            Table table = new Table(); 
            Hand hand = new Hand();

            int playerCards = 5;

            // Display the tables
            string initialCards = table.DisplayCards(table.tableCards);
            Console.WriteLine("These are the 2 table cards");
            Console.WriteLine("{0} \n", initialCards);

            Console.WriteLine("You will now be dealt 5 cards.");

            for (int i = 0; i < playerCards; i++)
            {
                table.DealCardTo(hand);
            }

            string playerHand = table.DisplayCards(hand.Cards());
            Console.WriteLine("These are your 5 cards.");
            Console.WriteLine("{0} \n", playerHand);

            // Evaluate the combined list to find the best possible card combination
            List<Card> bestHand = table.EvaluateHand(hand.Cards());
            string result = table.DisplayCards(bestHand);

            Console.WriteLine("This is the best possible hand composed of the 2 table cards and your 5 cards.");
            Console.WriteLine(result);

            Console.ReadLine();
        }
    }
}
