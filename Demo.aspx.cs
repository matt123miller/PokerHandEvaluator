using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Poker.Models;

namespace Poker
{
    public partial class Demo : System.Web.UI.Page
    {
        private string result;

        protected void Page_Load(object sender, EventArgs e)
        {
            Deck deck1 = new Deck();
            deck1.Shuffle();

            Hand hand = new Hand();
            hand.handList.Add(deck1.DealCard());
            hand.handList.Add(deck1.DealCard());
            hand.handList.Add(deck1.DealCard());
            hand.handList.Add(deck1.DealCard());
            hand.handList.Add(deck1.DealCard());
            hand.handList.Add(deck1.DealCard());
            hand.handList.Add(deck1.DealCard());

            Label1.Text = "These are all the cards available: " + hand.DisplayCards(hand.handList);

            List<Card> hList = hand.EvalHands();

            if (hList.Count == 0)
                result = "There has been an error";
            else
                result = hand.DisplayCards(hList);

            Label2.Text = "This is the best possible hand: " + result;
        }
    }
}