using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Speech.Synthesis;
using System.Threading;
using System;
using System.Collections.Concurrent;

namespace MultiplicationTable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Class Varibles
        MultiButton[] mulButton = new MultiButton[100];
        SpeechSynthesizer speech = new SpeechSynthesizer();

        public MainWindow()
        {
            InitializeComponent();

            speech.Rate = 1;
            speech.Volume = 100;

            //Set female voice if available 
            if (speech.GetInstalledVoices().Count > 1)
            {
                InstalledVoice voice = speech.GetInstalledVoices()[1];
                speech.SelectVoice(voice.VoiceInfo.Name);
            }


            int row = -1;

            for (int i = 0; i < 100; i++)
            {
                if (i < 11)
                {
                    mulButton[i] = new MultiButton(i+1);
                    mulButton[i].Background = Brushes.Blue;
                    mulButton[i].Content = i+1;
                }
                else
                {
                    mulButton[i] = new MultiButton(0);
                }

                if (i % 10 == 0)
                {
                    row++;
                    mulButton[i].Content = (row+1).ToString();
                    mulButton[i].Background = Brushes.Blue;
                }

                Grid.SetColumn(mulButton[i], i % 10);
                Grid.SetRow(mulButton[i], row);

                mulButton[i].Col = (i % 10) + 1;
                mulButton[i].Row = row + 1;

                myGrid.Children.Add(mulButton[i]);

                if (mulButton[i].Col > 1 && mulButton[i].Row > 1)
                {
                    mulButton[i].MouseEnter += mulButton_MouseEnter;
                    mulButton[i].MouseLeave += mulButton_MouseLeave;
                    mulButton[i].Click += mulButton_Click;
                }
            }
                
        }

        private void mulButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (MultiButton)sender;
            
            //What to say
            string speak = btn.Col.ToString() + " times " + btn.Row.ToString() + " equals " +
                (btn.Col * btn.Row).ToString();

            if(speech.State == SynthesizerState.Speaking)
            {
                speech.SpeakAsyncCancelAll();
            }

            speech.SpeakAsync(speak);

        }

        private void mulButton_MouseLeave(object sender, MouseEventArgs e)
        {
            var btn = (MultiButton)sender;
            btn.Number = btn.Col / btn.Row;

            mulButton[btn.Col - 1].Background = Brushes.Blue;
            mulButton[(btn.Row-1)*10].Background = Brushes.Blue;

            btn.Content = "";
        }

        private void mulButton_MouseEnter(object sender, MouseEventArgs e)
        {
            var btn = (MultiButton)sender;
            btn.Number = btn.Col * btn.Row;

            mulButton[btn.Col - 1].Background = Brushes.Red;
            mulButton[(btn.Row-1)*10].Background = Brushes.Red;

            btn.Content = btn.Number.ToString();
        }

        private void MultiTable_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
