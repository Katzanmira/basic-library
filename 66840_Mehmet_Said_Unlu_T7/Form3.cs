using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _66840_Mehmet_Said_Unlu_T7
{
    public partial class Form3 : Form
    {
        private Library library = new Library();
        private List<string> lines = new List<string>();
        private int currentBookIndex = 0;


        public Form3()
        {
            InitializeComponent();
            LoadBooks();
            DisplayCurrentBook();
            EnableEditing(false);
        }
        private void LoadBooks()
        {
            library.LoadBooksFromFile();
        }

        private void DisplayCurrentBook()
        {
            if (library.GetAllBooks().Count == 0)
            {
                MessageBox.Show("No books available.");
                return;
            }

            var currentBook = library.GetAllBooks()[currentBookIndex];

            textBoxTitle.Text = currentBook.Title;
            textBoxAuthor.Text = currentBook.Author;
            textBoxCategory.Text = currentBook.Category;
            textBoxType.Text = currentBook.Type;

            if (currentBook is PaperBook)
            {
                var paperBook = (PaperBook)currentBook;
                label3.Text = "ISBN:";
                label6.Text = "Page:";
                textBox3.Text = paperBook.ISBN;
                textBox6.Text = paperBook.NumberOfPages.ToString();
            }
            else if (currentBook is EBook)
            {
                var eBook = (EBook)currentBook;
                label3.Text = "Format:";
                label6.Text = "File Size:";
                textBox3.Text = eBook.Format;
                textBox6.Text = eBook.FileSize;
            }
            else if (currentBook is AudioBook)
            {
                var audioBook = (AudioBook)currentBook;
                label3.Text = "Narrator:";
                label6.Text = "Duration:";
                textBox3.Text = audioBook.Narrator;
                textBox6.Text = audioBook.Duration;
            }

            label8.Text = $"{currentBookIndex + 1} / {library.GetAllBooks().Count}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentBookIndex > 0)
            {
                currentBookIndex--;
                DisplayCurrentBook();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentBookIndex < library.GetAllBooks().Count - 1)
            {
                currentBookIndex++;
                DisplayCurrentBook();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "Edit")
            {
                EnableEditing(true);
                button5.Text = "Apply";
            }
            else if (button5.Text == "Apply")
            {
                var currentBook = library.GetAllBooks()[currentBookIndex];
                currentBook.Title = textBoxTitle.Text;
                currentBook.Author = textBoxAuthor.Text;
                currentBook.Category = textBoxCategory.Text;

                if (currentBook is PaperBook)
                {
                    ((PaperBook)currentBook).ISBN = textBox3.Text;
                    ((PaperBook)currentBook).NumberOfPages = int.Parse(textBox6.Text);
                }
                else if (currentBook is EBook)
                {
                    ((EBook)currentBook).Format = textBox3.Text;
                    ((EBook)currentBook).FileSize = textBox6.Text;
                }
                else if (currentBook is AudioBook)
                {
                    ((AudioBook)currentBook).Narrator = textBox3.Text;
                    ((AudioBook)currentBook).Duration = textBox6.Text;
                }

                library.GetAllBooks()[currentBookIndex] = currentBook;

                library.SaveBooksToFile();

                MessageBox.Show("Book information updated successfully.");
                EnableEditing(false);
                button5.Text = "Edit";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this book?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Delete();

                MessageBox.Show("Book deleted successfully.");

                if (library.GetAllBooks().Count == 0)
                {
                    this.Close();
                }
                else
                {
                    if (currentBookIndex >= library.GetAllBooks().Count)
                    {
                        currentBookIndex = library.GetAllBooks().Count - 1;
                    }
                    DisplayCurrentBook();
                }
            }
        }
        private void EnableEditing(bool enable)
        {
            textBoxTitle.Enabled = enable;
            textBoxAuthor.Enabled = enable;
            textBoxCategory.Enabled = enable;
            textBox3.Enabled = enable;
            textBox6.Enabled = enable;

            textBoxType.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.ShowDialog();
        }
        private void Delete()
        {
            FileStream fs = new FileStream(Library.FileName, FileMode.Open, FileAccess.Read);
            FileStream fs2 = new FileStream("Temp.txt", FileMode.Create, FileAccess.Write);
            StreamReader sr = new StreamReader(fs);
            using (StreamWriter sw = new StreamWriter(fs2))
            {
                string line;
                int lineIndex = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (lineIndex != currentBookIndex)
                    {
                        sw.WriteLine(line);
                    }
                    lineIndex++;
                }
            }

            sr.Close();
            fs.Close();
            fs2.Close();

            FileStream fs3 = new FileStream(Library.FileName, FileMode.Create, FileAccess.Write);
            FileStream fs4 = new FileStream("Temp.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr2 = new StreamReader(fs4);
            StreamWriter sw2 = new StreamWriter(fs3);

            while (!sr2.EndOfStream)
            {
                string s = sr2.ReadLine();
                sw2.WriteLine(s);
            }

            sr2.Close();
            sw2.Close();
            fs3.Close();
            fs4.Close();

            MessageBox.Show("Current Line is deleted");

            library.GetAllBooks().RemoveAt(currentBookIndex);

            library.GetAllBooks().Clear();

            LoadBooks();

            if (currentBookIndex >= library.GetAllBooks().Count)
            {
                currentBookIndex = library.GetAllBooks().Count - 1;
            }

            DisplayCurrentBook();
        }
    }
}
