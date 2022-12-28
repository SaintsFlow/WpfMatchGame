using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace WpfMatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        private bool _isSearching;
        private TextBlock _lastTextBlockClicked = null!;
        private readonly DispatcherTimer  _timer = new DispatcherTimer();
        private int _secondsElapsed;
        private int _gameSessionResult;

        public MainWindow()
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimerOnTick;
            SetUpGame();
        }
        private void SetUpGame()
        {
            SetSearching(false);
            SetSecondsElapsed(0);
            SetGameSessionResult(0);
            _timer.Start();
            List<string> textBlockContent = new List<string>
            {
                "🍕", "🍕",
                "🍔", "🍔",
                "🎈", "🎈",
                "🎃", "🎃",
                "🎀", "🎀",
                "🧀", "🧀",
                "🛹", "🛹",
                "🎄", "🎄",
            };

            foreach (TextBlock textBlock in Grid.Children.OfType<TextBlock>())
            {
                textBlock.Visibility = Visibility.Visible;
            }

            Random rand = new Random();
            foreach (TextBlock textBlock in Grid.Children.OfType<TextBlock>())
            {
                if(textBlock.Name == "TimerTextBlock")
                    continue;
                int index = rand.Next(textBlockContent.Count);
                textBlock.Text = textBlockContent[index];
                textBlockContent.RemoveAt(index);
            }
        }
        private void TimerOnTick(object? sender, EventArgs e)
        {
            SetSecondsElapsed(GetSecondsElapsed()+1);
            TimerTextBlock.Text = (GetSecondsElapsed() / 10F).ToString("0.0s");
            
            if (CheckIfGameHasEndedUp())
            {
                _timer.Stop();
                TimerTextBlock.Text += " Play again?";
            }
        }
        private void TextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock currentTextBlock = sender as TextBlock;

            if (!_isSearching)
            {
                currentTextBlock.Visibility = Visibility.Hidden;
                _lastTextBlockClicked = currentTextBlock;
            }
            else if (currentTextBlock.Text == _lastTextBlockClicked.Text)
            {
                currentTextBlock.Visibility = Visibility.Hidden;
                SetGameSessionResult(GetGameSessionResult()+1);
            }
            else
            {
                _lastTextBlockClicked.Visibility = Visibility.Visible;
            }

            ChangeSearchingStatus();

        }
        private void TimerTextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CheckIfGameHasEndedUp())
            {
                SetUpGame();
            }
        }
        private bool CheckIfGameHasEndedUp()
        {
            Console.WriteLine(GetGameSessionResult());
            return GetGameSessionResult() == (Grid.Children.Count - 1) / 2;
        }
        private void SetGameSessionResult(int value)
        {
            _gameSessionResult = value;
        }
        private int GetGameSessionResult()
        {
            return _gameSessionResult;
        }
        private void SetSecondsElapsed(int value)
        {
            _secondsElapsed= value;
        }
        private int GetSecondsElapsed()
        {
            return _secondsElapsed;
        }
        private void SetSearching(bool value)
        {
            _isSearching = value;
        }
        private bool GetSearching()
        {
            return _isSearching;
        }
        private void ChangeSearchingStatus()
        {
            SetSearching(!GetSearching());
        }

    }
}