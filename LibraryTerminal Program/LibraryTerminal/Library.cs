using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace LibraryTerminal
{
    class Library
    {
        public List<Item> Catalog { get; set; }
        public Library()
        {
            Catalog = new List<Item>();

            Item b1 = new Book("How Much of These Hills Is Gold", "C Pam Zhang", 2020, 368);
            Item b2 = new Book("Jade City", "Fonda Lee", 2017, 560);
            Item b3 = new Book("The Poppy War", "R F Kuang", 2018, 544);
            Item b4 = new Book("Beowulf: A New Translation", "Maria Dahvana Headley", 2020, 176);
            Item b5 = new Book("Pachinko", "Min Jin Lee", 2017, 490);
            Catalog.Add(b1);
            Catalog.Add(b2);
            Catalog.Add(b3);
            Catalog.Add(b4);
            Catalog.Add(b5);

            Item c1 = new CD("1", "The Beatles", 2000, 27);
            Item c2 = new CD("Thriller", "Michael Jackson", 1982, 9);
            Item c3 = new CD("21", "Adele", 2011, 11);
            Catalog.Add(c1);
            Catalog.Add(c2);
            Catalog.Add(c3);

            Item d1 = new DVD("Finding Nemo", "Andrew Stanton (Pixar)", 2003, 100);
            Item d2 = new DVD("Spider-Man", "Sam Raimi (Sony Pictures)", 2002, 121);
            Item d3 = new DVD("Avatar", "James Cameron (20th Century Fox)", 2009, 162);
            Catalog.Add(d1);
            Catalog.Add(d2);
            Catalog.Add(d3);

            Item mag1 = new Magazine("They're Making Another Super Mario Movie", "WIRED", 2018, 8);
            Item mag2 = new Magazine("The World's Rarest Pair of Tweezers", "Collectors", 2015, 9);
            Item mag3 = new Magazine("Mount Everest: The Secrets it Holds", "National Geographic", 1999, 2);
            Catalog.Add(mag1);
            Catalog.Add(mag2);
            Catalog.Add(mag3);
        }

        public Library(List<Item> Catalog)
        {
            this.Catalog = Catalog;
        }

        // Prints the Library's Catalog into the console as individual entries.
        // Each entry has their information displayed on the console.
        public void PrintItems()
        {
            for (int i = 0; i < Catalog.Count; i++)
            {
                Item item = Catalog[i];
                item.PrintInfo();
                CnslFormatter.MakeLineSpace(1);
            }
        }

        // Takes a List of Items and returns another List contains Items with an Author value that matches the string input
        public List<Item> SearchByAuthor(string input)
        {
            List<Item> results = new List<Item>();
            foreach (Item itemMatch in Catalog)
            {
                if (itemMatch.Author.Contains(input))
                {
                    results.Add(itemMatch);
                }
            }
            return results;
        }

        // Takes a List of Items and returns another List contains Items with an Title value that matches the string input
        public List<Item> SearchByTitle(string input)
        {
            List<Item> results = new List<Item>();
            foreach (Item itemMatch in Catalog)
            {
                if (itemMatch.Title.Contains(input))
                {
                    results.Add(itemMatch);
                }
            }
            return results;
        }

        // Finds the specified Item within the Library's Catalog and checks to see if it is available to be checked out.
        // If available, the Item's status is changed to CheckedOut, and their DueDate value is changed to 14 days following the current date/time.
        // If it is already checked out, the console states that the item is not available.
        public void Checkout(List<Item> itemsList)
        {
            Console.Clear();
            for (int i = 0; i < itemsList.Count; i++)
            {
                Console.Write($"Item {i + 1}. ");
                itemsList[i].PrintInfo();
                Console.WriteLine();
            }
            string input = CnslFormatter.PromptForInput($"Please select the item you would like to checkout. [{CnslFormatter.MoreThanOne(itemsList)}]: ");
            if (Int32.TryParse(input, out int num))
            {
                int index = num - 1;

                if (index < 0 || index >= itemsList.Count)
                {
                    Console.WriteLine($"Input out of range. {CnslFormatter.MoreThanOne(itemsList)}.");
                }
                else
                {
                    if (itemsList[index].Status == ItemStatus.OnShelf)
                    {
                        Console.WriteLine(Environment.NewLine + "You have checked out: ");
                        Console.WriteLine($"   {itemsList[index].Title} by {itemsList[index].Author}");
                        DateTime checkoutDate = DateTime.Now;
                        itemsList[index].Status = ItemStatus.CheckedOut;
                        itemsList[index].DueDate = checkoutDate.AddDays(14);
                        Console.WriteLine($"   Due back by {itemsList[index].DueDate:d}");
                        CnslFormatter.PauseByAnyKey();
                    }
                    else if (itemsList[index].Status == ItemStatus.CheckedOut || itemsList[index].Status == ItemStatus.Overdue)
                    {
                        Console.WriteLine("\nItem is already checked out. Cannot complete checkout at this time.");
                        CnslFormatter.PauseByAnyKey();
                    }
                    else
                    {
                        Console.WriteLine("\nCannot complete checkout at this time.");
                        CnslFormatter.PauseByAnyKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("\nNon-Integer input detected. Please enter an integer next time.");
            }
        }

        // Finds the specified Item within the Library's Catalog and checks to see if it is checked out to then be checked back in.
        // If checked out, the Item's status is changed to OnShelf (checked in).
        // If it is already checked in, the console states that the item is already checked in.
        public void CheckIn(List<Item> itemsList)
        {
            bool proceed = CnslFormatter.AskYesOrNo($"Would you like to return an item? ");

            while (proceed)
            {
                Console.Clear();
                for (int i = 0; i < itemsList.Count; i++)
                {
                    Console.Write($"Item {i + 1}. ");
                    itemsList[i].PrintInfo();
                    Console.WriteLine();
                }
                string input = CnslFormatter.PromptForInput($"Please select the item you would like to return? [{CnslFormatter.MoreThanOne(itemsList)}]: ");
                if (Int32.TryParse(input, out int num))
                {
                    int index = num - 1;

                    if (index < 0 || index >= itemsList.Count)
                    {
                        Console.WriteLine($"Input out of range. {CnslFormatter.MoreThanOne(itemsList)}.");
                    }
                    else
                    {
                        if (itemsList[index].Status == ItemStatus.CheckedOut || itemsList[index].Status == ItemStatus.Overdue)
                        {
                            Console.WriteLine(Environment.NewLine + "You have checked in: ");
                            Console.WriteLine($"   {itemsList[index].Title} by {itemsList[index].Author}");
                            itemsList[index].Status = ItemStatus.OnShelf;
                            proceed = false;
                        }
                        else
                        {
                            Console.WriteLine("Cannot complete checkout at this time.");
                            CnslFormatter.PauseByAnyKey();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Non-Integer input detected. Please enter an integer.");
                }
            }
            CnslFormatter.PauseByAnyKey();
        }
        // Prompts User for multiple inputs that will serve as the info for a new Item.
        // The new Item will then be placed into the Catalog
        public void AddNewItem()
        {
            string newTitle = CnslFormatter.PromptForInput("Please enter the Title of the new Item: ");
            string newAuthor = CnslFormatter.PromptForInput("Please enter the Author of the new Item: ");
            int newYear = -1;
            while (true)
            {
                string newYearStr = CnslFormatter.PromptForInput("Please enter the Release Year of the new Item: ");
                if (Int32.TryParse(newYearStr, out newYear))
                {
                    break;
                }
            }
            // Type Prompt, user enters a string to determine which kind of Item should be created through specialized calls.
            // Will repeat until a valid string value is given.
            while (true)
            {
                string newType = CnslFormatter.PromptForInput("Please enter the Type of the new Item [book, cd, dvd, magazine]: ");
                if (newType.ToLower().Equals("book"))
                {
                    Item newBook = MakeNewBook(newTitle, newAuthor, newYear);
                    Catalog.Add(newBook);
                    Console.WriteLine("New Book Added to Catalog!");
                    CnslFormatter.PauseByAnyKey();
                    break;
                }
                else if (newType.ToLower().Equals("cd"))
                {
                    Item newCD = MakeNewCD(newTitle, newAuthor, newYear);
                    Catalog.Add(newCD);
                    Console.WriteLine("New CD Added to Catalog!");
                    CnslFormatter.PauseByAnyKey();
                    break;
                }
                else if (newType.ToLower().Equals("dvd"))
                {
                    Item newDVD = MakeNewDVD(newTitle, newAuthor, newYear);
                    Catalog.Add(newDVD);
                    Console.WriteLine("New DVD Added to Catalog!");
                    CnslFormatter.PauseByAnyKey();
                    break;
                }
                else if (newType.ToLower().Equals("magazine"))
                {
                    Item newMagazine = MakeNewMagazine(newTitle, newAuthor, newYear);
                    Catalog.Add(newMagazine);
                    Console.WriteLine("New Magazine Added to Catalog!");
                    CnslFormatter.PauseByAnyKey();
                    break;
                }
            }
        }

        // Returns a new Book using base Item values for arguments, and specialized values given by user.
        // Input prompt will repeat until a valid integer is given.
        public Item MakeNewBook(string title, string author, int year)
        {
            int newPageCnt = -1;
            while (true)
            {
                string newPageCntStr = CnslFormatter.PromptForInput("Please enter the PageCount of the new Book: ");
                if (Int32.TryParse(newPageCntStr, out newPageCnt))
                {
                    break;
                }
            }

            Item newBook = new Book(title, author, year, newPageCnt);
            return newBook;
        }

        // Returns a new CD using base Item values for arguments, and specialized values given by user.
        public Item MakeNewCD(string title, string author, int year)
        {
            while(true)
            {
                try
                {
                    int newTracks = Int32.Parse(CnslFormatter.PromptForInput("Please enter the Tracks of the new CD: "));
                    Item newCD = new CD(title, author, year, newTracks);
                    return newCD;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }
        }

        // Returns a new DVD using base Item values for arguments, and specialized values given by user.
        // Input prompt will repeat until a valid integer is given.
        public Item MakeNewDVD(string title, string author, int year)
        {
            int newRunTime = -1;
            while (true)
            {
                string newRunTimeStr = CnslFormatter.PromptForInput("Please enter the RunTime of the new DVD: ");
                if (Int32.TryParse(newRunTimeStr, out newRunTime))
                {
                    break;
                }
            }

            Item newDVD = new DVD(title, author, year, newRunTime);
            return newDVD;
        }

        // Returns a new Magazine using base Item values for arguments, and specialized values given by user.
        // Input prompt will repeat until a valid integer between 1 and 12 is given.
        public Item MakeNewMagazine(string title, string author, int year)
        {
            int newMonth = -1;
            while (true)
            {
                string newRunTimeStr = CnslFormatter.PromptForInput("Please enter the Month of Publishing of the new Magazine: ");
                if (Int32.TryParse(newRunTimeStr, out newMonth))
                {
                    if (!(newMonth < 1 || newMonth > 12))
                    {
                        break;
                    }
                }
            }
            Item newMagazine = new Magazine(title, author, year, newMonth);
            return newMagazine;
        }
    }
}


