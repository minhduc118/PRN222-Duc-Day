using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using project_1.Models;


namespace project_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>\
    //

  
    public partial class MainWindow : Window
    {
        private readonly NorthwindContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new NorthwindContext();
            LoadProduct();
        }

        private void LoadProduct()
        {
            var products = _context.Products.ToList();
            Products.ItemsSource = products;
        }
    }
}