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



namespace KristisMatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        int matchesFound;
        int misses;
        string saveWins;
        int clicks; 
        const string magicWord = "**FLIP**";

        public MainWindow()
        {
            InitializeComponent();

            SetUpGame();
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "😺", "😺",
                "🙉", "🙉",
                "🐷", "🐷",
                "🐶", "🐶",
                "🐭", "🐭",
                "🐸", "🐸",
                "🦁", "🦁",
                "🐔", "🐔",
            };
            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "consoleTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    textBlock.Background = new SolidColorBrush(Colors.Blue);
                    textBlock.Foreground = new SolidColorBrush(Colors.Blue);
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
                else
                {
                    textBlock.Text = null;
                }
            }
            clicks = 0; 
            matchesFound = 0;
            saveWins = null;
            misses = 0;
         }
        TextBlock lastTextBlockClicked;
        TextBlock firstTextBlockClicked;
     
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            int attempts;
            if (textBlock.Name != "consoleTextBlock")
            {
                clicks++; 
                if (clicks == 1) 
                {
                    firstTextBlockClicked = textBlock;
                    // firstOfPair = false;
                    firstTextBlockClicked.Foreground = new SolidColorBrush(Colors.Black);
                    firstTextBlockClicked.Background = new SolidColorBrush(Colors.White);
                }
                else if (clicks == 2) 
                {
                    lastTextBlockClicked = textBlock;
                    lastTextBlockClicked.Background = new SolidColorBrush(Colors.White);
                    if (firstTextBlockClicked.Text == lastTextBlockClicked.Text)
                    {
                        clicks = 0; 
                        matchesFound++;
                        saveWins += firstTextBlockClicked.Text;
                        consoleTextBlock.Text = saveWins;
                        firstTextBlockClicked.Foreground = new SolidColorBrush(Colors.White);
                        lastTextBlockClicked.Foreground = new SolidColorBrush(Colors.White);
                        if (matchesFound == 8)
                        {
                            attempts = matchesFound + misses;
                            consoleTextBlock.Text = attempts + " tries for 8 matches - Play again?";
                        }
                    }
                    else // 2 clicks but not match
                    {
                        misses++;
                        firstTextBlockClicked.Foreground = new SolidColorBrush(Colors.Red);
                        lastTextBlockClicked.Foreground = new SolidColorBrush(Colors.Red);
                        consoleTextBlock.Text = magicWord;
                    }
                }
            }
            else // consoleTextBlock
            {
                    if (matchesFound == 8)
                    {
                        SetUpGame();
                    }
                    else if (textBlock.Text == magicWord)
                    {
                        lastTextBlockClicked.Foreground = new SolidColorBrush(Colors.Blue);
                        lastTextBlockClicked.Background = new SolidColorBrush(Colors.Blue);
                        firstTextBlockClicked.Foreground = new SolidColorBrush(Colors.Blue);
                        firstTextBlockClicked.Background = new SolidColorBrush(Colors.Blue);
                        textBlock.Text = saveWins; // restore saveWins
                        clicks = 0;
                    }
            }
        }
    }
}