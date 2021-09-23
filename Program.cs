using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Randomic;


namespace RandomDeckCardTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DeckCard exceptionCard = new DeckCard();
            List<DeckCard> exceptionCardList = new List<DeckCard>();
            DeckCard newCard = new DeckCard();

            try
            {
                Console.Write("How many cards will get randomly? ");
                int cardsQtyToBeGetted = 0;
                if (!int.TryParse(Console.ReadLine(), out cardsQtyToBeGetted))
                    throw new FormatException("Invalid value. Must be a number. Restart the application\n");

                Console.Write("How many cards will except? ");
                int cardsQtyToExcepted = 0;
                if (!int.TryParse(Console.ReadLine(), out cardsQtyToExcepted))
                    throw new FormatException("Invalid value. Must be a number. Restart the application\n");

                Console.WriteLine("");

                if (cardsQtyToExcepted > 0)
                {
                    for (int i = 1; i <= cardsQtyToExcepted; i++)
                    {
                        Console.WriteLine($"Except card {i} settings: ");

                        // get Except card {i} suit 
                        Console.Write("Type card suit [\u2665:Heart / \u2663:Club / \u2660:Spade / \u2666:Diamond]: ");
                        DeckCardSuit cardSuit;
                        if (!Enum.TryParse<DeckCardSuit>(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower()), out cardSuit))
                        {
                            throw new FormatException("Invalid value. Must be Heart/Club/Spade/Diamond. Restart the application\n");
                        }

                        // get Except card {i} value
                        Console.Write("Type card value [2~10/J/Q/K/A]: ");
                        string cardSymbol = Console.ReadLine();
                        if (int.TryParse(cardSymbol, out _))
                        {
                            int cardSymbolNumber = int.Parse(cardSymbol);
                            if (cardSymbolNumber < 2 || cardSymbolNumber > 10)
                                throw new FormatException("Invalid value. Must be 2~10/J/Q/K/A. Restart the application\n");
                        }
                        else
                        {
                            cardSymbol = cardSymbol.ToUpper();
                            if (!(cardSymbol.Contains('J') || cardSymbol.Contains('Q') ||
                            cardSymbol.Contains('K') || cardSymbol.Contains('A')))
                            {
                                throw new FormatException("Invalid value. Must be J/K/Q/A. Restart the application\n");
                            }

                        }

                        // Add Except card {i} to list
                        if (cardsQtyToExcepted > 1)
                        {
                            exceptionCardList.Add(new DeckCard(cardSuit, cardSymbol, 1));

                            // Print except card {i} from list
                            Console.Write($"Except card {i}: ");
                            PrintCard(exceptionCardList[i - 1]);
                            Console.WriteLine("\n");
                        }
                        else
                        {
                            exceptionCard = new DeckCard(cardSuit, cardSymbol, 1);

                            // Print the only except card
                            Console.Write($"Except card: ");
                            PrintCard(exceptionCard);
                            Console.WriteLine("\n");
                        }
                    }

                    if (cardsQtyToExcepted > 1)
                    {
                        // Print list of excepted cards
                        Console.WriteLine("Excepted cards:");
                        for (int j = 0; j < exceptionCardList.Count; j++)
                        {
                            PrintCard(exceptionCardList[j]);
                            Console.Write(" ");
                            if ((j % 10) == 0 && (j > 0))
                                Console.WriteLine(" \n");
                        }
                    }
                }

                Console.WriteLine("\n\nSorted Cards: ");

                for (int i = 1; i <= cardsQtyToBeGetted; i++)
                {
                    if (cardsQtyToExcepted == 0)
                    {
                        newCard = RandomDeckCard.Get();
                    }
                    else if (cardsQtyToExcepted == 1)
                    {
                        newCard = RandomDeckCard.GetExceptingOne(exceptionCard);
                    }
                    else
                    {
                        newCard = RandomDeckCard.GetExceptingList(exceptionCardList);
                    }

                    PrintCard(newCard);
                    if ((i % 20) == 0)
                        Console.WriteLine(" \n");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Console.WriteLine(Ex.StackTrace);
            }

            Console.WriteLine("\n");
        }

        static void PrintCard(DeckCard cardToBePrinted)
        {

            ConsoleColor BackgroundColor = Console.BackgroundColor;
            ConsoleColor ForegroundColor = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = cardToBePrinted.SuitColor;
            Console.Write(cardToBePrinted);
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.Write(" ");

            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
        }
    }
}

