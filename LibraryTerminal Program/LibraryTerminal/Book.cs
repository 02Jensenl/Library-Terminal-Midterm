using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryTerminal
{
    class Book : Item
    {
        public int PageCount { get; set; }

        public Book(string Title, string Author, int ReleaseYear, int PageCount) : base(Title, Author, ReleaseYear)
        {
            this.PageCount = PageCount;
        }

        public Book(string Title, string Author, ItemStatus Status, DateTime DueDate, int ReleaseYear, int PageCount) : base(Title, Author, Status, DueDate, ReleaseYear)
        {
            this.PageCount = PageCount;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"# of Pages: {PageCount}");
        }
    }
}
