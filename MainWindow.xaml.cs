using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace gameee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml 
    /// </summary>

    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int MathesFound;
        float best = 0.0f;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            tenthOfSecondsElapsed = 200;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthOfSecondsElapsed--;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");
            if (tenthOfSecondsElapsed == 0)
            {
                timer.Stop();
                MathesFound = -1;
                timeTextBlock.Text = timeTextBlock.Text + " - again?";
            }
            if (MathesFound == 10)
            {
                timer.Stop();
                if (best == 0.0f || best < tenthOfSecondsElapsed / 10F)
                {
                    best = tenthOfSecondsElapsed / 10F;
                    BestTime.Text = $"Best: {best}s";
                }
                timeTextBlock.Text = timeTextBlock.Text + " - again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "\U0001F9F8","\U0001F9F8",
                "\U0001F99D","\U0001F99D",
                "\U0001F984","\U0001F984",
                "\U0001F414","\U0001F414",
                "\U0001F42D","\U0001F42D",
                "\U0001F437","\U0001F437",
                "\U0001F42E","\U0001F42E",
                "\U0001F43B","\U0001F43B",
                "\U0001F648","\U0001F648",
                "\U0001F47B","\U0001F47B"
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock" && textBlock.Name != "BestTime")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthOfSecondsElapsed = 200;
            MathesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                MathesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetUpGame();
        }
    }
}