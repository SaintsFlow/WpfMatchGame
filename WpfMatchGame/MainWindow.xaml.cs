using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void SetUpGame()
        {
            List<string> textBlockContent = new List<string>
            {
                "a","a",
                "b","b",
                "c","c",
                "d","d",
                "e","e",
                "f","f",
                "g","g",
                "h","h",
            };

            Random rand = new Random();
            foreach (TextBlock textBlock in Grid.Children.OfType<TextBlock>())
            {
                int index = rand.Next(textBlockContent.Count);
                textBlock.Text = textBlockContent[index];
                textBlockContent.RemoveAt(index);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }
    }
}