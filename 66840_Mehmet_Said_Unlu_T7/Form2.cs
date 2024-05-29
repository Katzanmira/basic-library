using _66840_Mehmet_Said_Unlu_T7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace _66840_Mehmet_Said_Unlu_T6
{
    public partial class Form2 : Form
    {
        private Library library = new Library();

        public Form2()
        {
            InitializeComponent();
            library.LoadBooksFromFile();
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                if (comboBox1.SelectedItem.ToString() == "Paper Book")
                {
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = false;
                }
                else if (comboBox1.SelectedItem.ToString() == "E-Book")
                {
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = true;
                    groupBox3.Enabled = false;
                }
                else if (comboBox1.SelectedItem.ToString() == "Audio Book")
                {
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var book = new PaperBook
            {
                Title = textBox1.Text,
                Author = textBox2.Text,
                Category = textBox3.Text,
                Type = comboBox1.SelectedItem.ToString(),
                ISBN = textBox4.Text,
                NumberOfPages = int.Parse(textBox5.Text)
            };
            library.AddBook(book);


            MessageBox.Show("Paper Book added successfully.");
            ClearFields();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var book = new EBook
            {
                Title = textBox1.Text,
                Author = textBox2.Text,
                Category = textBox3.Text,
                Type = comboBox1.SelectedItem.ToString(),
                Format = textBox6.Text,
                FileSize = textBox7.Text
            };
            library.AddBook(book);


            MessageBox.Show("E-Book added successfully.");
            ClearFields();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var book = new AudioBook
            {
                Title = textBox1.Text,
                Author = textBox2.Text,
                Category = textBox3.Text,
                Type = comboBox1.SelectedItem.ToString(),
                Narrator = textBox8.Text,
                Duration = textBox9.Text
            };
            library.AddBook(book);


            MessageBox.Show("Audio Book added successfully.");
            ClearFields();
        }

        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.ShowDialog();
        }
    }
}
