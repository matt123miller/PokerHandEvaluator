# PokerHandEvaluator
### A system for emulating Texas Hold'em poker using C# and LINQ. 
Originally designed to work with to simulate online poker with an ASP.NET front end and a backend connected to a MySQL database. The poker part of that project was separated and now adapted as a command line application. It is hoped that anybody could employ this code to make a traditional card game and expand it as needs be. 
Can be sumarised as: A `Table` maintains a `Deck` of `Cards` which it deals to `Hand` objects. 


#### [Deck](https://github.com/matt123miller/PokerHandEvaluator/blob/master/Deck.cs)

The deck contains a deck of cards and controls access to them. It is tasked with creating 52 standard playing cards, shuffling itself and returning a card when requested. It shuffles through an implementation of the [Fisher-Yates](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle "Fisher-Yates Shuffle") shuffling algorithm. 

#### [Card](https://github.com/matt123miller/PokerHandEvaluator/blob/master/Card.cs)

Cards are simple objects that contain a `Suit`, `Value` and `Name` (which combines the 2 as a string)

#### [Hand](https://github.com/matt123miller/PokerHandEvaluator/blob/master/Hand.cs)

A Hand is only a collection of `Card` objects. Calling `GiveCard(Card)` allows you to add to it.

#### [Table](https://github.com/matt123miller/PokerHandEvaluator/blob/master/Table.cs)
The Table has a deck object and functions as a mediator between the deck and various hands, tasked with dealing cards via the `DealCardTo(Hand)` method. It's main function however is to evaluate any given hand of cards and returns the best possible combination of cards with `EvaluateHand(List<Card>)` 
It displays the cards in any given hand as well in a suitable format. This reposibility may be moved to the `Hand` itself as an instance method. Both approaches make thematic sense however a `Hand` should probably look after it's own affairs.

### [License](..LICENSE.txt)
Hosted under the MIT license. 

Please contact me via Twitter [@matt123miller](https://twitter.com/matt123miller "@matt123miller")
