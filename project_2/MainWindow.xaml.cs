using System.Windows;
using System.Windows.Controls;
using project_2.Model;
using project_2.Service;
using project_2.Service.Impl;

namespace project_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public MainWindow()
        {
            InitializeComponent();
            _authorService = new AuthorService();
            _bookService = new BookService();
            LoadAuthors();
            LoadAllBooks();
        }

        private void LoadAllBooks()
        {
            var books = _bookService.GetAllBooks();
            dgBooks.ItemsSource = books;
        }

        private void LoadAuthors()
        {
            var authors = _authorService.GetAllAuthors();
            cboAuthors.ItemsSource = authors;
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cboAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboAuthors.SelectedItem is Author selectedAuthor) {
                var books = _bookService.GetBooksByAuthorId(selectedAuthor.Id);
                dgBooks.ItemsSource = books;
            }
        }
    }
}
