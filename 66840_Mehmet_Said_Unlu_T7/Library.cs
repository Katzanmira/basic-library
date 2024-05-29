using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _66840_Mehmet_Said_Unlu_T7
{
    public class Library
    {
        private List<Book> books = new List<Book>();
        public const string FileName = "books.txt";

        public void AddBook(Book book)
        {
            books.Add(book);
            SaveBooksToFile();
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public void SaveBooksToFile()
        {
            var lines = books.Select(b => $"{b.Type};{b.Title};{b.Author};{b.Category};{GetBookDetails(b)}");

            using (FileStream fs = new FileStream("Temp.txt", FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }
            }

            File.Copy("Temp.txt", FileName, true); 
            File.Copy(FileName, "Backup.txt", true);
        }

        private string GetBookDetails(Book book)
        {
            switch (book)
            {
                case PaperBook paperBook:
                    return $"{paperBook.ISBN}; {paperBook.NumberOfPages}";
                case EBook eBook:
                    return $"{eBook.Format}; {eBook.FileSize}";
                case AudioBook audioBook:
                    return $"{audioBook.Narrator}; {audioBook.Duration}";
                default:
                    throw new InvalidDataException($"Unknown book type: {book.Type}");
            }
        }

        public void LoadBooksFromFile()
        {
            if (File.Exists(FileName))
            {
                var lines = File.ReadAllLines(FileName);
                foreach (var line in lines)
                {
                    var bookData = line.Split(';').Select(s => s.Trim()).ToArray();
                    var type = bookData[0];
                    Book book;
                    switch (type)
                    {
                        case "Paper Book":
                            book = new PaperBook
                            {
                                Title = bookData[1],
                                Author = bookData[2],
                                Category = bookData[3],
                                Type = type,
                                ISBN = bookData[4],
                                NumberOfPages = int.Parse(bookData[5])
                            };
                            break;
                        case "E-Book":
                            book = new EBook
                            {
                                Title = bookData[1],
                                Author = bookData[2],
                                Category = bookData[3],
                                Type = type,
                                Format = bookData[4],
                                FileSize = bookData[5]
                            };
                            break;
                        case "Audio Book":
                            book = new AudioBook
                            {
                                Title = bookData[1],
                                Author = bookData[2],
                                Category = bookData[3],
                                Type = type,
                                Narrator = bookData[4],
                                Duration = bookData[5]
                            };
                            break;
                        default:
                            throw new InvalidDataException($"Unknown book type: {type}");
                    }
                    books.Add(book);
                }
            }
        }
    }
}
