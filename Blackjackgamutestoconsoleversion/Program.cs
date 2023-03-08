using System;
using System.Collections.Generic;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Blackjack!");
        Console.WriteLine("----------------------");

        // Create a deck of cards and shuffle it
        List<string> deck = CreateDeck();
        ShuffleDeck(deck);

        // Set the initial balance and bet amount
        int balance = 100;
        int bet = 0;

        while (true)
        {
            Console.WriteLine("Balance: $" + balance);

            // Ask the player for their bet amount
            Console.Write("Enter your bet amount: ");
            bet = int.Parse(Console.ReadLine());
            balance -= bet;
            if (bet < balance)
            {
                
            }
                

            // Deal two cards to the player and two cards to the dealer
            List<string> playerHand = new List<string>();
            List<string> dealerHand = new List<string>();

            playerHand.Add(DealCard(deck));
            playerHand.Add(DealCard(deck));

            dealerHand.Add(DealCard(deck));
            dealerHand.Add(DealCard(deck));

            // Show the player's hand
            Console.WriteLine("Your hand:");
            foreach (string card in playerHand)
            {
                Console.WriteLine(card);
            }

            // Ask the player if they want to hit or stand
            while (true)
            {
                Console.Write("Do you want to hit or stand? ");
                string choice = Console.ReadLine().ToLower();

                if (choice == "hit")
                {
                    playerHand.Add(DealCard(deck));

                    // Show the updated hand
                    Console.WriteLine("Your hand:");
                    foreach (string card in playerHand)
                    {
                        Console.WriteLine(card);
                    }

                    // Check if the player busts
                    if (GetHandValue(playerHand) > 21)
                    {
                        Console.WriteLine("Bust! You lose.");
                        break;
                    }
                }
                else if (choice == "stand")
                {
                    // Show the dealer's hand
                    Console.WriteLine("Dealer's hand:");
                    foreach (string card in dealerHand)
                    {
                        Console.WriteLine(card);
                    }

                    // Keep dealing cards to the dealer until their hand is at least 17
                    while (GetHandValue(dealerHand) < 17)
                    {
                        dealerHand.Add(DealCard(deck));

                        // Show the updated hand
                        Console.WriteLine("Dealer's hand:");
                        foreach (string card in dealerHand)
                        {
                            Console.WriteLine(card);
                        }
                    }

                    // Compare the hands and determine the winner
                    int playerValue = GetHandValue(playerHand);
                    int dealerValue = GetHandValue(dealerHand);

                    if (playerValue > dealerValue && playerValue <= 21)
                    {
                        Console.WriteLine("You win!");
                        balance += bet * 2;
                    }
                    else if (dealerValue > playerValue && dealerValue <= 21)
                    {
                        Console.WriteLine("Dealer wins.");
                    }
                    else
                    {
                        Console.WriteLine("It's a tie!");
                        balance += bet;
                    }

                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 'hit' or 'stand'.");
                }
            }

            // Ask the player if they want to play another round
            Console.Write("Do you want to play another round? ");
            string playAgain = Console.ReadLine().ToLower();

                if (playAgain != "yes")
                if (playAgain != "ye")
                {
                Console.WriteLine("Thanks for playing!");
                break;
            }
        }
    }

    // Creates a standard deck of 52 cards
    static List<string> CreateDeck()
    {
        List<string> deck = new List<string>();

        string[] suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
        string[] ranks = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

        foreach (string suit in suits)
        {
            foreach (string rank in ranks)
            {
                deck.Add(rank + " of " + suit);
            }
        }

        return deck;
    }

    // Shuffles a deck of cards using the Fisher-Yates algorithm
    static void ShuffleDeck(List<string> deck)
    {
        Random random = new Random();

        for (int i = 0; i < deck.Count; i++)
        {
            int j = random.Next(i, deck.Count);
            string temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }

    // Deals a card from the deck
    static string DealCard(List<string> deck)
    {
        string card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    // Calculates the value of a hand
    static int GetHandValue(List<string> hand)
    {
        int value = 0;
        int aceCount = 0;

        foreach (string card in hand)
        {
            string rank = card.Substring(0, card.IndexOf(' '));

            if (rank == "Ace")
            {
                value += 11;
                aceCount++;
            }
            else if (rank == "King" || rank == "Queen" || rank == "Jack")
            {
                value += 10;
            }
            else
            {
                value += int.Parse(rank);
            }
        }

        while (value > 21 && aceCount > 0)
        {
            value -= 10;
            aceCount--;
        }

        return value;
    }
}