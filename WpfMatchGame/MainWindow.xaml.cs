using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfMatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isSearching;
        private TextBlock lastTextBlockClicked;

        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame()
        {
            List<string> textBlockContent = new List<string>
            {
                "a", "a",
                "b", "b",
                "c", "c",
                "d", "d",
                "e", "e",
                "f", "f",
                "g", "g",
                "h", "h",
            };

            Random rand = new Random();
            foreach (TextBlock textBlock in Grid.Children.OfType<TextBlock>())
            {
                int index = rand.Next(textBlockContent.Count);
                textBlock.Text = textBlockContent[index];
                textBlockContent.RemoveAt(index);
            }
        }

        private void TextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock currentTextBlock = sender as TextBlock;

            if (!isSearching)
            {
                currentTextBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = currentTextBlock;
                isSearching = true;
            }
            else if (currentTextBlock.Text == lastTextBlockClicked.Text)
            {
                currentTextBlock.Visibility = Visibility.Hidden;
                isSearching = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                isSearching = false;
            }

        }
    }
}