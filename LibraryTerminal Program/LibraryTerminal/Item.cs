using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryTerminal
{
    abstract class Item
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public ItemStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public int ReleaseYear { get; set; }
      
        public Item() { }
        public Item(string Title, string Author, int ReleaseYear)
        {
            this.Title = Title;
            this.Author = Author;
            this.Status = 0;
            this.DueDate = DateTime.Now;
            this.ReleaseYear = ReleaseYear;
        }

        public Item(string Title, string Author, ItemStatus Status, DateTime DueDate, int ReleaseYear)
        {
            this.Title = Title;
            this.Author = Author;
            this.Status = Status;
            this.DueDate = DueDate;
            this.ReleaseYear = ReleaseYear;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Year Released: {ReleaseYear}");
            CheckDueDate();
        }
        public void CheckDueDate()
        {
            if (Status.Equals(ItemStatus.CheckedOut))
            {
                int result = DateTime.Compare(DueDate, DateTime.Now);
                if (result <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Return Date: {DueDate:d}");
                    Console.ResetColor();
                    Status = ItemStatus.Overdue;
                }
                else if (result > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Return Date: {DueDate:d}");
                    Console.ResetColor();
                }
            }
            else if (Status.Equals(ItemStatus.Overdue))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Status: {Status}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Status: {Status}");
            }
        }
    }
}

